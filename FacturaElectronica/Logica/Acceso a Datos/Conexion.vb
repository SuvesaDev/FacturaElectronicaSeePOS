Imports System.Data.SqlClient
'***********************************************************************************
Namespace SQL
    Public Class Sentencias
        Private Conexion As New SqlConnection
        Private Adapter As SqlDataAdapter
        Private Parametros As New List(Of SqlParameter)

        Public Function Test(_Con As String) As String
            Try
                Me.Conexion = New SqlConnection(_Con)
                Me.Conexion.Open()
                Me.Conexion.Close()
                Return "Correcto!!!"
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Public Sub Fill(ByVal _strSQL As String, ByRef _dts As DataTable, Optional ByVal _cmd As CommandType = CommandType.Text)
            Try
                Me.Conexion.Open()
                Me.Adapter = New SqlDataAdapter(_strSQL, Me.Conexion)
                Me.Adapter.SelectCommand.CommandType = _cmd
                If _cmd = CommandType.StoredProcedure Then Me.Adapter.SelectCommand.Parameters.AddRange(Me.Parametros.ToArray)
                Me.Adapter.Fill(_dts)
                Me.Conexion.Close()
                Me.Parametros.Clear()
            Catch ex As Exception
                Conexion.Close()
                Me.Parametros.Clear()
            End Try
        End Sub

        Sub New(ByVal _Cadena As String)
            Me.Conexion = New SqlConnection(_Cadena)
        End Sub
        Public Sub AddParametro(ByVal _nombre As String, ByVal _valor As Object)
            Parametros.Add(New SqlParameter(_nombre, _valor))
        End Sub
        Public Sub AddParametrosSalida(ByVal _nombre As String, ByVal _valor As String)
            Dim parametro As New SqlParameter(_nombre, SqlDbType.Text)
            parametro.Value = _valor
            parametro.Direction = ParameterDirection.InputOutput
            Parametros.Add(parametro)
        End Sub
        Public Sub AddParametroSalida(ByVal _nombre As String, ByVal _valor As Integer)
            Dim parametro As New SqlParameter(_nombre, SqlDbType.BigInt)
            parametro.Value = _valor
            parametro.Direction = ParameterDirection.InputOutput
            Parametros.Add(parametro)
        End Sub
        Public Function Ejecutar(ByVal _strSQL As String, ByRef _dt As DataTable, Optional ByVal _Tipo As CommandType = CommandType.StoredProcedure) As DataTable
            Try
                Me.Conexion.Open()
                Me.Adapter = New SqlDataAdapter(_strSQL, Me.Conexion)
                Me.Adapter.SelectCommand.CommandType = _Tipo
                Me.Adapter.SelectCommand.Parameters.AddRange(Me.Parametros.ToArray)
                Me.Adapter.Fill(_dt)
                Me.Conexion.Close()
                Me.Parametros.Clear()
                Return _dt
            Catch ex As Exception
                Conexion.Close()
                Me.Parametros.Clear()
                Return Nothing
            End Try
        End Function

        Public Function Ejecutar(ByVal _strSQL As String, Optional ByVal _Tipo As CommandType = CommandType.StoredProcedure) As DataTable
            Try
                Dim dt As New DataTable
                Me.Conexion.Open()
                Me.Adapter = New SqlDataAdapter(_strSQL, Me.Conexion)
                Me.Adapter.SelectCommand.CommandType = _Tipo
                Me.Adapter.SelectCommand.Parameters.AddRange(Me.Parametros.ToArray)
                Me.Adapter.Fill(dt)
                Me.Conexion.Close()
                Me.Parametros.Clear()
                Return dt
            Catch ex As Exception
                Conexion.Close()
                Me.Parametros.Clear()
                Return Nothing
            End Try
        End Function

        Public Function Ejecutar(ByRef _ID As String, ByVal _index As Int16, ByVal _strSQL As String) As DataTable
            Try
                Dim dt As New DataTable
                Me.Conexion.Open()
                Me.Adapter = New SqlDataAdapter(_strSQL, Me.Conexion)
                Me.Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
                Me.Adapter.SelectCommand.Parameters.AddRange(Me.Parametros.ToArray)
                Me.Adapter.Fill(dt)
                Me.Conexion.Close()
                _ID = Parametros.Item(_index).Value
                Me.Parametros.Clear()
                Return dt
            Catch ex As Exception
                Conexion.Close()
                Me.Parametros.Clear()
                Return Nothing
            End Try
        End Function
    End Class
    Public Class Transaccion
        Private Conexion As SqlConnection
        Private Comando As SqlCommand
        Private Transaccion As SqlTransaction
        Private Parametros As New List(Of SqlParameter)
        Sub New(ByVal _Cadena As String)
            Me.Conexion = New SqlConnection(_Cadena)
            Me.Conexion.Open()
            Me.Transaccion = Me.Conexion.BeginTransaction()
        End Sub
        Public Sub SetParametro(ByVal _nombre As String, ByVal _valor As Object)
            Parametros.Add(New SqlParameter(_nombre, _valor))
        End Sub
        Public Sub AddParametrosSalida(ByVal _nombre As String, ByVal _valor As String)
            Dim parametro As New SqlParameter(_nombre, SqlDbType.Text)
            parametro.Value = _valor
            parametro.Direction = ParameterDirection.InputOutput
            Parametros.Add(parametro)
        End Sub
        Public Sub AddParametroSalida(ByVal _nombre As String, ByVal _valor As Integer)
            Dim parametro As New SqlParameter(_nombre, SqlDbType.BigInt)
            parametro.Value = _valor
            parametro.Direction = ParameterDirection.InputOutput
            Parametros.Add(parametro)
        End Sub
        Public Sub Commit()
            Me.Transaccion.Commit()
            Me.Transaccion.Dispose()
            Me.Conexion.Close()
        End Sub
        Public Sub Rollback()
            Me.Transaccion.Rollback()
            Me.Transaccion.Dispose()
            Me.Conexion.Close()
        End Sub
        Public Sub Ejecutar(ByVal _strSQL As String, Optional ByVal _Tipo As CommandType = CommandType.StoredProcedure)
            Me.Comando = New SqlCommand(_strSQL, Me.Conexion)
            Me.Comando.CommandType = _Tipo
            If _Tipo = CommandType.StoredProcedure Then Me.Comando.Parameters.AddRange(Me.Parametros.ToArray)
            Me.Comando.Transaction = Me.Transaccion
            Me.Comando.ExecuteNonQuery()
            Me.Parametros.Clear()
        End Sub
        Public Sub Ejecutar(ByVal _strSQL As String, ByRef _ID1 As String, ByVal _index1 As Int16, Optional ByRef _ID2 As String = "", Optional ByVal _index2 As Int16 = 0)
            Me.Comando = New SqlCommand(_strSQL, Me.Conexion)
            Me.Comando.CommandType = CommandType.StoredProcedure
            Me.Comando.Parameters.AddRange(Me.Parametros.ToArray)
            Me.Comando.Transaction = Me.Transaccion
            Me.Comando.ExecuteNonQuery()
            _ID1 = Parametros.Item(_index1).Value
            _ID2 = Parametros.Item(_index2).Value
            Me.Parametros.Clear()
        End Sub
    End Class
End Namespace
'***********************************************************************************
