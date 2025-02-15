﻿Imports System.Web.Http

Namespace DeviceTracker
    Public Module WebApiConfig
        Public Sub Register(ByVal config As HttpConfiguration)
            ' Web API configuration and services

            ' Web API routes
            config.MapHttpAttributeRoutes()
            config.Routes.MapHttpRoute(name:="DefaultApi", routeTemplate:="api/{controller}/{id}", defaults:=New With {
                .id = RouteParameter.Optional
            })
        End Sub
    End Module
End Namespace
