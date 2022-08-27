Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography

Public Class frmEnviar
    Private cls As New GestionDatos.Factura
    Private Comprobantes As New EnvioComprobantes
    Private dt As New DataTable
    Private Enviar As Boolean = False

    Public Function AccesoInternet() As Boolean

        'Try
        '    Dim cls As New EnvioComprobantes
        '    Dim acceso As String = ""
        '    acceso = cls.getToken()
        '    If acceso <> "" Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Catch ex As Exception
        '    Return False
        'End Try

        'Dim objUrl As New System.Uri("http://www.google.com/")
        'Dim objWebReq As System.Net.WebRequest
        'objWebReq = System.Net.WebRequest.Create(objUrl)
        'Dim objResp As System.Net.WebResponse
        'Try
        '    objResp = objWebReq.GetResponse
        '    objResp.Close()
        '    objWebReq = Nothing
        '    Return True
        'Catch ex As Exception
        '    objWebReq = Nothing
        '    Return False
        'End Try
        Return True
    End Function

    Private Sub Cargar_Pendientes()
        Me.dt = Me.cls.Cargar_Facturas_Pendientes()
        Me.viewFacturas.DataSource = dt
        If Me.Enviar = True Then Me.Enviar_Datos(Me.dt)
    End Sub


    Private Sub Enviar_Datos(_dt As DataTable)
        Try
            For Each r As DataRow In _dt.Rows
                General.BasedeDatos = r.Item("BasedeDatos")

                'General.BasedeDatos = "SeePOs"
                'Me.Comprobantes.GeneraPDFVenta(1451768)
                'Me.Comprobantes.GeneraPDFDevolucion(9398)
                'Exit Sub

                Select Case r.Item("EstadoMH")
                    Case "pendiente" 'primira vez que pasa la factura
                        Dim PVE As Boolean = False

                        Select Case r.Item("tipo")
                            Case "PVE" : PVE = True
                        End Select
                        Dim internet As Boolean = AccesoInternet()

                        Select Case r.Item("TipoDoc")
                            Case "FACTURA" : Me.Comprobantes.EnviarFactura(r.Item("Id"), internet, PVE)
                            Case "DEVOLUCION" : Me.Comprobantes.EnviarDevolucion(r.Item("Id"), internet)
                            Case "MENSAJE" : Me.Comprobantes.ActualizarEstadoMensaje(r.Item("Id"))
                            Case "FACTURACOMPRA" : Me.Comprobantes.EnviarCompra(r.Item("Id"))
                            Case "Recibo" : Me.Comprobantes.EnviarNotificacionReciboCXC(r.Item("Id"))

                        End Select
                    Case "procesando" 'en caso de estar en proceso, consulta nueva mente la factura para cambiar el estado
                        Select Case r.Item("TipoDoc")
                            Case "FACTURA" : Me.Comprobantes.ActualizarEstadoFactura(r.Item("Id"))
                            Case "DEVOLUCION" : Me.Comprobantes.ActualizarEstadoDevolucion(r.Item("Id"))
                            Case "MENSAJE" : Me.Comprobantes.ActualizarEstadoMensaje(r.Item("Id"))
                            Case "FACTURACOMPRA" : Me.Comprobantes.ActualizarEstadoCompra(r.Item("Id"))

                        End Select
                    Case "recibido" 'en caso de estar en proceso, consulta nueva mente la factura para cambiar el estado
                        Select Case r.Item("TipoDoc")
                            Case "FACTURA" : Me.Comprobantes.ActualizarEstadoFactura(r.Item("Id"))
                            Case "DEVOLUCION"
                            Case "MENSAJE" : Me.Comprobantes.ActualizarEstadoMensaje(r.Item("Id"))
                            Case "FACTURACOMPRA" : Me.Comprobantes.ActualizarEstadoCompra(r.Item("Id"))

                        End Select
                End Select
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmEnviar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Timer1.Start()
        Me.Cargar_Pendientes()
        If AccesoInternet() = True Then
            Me.lblInternet.Text = "Acceso a Internet: Bien"
        Else
            Me.lblInternet.Text = "Acceso a Internet: No hay"
        End If
    End Sub

    Private Sub btnEnviarDatos_Click(sender As Object, e As EventArgs) Handles btnEnviarDatos.Click
        If Me.btnEnviarDatos.Text = "Enviar Datos" Then
            Me.ParentForm.WindowState = FormWindowState.Minimized
            Me.Enviar = True
            Me.btnEnviarDatos.Text = "Pausar Envio"
            Me.Cargar_Pendientes()
        Else
            Me.Enviar = False
            Me.btnEnviarDatos.Text = "Enviar Datos"
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Cargar_Pendientes()
        If AccesoInternet() = True Then
            Me.lblInternet.Text = "Acceso a Internet: Bien"
        Else
            Me.lblInternet.Text = "Acceso a Internet: No hay"
        End If
    End Sub

    Private Sub frmEnviar_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class