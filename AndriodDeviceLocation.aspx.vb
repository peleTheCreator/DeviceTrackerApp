Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

Namespace DeviceTracker
    Public Partial Class AndriodDeviceLocation
        Inherits Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not IsPostBack Then
                Dim deviceId As Long = Convert.ToInt64(Request.QueryString("deviceId"))
                su_LoadDeviceLocation(deviceId)
            End If
        End Sub

        Private Sub su_LoadDeviceLocation(ByVal deviceId As Long)
            Dim strSQL = String.Empty
            Dim dbConnect = New DataBase()

            If dbConnect.StatoConnessione = 0 Then
                dbConnect.connettidb()
            End If

            Dim objCommand = New SqlCommand()
            strSQL = $"SELECT TOP 5 [LONGITUDE], [LATITUDE], [CREATED_AT] 
                       FROM [Android].[DEVICE_LOCATION]
                       WHERE[ID_ANDROID_DEVICE] = {deviceId} ORDER BY CREATED_AT DESC "
            objCommand.CommandText = strSQL
            objCommand.CommandType = CommandType.Text
            objCommand.Connection = dbConnect.Connessione
            Dim reader As SqlDataReader = objCommand.ExecuteReader()
            'if reader is null, display location does not exit
            rptMarkers.DataSource = reader
            rptMarkers.DataBind()
        End Sub
    End Class
End Namespace
