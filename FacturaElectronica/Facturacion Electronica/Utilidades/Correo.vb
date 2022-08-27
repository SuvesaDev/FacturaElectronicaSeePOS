Imports System.Net.Mail

Public Class BitacoraCorreo
    Public Clave As String
    Public Consecutivo As String
    Public Tipo As String
    Public Estado As String
    Public Correo As String
    Public Observaciones As String

    Sub New(_Clave As String, _Consecutivo As String, _Tipo As String, _Estado As String, _Correo As String, _Observaciones As String)
        Me.Clave = _Clave
        Me.Consecutivo = _Consecutivo
        Me.Tipo = _Tipo
        Me.Estado = _Estado
        Me.Correo = _Correo
        Me.Observaciones = _Observaciones
    End Sub

    Public Sub Guardar()
        Dim db As New SQL.Sentencias(GetSetting("SeeSOFT", "Seguridad", "Conexion"))
        db.Ejecutar("Insert into BitacoraCorreo(Clave,Consecutivo, Fecha,Tipo,Estado, Correo,Observaciones) Values('" & Me.Clave & "', '" & Me.Consecutivo & "', GetDate(), '" & Me.Tipo & "', '" & Me.Estado & "', '" & Me.Correo & "', '" & Me.Observaciones & "')", CommandType.Text)
    End Sub

End Class

