<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfiguraciones
    Inherits MetroFramework.Forms.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.txtIdentificacion = New MetroFramework.Controls.MetroTextBox()
        Me.cboTipoIdentificacion = New MetroFramework.Controls.MetroComboBox()
        Me.txtNombre = New MetroFramework.Controls.MetroTextBox()
        Me.txtCorreo = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.txtTelefono = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel5 = New MetroFramework.Controls.MetroLabel()
        Me.txtSucursal = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel6 = New MetroFramework.Controls.MetroLabel()
        Me.cboProvincia = New MetroFramework.Controls.MetroComboBox()
        Me.MetroLabel7 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel8 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel9 = New MetroFramework.Controls.MetroLabel()
        Me.cboCanton = New MetroFramework.Controls.MetroComboBox()
        Me.cboDistrito = New MetroFramework.Controls.MetroComboBox()
        Me.txtOtrasSeñas = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel10 = New MetroFramework.Controls.MetroLabel()
        Me.MetroTabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.MetroTabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroTabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.MetroLabel15 = New MetroFramework.Controls.MetroLabel()
        Me.txtFechaResolucion = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel14 = New MetroFramework.Controls.MetroLabel()
        Me.txtNumeroResolucion = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel13 = New MetroFramework.Controls.MetroLabel()
        Me.txtCertificado = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel12 = New MetroFramework.Controls.MetroLabel()
        Me.txtClave = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel11 = New MetroFramework.Controls.MetroLabel()
        Me.txtUsuario = New MetroFramework.Controls.MetroTextBox()
        Me.btnCancelar = New MetroFramework.Controls.MetroButton()
        Me.btnAceptar = New MetroFramework.Controls.MetroButton()
        Me.MetroTabControl1.SuspendLayout()
        Me.MetroTabPage1.SuspendLayout()
        Me.MetroTabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Location = New System.Drawing.Point(18, 19)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(122, 19)
        Me.MetroLabel1.TabIndex = 0
        Me.MetroLabel1.Text = "Tipo Identificacion :"
        '
        'MetroLabel2
        '
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(155, 19)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(92, 19)
        Me.MetroLabel2.TabIndex = 1
        Me.MetroLabel2.Text = "Identificacion :"
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.Location = New System.Drawing.Point(287, 19)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(90, 19)
        Me.MetroLabel3.TabIndex = 2
        Me.MetroLabel3.Text = "Razon Social :"
        '
        'txtIdentificacion
        '
        '
        '
        '
        Me.txtIdentificacion.CustomButton.Image = Nothing
        Me.txtIdentificacion.CustomButton.Location = New System.Drawing.Point(98, 1)
        Me.txtIdentificacion.CustomButton.Name = ""
        Me.txtIdentificacion.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtIdentificacion.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtIdentificacion.CustomButton.TabIndex = 1
        Me.txtIdentificacion.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtIdentificacion.CustomButton.UseSelectable = True
        Me.txtIdentificacion.CustomButton.Visible = False
        Me.txtIdentificacion.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtIdentificacion.Lines = New String(-1) {}
        Me.txtIdentificacion.Location = New System.Drawing.Point(155, 41)
        Me.txtIdentificacion.MaxLength = 32767
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtIdentificacion.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtIdentificacion.SelectedText = ""
        Me.txtIdentificacion.SelectionLength = 0
        Me.txtIdentificacion.SelectionStart = 0
        Me.txtIdentificacion.ShortcutsEnabled = True
        Me.txtIdentificacion.Size = New System.Drawing.Size(126, 29)
        Me.txtIdentificacion.TabIndex = 3
        Me.txtIdentificacion.UseSelectable = True
        Me.txtIdentificacion.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtIdentificacion.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'cboTipoIdentificacion
        '
        Me.cboTipoIdentificacion.FormattingEnabled = True
        Me.cboTipoIdentificacion.ItemHeight = 23
        Me.cboTipoIdentificacion.Items.AddRange(New Object() {"Cedula Fisica", "Cedula Juridica"})
        Me.cboTipoIdentificacion.Location = New System.Drawing.Point(19, 41)
        Me.cboTipoIdentificacion.Name = "cboTipoIdentificacion"
        Me.cboTipoIdentificacion.Size = New System.Drawing.Size(126, 29)
        Me.cboTipoIdentificacion.TabIndex = 4
        Me.cboTipoIdentificacion.UseSelectable = True
        '
        'txtNombre
        '
        Me.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        '
        '
        '
        Me.txtNombre.CustomButton.Image = Nothing
        Me.txtNombre.CustomButton.Location = New System.Drawing.Point(327, 1)
        Me.txtNombre.CustomButton.Name = ""
        Me.txtNombre.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtNombre.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtNombre.CustomButton.TabIndex = 1
        Me.txtNombre.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtNombre.CustomButton.UseSelectable = True
        Me.txtNombre.CustomButton.Visible = False
        Me.txtNombre.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtNombre.Lines = New String(-1) {}
        Me.txtNombre.Location = New System.Drawing.Point(287, 41)
        Me.txtNombre.MaxLength = 32767
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtNombre.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNombre.SelectedText = ""
        Me.txtNombre.SelectionLength = 0
        Me.txtNombre.SelectionStart = 0
        Me.txtNombre.ShortcutsEnabled = True
        Me.txtNombre.Size = New System.Drawing.Size(355, 29)
        Me.txtNombre.TabIndex = 5
        Me.txtNombre.UseSelectable = True
        Me.txtNombre.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtNombre.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'txtCorreo
        '
        '
        '
        '
        Me.txtCorreo.CustomButton.Image = Nothing
        Me.txtCorreo.CustomButton.Location = New System.Drawing.Point(234, 1)
        Me.txtCorreo.CustomButton.Name = ""
        Me.txtCorreo.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtCorreo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtCorreo.CustomButton.TabIndex = 1
        Me.txtCorreo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtCorreo.CustomButton.UseSelectable = True
        Me.txtCorreo.CustomButton.Visible = False
        Me.txtCorreo.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtCorreo.Lines = New String(-1) {}
        Me.txtCorreo.Location = New System.Drawing.Point(19, 106)
        Me.txtCorreo.MaxLength = 32767
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtCorreo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCorreo.SelectedText = ""
        Me.txtCorreo.SelectionLength = 0
        Me.txtCorreo.SelectionStart = 0
        Me.txtCorreo.ShortcutsEnabled = True
        Me.txtCorreo.Size = New System.Drawing.Size(262, 29)
        Me.txtCorreo.TabIndex = 7
        Me.txtCorreo.UseSelectable = True
        Me.txtCorreo.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtCorreo.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.Location = New System.Drawing.Point(19, 84)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(126, 19)
        Me.MetroLabel4.TabIndex = 6
        Me.MetroLabel4.Text = "Correo Electronico :"
        '
        'txtTelefono
        '
        '
        '
        '
        Me.txtTelefono.CustomButton.Image = Nothing
        Me.txtTelefono.CustomButton.Location = New System.Drawing.Point(98, 1)
        Me.txtTelefono.CustomButton.Name = ""
        Me.txtTelefono.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtTelefono.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtTelefono.CustomButton.TabIndex = 1
        Me.txtTelefono.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtTelefono.CustomButton.UseSelectable = True
        Me.txtTelefono.CustomButton.Visible = False
        Me.txtTelefono.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtTelefono.Lines = New String(-1) {}
        Me.txtTelefono.Location = New System.Drawing.Point(287, 106)
        Me.txtTelefono.MaxLength = 32767
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtTelefono.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtTelefono.SelectedText = ""
        Me.txtTelefono.SelectionLength = 0
        Me.txtTelefono.SelectionStart = 0
        Me.txtTelefono.ShortcutsEnabled = True
        Me.txtTelefono.Size = New System.Drawing.Size(126, 29)
        Me.txtTelefono.TabIndex = 9
        Me.txtTelefono.UseSelectable = True
        Me.txtTelefono.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtTelefono.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel5
        '
        Me.MetroLabel5.AutoSize = True
        Me.MetroLabel5.Location = New System.Drawing.Point(287, 84)
        Me.MetroLabel5.Name = "MetroLabel5"
        Me.MetroLabel5.Size = New System.Drawing.Size(65, 19)
        Me.MetroLabel5.TabIndex = 8
        Me.MetroLabel5.Text = "Telefono :"
        Me.MetroLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtSucursal
        '
        '
        '
        '
        Me.txtSucursal.CustomButton.Image = Nothing
        Me.txtSucursal.CustomButton.Location = New System.Drawing.Point(98, 1)
        Me.txtSucursal.CustomButton.Name = ""
        Me.txtSucursal.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtSucursal.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtSucursal.CustomButton.TabIndex = 1
        Me.txtSucursal.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtSucursal.CustomButton.UseSelectable = True
        Me.txtSucursal.CustomButton.Visible = False
        Me.txtSucursal.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtSucursal.Lines = New String(-1) {}
        Me.txtSucursal.Location = New System.Drawing.Point(419, 106)
        Me.txtSucursal.MaxLength = 32767
        Me.txtSucursal.Name = "txtSucursal"
        Me.txtSucursal.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtSucursal.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSucursal.SelectedText = ""
        Me.txtSucursal.SelectionLength = 0
        Me.txtSucursal.SelectionStart = 0
        Me.txtSucursal.ShortcutsEnabled = True
        Me.txtSucursal.Size = New System.Drawing.Size(126, 29)
        Me.txtSucursal.TabIndex = 11
        Me.txtSucursal.UseSelectable = True
        Me.txtSucursal.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtSucursal.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel6
        '
        Me.MetroLabel6.AutoSize = True
        Me.MetroLabel6.Location = New System.Drawing.Point(419, 84)
        Me.MetroLabel6.Name = "MetroLabel6"
        Me.MetroLabel6.Size = New System.Drawing.Size(80, 19)
        Me.MetroLabel6.TabIndex = 10
        Me.MetroLabel6.Text = "N. Sucursal :"
        Me.MetroLabel6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cboProvincia
        '
        Me.cboProvincia.FormattingEnabled = True
        Me.cboProvincia.ItemHeight = 23
        Me.cboProvincia.Location = New System.Drawing.Point(106, 166)
        Me.cboProvincia.Name = "cboProvincia"
        Me.cboProvincia.Size = New System.Drawing.Size(175, 29)
        Me.cboProvincia.TabIndex = 12
        Me.cboProvincia.UseSelectable = True
        '
        'MetroLabel7
        '
        Me.MetroLabel7.AutoSize = True
        Me.MetroLabel7.Location = New System.Drawing.Point(19, 166)
        Me.MetroLabel7.Name = "MetroLabel7"
        Me.MetroLabel7.Size = New System.Drawing.Size(69, 19)
        Me.MetroLabel7.TabIndex = 13
        Me.MetroLabel7.Text = "Provincia :"
        '
        'MetroLabel8
        '
        Me.MetroLabel8.AutoSize = True
        Me.MetroLabel8.Location = New System.Drawing.Point(19, 201)
        Me.MetroLabel8.Name = "MetroLabel8"
        Me.MetroLabel8.Size = New System.Drawing.Size(58, 19)
        Me.MetroLabel8.TabIndex = 14
        Me.MetroLabel8.Text = "Canton :"
        '
        'MetroLabel9
        '
        Me.MetroLabel9.AutoSize = True
        Me.MetroLabel9.Location = New System.Drawing.Point(19, 235)
        Me.MetroLabel9.Name = "MetroLabel9"
        Me.MetroLabel9.Size = New System.Drawing.Size(57, 19)
        Me.MetroLabel9.TabIndex = 15
        Me.MetroLabel9.Text = "Distrito :"
        '
        'cboCanton
        '
        Me.cboCanton.FormattingEnabled = True
        Me.cboCanton.ItemHeight = 23
        Me.cboCanton.Location = New System.Drawing.Point(106, 201)
        Me.cboCanton.Name = "cboCanton"
        Me.cboCanton.Size = New System.Drawing.Size(175, 29)
        Me.cboCanton.TabIndex = 16
        Me.cboCanton.UseSelectable = True
        '
        'cboDistrito
        '
        Me.cboDistrito.FormattingEnabled = True
        Me.cboDistrito.ItemHeight = 23
        Me.cboDistrito.Location = New System.Drawing.Point(106, 235)
        Me.cboDistrito.Name = "cboDistrito"
        Me.cboDistrito.Size = New System.Drawing.Size(175, 29)
        Me.cboDistrito.TabIndex = 17
        Me.cboDistrito.UseSelectable = True
        '
        'txtOtrasSeñas
        '
        '
        '
        '
        Me.txtOtrasSeñas.CustomButton.Image = Nothing
        Me.txtOtrasSeñas.CustomButton.Location = New System.Drawing.Point(257, 1)
        Me.txtOtrasSeñas.CustomButton.Name = ""
        Me.txtOtrasSeñas.CustomButton.Size = New System.Drawing.Size(97, 97)
        Me.txtOtrasSeñas.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtOtrasSeñas.CustomButton.TabIndex = 1
        Me.txtOtrasSeñas.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtOtrasSeñas.CustomButton.UseSelectable = True
        Me.txtOtrasSeñas.CustomButton.Visible = False
        Me.txtOtrasSeñas.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtOtrasSeñas.Lines = New String(-1) {}
        Me.txtOtrasSeñas.Location = New System.Drawing.Point(287, 166)
        Me.txtOtrasSeñas.MaxLength = 32767
        Me.txtOtrasSeñas.Multiline = True
        Me.txtOtrasSeñas.Name = "txtOtrasSeñas"
        Me.txtOtrasSeñas.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtOtrasSeñas.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtOtrasSeñas.SelectedText = ""
        Me.txtOtrasSeñas.SelectionLength = 0
        Me.txtOtrasSeñas.SelectionStart = 0
        Me.txtOtrasSeñas.ShortcutsEnabled = True
        Me.txtOtrasSeñas.Size = New System.Drawing.Size(355, 99)
        Me.txtOtrasSeñas.TabIndex = 18
        Me.txtOtrasSeñas.UseSelectable = True
        Me.txtOtrasSeñas.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtOtrasSeñas.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel10
        '
        Me.MetroLabel10.AutoSize = True
        Me.MetroLabel10.Location = New System.Drawing.Point(287, 143)
        Me.MetroLabel10.Name = "MetroLabel10"
        Me.MetroLabel10.Size = New System.Drawing.Size(151, 19)
        Me.MetroLabel10.TabIndex = 19
        Me.MetroLabel10.Text = "Otras Señas (Direccion) :"
        Me.MetroLabel10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'MetroTabControl1
        '
        Me.MetroTabControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage1)
        Me.MetroTabControl1.Controls.Add(Me.MetroTabPage2)
        Me.MetroTabControl1.Location = New System.Drawing.Point(17, 70)
        Me.MetroTabControl1.Multiline = True
        Me.MetroTabControl1.Name = "MetroTabControl1"
        Me.MetroTabControl1.SelectedIndex = 0
        Me.MetroTabControl1.Size = New System.Drawing.Size(656, 323)
        Me.MetroTabControl1.TabIndex = 20
        Me.MetroTabControl1.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabControl1.UseSelectable = True
        '
        'MetroTabPage1
        '
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel10)
        Me.MetroTabPage1.Controls.Add(Me.txtOtrasSeñas)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel1)
        Me.MetroTabPage1.Controls.Add(Me.cboDistrito)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel2)
        Me.MetroTabPage1.Controls.Add(Me.cboCanton)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel3)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel9)
        Me.MetroTabPage1.Controls.Add(Me.txtIdentificacion)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel8)
        Me.MetroTabPage1.Controls.Add(Me.cboTipoIdentificacion)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel7)
        Me.MetroTabPage1.Controls.Add(Me.txtNombre)
        Me.MetroTabPage1.Controls.Add(Me.cboProvincia)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel4)
        Me.MetroTabPage1.Controls.Add(Me.txtSucursal)
        Me.MetroTabPage1.Controls.Add(Me.txtCorreo)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel6)
        Me.MetroTabPage1.Controls.Add(Me.MetroLabel5)
        Me.MetroTabPage1.Controls.Add(Me.txtTelefono)
        Me.MetroTabPage1.HorizontalScrollbarBarColor = True
        Me.MetroTabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.HorizontalScrollbarSize = 10
        Me.MetroTabPage1.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage1.Name = "MetroTabPage1"
        Me.MetroTabPage1.Size = New System.Drawing.Size(648, 281)
        Me.MetroTabPage1.TabIndex = 0
        Me.MetroTabPage1.Text = "Datos Emisor"
        Me.MetroTabPage1.Theme = MetroFramework.MetroThemeStyle.Light
        Me.MetroTabPage1.VerticalScrollbarBarColor = True
        Me.MetroTabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage1.VerticalScrollbarSize = 10
        '
        'MetroTabPage2
        '
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel15)
        Me.MetroTabPage2.Controls.Add(Me.txtFechaResolucion)
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel14)
        Me.MetroTabPage2.Controls.Add(Me.txtNumeroResolucion)
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel13)
        Me.MetroTabPage2.Controls.Add(Me.txtCertificado)
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel12)
        Me.MetroTabPage2.Controls.Add(Me.txtClave)
        Me.MetroTabPage2.Controls.Add(Me.MetroLabel11)
        Me.MetroTabPage2.Controls.Add(Me.txtUsuario)
        Me.MetroTabPage2.HorizontalScrollbarBarColor = True
        Me.MetroTabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.HorizontalScrollbarSize = 10
        Me.MetroTabPage2.Location = New System.Drawing.Point(4, 38)
        Me.MetroTabPage2.Name = "MetroTabPage2"
        Me.MetroTabPage2.Size = New System.Drawing.Size(648, 281)
        Me.MetroTabPage2.TabIndex = 1
        Me.MetroTabPage2.Text = "Datos Tributarios"
        Me.MetroTabPage2.VerticalScrollbarBarColor = True
        Me.MetroTabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.MetroTabPage2.VerticalScrollbarSize = 10
        '
        'MetroLabel15
        '
        Me.MetroLabel15.AutoSize = True
        Me.MetroLabel15.Location = New System.Drawing.Point(332, 207)
        Me.MetroLabel15.Name = "MetroLabel15"
        Me.MetroLabel15.Size = New System.Drawing.Size(167, 19)
        Me.MetroLabel15.TabIndex = 16
        Me.MetroLabel15.Text = "Fecha Resolucion Hacienda"
        '
        'txtFechaResolucion
        '
        '
        '
        '
        Me.txtFechaResolucion.CustomButton.Image = Nothing
        Me.txtFechaResolucion.CustomButton.Location = New System.Drawing.Point(282, 1)
        Me.txtFechaResolucion.CustomButton.Name = ""
        Me.txtFechaResolucion.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtFechaResolucion.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtFechaResolucion.CustomButton.TabIndex = 1
        Me.txtFechaResolucion.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtFechaResolucion.CustomButton.UseSelectable = True
        Me.txtFechaResolucion.CustomButton.Visible = False
        Me.txtFechaResolucion.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtFechaResolucion.Lines = New String(-1) {}
        Me.txtFechaResolucion.Location = New System.Drawing.Point(332, 229)
        Me.txtFechaResolucion.MaxLength = 32767
        Me.txtFechaResolucion.Name = "txtFechaResolucion"
        Me.txtFechaResolucion.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtFechaResolucion.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtFechaResolucion.SelectedText = ""
        Me.txtFechaResolucion.SelectionLength = 0
        Me.txtFechaResolucion.SelectionStart = 0
        Me.txtFechaResolucion.ShortcutsEnabled = True
        Me.txtFechaResolucion.Size = New System.Drawing.Size(310, 29)
        Me.txtFechaResolucion.TabIndex = 17
        Me.txtFechaResolucion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtFechaResolucion.UseSelectable = True
        Me.txtFechaResolucion.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtFechaResolucion.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel14
        '
        Me.MetroLabel14.AutoSize = True
        Me.MetroLabel14.Location = New System.Drawing.Point(18, 207)
        Me.MetroLabel14.Name = "MetroLabel14"
        Me.MetroLabel14.Size = New System.Drawing.Size(189, 19)
        Me.MetroLabel14.TabIndex = 14
        Me.MetroLabel14.Text = "Numero Resolucion Hacienda :"
        '
        'txtNumeroResolucion
        '
        '
        '
        '
        Me.txtNumeroResolucion.CustomButton.Image = Nothing
        Me.txtNumeroResolucion.CustomButton.Location = New System.Drawing.Point(282, 1)
        Me.txtNumeroResolucion.CustomButton.Name = ""
        Me.txtNumeroResolucion.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtNumeroResolucion.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtNumeroResolucion.CustomButton.TabIndex = 1
        Me.txtNumeroResolucion.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtNumeroResolucion.CustomButton.UseSelectable = True
        Me.txtNumeroResolucion.CustomButton.Visible = False
        Me.txtNumeroResolucion.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtNumeroResolucion.Lines = New String(-1) {}
        Me.txtNumeroResolucion.Location = New System.Drawing.Point(18, 229)
        Me.txtNumeroResolucion.MaxLength = 32767
        Me.txtNumeroResolucion.Name = "txtNumeroResolucion"
        Me.txtNumeroResolucion.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtNumeroResolucion.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtNumeroResolucion.SelectedText = ""
        Me.txtNumeroResolucion.SelectionLength = 0
        Me.txtNumeroResolucion.SelectionStart = 0
        Me.txtNumeroResolucion.ShortcutsEnabled = True
        Me.txtNumeroResolucion.Size = New System.Drawing.Size(310, 29)
        Me.txtNumeroResolucion.TabIndex = 15
        Me.txtNumeroResolucion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtNumeroResolucion.UseSelectable = True
        Me.txtNumeroResolucion.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtNumeroResolucion.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel13
        '
        Me.MetroLabel13.AutoSize = True
        Me.MetroLabel13.Location = New System.Drawing.Point(18, 143)
        Me.MetroLabel13.Name = "MetroLabel13"
        Me.MetroLabel13.Size = New System.Drawing.Size(81, 19)
        Me.MetroLabel13.TabIndex = 12
        Me.MetroLabel13.Text = "Certificado :"
        '
        'txtCertificado
        '
        '
        '
        '
        Me.txtCertificado.CustomButton.Image = Nothing
        Me.txtCertificado.CustomButton.Location = New System.Drawing.Point(596, 1)
        Me.txtCertificado.CustomButton.Name = ""
        Me.txtCertificado.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtCertificado.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtCertificado.CustomButton.TabIndex = 1
        Me.txtCertificado.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtCertificado.CustomButton.UseSelectable = True
        Me.txtCertificado.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtCertificado.Lines = New String(-1) {}
        Me.txtCertificado.Location = New System.Drawing.Point(18, 165)
        Me.txtCertificado.MaxLength = 32767
        Me.txtCertificado.Name = "txtCertificado"
        Me.txtCertificado.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtCertificado.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCertificado.SelectedText = ""
        Me.txtCertificado.SelectionLength = 0
        Me.txtCertificado.SelectionStart = 0
        Me.txtCertificado.ShortcutsEnabled = True
        Me.txtCertificado.ShowButton = True
        Me.txtCertificado.Size = New System.Drawing.Size(624, 29)
        Me.txtCertificado.TabIndex = 13
        Me.txtCertificado.UseSelectable = True
        Me.txtCertificado.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtCertificado.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel12
        '
        Me.MetroLabel12.AutoSize = True
        Me.MetroLabel12.Location = New System.Drawing.Point(18, 83)
        Me.MetroLabel12.Name = "MetroLabel12"
        Me.MetroLabel12.Size = New System.Drawing.Size(130, 19)
        Me.MetroLabel12.TabIndex = 10
        Me.MetroLabel12.Text = "Clave API Hacienda :"
        '
        'txtClave
        '
        '
        '
        '
        Me.txtClave.CustomButton.Image = Nothing
        Me.txtClave.CustomButton.Location = New System.Drawing.Point(596, 1)
        Me.txtClave.CustomButton.Name = ""
        Me.txtClave.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtClave.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtClave.CustomButton.TabIndex = 1
        Me.txtClave.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtClave.CustomButton.UseSelectable = True
        Me.txtClave.CustomButton.Visible = False
        Me.txtClave.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtClave.Lines = New String(-1) {}
        Me.txtClave.Location = New System.Drawing.Point(18, 105)
        Me.txtClave.MaxLength = 32767
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtClave.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtClave.SelectedText = ""
        Me.txtClave.SelectionLength = 0
        Me.txtClave.SelectionStart = 0
        Me.txtClave.ShortcutsEnabled = True
        Me.txtClave.Size = New System.Drawing.Size(624, 29)
        Me.txtClave.TabIndex = 11
        Me.txtClave.UseSelectable = True
        Me.txtClave.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtClave.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel11
        '
        Me.MetroLabel11.AutoSize = True
        Me.MetroLabel11.Location = New System.Drawing.Point(18, 19)
        Me.MetroLabel11.Name = "MetroLabel11"
        Me.MetroLabel11.Size = New System.Drawing.Size(142, 19)
        Me.MetroLabel11.TabIndex = 8
        Me.MetroLabel11.Text = "Usuario API Hacienda :"
        '
        'txtUsuario
        '
        '
        '
        '
        Me.txtUsuario.CustomButton.Image = Nothing
        Me.txtUsuario.CustomButton.Location = New System.Drawing.Point(596, 1)
        Me.txtUsuario.CustomButton.Name = ""
        Me.txtUsuario.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.txtUsuario.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtUsuario.CustomButton.TabIndex = 1
        Me.txtUsuario.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtUsuario.CustomButton.UseSelectable = True
        Me.txtUsuario.CustomButton.Visible = False
        Me.txtUsuario.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.txtUsuario.Lines = New String(-1) {}
        Me.txtUsuario.Location = New System.Drawing.Point(18, 41)
        Me.txtUsuario.MaxLength = 32767
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUsuario.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtUsuario.SelectedText = ""
        Me.txtUsuario.SelectionLength = 0
        Me.txtUsuario.SelectionStart = 0
        Me.txtUsuario.ShortcutsEnabled = True
        Me.txtUsuario.Size = New System.Drawing.Size(624, 29)
        Me.txtUsuario.TabIndex = 9
        Me.txtUsuario.UseSelectable = True
        Me.txtUsuario.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtUsuario.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.FontSize = MetroFramework.MetroButtonSize.Tall
        Me.btnCancelar.Location = New System.Drawing.Point(508, 417)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(155, 40)
        Me.btnCancelar.TabIndex = 21
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseSelectable = True
        '
        'btnAceptar
        '
        Me.btnAceptar.FontSize = MetroFramework.MetroButtonSize.Tall
        Me.btnAceptar.Location = New System.Drawing.Point(347, 417)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(155, 40)
        Me.btnAceptar.TabIndex = 22
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseSelectable = True
        '
        'frmConfiguraciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(693, 480)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.MetroTabControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfiguraciones"
        Me.Text = "Configuraciones"
        Me.MetroTabControl1.ResumeLayout(False)
        Me.MetroTabPage1.ResumeLayout(False)
        Me.MetroTabPage1.PerformLayout()
        Me.MetroTabPage2.ResumeLayout(False)
        Me.MetroTabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtIdentificacion As MetroFramework.Controls.MetroTextBox
    Friend WithEvents cboTipoIdentificacion As MetroFramework.Controls.MetroComboBox
    Friend WithEvents txtNombre As MetroFramework.Controls.MetroTextBox
    Friend WithEvents txtCorreo As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtTelefono As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtSucursal As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel6 As MetroFramework.Controls.MetroLabel
    Friend WithEvents cboProvincia As MetroFramework.Controls.MetroComboBox
    Friend WithEvents MetroLabel7 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel8 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel9 As MetroFramework.Controls.MetroLabel
    Friend WithEvents cboCanton As MetroFramework.Controls.MetroComboBox
    Friend WithEvents cboDistrito As MetroFramework.Controls.MetroComboBox
    Friend WithEvents txtOtrasSeñas As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel10 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroTabControl1 As MetroFramework.Controls.MetroTabControl
    Friend WithEvents MetroTabPage1 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents MetroTabPage2 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents btnCancelar As MetroFramework.Controls.MetroButton
    Friend WithEvents btnAceptar As MetroFramework.Controls.MetroButton
    Friend WithEvents MetroLabel12 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtClave As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel11 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtUsuario As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel13 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtCertificado As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel15 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtFechaResolucion As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel14 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtNumeroResolucion As MetroFramework.Controls.MetroTextBox
End Class
