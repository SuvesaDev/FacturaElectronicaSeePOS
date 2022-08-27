Public Class frmPrincipal

    Private Sub btnEmpresa_Click(sender As Object, e As EventArgs) Handles btnEmpresa.Click
        Dim frm As New frmConfiguraciones
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub btnEnviarDatos_Click(sender As Object, e As EventArgs) Handles btnEnviarDatos2.Click
        Dim frm As New frmEnviar
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private PuedeCerrar As Boolean = False
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Dim pas As String = "acepto"
        Dim clave As String = InputBox("Digite la palabra clave para cerrar la aplicacion" & vbCrLf _
                                       & "Si cierra la aplicacion no se enviaran comprobantes a tributacion", "Confirmar accion")

        If clave = pas Then
            Me.PuedeCerrar = True
            End
        End If

    End Sub

    Private Sub frmPrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If PuedeCerrar = False Then
            e.Cancel = True
            Me.WindowState = FormWindowState.Minimized
        End If        
    End Sub

    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text + " v27042020"
    End Sub
End Class