Public Class Correo

    Public Archivos As New List(Of String)
    Private Directorio As String
    Private Bitacora As BitacoraCorreo
    Private Correo As String = ""
    Private Clave As String = ""

    Sub New()
        Dim _correo As String = ""
        Dim _clave As String = ""

        Try
            _correo = GetSetting("SeeSOFT", "Seguridad", "CorreoFE")
            _clave = GetSetting("SeeSOFT", "Seguridad", "ClaveFE")

            If _correo = "" Or _clave = "" Then
                SaveSetting("SeeSOFT", "Seguridad", "CorreoFE", "facturaelectronica@suvesacr.com")
                SaveSetting("SeeSOFT", "Seguridad", "ClaveFE", "fac2018")

                _correo = GetSetting("SeeSOFT", "Seguridad", "CorreoFE")
                _clave = GetSetting("SeeSOFT", "Seguridad", "ClaveFE")
            End If

            Me.Correo = _correo
            Me.Clave = _clave
        Catch ex As Exception
            SaveSetting("SeeSOFT", "Seguridad", "CorreoFE", "facturaelectronica@suvesacr.com")
            SaveSetting("SeeSOFT", "Seguridad", "ClaveFE", "fac2018")

            _correo = GetSetting("SeeSOFT", "Seguridad", "CorreoFE")
            _clave = GetSetting("SeeSOFT", "Seguridad", "ClaveFE")

            Me.Correo = _correo
            Me.Clave = _clave
        End Try
    End Sub

    Private Function GetEmisor() As String
        Dim cls As New GestionDatos.Emisor
        cls.Obtener_Datos()
        Return cls.nombre
    End Function

    Public Sub NotificarFacturaRechazada(_IdFactura As String, _Correo As String, _Observaciones As String)
        Dim dt As New DataTable
        Dim Correo, Consecutivo, Clave, Tipo, Nombre As String
        Correo = ""
        Consecutivo = ""
        Clave = ""
        Tipo = ""
        Nombre = ""
        Try
            dt = General.sql.Ejecutar("select v.Tipo, v.ConsecutivoMH, v.ClaveMH, v.nombre_cliente from " & General.BasedeDatos & ".dbo.Ventas v  where v.Id = " & _IdFactura, CommandType.Text)

            If dt.Rows.Count > 0 Then
                Tipo = dt.Rows(0).Item("Tipo")
                Correo = _Correo
                Consecutivo = dt.Rows(0).Item("ConsecutivoMH")
                Clave = dt.Rows(0).Item("ClaveMH")
                Nombre = dt.Rows(0).Item("nombre_cliente")

                Dim Correos() As String = Correo.Split(";")

                For Each mail As String In Correos

                    If mail.Contains("@") = True Then
                        Me.Directorio = "C:/Facturas/" & Consecutivo & "/"

                        Archivos.Clear()
                        Archivos.Add(Me.Directorio & Consecutivo & "_02_Firmado.xml")
                        Archivos.Add(Me.Directorio & Consecutivo & "_07_RespuestaClave.xml")
                        Archivos.Add(Me.Directorio & Clave & ".pdf")

                        Dim Asunto As String = "Rechazo, Cliente : " & Nombre
                        Dim Cuerpo As String = Me.GetEmisor & vbCrLf _
                                               & _Observaciones

                        Me.Enviar_Correo_Adjunto(mail, Asunto, Cuerpo, Me.Archivos, "Factura Electronica", Clave, Consecutivo)
                    Else
                        Me.Bitacora = New BitacoraCorreo(Clave, Consecutivo, "Factura Electronica", "Correo Invalido", mail, "Correo no paso validacion")
                        Me.Bitacora.Guardar()
                    End If

                Next
            End If

        Catch ex As Exception
            Me.Bitacora = New BitacoraCorreo(Clave, Consecutivo, Tipo, "Error", Correo, ex.Message)
            Me.Bitacora.Guardar()
        End Try
    End Sub

    Public Sub EnviarFactura(_IdFactura As String)
        Dim dt As New DataTable
        Dim Correo, Consecutivo, Clave, Tipo, Cedula, Orden As String
        Correo = ""
        Consecutivo = ""
        Clave = ""
        Tipo = ""
        Cedula = ""
        Orden = ""
        Try
            dt = General.sql.Ejecutar("select v.Cedula, v.Orden, v.Tipo, v.ConsecutivoMH, v.ClaveMH, isnull(c.CorreoComprobante,'') as CorreoComprobante from " & General.BasedeDatos & ".dbo.Ventas v left join " & General.BasedeDatos & ".dbo.Clientes c on v.Cod_Cliente = c.identificacion where Id = " & _IdFactura, CommandType.Text)

            If dt.Rows.Count > 0 Then
                Tipo = dt.Rows(0).Item("Tipo")
                Correo = dt.Rows(0).Item("CorreoComprobante")
                Consecutivo = dt.Rows(0).Item("ConsecutivoMH")
                Clave = dt.Rows(0).Item("ClaveMH")
                Cedula = dt.Rows(0).Item("Cedula")
                Orden = dt.Rows(0).Item("Orden")

                If Tipo = "CON" Or Tipo = "CRE" Or Tipo = "MCO" Or Tipo = "MCR" Or Tipo = "TCO" Or Tipo = "TCR" Then

                    Dim Correos() As String = Correo.Split(";")

                    For Each mail As String In Correos

                        If mail.Contains("@") = True Then
                            Me.Directorio = "C:/Facturas/" & Consecutivo & "/"

                            Archivos.Clear()
                            Archivos.Add(Me.Directorio & Consecutivo & "_02_Firmado.xml")
                            Archivos.Add(Me.Directorio & Consecutivo & "_07_RespuestaClave.xml")
                            Archivos.Add(Me.Directorio & Clave & ".pdf")

                            Dim Asunto As String = "Factura Electrónica N°" & Consecutivo & " del Emisor: " & Me.GetEmisor
                            Dim Cuerpo As String = "Factura Electrónica N°" & Consecutivo
                            '           "4000042139"
                            If Cedula = "4000042139" Then
                                'Las pegas del ice
                                Asunto = "Comprobante MM-" & Orden.Replace("MM-", "")
                                Cuerpo = "Se adjunta la referencia MM-" & Orden.Replace("MM-", "")
                                If Tipo = "CON" Or Tipo = "MCO" Or Tipo = "TCO" Then
                                    'correo para las ventas de contado
                                    mail = "facturasft@ice.go.cr"
                                ElseIf Tipo = "CRE" Or Tipo = "MCR" Or Tipo = "TCR" Then
                                    'correo para las ventas de credito
                                    mail = "facturasice@ice.go.cr"
                                End If
                                Me.Enviar_Correo_Adjunto(mail, Asunto, Cuerpo, Me.Archivos, "Factura Electronica", Clave, Consecutivo)
                            Else
                                Me.Enviar_Correo_Adjunto(mail, Asunto, Cuerpo, Me.Archivos, "Factura Electronica", Clave, Consecutivo)
                            End If
                        Else
                            Me.Bitacora = New BitacoraCorreo(Clave, Consecutivo, "Factura Electronica", "Correo Invalido", mail, "Correo no paso validacion")
                            Me.Bitacora.Guardar()
                        End If

                    Next
                End If
            End If

        Catch ex As Exception
            Me.Bitacora = New BitacoraCorreo(Clave, Consecutivo, Tipo, "Error", Correo, ex.Message)
            Me.Bitacora.Guardar()
        End Try
    End Sub

    Public Sub EnviarNotaCredito(_IdFactura As String)
        Dim dt As New DataTable
        Dim Correo, Consecutivo, Clave As String

        dt = General.sql.Ejecutar("select dv.CONSECUTIVODGT as ConsecutivoMH, dv.CLAVEDGT as ClaveMH, ISNULL(c.CorreoComprobante,'') as CorreoComprobante from " & General.BasedeDatos & ".dbo.devoluciones_ventas dv inner join " & General.BasedeDatos & ".dbo.Ventas v on dv.Id_Factura = v.Id left join " & General.BasedeDatos & ".dbo.Clientes c on v.Cod_Cliente = c.identificacion where dv.Devolucion =  " & _IdFactura, CommandType.Text)

        If dt.Rows.Count > 0 Then
            Correo = dt.Rows(0).Item("CorreoComprobante")
            Consecutivo = dt.Rows(0).Item("ConsecutivoMH")
            Clave = dt.Rows(0).Item("ClaveMH")

            Dim Correos() As String = Correo.Split(";")

            For Each mail As String In Correos

                If mail.Contains("@") = True Then
                    Me.Directorio = "C:/Facturas/" & Consecutivo & "/"

                    Archivos.Clear()
                    Archivos.Add(Me.Directorio & Consecutivo & "_02_Firmado.xml")
                    Archivos.Add(Me.Directorio & Consecutivo & "_07_RespuestaClave.xml")
                    Archivos.Add(Me.Directorio & Clave & ".pdf")

                    Dim Asunto As String = "Nota Credito N°" & Consecutivo & " del Emisor: " & Me.GetEmisor
                    Dim Cuerpo As String = "Nota Credito N°" & Consecutivo

                    Me.Enviar_Correo_Adjunto(mail, Asunto, Cuerpo, Me.Archivos, "Nota de Credito", Clave, Consecutivo)
                Else
                    Me.Bitacora = New BitacoraCorreo(Clave, Consecutivo, "Nota de Credito", "Correo Invalido", mail, "Correo no paso validacion")
                    Me.Bitacora.Guardar()
                End If

            Next

        End If

    End Sub

    Public Sub EnviarNotificacionRecibo(_CorreoPara As String, _Asunto As String, _Cuerpo As String, _Adjunto As List(Of String))
        Me.Enviar_Correo_Adjunto(_CorreoPara, _Asunto, _Cuerpo, _Adjunto, "Recibo", "000", "000")
    End Sub

    Private Sub Enviar_Correo_Adjunto(_CorreoPara As String, _Asunto As String, _Cuerpo As String, _Adjuntos As List(Of String), _Tipo As String, _Clave As String, _Consecutivo As String)
        Dim _Message As New System.Net.Mail.MailMessage()
        Dim _SMTP As New System.Net.Mail.SmtpClient
        _SMTP.Credentials = New System.Net.NetworkCredential(Me.Correo, Me.Clave)

        If Me.Correo.IndexOf("@gmail.com") > 0 Then
            _SMTP.Host = "smtp.gmail.com"
            _SMTP.Port = "587"
            _SMTP.EnableSsl = True
        End If

        If Me.Correo.IndexOf("@suvesacr.com") > 0 Then
            _SMTP.Host = "smtp.hosting24.com"
            _SMTP.Port = "587"
            _SMTP.EnableSsl = True
        End If
       
        ' CONFIGURACION_DEL_MENSAJE()
        _Message.[To].Add(_CorreoPara) 'Cuenta de Correo al que se le quiere enviar el e-mail
        _Message.From = New System.Net.Mail.MailAddress(Me.Correo, Me.Correo, System.Text.Encoding.UTF8) 'Quien lo envía
        _Message.Subject = _Asunto 'Sujeto del e-mail
        _Message.SubjectEncoding = System.Text.Encoding.UTF8 'Codificacion
        _Message.Body = _Cuerpo 'contenido del mail
        _Message.BodyEncoding = System.Text.Encoding.UTF8
        _Message.Priority = System.Net.Mail.MailPriority.Normal
        _Message.IsBodyHtml = False
        For Each adj As String In _Adjuntos
            If IO.File.Exists(adj) Then
                Dim adjunto = New System.Net.Mail.Attachment(adj) 'txtUbicacion.Text + "\" + txtNombreArchivo.Text)
                _Message.Attachments.Add(adjunto)
            End If
        Next
        _Message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
        'ENVIO()
        Try
            _SMTP.Send(_Message)
            Me.Bitacora = New BitacoraCorreo(_Clave, _Consecutivo, _Tipo, "Correcto", _CorreoPara, "")
            Me.Bitacora.Guardar()
        Catch ex As System.Net.Mail.SmtpException
            Me.Bitacora = New BitacoraCorreo(_Clave, _Consecutivo, _Tipo, "Error", _CorreoPara, ex.Message)
            Me.Bitacora.Guardar()
        End Try
        _Message = Nothing
        _SMTP = Nothing
    End Sub

End Class
