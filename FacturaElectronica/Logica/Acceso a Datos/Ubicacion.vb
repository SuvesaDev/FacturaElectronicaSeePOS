Namespace GestionDatos
    Public Class Ubicacion

        Public Function Obtener_Provincia() As DataTable
            Dim dt As New DataTable
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    dt = General.sql.Ejecutar("SELECT DISTINCT ID_PROVINCIA, PROVINCIA FROM UBICACION", CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
            Return dt
        End Function

        Public Function Obtener_Canton(_IdProvincia As Integer) As DataTable
            Dim dt As New DataTable
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    dt = General.sql.Ejecutar("SELECT DISTINCT ID_CANTON, CANTON FROM UBICACION WHERE ID_PROVINCIA = " & _IdProvincia, CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
            Return dt
        End Function

        Public Function Obtener_Distrito(_IdProvincia As Integer, _IdCanton As Integer) As DataTable
            Dim dt As New DataTable
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    dt = General.sql.Ejecutar("SELECT DISTINCT ID_DISTRITO, DISTRITO FROM UBICACION WHERE ID_PROVINCIA = " & _IdProvincia & " AND ID_CANTON = " & _IdCanton, CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
            Return dt
        End Function

    End Class

End Namespace
