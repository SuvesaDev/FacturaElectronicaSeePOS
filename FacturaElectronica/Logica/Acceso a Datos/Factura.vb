Namespace GestionDatos
    Public Class Factura
        Private Correo As New Correo

        Public Function Cargar_Facturas_Pendientes() As DataTable
            Dim dt As New DataTable
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    dt = General.sql.Ejecutar("select *  from dbo.viewFacturas", CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
            Return dt
        End Function
        '**********************************************************************************************
        'Funciones Generales
        '**********************************************************************************************
        Public Sub CambiarEstadoComprobante(_Clave As String, _Estado As String)
            'Actualiza estado de factura
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EstadoMH = '" & _Estado & "' Where ClaveMH = '" & _Clave & "'", CommandType.Text)
            'Actualiza estado de devoluciones
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.devoluciones_ventas Set ESTADODGT = '" & _Estado & "' Where CLAVEDGT = '" & _Clave & "'", CommandType.Text)
            'Actualiza estado de mensajes receptor
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.MensajeReceptor Set Estado = '" & _Estado & "' Where Clave = '" & _Clave & "'", CommandType.Text)
        End Sub
        '**********************************************************************************************
        'Datos Factura
        '**********************************************************************************************
        'Public Function Obtener_Factura_42(_IdFactura As String) As DataTable
        '    Dim dt As New DataTable
        '    Select Case General.TipoConexion
        '        Case TipoConexion.SQL
        '            General.sql.AddParametro("@IdFactura", _IdFactura)
        '            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
        '            dt = General.sql.Ejecutar("Obtener_Factura")
        '        Case TipoConexion.MYSQL

        '    End Select
        '    Return dt
        'End Function

        Public Function Obtener_Factura_43(_IdFactura As String) As DataTable
            Dim dt As New DataTable
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    General.sql.AddParametro("@IdFactura", _IdFactura)
                    General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
                    dt = General.sql.Ejecutar("Obtener_Factura43")
                Case TipoConexion.MYSQL

            End Select
            Return dt
        End Function

        'Public Function Obtener_DetallesFactura_42(_IdFactura As String) As DataTable
        '    Dim dt As New DataTable
        '    Select Case General.TipoConexion
        '        Case TipoConexion.SQL
        '            General.sql.AddParametro("@IdFactura", _IdFactura)
        '            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
        '            dt = General.sql.Ejecutar("Obtener_DetallesFactura")
        '        Case TipoConexion.MYSQL

        '    End Select
        '    Return dt
        'End Function

        Public Function Obtener_DetallesFactura_43(_IdFactura As String) As DataTable
            Dim dt As New DataTable
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    General.sql.AddParametro("@IdFactura", _IdFactura)
                    General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
                    dt = General.sql.Ejecutar("Obtener_DetallesFactura43")
                Case TipoConexion.MYSQL

            End Select
            Return dt
        End Function

        Public Function Obtener_ClaveFacturaMH(_IdFactura As String) As String
            Dim ClaveMH As String = ""
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    ClaveMH = General.sql.Ejecutar("Select ClaveMH From " & General.BasedeDatos & ".dbo.Ventas Where Id = " & _IdFactura, CommandType.Text).Rows(0).Item(0)
                Case TipoConexion.MYSQL

            End Select
            Return ClaveMH
        End Function
        Public Function Obtener_ConsecutivoFacturaMH(_IdFactura As String) As String
            Dim ConsecutivoMH As String = ""
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    ConsecutivoMH = General.sql.Ejecutar("Select ConsecutivoMH From " & General.BasedeDatos & ".dbo.Ventas Where Id = " & _IdFactura, CommandType.Text).Rows(0).Item(0)
                Case TipoConexion.MYSQL

            End Select
            Return ConsecutivoMH
        End Function
        Public Sub FacturaAceptada(_IdFactura As String, _Clave As String, _Consecutivo As String)
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    'General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EnviadoMH = 1, ClaveMH = '" & _Clave & "', ConsecutivoMH = '" & _Consecutivo & "', EstadoMH = 'procesando' where Id = " & _IdFactura, CommandType.Text)
                    General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EnviadoMH = 1, EstadoMH = 'procesando' where Id = " & _IdFactura, CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
        End Sub
        'Public Sub RegistrarEstado(_IdFactura As String, _Clave As String, _Estado As String)
        '    Select Case General.TipoConexion
        '        Case TipoConexion.SQL
        '            'General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EstadoMH = '" & _Estado & "', ClaveMH = '" & _Clave & "' where Id = " & _IdFactura, CommandType.Text)
        '            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EstadoMH = '" & _Estado & "' where Id = " & _IdFactura, CommandType.Text)
        '        Case TipoConexion.MYSQL

        '    End Select
        'End Sub
        Public Sub RegistrarEstadoFactura(_IdFactura As String, _Clave As String, _Consecutivo As String, _Estado As String)
            'General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EstadoMH = '" & _Estado & "', ClaveMH = '" & _Clave & "', ConsecutivoMH = '" & _Consecutivo & "' where Id = " & _IdFactura, CommandType.Text)
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EstadoMH = '" & _Estado & "' where Id = " & _IdFactura, CommandType.Text)
        End Sub
        Public Sub CambiarEstadoFactura_Aceptada(_IdFactura As String)
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    'General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EnviadoMH = 1, EstadoMH = 'aceptado' where Id = " & _IdFactura, CommandType.Text)
                    General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set EnviadoMH = 1, EstadoMH = 'aceptado' where Id = " & _IdFactura, CommandType.Text)
                    Me.Correo.EnviarFactura(_IdFactura)
                Case TipoConexion.MYSQL

            End Select
        End Sub
        Public Sub CambiarEstadoFactura(_IdFactura As String, _Estado As String)
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    'General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set  EstadoMH = '" & _Estado & "' where Id = " & _IdFactura, CommandType.Text)
                    General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.Ventas Set  EstadoMH = '" & _Estado & "' where Id = " & _IdFactura, CommandType.Text)
                Case TipoConexion.MYSQL

            End Select
        End Sub
        '**********************************************************************************************
        'Datos Devolucion
        '**********************************************************************************************
        Public Function Obtener_Devolucion(_IdDevolucion As String) As DataTable
            Dim dt As New DataTable
            General.sql.AddParametro("@IdDevolucion", _IdDevolucion)
            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
            dt = General.sql.Ejecutar("Obtener_Devolucion")
            Return dt
        End Function
        Public Function Obtener_DetallesDevolucion(_IdDevolucion As String) As DataTable
            Dim dt As New DataTable
            General.sql.AddParametro("@IdDevolucion", _IdDevolucion)
            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
            dt = General.sql.Ejecutar("Obtener_DetallesDevolucion")
            Return dt
        End Function
        Public Function Obtener_Devolucion43(_IdDevolucion As String) As DataTable
            Dim dt As New DataTable
            General.sql.AddParametro("@IdDevolucion", _IdDevolucion)
            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
            dt = General.sql.Ejecutar("Obtener_Devolucion43")
            Return dt
        End Function
        Public Function Obtener_DetallesDevolucion43(_IdDevolucion As String) As DataTable
            Dim dt As New DataTable
            General.sql.AddParametro("@IdDevolucion", _IdDevolucion)
            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
            dt = General.sql.Ejecutar("Obtener_DetallesDevolucion43")
            Return dt
        End Function
        Public Function Obtener_ClaveDevolucionMH(_IdDevolucion As String) As String
            Dim ClaveMH As String = ""
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    ClaveMH = General.sql.Ejecutar("Select CLAVEDGT From " & General.BasedeDatos & ".dbo.devoluciones_ventas Where DEVOLUCION = " & _IdDevolucion, CommandType.Text).Rows(0).Item(0)
                Case TipoConexion.MYSQL

            End Select
            Return ClaveMH
        End Function
        Public Function Obtener_ConsecutivoDevolucionMH(_IdDevolucion As String) As String
            Dim ConsecutivoMH As String = ""
            Select Case General.TipoConexion
                Case TipoConexion.SQL
                    ConsecutivoMH = General.sql.Ejecutar("Select CONSECUTIVODGT From " & General.BasedeDatos & ".dbo.devoluciones_ventas Where DEVOLUCION = " & _IdDevolucion, CommandType.Text).Rows(0).Item(0)
                Case TipoConexion.MYSQL

            End Select
            Return ConsecutivoMH
        End Function
        Public Sub DevolucionAceptada(_IdDevolucion As String, _Clave As String, _Consecutivo As String)
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.devoluciones_ventas Set ENVIADODGT = 1, CLAVEDGT = '" & _Clave & "', CONSECUTIVODGT = '" & _Consecutivo & "', ESTADODGT = 'aceptado' WHERE DEVOLUCION = " & _IdDevolucion, CommandType.Text)
        End Sub
        Public Sub RegistrarEstadoDevolucion(_IdDevolucion As String, _Clave As String, _Consecutivo As String, _Estado As String)
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.devoluciones_ventas Set ESTADODGT = '" & _Estado & "', CLAVEDGT = '" & _Clave & "', CONSECUTIVODGT = '" & _Consecutivo & "' WHERE DEVOLUCION = " & _IdDevolucion, CommandType.Text)
        End Sub
        Public Sub CambiarEstadoDevolucion(_IdDevolucion As String, _Estado As String)
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.devoluciones_ventas Set ESTADODGT = '" & _Estado & "' WHERE DEVOLUCION = " & _IdDevolucion, CommandType.Text)
        End Sub
        Public Sub CambiarEstadoDevolucion_Aceptada(_IdDevolucion As String)
            General.sql.Ejecutar("Update " & General.BasedeDatos & ".dbo.devoluciones_ventas Set ENVIADODGT = 1, ESTADODGT = 'aceptado' WHERE DEVOLUCION = " & _IdDevolucion, CommandType.Text)
            Me.Correo.EnviarNotaCredito(_IdDevolucion)
        End Sub
        '**********************************************************************************************
        'Datos Compra
        '**********************************************************************************************
        Public Function GET_COMPRA_x_CLAVE(_Clave As String) As DataTable
            Return General.sql.Ejecutar("Select c.id_compra as ID, c.Clavedgt as CLAVE, p.cedula as NUMEROCEDULAEMISOR, '' as MENSAJE, C.Impuesto as MONTOTOTALIMPUESTO, c.TotalFactura as TOTALFACTURA, c.EstadoDGT as ESTADO, c.Consecutivodgt as CONSECUTIVO   from " & General.BasedeDatos & ".dbo.Compras c inner join " & General.BasedeDatos & ".dbo.proveedores p on c.codigoprov = p.codigoprov WHERE c.Clavedgt = '" & _Clave & "'", CommandType.Text)
        End Function

        Public Function Obtener_FacturaCompra43(_IdMensaje As String) As DataTable
            Dim dt As New DataTable
            General.sql.AddParametro("@IdCompra", _IdMensaje)
            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
            dt = General.sql.Ejecutar("Obtener_FacturaCompra43")
            Return dt
        End Function

        Public Function Obtener_DetalleFacturaCompra(_IdMensaje As String) As DataTable
            Dim dt As New DataTable
            General.sql.AddParametro("@IdCompra", _IdMensaje)
            General.sql.AddParametro("@BasedeDatos", General.BasedeDatos)
            dt = General.sql.Ejecutar("Obtener_DetalleFacturaCompra")
            Return dt
        End Function

        Public Function GET_COMPRA_x_ID(_IdMensaje As String) As DataTable
            Return General.sql.Ejecutar("Select c.id_compra as ID, c.Clavedgt as CLAVE, p.cedula as NUMEROCEDULAEMISOR, '' as MENSAJE, C.Impuesto as MONTOTOTALIMPUESTO, c.TotalFactura as TOTALFACTURA, c.EstadoDGT as ESTADO, c.Consecutivodgt as CONSECUTIVO   from " & General.BasedeDatos & ".dbo.Compras c inner join " & General.BasedeDatos & ".dbo.proveedores p on c.codigoprov = p.codigoprov WHERE c.id_compra = '" & _IdMensaje & "'", CommandType.Text)
        End Function

        Public Sub CompraAceptado(_IdMensaje As String, _Consecutivo As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.Compras SET ESTADODGT = 'procesando' WHERE id_compra = " & _IdMensaje, CommandType.Text)
        End Sub

        Public Sub RegistrarEstadoCompra(_IdMensaje As String, _Consecutivo As String, _Estado As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.Compras SET ESTADODGT = '" & _Estado & "' WHERE id_compra = " & _IdMensaje, CommandType.Text)
        End Sub

        Public Sub CambiarEstadoCompra(_IdMensaje As String, _Estado As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.Compras SET ESTADO = '" & _Estado & "' WHERE id_compra = " & _IdMensaje, CommandType.Text)
        End Sub

        Public Sub CambiarEstadoCompra_Aceptado(_IdMensaje As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.Compras SET ESTADO = 'aceptado' WHERE id_compra = " & _IdMensaje, CommandType.Text)
        End Sub
        '**********************************************************************************************
        'Datos Mensaje Receptor
        '**********************************************************************************************
        'Public Function GET_MENSAJE_x_CLAVE(_Clave As String) As DataTable
        '    Return General.sql.Ejecutar("SELECT ID, CLAVE, NUMEROCEDULAEMISOR, FECHAEMISIONDOC, MENSAJE, MONTOTOTALIMPUESTO, TOTALFACTURA, ESTADO, CONSECUTIVO FROM " & General.BasedeDatos & ".dbo.MENSAJEreceptor WHERE CLAVE = '" & _Clave & "'", CommandType.Text)
        'End Function

        'Public Function GET_MENSAJE_XML(_IdMensaje As String) As DataTable
        '    Return General.sql.Ejecutar("SELECT m.CLAVE AS Clave, m.NUMEROCEDULAEMISOR AS NumeroCedulaEmisor, REPLACE(CONVERT(VARCHAR(32), CONVERT(DATETIMEOFFSET(3),FECHAEMISIONDOC), 127),'Z','') AS FechaEmisionDoc, case m.MENSAJE when 'ACEPTADO' THEN 1 ELSE 2 END AS Mensaje, m.MONTOTOTALIMPUESTO AS MontoTotalImpuesto, m.TOTALFACTURA AS TotalFactura, e.Identificacion AS NumeroCedulaReceptor, (e.Sucursal + '00001'+ CASE MENSAJE WHEN 'ACEPTADO' THEN '05' WHEN 'RECHAZADO' THEN '07' END + REPLACE(STR(M.ID,10),SPACE(1),'0')) AS NumeroConsecutivoReceptor FROM " & General.BasedeDatos & ".dbo.mensajeRECEPTOR m, Seguridad.dbo.emisor e WHERE m.ID = " & _IdMensaje, CommandType.Text)
        'End Function

        Public Function GET_MENSAJE_XML43(_IdMensaje As String) As DataTable
            Return General.sql.Ejecutar("SELECT m.Estado, m.CLAVE AS Clave, m.NUMEROCEDULAEMISOR AS NumeroCedulaEmisor, REPLACE(CONVERT(VARCHAR(32), CONVERT(DATETIMEOFFSET(3),FECHAEMISIONDOC), 127),'Z','') AS FechaEmisionDoc, case m.MENSAJE when 'ACEPTADO' THEN 1 ELSE 2 END AS Mensaje, m.MONTOTOTALIMPUESTO AS MontoTotalImpuesto, m.TOTALFACTURA AS TotalFactura, e.Identificacion AS NumeroCedulaReceptor, (e.Sucursal + '00001'+ CASE MENSAJE WHEN 'ACEPTADO' THEN '05' WHEN 'RECHAZADO' THEN '07' END + REPLACE(STR(M.ID,10),SPACE(1),'0')) AS NumeroConsecutivoReceptor, m.DetalleMensaje, " & General.BasedeDatos & ".dbo.getcodigo(m.CodigoActividad) as CodigoActividad, " & General.BasedeDatos & ".dbo.getcodigo(m.CondicionImpuesto) as CondicionImpuesto, m.MontoTotalImpuestoAcreditar, m.MontoTotalDeGastoAplicable FROM " & General.BasedeDatos & ".dbo.mensajeRECEPTOR m, Seguridad.dbo.emisor e WHERE m.ID = " & _IdMensaje, CommandType.Text)
        End Function

        Public Function GET_MENSAJE_x_ID(_IdMensaje As String) As DataTable
            Return General.sql.Ejecutar("SELECT m.ID, m.CLAVE, m.NUMEROCEDULAEMISOR, m.FECHAEMISIONDOC, m.MENSAJE, m.MONTOTOTALIMPUESTO, m.TOTALFACTURA, m.ESTADO, CASE M.CONSECUTIVO WHEN '' THEN (e.Sucursal + '00001' + CASE MENSAJE WHEN 'ACEPTADO' THEN '05' WHEN 'RECHAZADO' THEN '07' END + REPLACE(STR(M.ID,10),SPACE(1),'0')) ELSE M.CONSECUTIVO END AS CONSECUTIVO FROM " & General.BasedeDatos & ".dbo.mensajeRECEPTOR m, Seguridad.dbo.emisor e WHERE m.ID = " & _IdMensaje, CommandType.Text)
        End Function

        Public Sub MensajeAceptado(_IdMensaje As String, _Consecutivo As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.MENSAJERECEPTOR SET CONSECUTIVO = '" & _Consecutivo & "', ESTADO = 'procesando' WHERE ID = " & _IdMensaje, CommandType.Text)
        End Sub

        Public Sub RegistrarEstadoMensaje(_IdMensaje As String, _Consecutivo As String, _Estado As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.MENSAJERECEPTOR SET ESTADO = '" & _Estado & "', CONSECUTIVO = '" & _Consecutivo & "' WHERE ID = " & _IdMensaje, CommandType.Text)
        End Sub

        Public Sub CambiarEstadoMensaje(_IdMensaje As String, _Estado As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.MENSAJERECEPTOR SET ESTADO = '" & _Estado & "' WHERE ID = " & _IdMensaje, CommandType.Text)
        End Sub

        Public Sub CambiarEstadoMensaje_Aceptado(_IdMensaje As String)
            General.sql.Ejecutar("UPDATE " & General.BasedeDatos & ".dbo.MENSAJERECEPTOR SET ESTADO = 'aceptado' WHERE ID = " & _IdMensaje, CommandType.Text)
        End Sub

    End Class

End Namespace
