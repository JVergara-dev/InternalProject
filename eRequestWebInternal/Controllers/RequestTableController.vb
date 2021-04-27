Imports System.Web.Mvc
Imports System.Data.SqlClient
Imports Support
Imports eRequestWebInternal.Models
Namespace Controllers
    Public Class RequestTableController
        Inherits Controller

        Dim jsserializer As New Script.Serialization.JavaScriptSerializer
        <OutputCache(Duration:=0)>
        <Authorize>
        Function Index(Optional key As String = "", Optional PendingPI As Integer = 1, Optional ProcessPI As Integer = 1) As ActionResult
            If Not Request.IsAuthenticated Then Return Redirect(Url.Action("Index", "Login"))
            Dim authcookie As HttpCookie = Request.Cookies(".eRequestPSCCK")
            Dim accountCK As Accounts.Account = jsserializer.Deserialize(Of Accounts.Account)(Helpers.GetFormsAuthenticationCookie)

            Dim m As New RequestTableModel
            Dim RequestHeaders As New RequestHeaders
            m.PendingLIst = New List(Of RequestHeaders.RequestHeader)
            m.ProcessedList = New List(Of RequestHeaders.RequestHeader)

            m.CurrentPagePending = PendingPI
            m.CurrentPageProcess = ProcessPI

            'Dim con As New SqlConnection(Parameters.eRequestConnectionString)
            'con.Open()
            'Dim cmd As New SqlCommand("sp_SelectCountPSCQueuePendingByKeyword", con)
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@Key", key.Trim)
            'Dim NoPagesPending As Double = cmd.ExecuteScalar
            'm.NumberOfPagesPending = Math.Ceiling(NoPagesPending / m.PageSizePending)
            'cmd.Dispose()

            ''con.Open()
            'Dim cmd1 As New SqlCommand("sp_SelectCountPSCQueueProcessByKeyword", con)
            'cmd1.CommandType = CommandType.StoredProcedure
            'cmd1.Parameters.AddWithValue("@key", key.Trim)
            'Dim NoPagesProcess As Double = cmd1.ExecuteScalar
            'm.NumberOfPagesProcess = Math.Ceiling(NoPagesProcess / m.PageSizeProcess)
            'cmd1.Dispose()
            'con.Close()

            If m.CurrentPagePending < 0 Then
                m.CurrentPagePending = 1
            End If
            If m.CurrentPageProcess < 0 Then
                m.CurrentPageProcess = 1
            End If

            If m.CurrentPagePending < m.NumberOfPagesPending Then
                m.CurrentPagePending = PendingPI
            End If

            If m.CurrentPageProcess < m.CurrentPageProcess Then
                m.CurrentPageProcess = ProcessPI
            End If

            If m.CurrentPagePending > m.NumberOfPagesPending Then
                m.CurrentPagePending = m.NumberOfPagesPending
            End If

            If m.CurrentPageProcess > m.NumberOfPagesProcess Then
                m.CurrentPageProcess = m.NumberOfPagesProcess
            End If

            m.Keyword = key
            m.PendingLIst = RequestHeaders.SelectRequestHeaderTable(key, m.CurrentPagePending * m.PageSizePending)
            m.CurrentPagePending = m.CurrentPagePending

            m.ProcessedList = RequestHeaders.SelectRequestHeaderTableProcessed(key, m.CurrentPageProcess * m.PageSizeProcess)
            m.CurrentPageProcess = m.CurrentPageProcess

            Return View(m)
        End Function
    End Class
End Namespace