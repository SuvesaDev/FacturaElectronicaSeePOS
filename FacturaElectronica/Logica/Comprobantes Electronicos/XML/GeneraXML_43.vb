'***************************************************************************************************************
'Clase para generar los xml para la version 4.3 de facturacion electronica
'@utor
'       ing. Rolando Obando Rojas
'
'Fecha Inicio : 04/06/2019 
'***************************************************************************************************************
Imports System.IO
Imports System.Xml
Imports System.Data
Namespace OBSoluciones
    Namespace ComprobantesElectronicos
        Public Class GeneraXML_43
            Private xml As XmlTextWriter
            Private UbicacionXMLFacturas As String = "C:/Facturas"
            Private UbicacionXMLCompras As String = "C:/Compras"
            Public dtsEncabezado As New DataTable
            Public dtsDetalle As New DataTable
            Public Event DocumentoGenerado(ByVal _xml As String, ByVal _IdDocumento As String, _TipoDocumento As String)
            Public Event setError(ByVal _msg As String)
            Public Event setDifMontos(_IdFactura As String)
            'CONSTRUCTOR DE LA CLASE
            Sub New()
                If Directory.Exists(Me.UbicacionXMLFacturas) = False Then Directory.CreateDirectory(Me.UbicacionXMLFacturas)
            End Sub
            'METODOS PARA AGREGAR ELEMENTOS AL XML
            Private Sub AgregarElementoManual(ByVal _TituloXml As String, ByVal _Texto As String)
                If _Texto <> "" Then
                    Me.xml.WriteStartElement(_TituloXml)
                    Me.xml.WriteString(_Texto)
                    Me.xml.WriteEndElement()
                End If
            End Sub
            Private Sub AgregarElemento(ByVal _TituloXml As String, ByVal _ColumnName As String)
                Me.xml.WriteStartElement(_TituloXml)
                Me.xml.WriteString(Me.dtsEncabezado.Rows(0).Item(_ColumnName))
                Me.xml.WriteEndElement()
            End Sub
            Private Sub AgregarElemento(ByVal _TituloXml As String, ByVal _ColumnName As String, _SubAtributo As String, _ValorSubAtributo As String)
                Me.xml.WriteStartElement(_TituloXml)
                Me.xml.WriteAttributeString(_SubAtributo, _ValorSubAtributo)
                Me.xml.WriteString(Me.dtsEncabezado.Rows(0).Item(_ColumnName))
                Me.xml.WriteEndElement()
            End Sub
            Private Sub AgregarElemento(ByVal _Index As Integer, ByVal _TituloXml As String, ByVal _ColumnName As String)
                Dim Valor As String = Me.dtsDetalle.Rows(_Index).Item(_ColumnName)
                If Valor = "NO" Then Exit Sub
                Me.xml.WriteStartElement(_TituloXml)
                Me.xml.WriteString(Valor)
                Me.xml.WriteEndElement()
            End Sub
            Private Sub CargarDatosFactura(ByVal _IdFactura As String)
                Dim cls As New GestionDatos.Factura
                Me.dtsEncabezado = cls.Obtener_Factura_43(_IdFactura)
                Me.dtsDetalle = cls.Obtener_DetallesFactura_43(_IdFactura)
            End Sub
            Private Sub CargarDatosFacturaCompra(ByVal _IdFactura As String)
                Dim cls As New GestionDatos.Factura
                Me.dtsEncabezado = cls.Obtener_FacturaCompra43(_IdFactura)
                Me.dtsDetalle = cls.Obtener_DetalleFacturaCompra(_IdFactura)
            End Sub
            Private Sub CargarDatosDevolucion(ByVal _IdDevolucion As String)
                Dim cls As New GestionDatos.Factura
                Me.dtsEncabezado = cls.Obtener_Devolucion43(_IdDevolucion)
                Me.dtsDetalle = cls.Obtener_DetallesDevolucion43(_IdDevolucion)
            End Sub
            Private Sub CargarDatosMensaje(ByVal _IdMensaje As String)
                Dim cls As New GestionDatos.Factura
                Me.dtsEncabezado = cls.GET_MENSAJE_XML43(_IdMensaje)
            End Sub
            Private Function NumeroRandom() As String
                Dim Generator As System.Random = New System.Random()
                Return CStr(Generator.Next(0, 99999999).ToString).PadLeft(8, "0")
            End Function
            Private Function ObtenerClave(ByVal _Cons As String) As String
                Dim Clave As String = ""
                Try
                    Dim Pais As String = dtsEncabezado.Rows(0).Item("CodigoPais_Emisor")
                    Dim Fecha() As String = CDate(dtsEncabezado.Rows(0).Item("FECHA")).ToShortDateString.Split("/")
                    Dim Cedula As String = dtsEncabezado.Rows(0).Item("Numero_Emisor").ToString.Replace("-", "")
                    Clave = Pais + Fecha(0) + Fecha(1).PadLeft(2, "0") + Fecha(2).Substring(2, 2) + Cedula.PadLeft(12, "0") + _Cons + CStr(1) + Me.NumeroRandom
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try
                Return Clave
            End Function
            'GENERA XML FACTURA
            Public Function GeneraXMLFactura(ByVal _IdFactura As String) As Boolean
                Dim consecutivo As String = ""
                Dim clave As String = ""
                Me.CargarDatosFactura(_IdFactura)

                Dim TotalServGravados, TotalServExentos As Decimal

                Dim SubTotal, Descuento, Impuesto, Total As Decimal
                TotalServGravados = 0
                TotalServExentos = 0
                SubTotal = 0
                Descuento = 0
                Impuesto = 0
                Total = 0

                Try

                    consecutivo = dtsEncabezado.Rows(0).Item("NumeroConsecutivo")
                    clave = dtsEncabezado.Rows(0).Item("Clave")

                    Dim TotalComprobante, TotalImpreso, DifTotales As Decimal
                    TotalComprobante = 0
                    TotalImpreso = 0
                    DifTotales = 0

                    TotalComprobante = dtsEncabezado.Rows(0).Item("TotalComprobante")
                    TotalImpreso = dtsEncabezado.Rows(0).Item("TotalImpreso")
                    DifTotales = Math.Abs(TotalComprobante - TotalImpreso)

                    If DifTotales > 50 Then
                        RaiseEvent setDifMontos(_IdFactura)
                        Return False
                    End If

                    Me.UbicacionXMLFacturas = "C:/Facturas"
                    Me.UbicacionXMLFacturas += "/" & consecutivo
                    If Directory.Exists(Me.UbicacionXMLFacturas) = False Then Directory.CreateDirectory(Me.UbicacionXMLFacturas)
                    Me.xml = New XmlTextWriter(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml", System.Text.Encoding.UTF8)
                    Me.xml.WriteStartDocument(True)
                    Me.xml.Formatting = Formatting.Indented
                    Me.xml.Indentation = 2
                    Me.xml.WriteStartElement("FacturaElectronica") '                                    FACTURA ELECTRONICA     *********************************
                    Me.xml.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronica")
                    Me.xml.WriteAttributeString("xmlns:ns2", "http://www.w3.org/2000/09/xmldsig#")
                    Me.xml.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                    Me.xml.WriteAttributeString("xsi:schemaLocation", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicahttps://www.hacienda.go.cr/ATV/ComprobanteElectronico/docs/esquemas/2016/v4.3/FacturaElectronica_V4.3.xsd")
                    Me.AgregarElementoManual("Clave", clave) '                                              clave
                    Me.AgregarElemento("CodigoActividad", "CodigoActividad") '                              codigo actividad
                    Me.AgregarElementoManual("NumeroConsecutivo", consecutivo) '                            consecutivo
                    Me.AgregarElemento("FechaEmision", "FechaEmision") '                                    fecha emision
                    Me.xml.WriteStartElement("Emisor") '                                                EMISOR                  *********************************
                    Me.AgregarElemento("Nombre", "Nombre_Emisor") '                                         nombre
                    Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                    Me.AgregarElemento("Tipo", "Tipo_Emisor") '                                             tipo
                    Me.AgregarElemento("Numero", "Numero_Emisor") '                                         numero
                    Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                    Me.xml.WriteStartElement("Ubicacion") '                                             UBICACION               *********************************
                    Me.AgregarElemento("Provincia", "Provincia_Emisor") '                                   provincia
                    Me.AgregarElemento("Canton", "Canton_Emisor") '                                         canton
                    Me.AgregarElemento("Distrito", "Distrito_Emisor") '                                     distrito
                    Me.AgregarElemento("OtrasSenas", "OtrasSenas_Emisor") '                                 otras señas
                    Me.xml.WriteEndElement() '                                                          //UBICACION             *********************************
                    Me.xml.WriteStartElement("Telefono") '                                              TELEFONO                *********************************
                    Me.AgregarElemento("CodigoPais", "CodigoPais_Emisor") '                                 codigo pais
                    Me.AgregarElemento("NumTelefono", "NumTelefono_Emisor") '                               num telefono
                    Me.xml.WriteEndElement() '                                                          //TELEFONO              *********************************
                    Me.AgregarElemento("CorreoElectronico", "CorreoElectronico_Emisor") '                   correoelctronico
                    Me.xml.WriteEndElement() '                                                          //EMISOR                *********************************
                    Me.xml.WriteStartElement("Receptor") '                                              RECEPTOR                *********************************
                    Me.AgregarElemento("Nombre", "Nombre_Receptor") '                                       nombre
                    Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                    Me.AgregarElemento("Tipo", "Tipo_Receptor") '                                           tipo
                    Me.AgregarElemento("Numero", "Numero_Receptor") '                                       numero
                    Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                    'Me.AgregarElemento("CorreoElectronico", "CorreoElectronico_Receptor") '                 correoelctronico
                    Me.xml.WriteEndElement() '                                                          //RECEPTOR              *********************************
                    Me.AgregarElemento("CondicionVenta", "CondicionVenta") '                                condiconventa
                    Me.AgregarElemento("PlazoCredito", "PlazoCredito") '                                    plazocredito
                    Me.AgregarElemento("MedioPago", "MedioPago") '                                          mediopago
                    Me.xml.WriteStartElement("DetalleServicio") '                                       DETALLESERVICOS         *********************************
                    For i As Integer = 0 To Me.dtsDetalle.Rows.Count - 1
                        Me.xml.WriteStartElement("LineaDetalle") '                                      LINEADETALLE            ********************************* 
                        Me.AgregarElementoManual("NumeroLinea", i + 1) '                                    numerolinea
                        Me.AgregarElemento(i, "Codigo", "Cabys")
                        Me.xml.WriteStartElement("CodigoComercial") '                                            CODIGO                  *********************************
                        Me.AgregarElemento(i, "Tipo", "Tipo") '                                             tipo
                        Me.AgregarElemento(i, "Codigo", "Codigo") '                                         codigo
                        Me.xml.WriteEndElement() '                                                      //CODIGO                *********************************
                        Me.AgregarElemento(i, "Cantidad", "Cantidad") '                                     cantidad
                        Me.AgregarElemento(i, "UnidadMedida", "UnidadMedida") '                             unidadmedida
                        Me.AgregarElemento(i, "Detalle", "Detalle") '                                       detalle
                        Me.AgregarElemento(i, "PrecioUnitario", "PrecioUnitario") '                         preciounitario
                        Me.AgregarElemento(i, "MontoTotal", "MontoTotal") '                                 montototal
                        If Me.dtsDetalle.Rows(i).Item("MontoDescuento") > 0 Then
                            Me.xml.WriteStartElement("Descuento")
                            Me.AgregarElemento(i, "MontoDescuento", "MontoDescuento") '                         montodescuento                        
                            Me.AgregarElemento(i, "NaturalezaDescuento", "NaturalezaDescuento") '               naturalezadescuento
                            Me.xml.WriteEndElement()
                        End If
                        Me.AgregarElemento(i, "SubTotal", "SubTotal") '                                     subtotal
                        If Me.dtsDetalle.Rows(i).Item("Tarifa_Impuesto") > 0 Then
                            Me.xml.WriteStartElement("Impuesto") '                                          IMPUESTO                *********************************
                            Me.AgregarElemento(i, "Codigo", "Codigo_Impuesto") '                                codigo
                            Me.AgregarElemento(i, "CodigoTarifa", "CodigoTarifa") '                             codigotarifa
                            Me.AgregarElemento(i, "Tarifa", "Tarifa_Impuesto") '                                tarifa
                            Me.AgregarElemento(i, "Monto", "Monto_Impuesto") '                                  monto
                            If Me.dtsDetalle.Rows(i).Item("MontoImpuesto_Exoneracion") > 1 And Me.dtsDetalle.Rows(i).Item("NumeroDocumento_Exoneracion") <> "" Then
                                Me.xml.WriteStartElement("Exoneracion") '                                   EXONERACION             *********************************
                                Me.AgregarElemento(i, "TipoDocumento", "TipoDocumento_Exoneracion") '           tipodocumento
                                Me.AgregarElemento(i, "NumeroDocumento", "NumeroDocumento_Exoneracion") '       numerodocumento
                                Me.AgregarElemento(i, "NombreInstitucion", "NombreInstitucion_Exoneracion") '   nombreinstitucion
                                Me.AgregarElemento(i, "FechaEmision", "FechaEmision_Exoneracion") '             fechaemision
                                Me.AgregarElemento(i, "PorcentajeExoneracion", "PorcentajeCompra_Exoneracion") '     porcentajecompra
                                Me.AgregarElemento(i, "MontoExoneracion", "MontoImpuesto_Exoneracion") '           montoimpuesto
                                Me.xml.WriteEndElement() '                                                  //EXONERACION           *********************************
                            End If
                            Me.xml.WriteEndElement() '                                                      //IMPUESTO              *********************************
                            If Me.dtsDetalle.Rows(i).Item("MontoImpuesto_Exoneracion") > 1 And Me.dtsDetalle.Rows(i).Item("NumeroDocumento_Exoneracion") <> "" Then
                                Me.AgregarElemento(i, "ImpuestoNeto", "ImpuestoNeto") '                       impuesto neto
                            End If
                        End If                        
                        Me.AgregarElemento(i, "MontoTotalLinea", "MontoTotalLinea") '                       montototallinea
                        Me.xml.WriteEndElement() '                                                      //LINEADETALLE          *********************************

                        If CDec(Me.dtsDetalle.Rows(i).Item("Monto_Impuesto")) > 0 Then
                            TotalServGravados += CDec(Me.dtsDetalle.Rows(i).Item("SubTotal"))
                            TotalServExentos += 0
                        Else
                            TotalServGravados += 0
                            TotalServExentos += CDec(Me.dtsDetalle.Rows(i).Item("SubTotal"))
                        End If
                        SubTotal += CDec(Me.dtsDetalle.Rows(i).Item("SubTotal"))
                        Descuento += CDec(Me.dtsDetalle.Rows(i).Item("MontoDescuento"))
                        Impuesto += CDec(Me.dtsDetalle.Rows(i).Item("Monto_Impuesto"))
                        Total += CDec(Me.dtsDetalle.Rows(i).Item("MontoTotalLinea"))

                        'Dim TotalMercanciasGravadas, TotalMercanciasExentas As Decimal
                        'Dim TotalGravado, TotalExento As Decimal
                        'Dim TotalVenta, TotalDescuentos, TotalVentaNeta, TotalImpuesto, TotalComprobante As Decimal
                    Next
                    Me.xml.WriteEndElement() '                                                          //DETALLESERVICOS       *********************************
                    Me.xml.WriteStartElement("ResumenFactura") '                                        RESUMENFACTURA          *********************************
                    Me.xml.WriteStartElement("CodigoTipoMoneda") '                                      CODIGOTIPOMONEDA
                    Me.AgregarElemento("CodigoMoneda", "CodigoMoneda") '                                    codigomoneda
                    Me.AgregarElemento("TipoCambio", "TipoCambio") '                                        tipocambio
                    Me.xml.WriteEndElement() '                                                           //CODIGOTIPOMONEDA
                    Me.AgregarElemento("TotalServGravados", "TotalServGravados") '                          totalservgravados
                    Me.AgregarElemento("TotalServExentos", "TotalServExentos") '                            totalservexento
                    Me.AgregarElemento("TotalServExonerado", "TotalServExonerado") '                        totalservexonerado
                    Me.AgregarElemento("TotalMercanciasGravadas", "TotalMercanciasGravadas") '              totalmercanciasgravadas
                    Me.AgregarElemento("TotalMercanciasExentas", "TotalMercanciasExentas") '                totalmercanciasexenta
                    Me.AgregarElemento("TotalMercExonerada", "TotalMercExonerada") '                        totalmercexonerada
                    Me.AgregarElemento("TotalGravado", "TotalGravado") '                                    totalgravado
                    Me.AgregarElemento("TotalExento", "TotalExento") '                                      totalexento
                    Me.AgregarElemento("TotalExonerado", "TotalExonerado") '                                totalexonerado
                    Me.AgregarElemento("TotalVenta", "TotalVenta") '                                        totalventa
                    Me.AgregarElemento("TotalDescuentos", "TotalDescuentos") '  c                            totaldescuento
                    Me.AgregarElemento("TotalVentaNeta", "TotalVentaNeta") '                                totalventaneta
                    Me.AgregarElemento("TotalImpuesto", "TotalImpuesto") '                                  totalimpuesto
                    Me.AgregarElemento("TotalComprobante", "TotalComprobante") '                            totalcomprobante
                    Me.xml.WriteEndElement() '                                                          //RESUMENFACTURA        *********************************

                    Dim OrdenCompra As String = dtsEncabezado.Rows(0).Item("OrdenCompra")

                    If Me.dtsEncabezado.Rows(0).Item("Numero_Receptor") = "4000042139" Then
                        'las pegas del ice
                        If OrdenCompra <> "" Then
                            Me.xml.WriteStartElement("InformacionReferencia")
                            Me.AgregarElementoManual("TipoDoc", "99")
                            Me.AgregarElementoManual("Numero", CStr("MM-" & OrdenCompra.Replace("MM-", "")))
                            Me.AgregarElementoManual("FechaEmision", Me.dtsEncabezado.Rows(0).Item("FechaEmision"))
                            Me.AgregarElementoManual("Codigo", "99")
                            Me.AgregarElementoManual("Razon", "ICE")
                            Me.xml.WriteEndElement()
                        Else
                            'No se envia a tributacion porque no tiene orden de compra, asi se puede agregar y luego enviar.
                            Return False
                        End If
                    Else
                        If OrdenCompra <> "" And OrdenCompra <> "0" Then
                            Me.xml.WriteStartElement("Otros") '                                                 Otros                   *********************************
                            Me.AgregarElemento("OtroTexto", "OrdenCompra", "codigo", "WMNumeroOrden") '             OrdenCompra
                            Me.xml.WriteEndElement() '                                                          //Otros                 *********************************
                        End If
                    End If

                    'Me.xml.WriteStartElement("Normativa") '                                             NORMATIVA              *********************************
                    'Me.AgregarElemento("NumeroResolucion", "NumeroResolucion") '                            numeroresolucion
                    'Me.AgregarElemento("FechaResolucion", "FechaResolucion") '                              fecharesolucion
                    'Me.xml.WriteEndElement() '                                                          //NORMATIVA            *********************************

                    Me.xml.WriteEndElement() '                                                          //FACTURA ELECTRONICA   *********************************
                    Me.xml.WriteEndDocument()
                    Me.xml.Close()
                    Me.xml.Dispose()
                    Me.xml = Nothing
                    Dim xmlElectronica As New Xml.XmlDocument
                    xmlElectronica.Load(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml")
                    'xmlElectronica.PreserveWhitespace = True
                    Dim resultado As String = xmlElectronica.OuterXml
                    xmlElectronica = Nothing
                    RaiseEvent DocumentoGenerado(resultado, _IdFactura, "FACTURA")
                    Return True
                Catch ex As Exception
                    RaiseEvent setError(ex.Message)
                    Return False
                End Try
            End Function
            'GENERA XML TIQUETE CAJA
            Public Function GeneraXMLTiquete(ByVal _IdFactura As String) As Boolean
                Try
                    Dim consecutivo As String = ""
                    Dim clave As String = ""
                    Me.CargarDatosFactura(_IdFactura)

                    'consecutivo = dtsEncabezado.Rows(0).Item("Consecutivo")
                    'clave = Me.ObtenerClave(consecutivo)

                    consecutivo = dtsEncabezado.Rows(0).Item("NumeroConsecutivo")
                    clave = dtsEncabezado.Rows(0).Item("Clave")

                    Me.UbicacionXMLFacturas = "C:/Facturas"
                    Me.UbicacionXMLFacturas += "/" & consecutivo
                    If Directory.Exists(Me.UbicacionXMLFacturas) = False Then Directory.CreateDirectory(Me.UbicacionXMLFacturas)
                    Me.xml = New XmlTextWriter(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml", System.Text.Encoding.UTF8)
                    Me.xml.WriteStartDocument(True)
                    Me.xml.Formatting = Formatting.Indented
                    Me.xml.Indentation = 2
                    Me.xml.WriteStartElement("TiqueteElectronico") '                                    FACTURA ELECTRONICA     *********************************
                    Me.xml.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/tiqueteElectronico")
                    Me.xml.WriteAttributeString("xmlns:ns2", "http://www.w3.org/2000/09/xmldsig#")
                    Me.xml.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                    'Me.xml.WriteAttributeString("xsi:schemaLocation", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicahttps://www.hacienda.go.cr/ATV/ComprobanteElectronico/docs/esquemas/2016/v4.3/FacturaElectronica_V4.3.xsd")
                    Me.AgregarElementoManual("Clave", clave) '                                              clave
                    Me.AgregarElemento("CodigoActividad", "CodigoActividad") '                              codigoactividad
                    Me.AgregarElementoManual("NumeroConsecutivo", consecutivo) '                            consecutivo
                    Me.AgregarElemento("FechaEmision", "FechaEmision") '                                    fecha emision
                    Me.xml.WriteStartElement("Emisor") '                                                EMISOR                  *********************************
                    Me.AgregarElemento("Nombre", "Nombre_Emisor") '                                         nombre
                    Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                    Me.AgregarElemento("Tipo", "Tipo_Emisor") '                                             tipo
                    Me.AgregarElemento("Numero", "Numero_Emisor") '                                         numero
                    Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                    Me.AgregarElemento("NombreComercial", "Nombre_Emisor") '                                         nombre
                    Me.xml.WriteStartElement("Ubicacion") '                                             UBICACION               *********************************
                    Me.AgregarElemento("Provincia", "Provincia_Emisor") '                                   provincia
                    Me.AgregarElemento("Canton", "Canton_Emisor") '                                         canton
                    Me.AgregarElemento("Distrito", "Distrito_Emisor") '                                     distrito
                    Me.AgregarElemento("OtrasSenas", "OtrasSenas_Emisor") '                                 otras señas
                    Me.xml.WriteEndElement() '                                                          //UBICACION             *********************************
                    Me.xml.WriteStartElement("Telefono") '                                              TELEFONO                *********************************
                    Me.AgregarElemento("CodigoPais", "CodigoPais_Emisor") '                                 codigo pais
                    Me.AgregarElemento("NumTelefono", "NumTelefono_Emisor") '                               num telefono
                    Me.xml.WriteEndElement() '                                                          //TELEFONO              *********************************
                    Me.AgregarElemento("CorreoElectronico", "CorreoElectronico_Emisor") '                   correoelctronico
                    Me.xml.WriteEndElement() '                                                          //EMISOR                *********************************
                    Me.AgregarElemento("CondicionVenta", "CondicionVenta") '                                condiconventa
                    Me.AgregarElemento("MedioPago", "MedioPago") '                                          mediopago
                    Me.xml.WriteStartElement("DetalleServicio") '                                       DETALLESERVICOS         *********************************
                    For i As Integer = 0 To Me.dtsDetalle.Rows.Count - 1
                        Me.xml.WriteStartElement("LineaDetalle") '                                      LINEADETALLE            ********************************* 
                        Me.AgregarElementoManual("NumeroLinea", i + 1) '                                    numerolinea
                        Me.AgregarElemento(i, "Codigo", "Cabys")
                        Me.xml.WriteStartElement("CodigoComercial") '                                            CODIGO                  *********************************
                        Me.AgregarElemento(i, "Tipo", "Tipo") '                                             tipo
                        Me.AgregarElemento(i, "Codigo", "Codigo") '                                         codigo
                        Me.xml.WriteEndElement() '                                                      //CODIGO                *********************************
                        Me.AgregarElemento(i, "Cantidad", "Cantidad") '                                     cantidad
                        Me.AgregarElemento(i, "UnidadMedida", "UnidadMedida") '                             unidadmedida
                        Me.AgregarElemento(i, "Detalle", "Detalle") '                                       detalle
                        Me.AgregarElemento(i, "PrecioUnitario", "PrecioUnitario") '                         preciounitario
                        Me.AgregarElemento(i, "MontoTotal", "MontoTotal") '                                 montototal
                        If CDec(Me.dtsDetalle.Rows(i).Item("MontoDescuento")) > 0 Then
                            Me.xml.WriteStartElement("Descuento")
                            Me.AgregarElemento(i, "MontoDescuento", "MontoDescuento") '                         montodescuento
                            Me.AgregarElemento(i, "NaturalezaDescuento", "NaturalezaDescuento") '               naturalezadescuento
                            Me.xml.WriteEndElement()
                        End If
                        Me.AgregarElemento(i, "SubTotal", "SubTotal") '                                     subtotal
                        If CDec(Me.dtsDetalle.Rows(i).Item("Tarifa_Impuesto")) > 0 Then
                            Me.xml.WriteStartElement("Impuesto") '                                          IMPUESTO                *********************************
                            Me.AgregarElemento(i, "Codigo", "Codigo_Impuesto") '                                codigo
                            Me.AgregarElemento(i, "CodigoTarifa", "CodigoTarifa") '                             coditotarifa
                            Me.AgregarElemento(i, "Tarifa", "Tarifa_Impuesto") '                                tarifa
                            Me.AgregarElemento(i, "Monto", "Monto_Impuesto") '                                  monto
                            Me.xml.WriteEndElement() '                                                      //IMPUESTO              *********************************
                        End If
                        Me.AgregarElemento(i, "MontoTotalLinea", "MontoTotalLinea") '                       montototallinea
                        Me.xml.WriteEndElement() '                                                      //LINEADETALLE          *********************************
                    Next
                    Me.xml.WriteEndElement() '                                                          //DETALLESERVICOS       *********************************
                    Me.xml.WriteStartElement("ResumenFactura") '                                        RESUMENFACTURA          *********************************
                    Me.xml.WriteStartElement("CodigoTipoMoneda") '                                      CODIGOTIPOMONEDA
                    Me.AgregarElemento("CodigoMoneda", "CodigoMoneda") '                                    codigomoneda
                    Me.AgregarElemento("TipoCambio", "TipoCambio") '                                        tipocambio
                    Me.xml.WriteEndElement() '                                                           //CODIGOTIPOMONEDA
                    Me.AgregarElemento("TotalServGravados", "TotalServGravados") '                          totalservgravados
                    Me.AgregarElemento("TotalServExentos", "TotalServExentos") '                            totalservexento
                    Me.AgregarElemento("TotalMercanciasGravadas", "TotalMercanciasGravadas") '              totalmercanciasgravadas
                    Me.AgregarElemento("TotalMercanciasExentas", "TotalMercanciasExentas") '                totalmercanciasexenta
                    Me.AgregarElemento("TotalGravado", "TotalGravado") '                                    totalgravado
                    Me.AgregarElemento("TotalExento", "TotalExento") '                                      totalexento
                    Me.AgregarElemento("TotalVenta", "TotalVenta") '                                        totalventa
                    Me.AgregarElemento("TotalDescuentos", "TotalDescuentos") '                              totaldescuento
                    Me.AgregarElemento("TotalVentaNeta", "TotalVentaNeta") '                                totalventaneta
                    Me.AgregarElemento("TotalImpuesto", "TotalImpuesto") '                                  totalimpuesto
                    Me.AgregarElemento("TotalComprobante", "TotalComprobante") '                            totalcomprobante
                    Me.xml.WriteEndElement() '                                                          //RESUMENFACTURA        *********************************
                    'Me.xml.WriteStartElement("Normativa") '                                             NORMATIVA               *********************************
                    'Me.AgregarElemento("NumeroResolucion", "NumeroResolucion") '                            numeroresolucion
                    'Me.AgregarElemento("FechaResolucion", "FechaResolucion") '                              fecharesolucion
                    'Me.xml.WriteEndElement() '                                                          //NORMATIVA             *********************************
                    Me.xml.WriteEndElement() '                                                          //FACTURA ELECTRONICA   *********************************
                    Me.xml.WriteEndDocument()
                    Me.xml.Close()
                    Me.xml.Dispose()
                    Me.xml = Nothing
                    Dim xmlElectronica As New Xml.XmlDocument
                    xmlElectronica.Load(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml")
                    'xmlElectronica.PreserveWhitespace = True
                    Dim resultado As String = xmlElectronica.OuterXml
                    xmlElectronica = Nothing
                    RaiseEvent DocumentoGenerado(resultado, _IdFactura, "TIQUETE")
                    Return True
                Catch ex As Exception
                    RaiseEvent setError(ex.Message)
                    Return False
                End Try
            End Function
            'GENERA XML NOTA CREDITO POR DEVOLUCION
            Public Function GeneraXMLDevoluciones(ByVal _IdDevolucion As String) As Boolean
                Try
                    Dim consecutivo As String = ""
                    Dim clave As String = ""
                    Me.CargarDatosDevolucion(_IdDevolucion)
                    consecutivo = dtsEncabezado.Rows(0).Item("Consecutivo")
                    clave = Me.ObtenerClave(consecutivo)
                    Me.UbicacionXMLFacturas = "C:/Facturas"
                    Me.UbicacionXMLFacturas += "/" & consecutivo
                    If Directory.Exists(Me.UbicacionXMLFacturas) = False Then Directory.CreateDirectory(Me.UbicacionXMLFacturas)
                    Me.xml = New XmlTextWriter(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml", System.Text.Encoding.UTF8)
                    Me.xml.WriteStartDocument(True)
                    Me.xml.Formatting = Formatting.Indented
                    Me.xml.Indentation = 2
                    Me.xml.WriteStartElement("NotaCreditoElectronica") '                                    FACTURA ELECTRONICA     *********************************
                    Me.xml.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                    Me.xml.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema")
                    Me.xml.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/notaCreditoElectronica")
                    Me.AgregarElementoManual("Clave", clave) '                                              clave
                    Me.AgregarElemento("CodigoActividad", "CodigoActividad") '                              codigo actividad
                    Me.AgregarElementoManual("NumeroConsecutivo", consecutivo) '                            consecutivo
                    Me.AgregarElemento("FechaEmision", "FechaEmision") '                                    fecha emision
                    Me.xml.WriteStartElement("Emisor") '                                                EMISOR                  *********************************
                    Me.AgregarElemento("Nombre", "Nombre_Emisor") '                                         nombre
                    Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                    Me.AgregarElemento("Tipo", "Tipo_Emisor") '                                             tipo
                    Me.AgregarElemento("Numero", "Numero_Emisor") '                                         numero
                    Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                    Me.xml.WriteStartElement("Ubicacion") '                                             UBICACION               *********************************
                    Me.AgregarElemento("Provincia", "Provincia_Emisor") '                                   provincia
                    Me.AgregarElemento("Canton", "Canton_Emisor") '                                         canton
                    Me.AgregarElemento("Distrito", "Distrito_Emisor") '                                     distrito
                    Me.AgregarElemento("OtrasSenas", "OtrasSenas_Emisor") '                                 otras señas
                    Me.xml.WriteEndElement() '                                                          //UBICACION             *********************************
                    Me.xml.WriteStartElement("Telefono") '                                              TELEFONO                *********************************
                    Me.AgregarElemento("CodigoPais", "CodigoPais_Emisor") '                                 codigo pais
                    Me.AgregarElemento("NumTelefono", "NumTelefono_Emisor") '                               num telefono
                    Me.xml.WriteEndElement() '                                                          //TELEFONO              *********************************
                    Me.AgregarElemento("CorreoElectronico", "CorreoElectronico_Emisor") '                   correoelctronico
                    Me.xml.WriteEndElement() '                                                          //EMISOR                *********************************

                    If dtsEncabezado.Rows(0).Item("TipoFactura") = "CON" Or dtsEncabezado.Rows(0).Item("TipoFactura") = "CRE" Then
                        Me.xml.WriteStartElement("Receptor") '                                              RECEPTOR                *********************************
                        Me.AgregarElemento("Nombre", "Nombre_Receptor") '                                       nombre
                        Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                        Me.AgregarElemento("Tipo", "Tipo_Receptor") '                                           tipo
                        Me.AgregarElemento("Numero", "Numero_Receptor") '                                       numero
                        Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                        Me.xml.WriteEndElement() '                                                          //RECEPTOR              *********************************
                    End If

                    Me.AgregarElemento("CondicionVenta", "CondicionVenta") '                                condiconventa
                    Me.AgregarElemento("MedioPago", "MedioPago") '                                          mediopago
                    Me.xml.WriteStartElement("DetalleServicio") '                                       DETALLESERVICOS         *********************************
                    For i As Integer = 0 To Me.dtsDetalle.Rows.Count - 1
                        Me.xml.WriteStartElement("LineaDetalle") '                                      LINEADETALLE            ********************************* 
                        Me.AgregarElementoManual("NumeroLinea", i + 1) '                                    numerolinea
                        Me.AgregarElemento(i, "Codigo", "Cabys")
                        Me.xml.WriteStartElement("CodigoComercial") '                                   CODIGO                  *********************************
                        Me.AgregarElemento(i, "Tipo", "Tipo") '                                             tipo
                        Me.AgregarElemento(i, "Codigo", "Codigo") '                                         codigo
                        Me.xml.WriteEndElement() '                                                      //CODIGO                *********************************
                        Me.AgregarElemento(i, "Cantidad", "Cantidad") '                                     cantidad
                        Me.AgregarElemento(i, "UnidadMedida", "UnidadMedida") '                             unidadmedida
                        Me.AgregarElemento(i, "Detalle", "Detalle") '                                       detalle
                        Me.AgregarElemento(i, "PrecioUnitario", "PrecioUnitario") '                         preciounitario
                        Me.AgregarElemento(i, "MontoTotal", "MontoTotal") '                                 montototal
                        If dtsDetalle.Rows(i).Item("MontoDescuento") > 0 Then
                            Me.xml.WriteStartElement("Descuento")
                            Me.AgregarElemento(i, "MontoDescuento", "MontoDescuento") '                     montodescuento
                            Me.AgregarElemento(i, "NaturalezaDescuento", "NaturalezaDescuento") '           naturalezadescuento
                            Me.xml.WriteEndElement()
                        End If
                        Me.AgregarElemento(i, "SubTotal", "SubTotal") '                                     subtotal
                        If CDec(Me.dtsDetalle.Rows(i).Item("Tarifa_Impuesto")) > 0 Then
                            Me.xml.WriteStartElement("Impuesto") '                                          IMPUESTO                *********************************
                            Me.AgregarElemento(i, "Codigo", "Codigo_Impuesto") '                                codigo
                            Me.AgregarElemento(i, "CodigoTarifa", "CodigoTarifa") '                                codigo
                            Me.AgregarElemento(i, "Tarifa", "Tarifa_Impuesto") '                                tarifa
                            Me.AgregarElemento(i, "Monto", "Monto_Impuesto") '                                  monto
                            If Me.dtsDetalle.Rows(i).Item("PorcentajeCompra_Exoneracion") >= 1 Then
                                Me.xml.WriteStartElement("Exoneracion") '                                   EXONERACION             *********************************
                                Me.AgregarElemento(i, "TipoDocumento", "TipoDocumento_Exoneracion") '           tipodocumento
                                Me.AgregarElemento(i, "NumeroDocumento", "NumeroDocumento_Exoneracion") '       numerodocumento
                                Me.AgregarElemento(i, "NombreInstitucion", "NombreInstitucion_Exoneracion") '   nombreinstitucion
                                Me.AgregarElemento(i, "FechaEmision", "FechaEmision_Exoneracion") '             fechaemision
                                Me.AgregarElemento(i, "PorcentajeExoneracion", "PorcentajeCompra_Exoneracion") '     porcentajecompra
                                Me.AgregarElemento(i, "MontoExoneracion", "MontoImpuesto_Exoneracion") '           montoimpuesto
                                Me.xml.WriteEndElement() '                                                  //EXONERACION           *********************************
                            End If
                            Me.xml.WriteEndElement() '                                                      //IMPUESTO              *********************************
                            If Me.dtsDetalle.Rows(i).Item("MontoImpuesto_Exoneracion") > 1 Then
                                Me.AgregarElemento(i, "ImpuestoNeto", "ImpuestoNeto") '                         impuesto neto
                            End If
                        End If                        
                        Me.AgregarElemento(i, "MontoTotalLinea", "MontoTotalLinea") '                       montototallinea
                        Me.xml.WriteEndElement() '                                                      //LINEADETALLE          *********************************
                    Next
                    Me.xml.WriteEndElement() '                                                          //DETALLESERVICOS       *********************************
                    Me.xml.WriteStartElement("ResumenFactura") '                                        RESUMENFACTURA          *********************************
                    Me.xml.WriteStartElement("CodigoTipoMoneda") '                                      CODIGOTIPOMONEDA
                    Me.AgregarElemento("CodigoMoneda", "CodigoMoneda") '                                    codigomoneda
                    Me.AgregarElemento("TipoCambio", "TipoCambio") '                                        tipocambio
                    Me.xml.WriteEndElement()
                    Me.AgregarElemento("TotalServGravados", "TotalServGravados") '                          totalservgravados
                    Me.AgregarElemento("TotalServExentos", "TotalServExentos") '                            totalservexento
                    Me.AgregarElemento("TotalServExonerado", "TotalServExonerado") '                        totalservexonerado
                    Me.AgregarElemento("TotalMercanciasGravadas", "TotalMercanciasGravadas") '              totalmercanciasgravadas
                    Me.AgregarElemento("TotalMercanciasExentas", "TotalMercanciasExentas") '                totalmercanciasexenta
                    Me.AgregarElemento("TotalMercExonerada", "TotalMercExonerada") '                        totalmercexonerada
                    Me.AgregarElemento("TotalGravado", "TotalGravado") '                                    totalgravado
                    Me.AgregarElemento("TotalExento", "TotalExento") '                                      totalexento
                    Me.AgregarElemento("TotalExonerado", "TotalExonerado") '                                totalexonerado
                    Me.AgregarElemento("TotalVenta", "TotalVenta") '                                        totalventa
                    Me.AgregarElemento("TotalDescuentos", "TotalDescuentos") '                              totaldescuento
                    Me.AgregarElemento("TotalVentaNeta", "TotalVentaNeta") '                                totalventaneta
                    Me.AgregarElemento("TotalImpuesto", "TotalImpuesto") '                                  totalimpuesto
                    Me.AgregarElemento("TotalComprobante", "TotalComprobante") '                            totalcomprobante
                    Me.xml.WriteEndElement() '                                                          //RESUMENFACTURA        *********************************
                    Me.xml.WriteStartElement("InformacionReferencia") '                                 INFORMACIONREFERENCIA   *********************************
                    Me.AgregarElemento("TipoDoc", "TipoDoc_Referencia") '                                   tipodoc
                    Me.AgregarElemento("Numero", "Numero_Referencia") '                                     numero
                    Me.AgregarElemento("FechaEmision", "FechaEmision_Referencia") '                         fechaemision
                    Me.AgregarElemento("Codigo", "Codigo_Referencia") '                                     codigo
                    Me.AgregarElemento("Razon", "Razon_Referencia") '                                       razon
                    Me.xml.WriteEndElement() '                                                          //INFORMACIONREFERENCIA *********************************
                    'Me.xml.WriteStartElement("Normativa") '                                             NORMATIVA               *********************************
                    'Me.AgregarElemento("NumeroResolucion", "NumeroResolucion") '                            numeroresolucion
                    'Me.AgregarElemento("FechaResolucion", "FechaResolucion") '                              fecharesolucion
                    'Me.xml.WriteEndElement() '                                                          //NORMATIVA             *********************************
                    Me.xml.WriteEndElement() '                                                          //FACTURA ELECTRONICA   *********************************
                    Me.xml.WriteEndDocument()
                    Me.xml.Close()
                    Me.xml.Dispose()
                    Me.xml = Nothing
                    Dim xmlElectronica As New Xml.XmlDocument
                    'xmlElectronica.PreserveWhitespace = True
                    xmlElectronica.Load(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml")
                    Dim resultado As String = xmlElectronica.OuterXml
                    xmlElectronica = Nothing
                    RaiseEvent DocumentoGenerado(resultado, _IdDevolucion, "DEVOLUCION")
                    Return True
                Catch ex As Exception
                    RaiseEvent setError(ex.Message)
                    Return False
                End Try
            End Function

            Public Function GeneraXMLCompras(_IdCompra As String)
                Dim consecutivo As String = ""
                Me.CargarDatosFacturaCompra(_IdCompra)

                Try

                    consecutivo = dtsEncabezado.Rows(0).Item("NumeroConsecutivo")

                    Me.UbicacionXMLFacturas = "C:/Compras"
                    Me.UbicacionXMLFacturas += "/" & consecutivo
                    If Directory.Exists(Me.UbicacionXMLFacturas) = False Then Directory.CreateDirectory(Me.UbicacionXMLFacturas)
                    Me.xml = New XmlTextWriter(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml", System.Text.Encoding.UTF8)
                    Me.xml.WriteStartDocument(True)
                    Me.xml.Formatting = Formatting.Indented
                    Me.xml.Indentation = 2
                    Me.xml.WriteStartElement("FacturaElectronicaCompra") '                                    FACTURA ELECTRONICA     *********************************

                    Me.xml.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/facturaElectronicaCompra")
                    Me.xml.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema")
                    Me.xml.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")

                    Me.AgregarElemento("Clave", "Clave") '                                              clave
                    Me.AgregarElemento("CodigoActividad", "CodigoActividad") '                              codigo actividad
                    Me.AgregarElemento("NumeroConsecutivo", "NumeroConsecutivo") '                            consecutivo
                    Me.AgregarElemento("FechaEmision", "FechaEmision") '                                    fecha emision

                    Me.xml.WriteStartElement("Emisor") '                                                EMISOR                  *********************************
                    Me.AgregarElemento("Nombre", "Nombre_Emisor") '                                         nombre
                    Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                    Me.AgregarElemento("Tipo", "Tipo_Emisor") '                                             tipo
                    Me.AgregarElemento("Numero", "Numero_Emisor") '                                         numero
                    Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                    Me.xml.WriteStartElement("Ubicacion") '                                             UBICACION               *********************************
                    Me.AgregarElemento("Provincia", "Provincia_Emisor") '                                   provincia
                    Me.AgregarElemento("Canton", "Canton_Emisor") '                                         canton
                    Me.AgregarElemento("Distrito", "Distrito_Emisor") '                                     distrito
                    Me.AgregarElemento("OtrasSenas", "OtrasSenas_Emisor") '                                 otras señas
                    Me.xml.WriteEndElement() '                                                          //UBICACION             *********************************
                    Me.xml.WriteStartElement("Telefono") '                                              TELEFONO                *********************************
                    Me.AgregarElemento("CodigoPais", "CodigoPais_Emisor") '                                 codigo pais
                    Me.AgregarElemento("NumTelefono", "NumTelefono_Emisor") '                               num telefono
                    Me.xml.WriteEndElement() '                                                          //TELEFONO              *********************************
                    Me.AgregarElemento("CorreoElectronico", "CorreoElectronico_Emisor") '                   correoelctronico
                    Me.xml.WriteEndElement() '                                                          //EMISOR                *********************************

                    Me.xml.WriteStartElement("Receptor") '                                              RECEPTOR                *********************************
                    Me.AgregarElemento("Nombre", "Nombre_Receptor") '                                       nombre
                    Me.xml.WriteStartElement("Identificacion") '                                        IDENTIFICACION          *********************************
                    Me.AgregarElemento("Tipo", "Tipo_Receptor") '                                           tipo
                    Me.AgregarElemento("Numero", "Numero_Receptor") '                                       numero
                    Me.xml.WriteEndElement() '                                                          //IDENTIFICACION        *********************************
                    'Me.AgregarElemento("CorreoElectronico", "CorreoElectronico_Receptor") '                 correoelctronico
                    Me.xml.WriteEndElement() '                                                          //RECEPTOR              *********************************
                    Me.AgregarElemento("CondicionVenta", "CondicionVenta") '                                condiconventa
                    Me.AgregarElemento("PlazoCredito", "PlazoCredito") '                                    plazocredito
                    Me.AgregarElemento("MedioPago", "MedioPago") '                                          mediopago
                    Me.xml.WriteStartElement("DetalleServicio") '                                       DETALLESERVICOS         *********************************
                    For i As Integer = 0 To Me.dtsDetalle.Rows.Count - 1
                        Me.xml.WriteStartElement("LineaDetalle") '                                      LINEADETALLE            ********************************* 
                        Me.AgregarElementoManual("NumeroLinea", i + 1) '                                    numerolinea
                        'Me.xml.WriteStartElement("CodigoComercial") '                                            CODIGO                  *********************************
                        'Me.AgregarElemento(i, "Tipo", "Tipo") '                                             tipo
                        'Me.AgregarElemento(i, "Codigo", "Codigo") '                                         codigo
                        'Me.xml.WriteEndElement() '                                                      //CODIGO                *********************************
                        Me.AgregarElemento(i, "Cantidad", "Cantidad") '                                     cantidad
                        Me.AgregarElemento(i, "UnidadMedida", "UnidadMedida") '                             unidadmedida
                        Me.AgregarElemento(i, "Detalle", "Detalle") '                                       detalle
                        Me.AgregarElemento(i, "PrecioUnitario", "PrecioUnitario") '                         preciounitario
                        Me.AgregarElemento(i, "MontoTotal", "MontoTotal") '                                 montototal
                        If Me.dtsDetalle.Rows(i).Item("MontoDescuento") > 0 Then
                            Me.xml.WriteStartElement("Descuento")
                            Me.AgregarElemento(i, "MontoDescuento", "MontoDescuento") '                         montodescuento                        
                            Me.AgregarElemento(i, "NaturalezaDescuento", "NaturalezaDescuento") '               naturalezadescuento
                            Me.xml.WriteEndElement()
                        End If
                        Me.AgregarElemento(i, "SubTotal", "SubTotal") '                                     subtotal
                        If Me.dtsDetalle.Rows(i).Item("Tarifa_Impuesto") > 0 Then
                            Me.xml.WriteStartElement("Impuesto") '                                          IMPUESTO                *********************************
                            Me.AgregarElemento(i, "Codigo", "Codigo_Impuesto") '                                codigo
                            Me.AgregarElemento(i, "CodigoTarifa", "CodigoTarifa") '                             codigotarifa
                            Me.AgregarElemento(i, "Tarifa", "Tarifa_Impuesto") '                                tarifa
                            Me.AgregarElemento(i, "Monto", "Monto_Impuesto") '                                  monto
                            'If Me.dtsDetalle.Rows(i).Item("MontoImpuesto_Exoneracion") > 1 And Me.dtsDetalle.Rows(i).Item("NumeroDocumento_Exoneracion") <> "" Then
                            '    Me.xml.WriteStartElement("Exoneracion") '                                   EXONERACION             *********************************
                            '    Me.AgregarElemento(i, "TipoDocumento", "TipoDocumento_Exoneracion") '           tipodocumento
                            '    Me.AgregarElemento(i, "NumeroDocumento", "NumeroDocumento_Exoneracion") '       numerodocumento
                            '    Me.AgregarElemento(i, "NombreInstitucion", "NombreInstitucion_Exoneracion") '   nombreinstitucion
                            '    Me.AgregarElemento(i, "FechaEmision", "FechaEmision_Exoneracion") '             fechaemision
                            '    Me.AgregarElemento(i, "PorcentajeExoneracion", "PorcentajeCompra_Exoneracion") '     porcentajecompra
                            '    Me.AgregarElemento(i, "MontoExoneracion", "MontoImpuesto_Exoneracion") '           montoimpuesto
                            '    Me.xml.WriteEndElement() '                                                  //EXONERACION           *********************************
                            'End If
                            Me.xml.WriteEndElement() '                                                      //IMPUESTO              *********************************
                            'If Me.dtsDetalle.Rows(i).Item("MontoImpuesto_Exoneracion") > 1 And Me.dtsDetalle.Rows(i).Item("NumeroDocumento_Exoneracion") <> "" Then
                            '    Me.AgregarElemento(i, "ImpuestoNeto", "ImpuestoNeto") '                       impuesto neto
                            'End If
                        End If
                        Me.AgregarElemento(i, "MontoTotalLinea", "MontoTotalLinea") '                       montototallinea
                        Me.xml.WriteEndElement() '                                                      //LINEADETALLE          *********************************
                    Next
                    Me.xml.WriteEndElement() '                                                          //DETALLESERVICOS       *********************************
                    Me.xml.WriteStartElement("ResumenFactura") '                                        RESUMENFACTURA          *********************************
                    Me.xml.WriteStartElement("CodigoTipoMoneda") '                                      CODIGOTIPOMONEDA
                    Me.AgregarElemento("CodigoMoneda", "CodigoMoneda") '                                    codigomoneda
                    Me.AgregarElemento("TipoCambio", "TipoCambio") '                                        tipocambio
                    Me.xml.WriteEndElement() '                                                           //CODIGOTIPOMONEDA
                    Me.AgregarElemento("TotalServGravados", "TotalServGravados") '                          totalservgravados
                    Me.AgregarElemento("TotalServExentos", "TotalServExentos") '                            totalservexento
                    Me.AgregarElemento("TotalServExonerado", "TotalServExonerado") '                        totalservexonerado
                    Me.AgregarElemento("TotalMercanciasGravadas", "TotalMercanciasGravadas") '              totalmercanciasgravadas
                    Me.AgregarElemento("TotalMercanciasExentas", "TotalMercanciasExentas") '                totalmercanciasexenta
                    Me.AgregarElemento("TotalMercExonerada", "TotalMercExonerada") '                        totalmercexonerada
                    Me.AgregarElemento("TotalGravado", "TotalGravado") '                                    totalgravado
                    Me.AgregarElemento("TotalExento", "TotalExento") '                                      totalexento
                    Me.AgregarElemento("TotalExonerado", "TotalExonerado") '                                totalexonerado
                    Me.AgregarElemento("TotalVenta", "TotalVenta") '                                        totalventa
                    Me.AgregarElemento("TotalDescuentos", "TotalDescuentos") '                              totaldescuento
                    Me.AgregarElemento("TotalVentaNeta", "TotalVentaNeta") '                                totalventaneta
                    Me.AgregarElemento("TotalImpuesto", "TotalImpuesto") '                                  totalimpuesto
                    Me.AgregarElemento("TotalComprobante", "TotalComprobante") '                            totalcomprobante
                    Me.xml.WriteEndElement() '                                                          //RESUMENFACTURA        *********************************
                    'Me.xml.WriteStartElement("Normativa") '                                             NORMATIVA               *********************************
                    'Me.AgregarElemento("NumeroResolucion", "NumeroResolucion") '                            numeroresolucion
                    'Me.AgregarElemento("FechaResolucion", "FechaResolucion") '                              fecharesolucion
                    'Me.xml.WriteEndElement() '                                                          //NORMATIVA             *********************************
                    Me.xml.WriteEndElement() '                                                          //FACTURA ELECTRONICA   *********************************
                    Me.xml.WriteEndDocument()
                    Me.xml.Close()
                    Me.xml.Dispose()
                    Me.xml = Nothing
                    Dim xmlElectronica As New Xml.XmlDocument
                    xmlElectronica.Load(UbicacionXMLFacturas & "/" & consecutivo & "_00_SF.xml")
                    'xmlElectronica.PreserveWhitespace = True
                    Dim resultado As String = xmlElectronica.OuterXml
                    xmlElectronica = Nothing
                    RaiseEvent DocumentoGenerado(resultado, _IdCompra, "FACTURACOMPRA")
                    Return True
                Catch ex As Exception
                    RaiseEvent setError(ex.Message)
                    Return False
                End Try
            End Function

            Public Function GeneraXMLMensajeReceptor(_IdMensaje As String) As Boolean
                Try
                    Me.CargarDatosMensaje(_IdMensaje)
                    Dim consecutivo As String = dtsEncabezado.Rows(0).Item("NumeroConsecutivoReceptor")
                    Me.UbicacionXMLCompras = "C:/Compras"
                    Me.UbicacionXMLCompras += "/" & consecutivo
                    If Directory.Exists(Me.UbicacionXMLCompras) = False Then Directory.CreateDirectory(Me.UbicacionXMLCompras)
                    Me.xml = New XmlTextWriter(Me.UbicacionXMLCompras & "/" & consecutivo & "_00_SF.xml", System.Text.Encoding.UTF8)
                    Me.xml.WriteStartDocument(True)
                    Me.xml.Formatting = Formatting.Indented
                    Me.xml.Indentation = 2
                    Me.xml.WriteStartElement("MensajeReceptor")

                    Me.xml.WriteAttributeString("xmlns", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor")
                    Me.xml.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema")
                    Me.xml.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                    Me.xml.WriteAttributeString("xsi:schemaLocation", "https://cdn.comprobanteselectronicos.go.cr/xml-schemas/v4.3/mensajeReceptor https://tribunet.hacienda.go.cr/docs/esquemas/2017/v4.3/mensajeReceptor.xsd")

                    Me.AgregarElemento("Clave", "Clave")
                    Me.AgregarElemento("NumeroCedulaEmisor", "NumeroCedulaEmisor")
                    Me.AgregarElemento("FechaEmisionDoc", "FechaEmisionDoc")
                    Me.AgregarElemento("Mensaje", "Mensaje")
                    If Me.dtsEncabezado.Rows(0).Item("Mensaje") = "3" Then
                        Me.AgregarElemento("DetalleMensaje", "DetalleMensaje") ' si es de rechazo debo de detallar porque
                    End If
                    Me.AgregarElemento("CodigoActividad", "CodigoActividad") ' el codigo de la actividad
                    Me.AgregarElemento("CondicionImpuesto", "CondicionImpuesto") '
                    If Me.dtsEncabezado.Rows(0).Item("Mensaje") = "1" Then
                        If Me.dtsEncabezado.Rows(0).Item("CondicionImpuesto") <> "05" Then
                            Me.AgregarElemento("MontoTotalImpuestoAcreditar", "MontoTotalImpuestoAcreditar")
                            Me.AgregarElemento("MontoTotalDeGastoAplicable", "MontoTotalDeGastoAplicable")
                        End If
                    End If
                    Me.AgregarElemento("MontoTotalImpuesto", "MontoTotalImpuesto")
                    Me.AgregarElemento("TotalFactura", "TotalFactura")
                    Me.AgregarElemento("NumeroCedulaReceptor", "NumeroCedulaReceptor")
                    Me.AgregarElemento("NumeroConsecutivoReceptor", "NumeroConsecutivoReceptor")
                    Me.xml.WriteEndDocument()
                    Me.xml.Close()
                    Me.xml.Dispose()
                    Me.xml = Nothing

                    Dim xmlElectronica As New Xml.XmlDocument
                    xmlElectronica.Load(Me.UbicacionXMLCompras & "/" & consecutivo & "_00_SF.xml")
                    'xmlElectronica.PreserveWhitespace = True
                    Dim resultado As String = xmlElectronica.OuterXml
                    xmlElectronica = Nothing
                    RaiseEvent DocumentoGenerado(resultado, _IdMensaje, "MENSAJERECEPTOR")
                    Return True
                Catch ex As Exception
                    RaiseEvent setError(ex.Message)
                    Return False
                End Try
            End Function
        End Class
    End Namespace
End Namespace
