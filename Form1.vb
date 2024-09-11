Imports System.Windows.Forms
Imports System.IO
Imports System.IO.Ports
Imports System.Management
Imports YamlDotNet.RepresentationModel
Imports System.Threading
Imports System.Net.Http
Imports YamlDotNet
Imports System.Security


Public Class Form1

    Private serialPort As SerialPort
    Private serverThread As Thread

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Define button size and padding
        Dim buttonWidth As Integer = 100
        Dim buttonHeight As Integer = 50
        Dim padding As Integer = 10
        Dim rightPadding As Integer = 0

        Dim configFilePath As String = Path.Combine(Application.StartupPath, "config.yaml")

        If Not File.Exists(configFilePath) Then
            ' Create the config.yaml file with default content
            Dim defaultContent As String = "remotes: " & vbCrLf & vbTab & "REMOTE CONTROL NAME:" & vbCrLf & vbTab & vbTab & "TV REMOTE code OR url: NEC,12345678,32"
            File.WriteAllText(configFilePath, defaultContent)
        End If

        Dim yaml As New YamlStream()
        Using reader As New StreamReader("config.yaml")
            yaml.Load(reader)
        End Using

        ' Access the root mapping node
        Dim root = CType(yaml.Documents(0).RootNode, YamlMappingNode)
        Dim remotesMapping = CType(root.Children(New YamlScalarNode("remotes")), YamlMappingNode)

        Dim filePath As String = "batchitems.txt"

        lstBxRemotesForBatch.Enabled = False
        btnSendBatch.Enabled = False

        If File.Exists(filePath) Then
            ' Open the file and read its contents
            Using reader As New StreamReader(filePath)
                ' Loop through each line in the file
                Dim line As String
                Do
                    line = reader.ReadLine()
                    ' Add the line to the ListBox
                    If line IsNot Nothing Then
                        lstBxRemotesForBatch.Items.Add(line)
                    End If
                Loop Until line Is Nothing
            End Using

            ' Handle if batch file is empty so the send batch button is disabled
            Dim fileInfo As New FileInfo(filePath)
            If fileInfo.Length > 0 Then
                btnSendBatch.Enabled = True
            End If
        End If

        ' Disable disconnect device
        disconnectDevice.Enabled = False

        ' Get ports for the available devices menu
        availableDevicesMenu.DropDownItems.Clear()

        Dim comPorts As String() = SerialPort.GetPortNames()

        For Each port As String In comPorts
            Dim menutItem As New ToolStripMenuItem($"{port}")
            AddHandler menutItem.Click, AddressOf ComPortMenuItem_Click
            availableDevicesMenu.DropDownItems.Add(menutItem)
        Next

        If My.Settings.lastPort <> "" Then
            Dim lastPort = My.Settings.lastPort
            serialPort = New SerialPort(lastPort, 9600, Parity.None, 8, StopBits.One)

            Try
                serialPort.Open()
                toolStripStatusConnection.Text = "Connection on " & lastPort
                disconnectDevice.Enabled = True
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        ' Start server from server module
        serverThread = New Thread(AddressOf Server.StartServer)
        serverThread.IsBackground = True
        serverThread.Start()

        If remotesMapping.Children.Count > 4 Then
            rightPadding = 30
        Else
            rightPadding = 20
        End If

        btnFlowPanel.Padding = New Padding(padding)
        btnFlowPanel.Width = (buttonWidth + padding + rightPadding) * 2
        btnFlowPanel.Height = (buttonHeight + padding) * 3

        For Each remote In remotesMapping.Children
            Dim remoteName As String = CType(remote.Key, YamlScalarNode).Value
            Dim codeMapping = CType(remote.Value, YamlMappingNode)

            Dim code As String = Nothing
            Dim url As String = Nothing

            ' Check if "code" exists
            If codeMapping.Children.ContainsKey(New YamlScalarNode("code")) Then
                code = CType(codeMapping.Children(New YamlScalarNode("code")), YamlScalarNode).Value
            End If

            ' Check if "url" exists
            If codeMapping.Children.ContainsKey(New YamlScalarNode("url")) Then
                url = CType(codeMapping.Children(New YamlScalarNode("url")), YamlScalarNode).Value
            End If

            ' Create the button
            Dim btn As New Button With {
                .Text = remoteName,
                .Width = buttonWidth,
                .Height = buttonHeight,
                .Margin = New Padding(padding)
            }

            ' Determine what to store in the Tag property based on the presence of code or url
            If code IsNot Nothing Then
                btn.Tag = code ' Store the code value in the Tag property
            ElseIf url IsNot Nothing Then
                btn.Tag = url ' Store the url value in the Tag property
            End If

            ' Add a click event handler
            AddHandler btn.Click, AddressOf Button_Click

            ' Add the button to the FlowLayoutPanel
            btnFlowPanel.Controls.Add(btn)
        Next
    End Sub

    Private Sub ComPortMenuItem_Click(sender As Object, e As EventArgs)
        Dim selectedItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim selectedPort As String = selectedItem.Text

        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            serialPort.Close()
        End If

        serialPort = New SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One)

        Try
            serialPort.Open()
            MessageBox.Show("Connection to Arduino successful on " & selectedPort, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            toolStripStatusConnection.Text = "Connection on " & selectedPort
            My.Settings.lastPort = selectedPort
            My.Settings.Save()
            disconnectDevice.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Event handler for button click
    Private Async Sub Button_Click(sender As Object, e As EventArgs)
        Dim clickedButton As Button = CType(sender, Button)
        Dim tagValue As String = CType(clickedButton.Tag, String) ' Retrieve the code from the Tag property

        If chkBxMakeBatch.Checked Then
            lstBxRemotesForBatch.Items.Add(clickedButton.Text)
            btnSendBatch.Enabled = True
        Else
            If tagValue.StartsWith("http://") OrElse tagValue.StartsWith("https://") Then
                ' Handle URL: Access the URL
                Try
                    Using client As New HttpClient()
                        Dim response As HttpResponseMessage = Await client.GetAsync(tagValue)
                        Dim responseContent As String = Await response.Content.ReadAsStringAsync()
                    End Using
                Catch ex As Exception
                    'MessageBox.Show($"Error accessing URL: {tagValue}, Message: {ex.Message}")
                End Try
            Else
                ' Handle remote code sending
                SendSerialText("send")
                Thread.Sleep(1000)
                SendSerialText(tagValue)
            End If
        End If
    End Sub

    Public Shared Sub Main()
        Application.Run(New Form1())
    End Sub

    Private Sub SendBatch(buttonText As String)
        ' Iterate through all controls in the FlowLayoutPanel
        For Each ctrl As Control In btnFlowPanel.Controls
            ' Check if the control is a Button and if its Text matches the provided text
            If TypeOf ctrl Is Button AndAlso CType(ctrl, Button).Text = buttonText Then
                Dim btn As Button = CType(ctrl, Button)
                ' Simulate a click
                btn.PerformClick()
                Exit For
            End If
        Next
    End Sub

    Private Sub SendSerialText(text As String)
        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            Try
                serialPort.Write(text)
            Catch ex As Exception
                MessageBox.Show("Error sending text: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Serial port is not open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            Try
                serialPort.Close()
            Catch ex As Exception
                ' Optionally, log or display an error message
                MessageBox.Show("Error closing the COM port: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        Server.StopServer()
        If serverThread IsNot Nothing AndAlso serverThread.IsAlive Then
            serverThread.Join()
        End If
    End Sub

    Private Sub disconnectDevice_Click(sender As Object, e As EventArgs) Handles disconnectDevice.Click
        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            Try
                serialPort.Close()
                toolStripStatusConnection.Text = "Disconnected"
            Catch ex As Exception
                ' Optionally, log or display an error message
                MessageBox.Show("Error closing the COM port: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub chkBxMakeBatch_CheckedChanged(sender As Object, e As EventArgs) Handles chkBxMakeBatch.CheckedChanged
        If Not chkBxMakeBatch.Checked Then
            lstBxRemotesForBatch.Enabled = False
            If lstBxRemotesForBatch.Items.Count = 0 Then
                btnSendBatch.Enabled = False
            End If
            ' Specify the path of the text file
            Dim filePath As String = "batchitems.txt"

            ' Open the file for writing
            Using writer As New StreamWriter(filePath)
                ' Iterate through the ListBox items and write them to the file
                For Each item As String In lstBxRemotesForBatch.Items
                    writer.WriteLine(item)
                Next
            End Using

            MessageBox.Show("Batch items saved.")
        Else
            lstBxRemotesForBatch.Enabled = True
        End If
    End Sub

    Private Sub lstBxRemotesForBatch_KeyDown(sender As Object, e As KeyEventArgs) Handles lstBxRemotesForBatch.KeyDown
        If e.KeyCode = Keys.Delete Then
            ' Check if an item is selected
            If lstBxRemotesForBatch.SelectedIndex >= 0 Then
                ' Remove the selected item
                lstBxRemotesForBatch.Items.RemoveAt(lstBxRemotesForBatch.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub btnSendBatch_Click(sender As Object, e As EventArgs) Handles btnSendBatch.Click
        For Each item In lstBxRemotesForBatch.Items
            SendBatch(item)
            Thread.Sleep(1000)
        Next
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon.Visible = True ' Show the icon in the system tray
            Me.Hide() ' Hide the form from the taskbar
        End If
    End Sub

    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        Me.Show() ' Show the form when the icon is double-clicked
        Me.WindowState = FormWindowState.Normal ' Restore the form to its normal state
        NotifyIcon.Visible = False ' Hide the icon from the system tray
    End Sub

    Private Sub ShowIRControllerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowIRControllerToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon.Visible = False
    End Sub

    Private Sub EditRemotesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditRemotesToolStripMenuItem.Click
        ' Open the config.yaml file with the default text editor
        Dim configFilePath As String = Path.Combine(Application.StartupPath, "config.yaml")
        Process.Start("notepad.exe", configFilePath)
    End Sub

    Private Sub ExitNotifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitNotifyToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class
