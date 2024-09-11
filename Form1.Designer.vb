<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.toolStripStatusConnection = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.availableDevicesMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.disconnectDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grpBxBatchSend = New System.Windows.Forms.GroupBox()
        Me.btnSendBatch = New System.Windows.Forms.Button()
        Me.chkBxMakeBatch = New System.Windows.Forms.CheckBox()
        Me.lstBxRemotesForBatch = New System.Windows.Forms.ListBox()
        Me.btnFlowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.NotifyIconMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowIRControllerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditRemotesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitNotifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.grpBxBatchSend.SuspendLayout()
        Me.NotifyIconMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusConnection})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 244)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(426, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'toolStripStatusConnection
        '
        Me.toolStripStatusConnection.Name = "toolStripStatusConnection"
        Me.toolStripStatusConnection.Size = New System.Drawing.Size(88, 17)
        Me.toolStripStatusConnection.Text = "No Connection"
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(426, 24)
        Me.MenuStrip.TabIndex = 1
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.availableDevicesMenu, Me.disconnectDevice, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'availableDevicesMenu
        '
        Me.availableDevicesMenu.Name = "availableDevicesMenu"
        Me.availableDevicesMenu.Size = New System.Drawing.Size(171, 22)
        Me.availableDevicesMenu.Text = "Available Devices"
        '
        'disconnectDevice
        '
        Me.disconnectDevice.Name = "disconnectDevice"
        Me.disconnectDevice.Size = New System.Drawing.Size(171, 22)
        Me.disconnectDevice.Text = "Disconnect Device"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'grpBxBatchSend
        '
        Me.grpBxBatchSend.Controls.Add(Me.btnSendBatch)
        Me.grpBxBatchSend.Controls.Add(Me.chkBxMakeBatch)
        Me.grpBxBatchSend.Controls.Add(Me.lstBxRemotesForBatch)
        Me.grpBxBatchSend.Location = New System.Drawing.Point(312, 27)
        Me.grpBxBatchSend.Name = "grpBxBatchSend"
        Me.grpBxBatchSend.Size = New System.Drawing.Size(106, 209)
        Me.grpBxBatchSend.TabIndex = 2
        Me.grpBxBatchSend.TabStop = False
        Me.grpBxBatchSend.Text = "Batch Send:"
        '
        'btnSendBatch
        '
        Me.btnSendBatch.Location = New System.Drawing.Point(9, 145)
        Me.btnSendBatch.Name = "btnSendBatch"
        Me.btnSendBatch.Size = New System.Drawing.Size(91, 58)
        Me.btnSendBatch.TabIndex = 2
        Me.btnSendBatch.Text = "Send Batch"
        Me.btnSendBatch.UseVisualStyleBackColor = True
        '
        'chkBxMakeBatch
        '
        Me.chkBxMakeBatch.AutoSize = True
        Me.chkBxMakeBatch.Location = New System.Drawing.Point(9, 19)
        Me.chkBxMakeBatch.Name = "chkBxMakeBatch"
        Me.chkBxMakeBatch.Size = New System.Drawing.Size(80, 17)
        Me.chkBxMakeBatch.TabIndex = 1
        Me.chkBxMakeBatch.Text = "Build Batch"
        Me.chkBxMakeBatch.UseVisualStyleBackColor = True
        '
        'lstBxRemotesForBatch
        '
        Me.lstBxRemotesForBatch.FormattingEnabled = True
        Me.lstBxRemotesForBatch.Location = New System.Drawing.Point(9, 44)
        Me.lstBxRemotesForBatch.Name = "lstBxRemotesForBatch"
        Me.lstBxRemotesForBatch.Size = New System.Drawing.Size(91, 95)
        Me.lstBxRemotesForBatch.TabIndex = 0
        '
        'btnFlowPanel
        '
        Me.btnFlowPanel.AutoScroll = True
        Me.btnFlowPanel.Location = New System.Drawing.Point(12, 27)
        Me.btnFlowPanel.Name = "btnFlowPanel"
        Me.btnFlowPanel.Size = New System.Drawing.Size(269, 209)
        Me.btnFlowPanel.TabIndex = 3
        '
        'NotifyIcon
        '
        Me.NotifyIcon.ContextMenuStrip = Me.NotifyIconMenu
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "NotifyIcon1"
        '
        'NotifyIconMenu
        '
        Me.NotifyIconMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowIRControllerToolStripMenuItem, Me.EditRemotesToolStripMenuItem, Me.ExitNotifyToolStripMenuItem})
        Me.NotifyIconMenu.Name = "NotifyIconMenu"
        Me.NotifyIconMenu.Size = New System.Drawing.Size(181, 92)
        '
        'ShowIRControllerToolStripMenuItem
        '
        Me.ShowIRControllerToolStripMenuItem.Name = "ShowIRControllerToolStripMenuItem"
        Me.ShowIRControllerToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ShowIRControllerToolStripMenuItem.Text = "Show IR Controller"
        '
        'EditRemotesToolStripMenuItem
        '
        Me.EditRemotesToolStripMenuItem.Name = "EditRemotesToolStripMenuItem"
        Me.EditRemotesToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.EditRemotesToolStripMenuItem.Text = "Edit Remotes"
        '
        'ExitNotifyToolStripMenuItem
        '
        Me.ExitNotifyToolStripMenuItem.Name = "ExitNotifyToolStripMenuItem"
        Me.ExitNotifyToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExitNotifyToolStripMenuItem.Text = "Exit"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(426, 266)
        Me.Controls.Add(Me.btnFlowPanel)
        Me.Controls.Add(Me.grpBxBatchSend)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IR Controller"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.grpBxBatchSend.ResumeLayout(False)
        Me.grpBxBatchSend.PerformLayout()
        Me.NotifyIconMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents toolStripStatusConnection As ToolStripStatusLabel
    Friend WithEvents MenuStrip As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents availableDevicesMenu As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents grpBxBatchSend As GroupBox
    Friend WithEvents btnFlowPanel As FlowLayoutPanel
    Friend WithEvents btnSendBatch As Button
    Friend WithEvents chkBxMakeBatch As CheckBox
    Friend WithEvents lstBxRemotesForBatch As ListBox
    Friend WithEvents disconnectDevice As ToolStripMenuItem
    Friend WithEvents NotifyIcon As NotifyIcon
    Friend WithEvents NotifyIconMenu As ContextMenuStrip
    Friend WithEvents EditRemotesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitNotifyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowIRControllerToolStripMenuItem As ToolStripMenuItem
End Class
