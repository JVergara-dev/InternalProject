Imports System.Web.Mvc
Imports System.Web.Routing
Imports System.Web.Optimization
Imports Support.Parameters
Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        RegisterBundles(BundleTable.Bundles)
        eRequestConnectionString = ConfigurationManager.ConnectionStrings("eRequest").ConnectionString
        'eRequestConnectionString = ConfigurationManager.ConnectionStrings("eRequest_Beta").ConnectionString
        eRequestPRMS = ConfigurationManager.ConnectionStrings("eRequestPRMS").ConnectionString
        Try
            'ServerCode = ConfigurationManager.AppSettings("Server")
            ServerCode = "0001"
        Catch ex As Exception
            ServerCode = "0001"
        End Try
    End Sub
End Class
