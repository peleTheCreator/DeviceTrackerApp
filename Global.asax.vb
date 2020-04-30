Imports System
Imports System.Web
Imports System.Web.Http

Namespace DeviceTracker
    Public Class [Global]
        Inherits HttpApplication

        Private Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            ' Code that runs on application startup
            GlobalConfiguration.Configure(New Action(Of HttpConfiguration)(AddressOf Register))
        End Sub
    End Class
End Namespace
