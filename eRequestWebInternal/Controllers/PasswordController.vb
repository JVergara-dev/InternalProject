Imports System.Web.Mvc
Imports Support
Imports eRequestWebInternal.Models
Namespace Controllers
    Public Class PasswordController 
        Inherits Controller

        <Authorize>
        <OutputCache(Duration:=0)>
        Function MainPage() As ActionResult
            'm.First = False
            Return View()
        End Function
        <Authorize>
        <HttpPost>
        <ValidateAntiForgeryToken>
        <OutputCache(Duration:=0)>
        Function MainPage(m As PasswordModel) As ActionResult
            Dim oldData As PasswordModel = m
            If oldData.NewPassword Is Nothing Or oldData.NewPassword = "" Then ModelState.AddModelError("pw", "Invalid New Password")
            If oldData.OldPassword Is Nothing Or oldData.OldPassword = "" Then ModelState.AddModelError("pw", "Invalid Old Password")
            If oldData.ConfirmPassword Is Nothing Or oldData.ConfirmPassword = "" Then ModelState.AddModelError("pw", "Invalid Confirm Password")
            If oldData.NewPassword <> oldData.ConfirmPassword Then ModelState.AddModelError("pw", "Password didnt match")
            If oldData.OldPassword <> Parameters.Password Then ModelState.AddModelError("pw", "Old password is invalid")

            If ModelState.IsValid Then
                Dim Account As New Accounts
                Dim updated As Boolean = Account.UpdatePassword(Parameters.UserID, m.NewPassword)
                If updated Then
                    Return Redirect(Url.Action("Index", "RequestTable"))
                Else
                    Return View(m)
                End If
            End If
            Return View(m)
        End Function
    End Class
End Namespace