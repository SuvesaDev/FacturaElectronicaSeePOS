Imports System.IO

Public Class EnvioComprobantes
    Private Documentos As New GestionDatos.Factura
    Private FTP As New OBSoluciones.Utilidades.Cliente_FTP
    Private QR As New OBSoluciones.Utilidades.QR
    Private PDF As New OBSoluciones.Utilidades.PDF
    Public Archivos As New List(Of String)
    Public Event SubirFactura()

    Private WithEvents xml As New OBSoluciones.ComprobantesElectronicos.GeneraXML_43
    Private clsEmisor As New GestionDatos.Emisor
    Private Directorio As String = "C:/Facturas/"
    Private XMLConsecutivo, XMLClave, XMLEmisorNumero, XMLEmisorTipo, XMLReceptorNumero, XMLReceptorTipo As String

    Sub New()
        Me.clsEmisor.Obtener_Datos()
    End Sub

    Public Function EnviarDevolucion(_Id As String, _Internet As Boolean) As Boolean
        If _Internet = True Then
            'Si hay internet, Envia la devolucion
            Return Me.xml.GeneraXMLDevoluciones(_Id)
        Else
            Return False
        End If
    End Function

    Public Sub EnviarFactura(_Id As String, _Internet As Boolean, _PVE As Boolean)
        If _Internet = True Then
            'Si hay internet, Envia la factura
            If _PVE = True Then
                Me.xml.GeneraXMLTiquete(_Id)
            Else
                Me.xml.GeneraXMLFactura(_Id)
            End If
        Else
            'Si no hay internet, marca la factura como contingencia            
        End If
    End Sub

    Public Function getToken() As String
        Try
            Dim iTokenHacienda As New TokenHacienda
            iTokenHacienda.GetTokenHacienda(Me.clsEmisor.usuario, Me.clsEmisor.clave)
            If iTokenHacienda.AccesoConcedido = True Then
                Return iTokenHacienda.accessToken
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private XMLConsecutivoReceptor As String = ""
    Private Sub ObtenerDatosXML(_xmlSinFirmar As String, Optional ByVal _MensajeReceptor As Boolean = False)
        Try

            Dim xmlEnvia As New Xml.XmlDocument
            xmlEnvia.LoadXml(_xmlSinFirmar)
            Me.XMLConsecutivo = ""
            Me.XMLClave = ""
            Me.XMLEmisorNumero = ""
            Me.XMLEmisorTipo = ""
            Me.XMLReceptorNumero = ""
            Me.XMLReceptorTipo = ""

            If _MensajeReceptor = False Then
                Me.XMLConsecutivo = xmlEnvia.GetElementsByTagName("NumeroConsecutivo")(0).InnerText
                Me.XMLClave = xmlEnvia.GetElementsByTagName("Clave")(0).InnerText
                Me.XMLEmisorNumero = xmlEnvia.GetElementsByTagName("Emisor")(0)("Identificacion")("Numero").InnerText
                Me.XMLEmisorTipo = xmlEnvia.GetElementsByTagName("Emisor")(0)("Identificacion")("Tipo").InnerText
                If xmlEnvia.GetElementsByTagName("Receptor").Count > 0 Then
                    If Not IsNothing(xmlEnvia.GetElementsByTagName("Receptor")(0)("Identificacion")) Then
                        Me.XMLReceptorNumero = xmlEnvia.GetElementsByTagName("Receptor")(0)("Identificacion")("Numero").InnerText
                        Me.XMLReceptorTipo = xmlEnvia.GetElementsByTagName("Receptor")(0)("Identificacion")("Tipo").InnerText
                    End If
                End If
            Else
                Me.XMLConsecutivo = xmlEnvia.GetElementsByTagName("NumeroConsecutivoReceptor")(0).InnerText
                Me.XMLClave = xmlEnvia.GetElementsByTagName("Clave")(0).InnerText
                Me.XMLEmisorNumero = xmlEnvia.GetElementsByTagName("NumeroCedulaEmisor")(0).InnerText
                Me.XMLConsecutivoReceptor = xmlEnvia.GetElementsByTagName("NumeroConsecutivoReceptor")(0).InnerText
                Select Case Len(Me.XMLEmisorNumero)
                    Case 9 : Me.XMLEmisorTipo = "01" 'FICICA
                    Case 10 : Me.XMLEmisorTipo = "02" 'JURIDICA
                    Case 11 : Me.XMLEmisorTipo = "03" 'DIMEX
                    Case 12 : Me.XMLEmisorTipo = "03" 'DIMEX
                End Select

                Me.XMLReceptorNumero = xmlEnvia.GetElementsByTagName("NumeroCedulaReceptor")(0).InnerText
                Select Case Len(Me.XMLReceptorNumero)
                    Case 9 : Me.XMLReceptorTipo = "01" 'FICICA
                    Case 10 : Me.XMLReceptorTipo = "02" 'JURIDICA
                    Case 11 : Me.XMLReceptorTipo = "03" 'DIMEX
                    Case 12 : Me.XMLReceptorTipo = "03" 'DIMEX
                End Select
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub GeneraPDFVenta(_IdFactura As String)
        Dim dtsEncabezado As New DataTable
        Dim dtsDetalle As New DataTable

        Dim cls As New GestionDatos.Factura
        dtsEncabezado = cls.Obtener_Factura_43(_IdFactura)
        dtsDetalle = cls.Obtener_DetallesFactura_43(_IdFactura)

        Me.PDF.Autor = "OBSoluciones"
        Me.PDF.Titulo = "Comprobantes Electronicos"
        Me.PDF.CrearFactura(dtsEncabezado.Rows(0).Item("CLAVE"), dtsEncabezado, dtsDetalle)
    End Sub

    Public Sub GeneraPDFDevolucion(_IdFactura As String)
        Dim dtsEncabezado As New DataTable
        Dim dtsDetalle As New DataTable

        Dim cls As New GestionDatos.Factura
        dtsEncabezado = cls.Obtener_Devolucion43(_IdFactura)
        dtsDetalle = cls.Obtener_DetallesDevolucion43(_IdFactura)

        Me.PDF.Autor = "OBSoluciones"
        Me.PDF.Titulo = "Comprobantes Electronicos"
        Me.PDF.CrearNotaCredito(dtsEncabezado.Rows(0).Item("CLAVE"), dtsEncabezado, dtsDetalle)
    End Sub

    Private Sub xml_DocumentoGenerado(_xml As String, _IdDocumento As String, _TipoDocumento As String) Handles xml.DocumentoGenerado
        Try
            If _TipoDocumento = "MENSAJERECEPTOR" Then
                Me.ObtenerDatosXML(_xml, True)
                Dim DirectorioTemp As String = "C:/Compras/" & Me.XMLConsecutivo & "/"
                'Guarda xml sin Firmar de la factura
                Dim xmlDocSF As New Xml.XmlDocument
                xmlDocSF.LoadXml(_xml)
                xmlDocSF.Save(DirectorioTemp & XMLConsecutivo & "_01_SF.xml")
                Dim xmlTextWriter As New Xml.XmlTextWriter(DirectorioTemp & XMLConsecutivo & "_01_SF.xml", New System.Text.UTF8Encoding(False))
                xmlDocSF.WriteTo(xmlTextWriter)
                xmlTextWriter.Close()
                xmlDocSF = Nothing
                'Guarda xml Firmado de la factura
                Dim _firma As New Firma
                _firma.FirmaXML_Xades_MensajeReceptor(DirectorioTemp & XMLConsecutivo, "C:\Facturas\certificado.p12", False, Me.clsEmisor.certificado)
                Dim xmlElectronica As New Xml.XmlDocument
                xmlElectronica.Load(DirectorioTemp & XMLConsecutivo & "_02_Firmado.xml")
                '*********************************************************************************************
                Dim myEmisor As New Emisor With {.numeroIdentificacion = Me.XMLEmisorNumero, .TipoIdentificacion = Me.XMLEmisorTipo}
                Dim myReceptor As New Receptor
                If Me.XMLReceptorNumero <> "" Then
                    myReceptor = New Receptor With {.numeroIdentificacion = Me.XMLReceptorNumero, .TipoIdentificacion = Me.XMLReceptorTipo}
                Else
                    myReceptor.sinReceptor = True
                End If
                Dim myRecepcion As New Recepcion
                myRecepcion.emisor = myEmisor
                myRecepcion.receptor = myReceptor
                myRecepcion.clave = Me.XMLClave
                myRecepcion.consecutivoReceptor = Me.XMLConsecutivoReceptor
                myRecepcion.fecha = Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
                myRecepcion.comprobanteXml = Funciones.EncodeStrToBase64(xmlElectronica.OuterXml)
                xmlElectronica = Nothing
                'Envia Informacion 
                Dim Token As String = getToken()
                Dim enviaFactura As New Comunicacion
                enviaFactura.EnvioDatos(Token, myRecepcion)
                'Guarda el Json del envio
                Dim jsonEnvio As String = enviaFactura.jsonEnvio
                Dim jsonRespuesta As String = enviaFactura.jsonRespuesta
                Dim outputFile As New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_03_jsonEnvio.txt")
                outputFile.Write(jsonEnvio)
                outputFile.Close()
                'Guarda el Json de la respuesta
                outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_04_jsonRespuesta.txt")
                outputFile.Write(jsonRespuesta)
                outputFile.Close()
                'Guarda respuesta
                If Not IsNothing(enviaFactura.xmlRespuesta) Then
                    enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_05_RESP.xml")
                Else
                    outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_05_RESP_SinRespuesta.txt")
                    outputFile.Write("")
                    outputFile.Close()
                End If

                If _TipoDocumento = "MENSAJERECEPTOR" Then
                    'Actualizar estados Factura
                    Select Case enviaFactura.estadoFactura
                        Case "aceptado" : Me.Documentos.MensajeAceptado(_IdDocumento, Me.XMLConsecutivo)
                        Case Else : Me.Documentos.RegistrarEstadoMensaje(_IdDocumento, Me.XMLConsecutivo, enviaFactura.estadoFactura)
                    End Select
                    'Vuelve a consultar el estado de la factura
                    Token = getToken()
                    If Token <> "" Then
                        enviaFactura = New Comunicacion
                        If enviaFactura.ConsultaEstatus(Token, Me.XMLClave) = True Then
                            jsonRespuesta = enviaFactura.jsonRespuesta
                            outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                            outputFile.Write(jsonRespuesta)
                            outputFile.Close()
                            If Not IsNothing(enviaFactura.xmlRespuesta) Then
                                enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                            End If
                            'Actualizar estados Factura
                            Select Case enviaFactura.estadoFactura
                                Case "aceptado" : Me.Documentos.CambiarEstadoMensaje_Aceptado(_IdDocumento)
                                Case Else : Me.Documentos.CambiarEstadoMensaje(_IdDocumento, enviaFactura.estadoFactura)
                            End Select
                        End If
                    End If

                End If
            ElseIf _TipoDocumento = "FACTURACOMPRA" Then
                '///////////////////////////////////////////////////////////////////////
                Me.ObtenerDatosXML(_xml, False)
                Dim DirectorioTemp As String = "C:/Compras/" & Me.XMLConsecutivo & "/"
                'Guarda xml sin Firmar de la factura
                Dim xmlDocSF As New Xml.XmlDocument
                xmlDocSF.LoadXml(_xml)
                xmlDocSF.Save(DirectorioTemp & XMLConsecutivo & "_01_SF.xml")
                Dim xmlTextWriter As New Xml.XmlTextWriter(DirectorioTemp & XMLConsecutivo & "_01_SF.xml", New System.Text.UTF8Encoding(False))
                xmlDocSF.WriteTo(xmlTextWriter)
                xmlTextWriter.Close()
                xmlDocSF = Nothing
                'Guarda xml Firmado de la factura
                Dim _firma As New Firma
                _firma.FirmaXML_Xades_MensajeReceptor(DirectorioTemp & XMLConsecutivo, "C:\Facturas\certificado.p12", False, Me.clsEmisor.certificado)
                Dim xmlElectronica As New Xml.XmlDocument
                xmlElectronica.Load(DirectorioTemp & XMLConsecutivo & "_02_Firmado.xml")

                '*********************************************************************************************
                Dim myEmisor As New Emisor With {.numeroIdentificacion = Me.XMLEmisorNumero, .TipoIdentificacion = Me.XMLEmisorTipo}
                Dim myReceptor As New Receptor
                If Me.XMLReceptorNumero <> "" Then
                    myReceptor = New Receptor With {.numeroIdentificacion = Me.XMLReceptorNumero, .TipoIdentificacion = Me.XMLReceptorTipo}
                Else
                    myReceptor.sinReceptor = True
                End If
                Dim myRecepcion As New Recepcion
                myRecepcion.emisor = myEmisor
                myRecepcion.receptor = myReceptor
                myRecepcion.clave = Me.XMLClave
                myRecepcion.consecutivoReceptor = Me.XMLConsecutivoReceptor
                myRecepcion.fecha = Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
                myRecepcion.comprobanteXml = Funciones.EncodeStrToBase64(xmlElectronica.OuterXml)
                xmlElectronica = Nothing
                'Envia Informacion 
                Dim Token As String = getToken()
                Dim enviaFactura As New Comunicacion
                enviaFactura.EnvioDatos(Token, myRecepcion)
                'Guarda el Json del envio
                Dim jsonEnvio As String = enviaFactura.jsonEnvio
                Dim jsonRespuesta As String = enviaFactura.jsonRespuesta
                Dim outputFile As New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_03_jsonEnvio.txt")
                outputFile.Write(jsonEnvio)
                outputFile.Close()
                'Guarda el Json de la respuesta
                outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_04_jsonRespuesta.txt")
                outputFile.Write(jsonRespuesta)
                outputFile.Close()
                'Guarda respuesta
                If Not IsNothing(enviaFactura.xmlRespuesta) Then
                    enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_05_RESP.xml")
                Else
                    outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_05_RESP_SinRespuesta.txt")
                    outputFile.Write("")
                    outputFile.Close()
                End If

                If _TipoDocumento = "FACTURACOMPRA" Then
                    'Actualizar estados Factura
                    Select Case enviaFactura.estadoFactura
                        Case "aceptado" : Me.Documentos.CompraAceptado(_IdDocumento, Me.XMLConsecutivo)
                        Case Else : Me.Documentos.RegistrarEstadoCompra(_IdDocumento, Me.XMLConsecutivo, enviaFactura.estadoFactura)
                    End Select
                    'Vuelve a consultar el estado de la factura
                    Token = getToken()
                    If Token <> "" Then
                        enviaFactura = New Comunicacion
                        If enviaFactura.ConsultaEstatus(Token, Me.XMLClave) = True Then
                            jsonRespuesta = enviaFactura.jsonRespuesta
                            outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                            outputFile.Write(jsonRespuesta)
                            outputFile.Close()
                            If Not IsNothing(enviaFactura.xmlRespuesta) Then
                                enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                            End If
                            'Actualizar estados Factura
                            Select Case enviaFactura.estadoFactura
                                Case "aceptado" : Me.Documentos.CambiarEstadoCompra_Aceptado(_IdDocumento)
                                Case Else : Me.Documentos.CambiarEstadoCompra(_IdDocumento, enviaFactura.estadoFactura)
                            End Select
                        End If
                    End If
                End If
                '///////////////////////////////////////////////////////////////////////
            Else
                'Obtiene los datos del xml
                Me.ObtenerDatosXML(_xml)
                Dim DirectorioTemp As String = Me.Directorio & Me.XMLConsecutivo & "/"
                'Guarda xml sin Firmar de la factura
                Dim xmlDocSF As New Xml.XmlDocument
                xmlDocSF.LoadXml(_xml)
                xmlDocSF.Save(DirectorioTemp & XMLConsecutivo & "_01_SF.xml")
                Dim xmlTextWriter As New Xml.XmlTextWriter(DirectorioTemp & XMLConsecutivo & "_01_SF.xml", New System.Text.UTF8Encoding(False))
                xmlDocSF.WriteTo(xmlTextWriter)
                xmlTextWriter.Close()
                xmlDocSF = Nothing
                'Guarda xml Firmado de la factura
                Dim _firma As New Firma
                _firma.FirmaXML_Xades(DirectorioTemp & XMLConsecutivo, "C:\Facturas\certificado.p12", False, Me.clsEmisor.certificado)
                Dim xmlElectronica As New Xml.XmlDocument
                xmlElectronica.Load(DirectorioTemp & XMLConsecutivo & "_02_Firmado.xml")

                Dim myEmisor As New Emisor With {.numeroIdentificacion = Me.XMLEmisorNumero, .TipoIdentificacion = Me.XMLEmisorTipo}
                Dim myReceptor As New Receptor
                If Me.XMLReceptorNumero <> "" Then
                    myReceptor = New Receptor With {.numeroIdentificacion = Me.XMLReceptorNumero, .TipoIdentificacion = Me.XMLReceptorTipo}
                Else
                    myReceptor.sinReceptor = True
                End If
                Dim myRecepcion As New Recepcion
                myRecepcion.emisor = myEmisor
                myRecepcion.receptor = myReceptor
                myRecepcion.clave = Me.XMLClave
                myRecepcion.consecutivoReceptor = ""
                myRecepcion.fecha = Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
                myRecepcion.comprobanteXml = Funciones.EncodeStrToBase64(xmlElectronica.OuterXml)
                xmlElectronica = Nothing
                'Envia Informacion 
                Dim Token As String = getToken()
                If Token <> "" Then
                    Dim enviaFactura As New Comunicacion
                    If enviaFactura.EnvioDatos(Token, myRecepcion) = True Then

                        'Guarda el Json del envio
                        Dim jsonEnvio As String = enviaFactura.jsonEnvio
                        Dim jsonRespuesta As String = enviaFactura.jsonRespuesta
                        Dim outputFile As New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_03_jsonEnvio.txt")
                        outputFile.Write(jsonEnvio)
                        outputFile.Close()
                        'Guarda el Json de la respuesta
                        outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_04_jsonRespuesta.txt")
                        outputFile.Write(jsonRespuesta)
                        outputFile.Close()
                        'Guarda respuesta
                        If Not IsNothing(enviaFactura.xmlRespuesta) Then
                            enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_05_RESP.xml")
                        Else
                            outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_05_RESP_SinRespuesta.txt")
                            outputFile.Write("")
                            outputFile.Close()
                        End If

                        If _TipoDocumento = "FACTURA" Then
                            'Crea Codigo QR
                            'QR.GenerarCodigo(DirectorioTemp & Me.XMLClave & ".png", Me.FTP.DireccionPublica + "/COMPROBANTES/" & Me.XMLConsecutivo & "/" & Me.XMLClave & ".pdf")
                            'Crear pdf con el codigo QR
                            Me.PDF.Autor = "OBSoluciones"
                            Me.PDF.Titulo = "Comprobantes Electronicos"
                            Me.PDF.CrearFactura(Me.XMLClave, Me.xml.dtsEncabezado, Me.xml.dtsDetalle)
                            'Manda a subir los archivos al FTP
                            Me.Archivos.Clear()
                            Dim Archivo As String = ""
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_01_SF.xml"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_02_Firmado.xml"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_03_jsonEnvio.txt"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_04_jsonRespuesta.txt"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_05_RESP.txt"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_06_jsonRespuestaClave.txt"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLConsecutivo & "_07_RespuestaClave.xml"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            'Archivo = DirectorioTemp & Me.XMLClave & ".png"
                            'If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            Archivo = DirectorioTemp & Me.XMLClave & ".pdf"
                            If IO.File.Exists(Archivo) Then Archivos.Add(Archivo)
                            RaiseEvent SubirFactura()
                        End If

                        If _TipoDocumento = "FACTURA" Or _TipoDocumento = "TIQUETE" Then
                            'Actualizar estados Factura
                            Select Case enviaFactura.estadoFactura
                                Case "aceptado" : Me.Documentos.FacturaAceptada(_IdDocumento, Me.XMLClave, Me.XMLConsecutivo)
                                Case "rechazado"
                                    Me.Documentos.RegistrarEstadoFactura(_IdDocumento, Me.XMLClave, Me.XMLConsecutivo, enviaFactura.estadoFactura)
                                    If enviaFactura.MotiviRechazo.IndexOf("firma del comprobante electrónico no es válida") > 0 Then
                                        Me.Documentos.CambiarEstadoComprobante(Me.XMLClave, "procesando")
                                    Else
                                        Dim correo As New Correo
                                        correo.NotificarFacturaRechazada(_IdDocumento, "rolando.obando@gmail.com", enviaFactura.MotiviRechazo)
                                    End If
                                Case Else : Me.Documentos.RegistrarEstadoFactura(_IdDocumento, Me.XMLClave, Me.XMLConsecutivo, enviaFactura.estadoFactura)
                            End Select
                            'Vuelve a consultar el estado de la factura
                            Token = getToken()
                            If Token <> "" Then
                                enviaFactura = New Comunicacion
                                If enviaFactura.ConsultaEstatus(Token, Me.XMLClave) = True Then
                                    jsonRespuesta = enviaFactura.jsonRespuesta
                                    outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                                    outputFile.Write(jsonRespuesta)
                                    outputFile.Close()
                                    If Not IsNothing(enviaFactura.xmlRespuesta) Then
                                        enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                                    End If
                                    'Actualizar estados Factura
                                    Select Case enviaFactura.estadoFactura
                                        Case "aceptado" : Me.Documentos.CambiarEstadoFactura_Aceptada(_IdDocumento)
                                        Case "rechazado"
                                            Me.Documentos.CambiarEstadoFactura(_IdDocumento, enviaFactura.estadoFactura)
                                            If enviaFactura.MotiviRechazo.IndexOf("firma del comprobante electrónico no es válida") > 0 Then
                                                Me.Documentos.CambiarEstadoComprobante(Me.XMLClave, "procesando")
                                            End If
                                        Case Else : Me.Documentos.CambiarEstadoFactura(_IdDocumento, enviaFactura.estadoFactura)
                                    End Select
                                End If
                            End If

                        End If
                        If _TipoDocumento = "DEVOLUCION" Then
                            Me.PDF.Autor = "OBSoluciones"
                            Me.PDF.Titulo = "Comprobantes Electronicos"
                            Me.PDF.CrearNotaCredito(Me.XMLClave, Me.xml.dtsEncabezado, Me.xml.dtsDetalle)
                            'Actualizar estados Factura
                            Select Case enviaFactura.estadoFactura
                                Case "aceptado" : Me.Documentos.DevolucionAceptada(_IdDocumento, Me.XMLClave, Me.XMLConsecutivo)
                                Case "rechazado"
                                    Me.Documentos.RegistrarEstadoDevolucion(_IdDocumento, Me.XMLClave, Me.XMLConsecutivo, enviaFactura.estadoFactura)
                                    If enviaFactura.MotiviRechazo.IndexOf("firma del comprobante electrónico no es válida") > 0 Then
                                        Me.Documentos.CambiarEstadoComprobante(Me.XMLClave, "procesando")
                                    End If
                                Case Else : Me.Documentos.RegistrarEstadoDevolucion(_IdDocumento, Me.XMLClave, Me.XMLConsecutivo, enviaFactura.estadoFactura)
                            End Select
                            'Vuelve a consultar el estado de la factura
                            Token = getToken()
                            If Token <> "" Then
                                enviaFactura = New Comunicacion
                                If enviaFactura.ConsultaEstatus(Token, Me.XMLClave) = True Then
                                    jsonRespuesta = enviaFactura.jsonRespuesta
                                    outputFile = New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                                    outputFile.Write(jsonRespuesta)
                                    outputFile.Close()
                                    If Not IsNothing(enviaFactura.xmlRespuesta) Then
                                        enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                                    End If
                                    'Actualizar estados Factura
                                    Select Case enviaFactura.estadoFactura
                                        Case "aceptado" : Me.Documentos.CambiarEstadoDevolucion_Aceptada(_IdDocumento)
                                        Case "rechazado"
                                            Me.Documentos.CambiarEstadoDevolucion(_IdDocumento, enviaFactura.estadoFactura)
                                            If enviaFactura.MotiviRechazo.IndexOf("firma del comprobante electrónico no es válida") > 0 Then
                                                Me.Documentos.CambiarEstadoComprobante(Me.XMLClave, "procesando")
                                            End If
                                        Case Else : Me.Documentos.CambiarEstadoDevolucion(_IdDocumento, enviaFactura.estadoFactura)
                                    End Select
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
fin:

    End Sub

    Public Sub ActualizarEstadoFactura(_IdFactura As String)
        Dim ClaveMH As String = Documentos.Obtener_ClaveFacturaMH(_IdFactura)
        Me.XMLConsecutivo = Documentos.Obtener_ConsecutivoFacturaMH(_IdFactura)
        Dim DirectorioTemp As String = Me.Directorio & Me.XMLConsecutivo & "/"
        If IO.Directory.Exists(DirectorioTemp) Then IO.Directory.CreateDirectory(DirectorioTemp)

        Dim Token As String = getToken()
        If Token <> "" Then
            Dim enviaFactura As New Comunicacion
            If enviaFactura.ConsultaEstatus(Token, ClaveMH) = True Then
                Dim jsonRespuesta As String = enviaFactura.jsonRespuesta
                Try
                    Dim outputFile As New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                    outputFile.Write(jsonRespuesta)
                    outputFile.Close()
                Catch ex As Exception
                End Try
                If Not IsNothing(enviaFactura.xmlRespuesta) Then
                    enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                End If

                Select Case enviaFactura.estadoFactura
                    Case "aceptado" : Me.Documentos.CambiarEstadoFactura_Aceptada(_IdFactura)
                    Case "rechazado"
                        Me.Documentos.CambiarEstadoFactura(_IdFactura, enviaFactura.estadoFactura)
                        If enviaFactura.MotiviRechazo.IndexOf("firma del comprobante electrónico no es válida") > 0 Then
                            Me.Documentos.CambiarEstadoComprobante(ClaveMH, "procesando")
                        End If
                    Case Else : Me.Documentos.CambiarEstadoFactura(_IdFactura, enviaFactura.estadoFactura)
                End Select
            End If
        End If
    End Sub

    Public Sub ActualizarEstadoDevolucion(_IdDevolucion As String)
        Dim ClaveMH As String = Documentos.Obtener_ClaveDevolucionMH(_IdDevolucion)
        Me.XMLConsecutivo = Documentos.Obtener_ConsecutivoDevolucionMH(_IdDevolucion)
        Dim DirectorioTemp As String = Me.Directorio & Me.XMLConsecutivo & "/"
        Dim Token As String = getToken()
        If Token <> "" Then
            Dim enviaFactura As New Comunicacion
            If enviaFactura.ConsultaEstatus(Token, ClaveMH) = True Then
                Dim jsonRespuesta As String = enviaFactura.jsonRespuesta
                Dim outputFile As New IO.StreamWriter(DirectorioTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                outputFile.Write(jsonRespuesta)
                outputFile.Close()
                If Not IsNothing(enviaFactura.xmlRespuesta) Then
                    enviaFactura.xmlRespuesta.Save(DirectorioTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                End If

                Select Case enviaFactura.estadoFactura
                    Case "aceptado" : Me.Documentos.CambiarEstadoDevolucion_Aceptada(_IdDevolucion)
                    Case "rechazado"
                        Me.Documentos.CambiarEstadoDevolucion(_IdDevolucion, enviaFactura.estadoFactura)
                        If enviaFactura.MotiviRechazo.IndexOf("firma del comprobante electrónico no es válida") > 0 Then
                            Me.Documentos.CambiarEstadoComprobante(ClaveMH, "procesando")
                        End If
                    Case Else : Me.Documentos.CambiarEstadoDevolucion(_IdDevolucion, enviaFactura.estadoFactura)
                End Select
            End If
        End If
    End Sub

    Public Function EnviarMensajeReceptor(_Id As String) As Boolean
        Return Me.xml.GeneraXMLMensajeReceptor(_Id)
    End Function

    Private Function ObtenerCorreo(_IdRecibo As String) As String
        Dim Correo As String = ""
        Dim dt As New DataTable
        dt = General.sql.Ejecutar("select c.CorreoRecibo As Correo from " & General.BasedeDatos & ".dbo.abonoccobrar a inner join " & General.BasedeDatos & ".dbo.Clientes c on c.identificacion = a.Cod_Cliente where a.Id_Recibo = " & _IdRecibo, CommandType.Text)
        If dt.Rows.Count > 0 Then
            Correo = dt.Rows(0).Item("Correo")
        Else
            Correo = "rolando.obando@gmail.com"
        End If
        Return Correo
    End Function

    Private Function ObtenerRecibo(_IdRecibo As String) As String
        Dim Correo As String = ""
        Dim dt As New DataTable
        dt = General.sql.Ejecutar("select a.Num_Recibo As Correo from " & General.BasedeDatos & ".dbo.abonoccobrar a where a.Id_Recibo = " & _IdRecibo, CommandType.Text)
        If dt.Rows.Count > 0 Then
            Correo = dt.Rows(0).Item("Correo")
        Else
            Correo = "0"
        End If
        Return Correo
    End Function

    Private ProcesandoIdRecibo As Boolean = False

    Public Function EnviarNotificacionReciboCXC(_Id As String) As Boolean
        Try
            If ProcesandoIdRecibo = False Then

                Dim dt As New DataTable
                dt = General.sql.Ejecutar("select * from " & General.BasedeDatos & ".dbo.abonoccobrar a where a.Id_Recibo = " & _Id, CommandType.Text)
                If dt.Rows.Count > 0 Then
                    'si el recibo existe lo envia

                    Dim Archivo As String = System.IO.Path.GetTempFileName.Replace(".tmp", ".pdf")
                    Dim ArchivoRPT As String = System.IO.Path.GetTempFileName.Replace(".pdf", ".rpt")
                    Me.ProcesandoIdRecibo = True

                    Dim Resultado As Boolean = False
                    Dim Correo As New Correo
                    Dim Adjuntos As New List(Of String)
                    Dim Asunto As String = "Notificacion comprobante de pago #" & Me.ObtenerRecibo(_Id)
                    Dim Cuerpo As String = ""
                    Dim CorreoPara As String = ""
                    Dim Empresa As String = "Super Veterinaria Liberia"

                    Dim rpt As New ReciboDinero
                    rpt.SetParameterValue(0, CInt(_Id))
                    CrystalReportsConexion.LoadReportViewer(Nothing, rpt, True)
                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.CrystalReport, ArchivoRPT)

                    If IO.File.Exists(Archivo) Then
                        Adjuntos.Add(Archivo)
                        IO.File.Copy(Archivo, "C:\temporales\Recibos\" & _Id & ".pdf")
                    End If

                    If Adjuntos.Count > 0 Then
                        Cuerpo = "reciba un cordial saludo de parte del equipo de trabajo de " & Empresa & "." & vbNewLine _
                        & "Adjunto comprobante de pago" & vbNewLine _
                        & "" & vbNewLine _
                        & "Gracias por preferirnos." & vbNewLine _
                        & "" & _Id
                        CorreoPara = Me.ObtenerCorreo(_Id)
                        Correo.EnviarNotificacionRecibo(CorreoPara, Asunto, Cuerpo, Adjuntos)
                        General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.abonoccobrar set EstadoEnvio = 'Enviado' Where Id_Recibo = " & _Id, CommandType.Text)
                        Me.ProcesandoIdRecibo = False
                    End If

                End If
            End If
        Catch ex As Exception
            Me.Escribir(ex.StackTrace)
            Me.ProcesandoIdRecibo = False
        End Try
        Return True
    End Function

    Private Sub Escribir(_Texto As String)
        Try
            Dim ruta As String = "c:\logErrorReciboCxC.txt"
            Dim escritor As StreamWriter
            escritor = File.AppendText(ruta)
            escritor.Write(_Texto)
            escritor.Flush()
            escritor.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Function EnviarCompra(_Id As String) As Boolean
        Return Me.xml.GeneraXMLCompras(_Id)
    End Function

    Public Sub ActualizarEstadoCompra(_IdCompra As String)
        Dim cls As New GestionDatos.Factura
        Dim dt As New DataTable
        Dim estado As String = ""

        dt = cls.GET_COMPRA_x_ID(_IdCompra)
        estado = dt.Rows(0).Item("ESTADO")
        Me.XMLClave = dt.Rows(0).Item("CLAVE")
        Me.XMLConsecutivo = dt.Rows(0).Item("CONSECUTIVO")
        Dim DirecionTemp As String = "C:/Compras/" & Me.XMLConsecutivo & "/"

        If estado = "pendiente" Then
            Me.EnviarCompra(_IdCompra)
        End If

        If estado = "procesando" Or estado = "recibido" Or estado = "" Then
            If IO.Directory.Exists(DirecionTemp) = False Then IO.Directory.CreateDirectory(DirecionTemp)
            Dim Token As String = getToken()
            If Token <> "" Then
                Dim enviaComprobante As New Comunicacion
                If enviaComprobante.ConsultaEstatus(Token, Me.XMLClave) = True Then
                    Dim jsonEnvio As String = enviaComprobante.jsonEnvio
                    Dim jsonRespuesta As String = enviaComprobante.jsonRespuesta
                    Dim outputFile As New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_03_jsonEnvio.txt")
                    outputFile.Write(jsonEnvio)
                    outputFile.Close()
                    'Guarda el Json de la respuesta
                    outputFile = New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_04_jsonRespuesta.txt")
                    outputFile.Write(jsonRespuesta)
                    outputFile.Close()
                    'Guarda respuesta
                    If Not IsNothing(enviaComprobante.xmlRespuesta) Then
                        enviaComprobante.xmlRespuesta.Save(DirecionTemp & XMLConsecutivo & "_05_RESP.xml")
                    Else
                        outputFile = New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_05_RESP_SinRespuesta.txt")
                        outputFile.Write("")
                        outputFile.Close()
                    End If

                    jsonRespuesta = enviaComprobante.jsonRespuesta
                    outputFile = New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                    outputFile.Write(jsonRespuesta)
                    outputFile.Close()
                    If Not IsNothing(enviaComprobante.xmlRespuesta) Then
                        enviaComprobante.xmlRespuesta.Save(DirecionTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                    End If

                    Select Case enviaComprobante.estadoFactura
                        Case "aceptado" : Me.Documentos.CambiarEstadoMensaje_Aceptado(_IdCompra)
                        Case Else : Me.Documentos.CambiarEstadoMensaje(_IdCompra, enviaComprobante.estadoFactura)
                    End Select
                End If
            End If
        End If
    End Sub

    Public Sub ActualizarEstadoMensaje(_IdMensaje As Integer)
        Dim cls As New GestionDatos.Factura
        Dim dt As New DataTable
        Dim estado As String = ""

        dt = cls.GET_MENSAJE_x_ID(_IdMensaje)
        estado = dt.Rows(0).Item("ESTADO")
        Me.XMLClave = dt.Rows(0).Item("CLAVE")
        Me.XMLConsecutivo = dt.Rows(0).Item("CONSECUTIVO")
        Dim DirecionTemp As String = "C:/Compras/" & Me.XMLConsecutivo & "/"

        If estado = "pendiente" Then
            Me.EnviarMensajeReceptor(_IdMensaje)
        End If

        If estado = "procesando" Or estado = "recibido" Or estado = "" Then
            If IO.Directory.Exists(DirecionTemp) = False Then IO.Directory.CreateDirectory(DirecionTemp)
            Dim Token As String = getToken()
            If Token <> "" Then
                Dim enviaComprobante As New Comunicacion
                If enviaComprobante.ConsultaEstatus(Token, Me.XMLClave) = True Then
                    Dim jsonEnvio As String = enviaComprobante.jsonEnvio
                    Dim jsonRespuesta As String = enviaComprobante.jsonRespuesta
                    Dim outputFile As New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_03_jsonEnvio.txt")
                    outputFile.Write(jsonEnvio)
                    outputFile.Close()
                    'Guarda el Json de la respuesta
                    outputFile = New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_04_jsonRespuesta.txt")
                    outputFile.Write(jsonRespuesta)
                    outputFile.Close()
                    'Guarda respuesta
                    If Not IsNothing(enviaComprobante.xmlRespuesta) Then
                        enviaComprobante.xmlRespuesta.Save(DirecionTemp & XMLConsecutivo & "_05_RESP.xml")
                    Else
                        outputFile = New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_05_RESP_SinRespuesta.txt")
                        outputFile.Write("")
                        outputFile.Close()
                    End If

                    jsonRespuesta = enviaComprobante.jsonRespuesta
                    outputFile = New IO.StreamWriter(DirecionTemp & XMLConsecutivo & "_06_jsonRespuestaClave.txt")
                    outputFile.Write(jsonRespuesta)
                    outputFile.Close()
                    If Not IsNothing(enviaComprobante.xmlRespuesta) Then
                        enviaComprobante.xmlRespuesta.Save(DirecionTemp & XMLConsecutivo & "_07_RespuestaClave.xml")
                    End If

                    Select Case enviaComprobante.estadoFactura
                        Case "aceptado" : Me.Documentos.CambiarEstadoMensaje_Aceptado(_IdMensaje)
                        Case Else : Me.Documentos.CambiarEstadoMensaje(_IdMensaje, enviaComprobante.estadoFactura)
                    End Select
                End If
            End If
        End If

    End Sub

    Private Sub xml_setDifMontos(_IdFactura As String) Handles xml.setDifMontos
        Me.Documentos.RegistrarEstadoFactura(_IdFactura, Me.XMLClave, Me.XMLConsecutivo, "-PROCESANDO-")
    End Sub

End Class
