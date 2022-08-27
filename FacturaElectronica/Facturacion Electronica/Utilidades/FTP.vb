Imports System.Net.FtpWebRequest
Imports System.Net
Imports System.IO
Imports System.Windows.Forms
Imports System.Data

Namespace OBSoluciones
    Namespace Utilidades
        Public Class Cliente_FTP

            Private localhost, Usuario, Clave As String
            Private PublicUsuario, PublicClave As String
            Private vRaiz As String = ""
            Private vUsaFTP As Boolean = True

            Public ReadOnly Property UsaFTP As String
                Get
                    Return Me.vUsaFTP
                End Get
            End Property

            Public ReadOnly Property Raiz() As String
                Get
                    Return vRaiz
                End Get
            End Property

            Public ReadOnly Property DireccionPublica As String
                Get
                    Return "ftp://" & PublicUsuario & ":" & PublicClave & "@" & localhost
                End Get
            End Property

            Private Sub GetDatosServidorFTP()
                Dim db As New SQL.Sentencias(GetSetting("SeeSOFT", "SeePOS", "Conexion"))
                Dim dt As New DataTable
                dt = db.Ejecutar("Select TieneFTP, LocalHost, Usuario, Clave, UsuarioPublico, ClavePublica From ServidorFTP Where Id = 1", CommandType.Text)
                If dt.Rows.Count > 0 Then

                    Me.vUsaFTP = dt.Rows(0).Item("TieneFTP")
                    Me.localhost = dt.Rows(0).Item("LocalHost")
                    Me.Usuario = dt.Rows(0).Item("Usuario")
                    Me.Clave = dt.Rows(0).Item("Clave")
                    Me.vRaiz = "ftp://" & localhost
                    Me.PublicUsuario = dt.Rows(0).Item("UsuarioPublico")
                    Me.PublicClave = dt.Rows(0).Item("ClavePublica")

                End If
            End Sub

            Public Sub New()
                Me.GetDatosServidorFTP()
            End Sub

            Public Sub Crear_Carpeta(ByVal _direccion As String)
                If vUsaFTP = False Then Exit Sub
                Dim reqFTP As FtpWebRequest
                Try
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(Convert.ToString(Me.vRaiz + "/") & _direccion)), FtpWebRequest)
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory
                    reqFTP.UseBinary = True
                    reqFTP.Credentials = New NetworkCredential(Me.Usuario, Me.Clave)
                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
                    Dim ftpStream As Stream = response.GetResponseStream()
                    ftpStream.Close()
                    response.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Sub

            Public Function Existe(ByVal _direccion As String) As Boolean
                If vUsaFTP = False Then Return False
                Dim directoryExists__1 As Boolean

                Dim request = DirectCast(WebRequest.Create(_direccion), FtpWebRequest)
                request.Method = WebRequestMethods.Ftp.ListDirectory
                request.Credentials = New NetworkCredential(Me.Usuario, Me.Clave)

                Try
                    Using request.GetResponse()
                        directoryExists__1 = True
                    End Using
                Catch generatedExceptionName As WebException
                    directoryExists__1 = False
                End Try

                Return directoryExists__1
            End Function

            Public Sub Subir(ByVal _archivo() As String, ByVal _direccion As String)
                If vUsaFTP = False Then Exit Sub
                Dim Direccion As String = Me.vRaiz + _direccion
                If Me.Existe(Direccion) = False Then Me.Crear_Carpeta(_direccion)

                For Each File As String In _archivo
                    Dim fileInf As New FileInfo(File)
                    Dim uri As String = Direccion + fileInf.Name
                    'If Me.Existe(uri) = True Then Me.Eliminar(fileInf.Name, Direccion)

                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Me.Usuario, Me.Clave)
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.UploadFile
                    reqFTP.UseBinary = True
                    reqFTP.ContentLength = fileInf.Length

                    Dim buffLength As Integer = 2048
                    Dim buff As Byte() = New Byte(buffLength - 1) {}
                    Dim contentLen As Integer
                    Dim fs As FileStream = fileInf.OpenRead()

                    Try
                        Dim strm As Stream = reqFTP.GetRequestStream()
                        contentLen = fs.Read(buff, 0, buffLength)

                        While contentLen <> 0
                            strm.Write(buff, 0, contentLen)
                            contentLen = fs.Read(buff, 0, buffLength)
                        End While

                        strm.Close()
                        fs.Close()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Upload Error")
                    End Try

                Next

            End Sub

            Public Sub Subir(ByVal _archivo As String, ByVal _direccion As String)
                If vUsaFTP = False Then Exit Sub
                Dim Direccion As String = Me.vRaiz + _direccion
                Dim fileInf As New FileInfo(_archivo)
                Dim uri As String = Direccion + fileInf.Name

                If Me.Existe(Direccion) = False Then Me.Crear_Carpeta(_direccion)
                If Me.Existe(uri) = True Then Me.Eliminar(fileInf.Name, Direccion)

                Dim reqFTP As FtpWebRequest
                reqFTP = DirectCast(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
                reqFTP.Credentials = New NetworkCredential(Me.Usuario, Me.Clave)
                reqFTP.KeepAlive = False
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile
                reqFTP.UseBinary = True
                reqFTP.ContentLength = fileInf.Length

                Dim buffLength As Integer = 2048
                Dim buff As Byte() = New Byte(buffLength - 1) {}
                Dim contentLen As Integer
                Dim fs As FileStream = fileInf.OpenRead()

                Try
                    Dim strm As Stream = reqFTP.GetRequestStream()
                    contentLen = fs.Read(buff, 0, buffLength)

                    While contentLen <> 0
                        strm.Write(buff, 0, contentLen)
                        contentLen = fs.Read(buff, 0, buffLength)
                    End While

                    strm.Close()
                    fs.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Upload Error")
                End Try
            End Sub

            Public Sub Eliminar(ByVal _archivo As String, ByVal _direccion As String)
                If vUsaFTP = False Then Exit Sub
                Try
                    Dim uri As String = IIf(_direccion.Contains("tp://") = True, _direccion & _archivo, Convert.ToString(Me.vRaiz + _direccion + "/") & _archivo)
                    Dim reqFTP As FtpWebRequest
                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(uri)), FtpWebRequest)
                    reqFTP.Credentials = New NetworkCredential(Me.Usuario, Me.Clave)
                    reqFTP.KeepAlive = False
                    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile

                    Dim result As String = [String].Empty
                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
                    Dim size As Long = response.ContentLength
                    Dim datastream As Stream = response.GetResponseStream()
                    Dim sr As New StreamReader(datastream)
                    result = sr.ReadToEnd()
                    sr.Close()
                    datastream.Close()
                    response.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "FTP 2.0 Delete")
                End Try
            End Sub

            Public Sub Descargar(ByVal _OrigenFTP As String, ByVal _DestinoPC As String)
                If vUsaFTP = False Then Exit Sub
                Dim reqFTP As FtpWebRequest
                Try
                    Dim outputStream As New FileStream(Convert.ToString(_DestinoPC), FileMode.Create)

                    reqFTP = DirectCast(FtpWebRequest.Create(New Uri(Convert.ToString(_OrigenFTP))), FtpWebRequest)
                    reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
                    reqFTP.UseBinary = True
                    reqFTP.Credentials = New NetworkCredential(Me.Usuario, Me.Clave)
                    Dim response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
                    Dim ftpStream As Stream = response.GetResponseStream()
                    Dim cl As Long = response.ContentLength
                    Dim bufferSize As Integer = 2048
                    Dim readCount As Integer
                    Dim buffer As Byte() = New Byte(bufferSize - 1) {}

                    readCount = ftpStream.Read(buffer, 0, bufferSize)
                    While readCount > 0
                        outputStream.Write(buffer, 0, readCount)
                        readCount = ftpStream.Read(buffer, 0, bufferSize)
                    End While

                    ftpStream.Close()
                    outputStream.Close()
                    response.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Sub

        End Class
    End Namespace
End Namespace

