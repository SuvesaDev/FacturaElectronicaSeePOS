Namespace GestionDatos
    Public Class Emisor

        Public tipoidentificacion As String
        Public identificacion As String
        Public nombre As String
        Public correo As String
        Public telefono As String
        Public sucursal As String
        Public provincia As Integer
        Public canton As Integer
        Public distrito As Integer
        Public otrasseñas As String
        Public usuario As String
        Public clave As String
        Public certificado As String
        Public numeroresolucion As String
        Public fecharesolucion As String

        Public Sub Actualizar_Datos()
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    General.sql.AddParametro("@tipoidentificacion", Me.tipoidentificacion)
                    General.sql.AddParametro("@identificacion", Me.identificacion)
                    General.sql.AddParametro("@nombre", Me.nombre)
                    General.sql.AddParametro("@correo", Me.correo)
                    General.sql.AddParametro("@telefono", Me.telefono)
                    General.sql.AddParametro("@sucursal", Me.sucursal)
                    General.sql.AddParametro("@provincia", Me.provincia)
                    General.sql.AddParametro("@canton", Me.canton)
                    General.sql.AddParametro("@distrito", Me.distrito)
                    General.sql.AddParametro("@otrasseñas", Me.otrasseñas)
                    General.sql.AddParametro("@usuario", Me.usuario)
                    General.sql.AddParametro("@clave", Me.clave)
                    General.sql.AddParametro("@certificado", Me.certificado)
                    General.sql.AddParametro("@numeroresolucion", Me.numeroresolucion)
                    General.sql.AddParametro("@fecharesolucion", Me.fecharesolucion)
                    General.sql.Ejecutar("UPDATE Emisor SET TipoIdentificacion = @TipoIdentificacion, Identificacion = @Identificacion, Nombre = @Nombre, Correo = @Correo, Telefono = @Telefono, Sucursal = @Sucursal, Provincia = @Provincia, Canton = @Canton, Distrito = @Distrito, OtrasSeñas = @OtrasSeñas, Usuario = @Usuario, Clave = @Clave, Certificado = @Certificado, NumeroResolucion = @NumeroResolucion, FechaResolucion = @FechaResolucion", CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
        End Sub

        Public Sub Obtener_Datos()
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    Dim dt As New DataTable
                    dt = General.sql.Ejecutar("SELECT [TipoIdentificacion],[Identificacion],[Nombre],[Correo],[Telefono],[Sucursal],[Provincia],[Canton],[Distrito],[OtrasSeñas],[Usuario],[Clave],[Certificado],[NumeroResolucion],[FechaResolucion]FROM [Seguridad].[dbo].[Emisor]", CommandType.Text)
                    Me.tipoidentificacion = dt.Rows(0).Item("tipoidentificacion")
                    Me.identificacion = dt.Rows(0).Item("identificacion")
                    Me.nombre = dt.Rows(0).Item("nombre")
                    Me.telefono = dt.Rows(0).Item("telefono")
                    Me.correo = dt.Rows(0).Item("correo")
                    Me.sucursal = dt.Rows(0).Item("sucursal")
                    Me.provincia = dt.Rows(0).Item("provincia")
                    Me.canton = dt.Rows(0).Item("canton")
                    Me.distrito = dt.Rows(0).Item("distrito")
                    Me.otrasseñas = dt.Rows(0).Item("otrasseñas")
                    Me.usuario = dt.Rows(0).Item("usuario")
                    Me.clave = dt.Rows(0).Item("clave")
                    Me.certificado = dt.Rows(0).Item("certificado")
                    Me.numeroresolucion = dt.Rows(0).Item("numeroresolucion")
                    Me.fecharesolucion = dt.Rows(0).Item("fecharesolucion")
                Case TipoConexion.MYSQL

            End Select
        End Sub

    End Class

End Namespace
