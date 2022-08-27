Public Class frmConfiguraciones
    Private clsUbicaciones As New GestionDatos.Ubicacion
    Private clsEmisor As New GestionDatos.Emisor

    Private Sub Cargar_Provincias()
        Dim dt As New DataTable
        dt = Me.clsUbicaciones.Obtener_Provincia
        Me.cboProvincia.DataSource = dt
        Me.cboProvincia.DisplayMember = "PROVINCIA"
        Me.cboProvincia.ValueMember = "ID_PROVINCIA"
    End Sub

    Private Sub Cargar_Cantones(_IdProvincia As Integer)
        Dim dt As New DataTable
        dt = Me.clsUbicaciones.Obtener_Canton(_IdProvincia)
        Me.cboCanton.DataSource = dt
        Me.cboCanton.DisplayMember = "CANTON"
        Me.cboCanton.ValueMember = "ID_CANTON"
    End Sub

    Private Sub Cargar_Distrito(_IdProvincia As Integer, _IdCanton As Integer)
        Dim dt As New DataTable
        dt = Me.clsUbicaciones.Obtener_Distrito(_IdProvincia, _IdCanton)
        Me.cboDistrito.DataSource = dt
        Me.cboDistrito.DisplayMember = "DISTRITO"
        Me.cboDistrito.ValueMember = "ID_DISTRITO"
    End Sub

    Private Sub Cargar_Emisor()
        Me.clsEmisor.Obtener_Datos()
        Me.cboTipoIdentificacion.Text = Me.clsEmisor.tipoidentificacion
        Me.txtIdentificacion.Text = Me.clsEmisor.identificacion
        Me.txtNombre.Text = Me.clsEmisor.nombre
        Me.txtCorreo.Text = Me.clsEmisor.correo
        Me.txtTelefono.Text = Me.clsEmisor.telefono
        Me.txtSucursal.Text = Me.clsEmisor.sucursal
        Me.cboProvincia.SelectedValue = Me.clsEmisor.provincia
        Me.cboCanton.SelectedValue = Me.clsEmisor.canton
        Me.cboDistrito.SelectedValue = Me.clsEmisor.distrito
        Me.txtOtrasSeñas.Text = Me.clsEmisor.otrasseñas
        Me.txtUsuario.Text = Me.clsEmisor.usuario
        Me.txtClave.Text = Me.clsEmisor.clave
        Me.txtCertificado.Text = Me.clsEmisor.certificado
        Me.txtNumeroResolucion.Text = Me.clsEmisor.numeroresolucion
        Me.txtFechaResolucion.Text = Me.clsEmisor.fecharesolucion
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Me.clsEmisor.tipoidentificacion = Me.cboTipoIdentificacion.Text
        Me.clsEmisor.identificacion = Me.txtIdentificacion.Text
        Me.clsEmisor.nombre = Me.txtNombre.Text
        Me.clsEmisor.correo = Me.txtCorreo.Text
        Me.clsEmisor.telefono = Me.txtTelefono.Text
        Me.clsEmisor.sucursal = Me.txtSucursal.Text
        Me.clsEmisor.provincia = Me.cboProvincia.SelectedValue
        Me.clsEmisor.canton = Me.cboCanton.SelectedValue
        Me.clsEmisor.distrito = Me.cboDistrito.SelectedValue
        Me.clsEmisor.otrasseñas = Me.txtOtrasSeñas.Text
        Me.clsEmisor.usuario = Me.txtUsuario.Text
        Me.clsEmisor.clave = Me.txtClave.Text
        Me.clsEmisor.certificado = Me.txtCertificado.Text
        Me.clsEmisor.numeroresolucion = Me.txtNumeroResolucion.Text
        Me.clsEmisor.fecharesolucion = Me.txtFechaResolucion.Text
        Me.clsEmisor.Actualizar_Datos()
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub frmConfiguraciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cargar_Provincias()
        Me.cboProvincia.SelectedValue = 1
        Me.cboCanton.SelectedValue = 1
        Me.cboCanton.SelectedValue = 1
        Me.Cargar_Emisor()
    End Sub

    Private Sub cboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        On Error Resume Next
        Cargar_Cantones(Me.cboProvincia.SelectedValue)
    End Sub

    Private Sub cboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        On Error Resume Next
        Cargar_Distrito(Me.cboProvincia.SelectedValue, Me.cboCanton.SelectedValue)
    End Sub

    Private Sub txtCertificado_ButtonClick(sender As Object, e As EventArgs) Handles txtCertificado.ButtonClick
        Dim iCertificados As New frmBuscarCertificado
        iCertificados.ShowDialog()
        Me.txtCertificado.Text = iCertificados.thumbprint
    End Sub

End Class