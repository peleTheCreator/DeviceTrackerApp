Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Script.Serialization

Namespace DeviceTracker.Controllers
    Public Class DeviceTrackerController
        Inherits ApiController

        <Route("api/withinRegion")>
        <HttpPost>
        Public Function withinRegion(ByVal request As HttpRequestMessage) As Object
            Dim body As String = request.Content.ReadAsStringAsync().Result
            Dim AdroidDeviceLog = New AndriodDevice.AndroidDeviceLog()
            Dim json1 As String
            json1 = AdroidDeviceLog.UpdateDeviceTrackingregion(body)
            Dim JavaScriptSerializer = New JavaScriptSerializer()
            Dim Json = JavaScriptSerializer.Deserialize(json1, GetType(Object))
            Return Json
        End Function

        <Route("api/UploadData")>
        <HttpPost>
        Public Function UploadData(ByVal request As HttpRequestMessage) As Object
            Dim body As String = request.Content.ReadAsStringAsync().Result
            Dim AdroidDeviceLog = New AndriodDevice.AndroidDeviceLog()
            Dim json1 As String
            json1 = AdroidDeviceLog.UploadDeviceLocation(body)
            Dim JavaScriptSerializer = New JavaScriptSerializer()
            Dim Json = JavaScriptSerializer.Deserialize(json1, GetType(Object))
            Return Json
        End Function
    End Class
End Namespace
