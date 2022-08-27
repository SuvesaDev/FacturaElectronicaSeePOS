Module General

    Public sql As New SQL.Sentencias(GetSetting("SeeSOFT", "Seguridad", "Conexion"))
    Public mysql As New MySql.Sentencias("")
    Public BasedeDatos As String = ""

    Public Function TipoConexion() As TipoConexion
        'debe de validar a que servidor se ba a conectar
        Return FacturaElectronica.TipoConexion.SQL
    End Function


End Module

Public Enum TipoConexion
    SQL
    MYSQL
End Enum