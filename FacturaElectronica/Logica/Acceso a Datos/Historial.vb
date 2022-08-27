Imports System.IO
Imports System.Xml
Imports System.Data

Namespace GestionDatos
    Public Class Historial
        Private Ubicacion As String = "C:/Facturas/Historial"
        Private xml As XmlTextWriter

        Sub New()
            If Directory.Exists(Me.Ubicacion) = False Then
                Directory.CreateDirectory(Me.Ubicacion)
            End If
        End Sub

        Private Sub AgregarElementoManual(ByVal _TituloXml As String, ByVal _Texto As String)
            If _Texto <> "" Then
                Me.xml.WriteStartElement(_TituloXml)
                Me.xml.WriteString(_Texto)
                Me.xml.WriteEndElement()
            End If
        End Sub

        Public Sub Guardar_Historial(_clave As String, _Mensaje As String)
            Dim NuevoHistorial As String = Me.Ubicacion & "/" & _clave & ".xml"
            If File.Exists(Me.Ubicacion & "/" & _clave & ".xml") = False Then
                Me.xml = New XmlTextWriter(NuevoHistorial, System.Text.Encoding.UTF8)
                Me.xml.WriteStartDocument(True)
                Me.xml.Formatting = Formatting.Indented
                Me.xml.Indentation = 2
                Me.xml.WriteStartElement("Historial")
                Me.AgregarElementoManual("Clave", _clave)
                Me.AgregarElementoManual("Mensaje", _Mensaje)
                Me.xml.WriteEndDocument()
                Me.xml.Close()
                Me.xml.Dispose()
                Me.xml = Nothing
            End If
        End Sub
    End Class
End Namespace
