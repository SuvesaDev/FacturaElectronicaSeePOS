'' FirmaElectronicaCR es un programa para la firma y envio de documentos XML para la Factura Electrónica de Costa Rica
''
'' Comunicacion es la clase para el envío del documento XML para la Factura Electrónica de Costa Rica
''
'' Esta clase de Firma fue realizado tomando como base el trabajo realizado por:
'' - Departamento de Nuevas Tecnologías - Dirección General de Urbanismo Ayuntamiento de Cartagena
'' - XAdES Starter Kit desarrollado por Microsoft Francia
'' - Cambios y funcionalidad para Costa Rica - Roy Rojas - royrojas@dotnetcr.com
''
'' La clase comunicación fue creada en conjunto con Cristhian Sancho
''
'' Este programa es software libre: puede redistribuirlo y / o modificarlo
'' bajo los + términos de la Licencia Pública General Reducida de GNU publicada por
'' la Free Software Foundation, ya sea la versión 3 de la licencia, o
'' (a su opción) cualquier versión posterior.

'' Este programa se distribuye con la esperanza de que sea útil,
'' pero SIN NINGUNA GARANTÍA; sin siquiera la garantía implícita de
'' COMERCIABILIDAD O IDONEIDAD PARA UN PROPÓSITO PARTICULAR. Ver el
'' Licencia pública general menor de GNU para más detalles.
''
'' Deberías haber recibido una copia de la Licencia Pública General Reducida de GNU
'' junto con este programa. Si no, vea http://www.gnu.org/licenses/.
''
'' This program Is distributed in the hope that it will be useful,
'' but WITHOUT ANY WARRANTY; without even the implied warranty of
'' MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'' GNU Lesser General Public License for more details.
''
'' You should have received a copy of the GNU Lesser General Public License
'' along with this program.  If Not, see http://www.gnu.org/licenses/. 

Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class Comunicacion

    Public Property xmlRespuesta As Xml.XmlDocument
    Public Property jsonEnvio As String = ""
    Public Property jsonRespuesta As String = ""
    Public Property mensajeRespuesta As String = ""
    Public Property estadoFactura As String = ""
    Public Property statusCode As String = ""
    Public Property MotiviRechazo As String = ""

    Private Function UrlApi() As String
        Try
            Dim dt As New DataTable
            dt = General.sql.Ejecutar("Select url from [Seguridad].[dbo].[DireccionUrl]", CommandType.Text)
            Return dt.Rows(0).Item("url")
        Catch ex As Exception
            Return "https://api.comprobanteselectronicos.go.cr/recepcion/v1/"
        End Try
    End Function

    ''' <summary>
    ''' Envia los datos a los servidores de Hacienda y obtiene las respuestas necesarias
    ''' </summary>
    ''' <param name="TK">Token que generó Hacienda</param>
    ''' <param name="objRecepcion">Objeto que contiene todos las variables de comunicación</param>
    Public Function EnvioDatos(TK As String, ByVal objRecepcion As Recepcion) As Boolean
        Try
            Dim URL_RECEPCION As String = Me.UrlApi

            'Dim http As HttpClient = New HttpClient

            'Dim JsonObject As Newtonsoft.Json.Linq.JObject = New Newtonsoft.Json.Linq.JObject()
            'JsonObject.Add(New JProperty("clave", objRecepcion.clave))
            'JsonObject.Add(New JProperty("fecha", objRecepcion.fecha))
            'JsonObject.Add(New JProperty("emisor", New JObject(New JProperty("tipoIdentificacion", objRecepcion.emisor.TipoIdentificacion),
            '                                                   New JProperty("numeroIdentificacion", objRecepcion.emisor.numeroIdentificacion))))
            'JsonObject.Add(New JProperty("receptor", New JObject(New JProperty("tipoIdentificacion", objRecepcion.receptor.TipoIdentificacion),
            '                                                     New JProperty("numeroIdentificacion", objRecepcion.receptor.numeroIdentificacion))))
            'JsonObject.Add(New JProperty("comprobanteXml", objRecepcion.comprobanteXml))

            'jsonEnvio = JsonObject.ToString

            'Dim oString As StringContent = New StringContent(JsonObject.ToString)

            'http.DefaultRequestHeaders.Add("authorization", "Bearer " + TK)

            'Dim response As HttpResponseMessage = http.PostAsync(URL_RECEPCION + "recepcion", oString).Result
            Dim http As HttpClient = New HttpClient

            '*********************************************************************************
            'TLS 1.2 Cambio de seguridad por Hacienda
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 'Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            '*********************************************************************************

            Dim JsonObject As Newtonsoft.Json.Linq.JObject = New Newtonsoft.Json.Linq.JObject()
            JsonObject.Add(New JProperty("clave", objRecepcion.clave))
            JsonObject.Add(New JProperty("fecha", objRecepcion.fecha))
            JsonObject.Add(New JProperty("emisor", New JObject(New JProperty("tipoIdentificacion", objRecepcion.emisor.TipoIdentificacion),
                                                               New JProperty("numeroIdentificacion", objRecepcion.emisor.numeroIdentificacion))))
            If objRecepcion.receptor.sinReceptor = False Then
                JsonObject.Add(New JProperty("receptor", New JObject(New JProperty("tipoIdentificacion", objRecepcion.receptor.TipoIdentificacion),
                                                                     New JProperty("numeroIdentificacion", objRecepcion.receptor.numeroIdentificacion))))
            End If
            If objRecepcion.consecutivoReceptor <> "" Then
                JsonObject.Add(New JProperty("consecutivoReceptor", objRecepcion.consecutivoReceptor))
            End If
            JsonObject.Add(New JProperty("comprobanteXml", objRecepcion.comprobanteXml))
            jsonEnvio = JsonObject.ToString
            Dim oString As StringContent = New StringContent(JsonObject.ToString)
            http.DefaultRequestHeaders.Add("authorization", "Bearer " + TK)
            Dim response As HttpResponseMessage = http.PostAsync(URL_RECEPCION + "recepcion", oString).Result

            If response.StatusCode <> Net.HttpStatusCode.Accepted Then
                Try
                    Dim Historial As New GestionDatos.Historial
                    Historial.Guardar_Historial(objRecepcion.clave, response.Headers.GetValues("X-Error-Cause")(0))
                Catch ex As Exception
                End Try

                '    Return False
            End If

            'If objRecepcion.consecutivoReceptor <> "" Then
            '    If response.StatusCode = Net.HttpStatusCode.Accepted Then
            '        statusCode = "procesando"
            '        statusCode = response.StatusCode
            '        Return True
            '    End If
            'End If

            Dim res As String = response.Content.ReadAsStringAsync.Result

            Dim Localizacion = response.StatusCode
            ''mensajeRespuesta = Localizacion

            http = New HttpClient
            http.DefaultRequestHeaders.Add("authorization", "Bearer " + TK)
            response = http.GetAsync(URL_RECEPCION + "recepcion/" + objRecepcion.clave).Result
            res = response.Content.ReadAsStringAsync.Result

            jsonRespuesta = res.ToString

            Dim RH As RespuestaHacienda = Newtonsoft.Json.JsonConvert.DeserializeObject(Of RespuestaHacienda)(res)


            If RH.respuesta_xml <> "" Then
                xmlRespuesta = Funciones.DecodeBase64ToXML(RH.respuesta_xml)
            End If

            estadoFactura = RH.ind_estado
            statusCode = response.StatusCode


            If estadoFactura = "rechazado" Then
                MotiviRechazo = xmlRespuesta.GetElementsByTagName("DetalleMensaje")(0).InnerText
            Else
                MotiviRechazo = ""
            End If

            mensajeRespuesta = "Confirmación: " & statusCode & vbCrLf
            mensajeRespuesta += "Estado: " & estadoFactura
            Return True
        Catch e As Exception
            Return False
        End Try
    End Function

    Public Function ConsultaEstatus(TK As String, claveConsultar As String) As Boolean
        Try

            Dim URL_RECEPCION As String = Me.UrlApi

            Dim http As HttpClient = New HttpClient

            '*********************************************************************************
            'TLS 1.2 Cambio de seguridad por Hacienda
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            '*********************************************************************************

            http.DefaultRequestHeaders.Add("authorization", "Bearer " + TK)

            Dim response As HttpResponseMessage = http.GetAsync(URL_RECEPCION & "recepcion/" & claveConsultar).Result
            Dim res As String = response.Content.ReadAsStringAsync.Result

            Dim Localizacion = response.StatusCode

            jsonRespuesta = res.ToString

            Dim RH As RespuestaHacienda = Newtonsoft.Json.JsonConvert.DeserializeObject(Of RespuestaHacienda)(res)

            If RH.respuesta_xml <> "" Then
                xmlRespuesta = Funciones.DecodeBase64ToXML(RH.respuesta_xml)

                'If RH.ind_estado = "rechazado" Then
                '    Dim mensaje As String = xmlRespuesta.GetElementsByTagName("DetalleMensaje")(0).InnerText
                '    MsgBox(mensaje)
                'End If

            End If

            estadoFactura = RH.ind_estado
            statusCode = response.StatusCode

            If estadoFactura = "rechazado" Then
                MotiviRechazo = xmlRespuesta.GetElementsByTagName("DetalleMensaje")(0).InnerText
            Else
                MotiviRechazo = ""
            End If

            mensajeRespuesta = "Confirmación: " & statusCode & vbCrLf
            mensajeRespuesta += "Estado: " & estadoFactura
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class
