Imports System.Net.Http
Imports System.Net

Public Class TokenHacienda

    Public Property accessToken As String
    Public Property refreshToken As String
    Public Property AccesoConcedido As Boolean

    Public Sub GetTokenHacienda(usuario As String, password As String)
        Try
            Me.AccesoConcedido = False
            Me.accessToken = ""
            Me.refreshToken = ""

            Dim IDP_URI = ""
            Dim IDP_CLIENT_ID = ""

            If usuario.Contains("stag") Then
                IDP_CLIENT_ID = "api-stag"
                IDP_URI = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token"
            Else
                IDP_CLIENT_ID = "api-prod"
                IDP_URI = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut/protocol/openid-connect/token"
            End If

            Dim http As HttpClient = New HttpClient

            '*********************************************************************************
            'TLS 1.2 Cambio de seguridad por Hacienda
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 'Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
            '*********************************************************************************

            Dim param = New List(Of KeyValuePair(Of String, String))()
            param.Add(New KeyValuePair(Of String, String)("client_id", IDP_CLIENT_ID))
            param.Add(New KeyValuePair(Of String, String)("grant_type", "password"))
            param.Add(New KeyValuePair(Of String, String)("username", usuario))
            param.Add(New KeyValuePair(Of String, String)("password", password))
            Dim content As FormUrlEncodedContent = New FormUrlEncodedContent(param)

            Dim response As HttpResponseMessage = http.PostAsync(IDP_URI, content).Result
            Dim res As String = response.Content.ReadAsStringAsync.Result
            Dim tk As Token = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Token)(res)

            If response.StatusCode = Net.HttpStatusCode.OK Then
                Me.AccesoConcedido = True
                accessToken = tk.access_token
                refreshToken = tk.refresh_token
            Else
                Me.AccesoConcedido = False
                Me.accessToken = ""
                Me.refreshToken = ""
            End If

        Catch e As Exception
            Me.AccesoConcedido = False
            Me.accessToken = ""
            Me.refreshToken = ""
        End Try
    End Sub

End Class
