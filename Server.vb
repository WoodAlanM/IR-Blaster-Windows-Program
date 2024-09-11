Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports YamlDotNet.RepresentationModel

Module Server
    Private running As Boolean = True

    Public Sub StartServer()
        Dim server As New TcpListener(IPAddress.Any, 8080)
        server.Start()
        Console.WriteLine("Server started...")

        While running
            Console.WriteLine("Waiting for connection...")
            Dim client As TcpClient = server.AcceptTcpClient()
            Dim stream As NetworkStream = client.GetStream()
            Dim request As String = ReadRequest(stream)

            Console.WriteLine("Server running...")

            ' Determine the endpoint and respond
            If request.StartsWith("GET /getRemotes") Then
                Console.WriteLine("Received request.")
                HandleGetRemotesEndpoint(stream)
            ElseIf request.StartsWith("GET /sendRemote/") Then
                Dim remoteName As String = ExtractVariableFromUrl(request, "/sendRemote/", stream)
                Console.WriteLine("Sent remote name: " + remoteName)
            Else
                HandleNotFound(stream)
            End If

            stream.Close()
            client.Close()
        End While
    End Sub

    Public Sub StopServer()
        running = False
    End Sub

    Private Function ExtractVariableFromUrl(request As String, route As String, stream As NetworkStream) As String
        Dim parts As String() = request.Split(" "c)(1).Split("/"c)
        If parts.Length > 2 AndAlso parts(1).ToLower() = route.Trim("/"c).ToLower() Then
            Return parts(2)
        End If

        Dim response As String = "HTTP/1.1 200 OK" & vbCrLf &
                                 "Content-Type: text/plain" & vbCrLf &
                                 vbCrLf &
                                 "200 - OK"
        Dim responseData As Byte() = Encoding.ASCII.GetBytes(response)
        stream.Write(responseData, 0, responseData.Length)

        Return String.Empty
    End Function

    Private Function ReadRequest(stream As NetworkStream) As String
        Dim bytes(1024) As Byte
        Dim count As Integer = stream.Read(bytes, 0, bytes.Length)
        Return Encoding.ASCII.GetString(bytes, 0, count)
    End Function

    ' Handle /hello endpoint
    Private Sub HandleGetRemotesEndpoint(stream As NetworkStream)
        Dim yaml As New YamlStream()

        Using reader As New StreamReader("config.yaml")
            yaml.Load(reader)
        End Using

        Dim root = CType(yaml.Documents(0).RootNode, YamlMappingNode)
        Dim remotesMapping = CType(root.Children(New YamlScalarNode("remotes")), YamlMappingNode)

        Dim remoteString As String = ""

        For Each remote In remotesMapping.Children
            remoteString = remoteString + remote.Key.ToString() + ","
        Next

        If remoteString.Length > 0 Then
            remoteString = remoteString.TrimEnd(","c)
        End If

        Dim response As String = "HTTP/1.1 200 OK" & vbCrLf &
                                 "Content-Type: text/plain" & vbCrLf &
                                 vbCrLf &
                                 remoteString
        Dim responseData As Byte() = Encoding.ASCII.GetBytes(response)
        stream.Write(responseData, 0, responseData.Length)
    End Sub

    ' Handle 404 Not Found
    Private Sub HandleNotFound(stream As NetworkStream)
        Dim response As String = "HTTP/1.1 404 Not Found" & vbCrLf &
                                 "Content-Type: text/plain" & vbCrLf &
                                 vbCrLf &
                                 "404 - Not Found"
        Dim responseData As Byte() = Encoding.ASCII.GetBytes(response)
        stream.Write(responseData, 0, responseData.Length)
    End Sub

End Module
