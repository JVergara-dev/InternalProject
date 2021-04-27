Imports System.Web.Mvc
Imports eRequestWebInternal.Models
Imports Support
Namespace Controllers
    Public Class LoginController
        Inherits Controller

        <OutputCache(Duration:=0)>
        <AllowAnonymous>
        Function Index(returnUrl As String) As ActionResult
            Response.Cookies.Clear()
            If Request.IsAuthenticated Then Return Redirect(Url.Action("Index", "RequestTable"))
            If returnUrl IsNot Nothing Then TempData("returnurl") = returnUrl
            Dim lm As New LoginModel
            If TempData("LoginData") Is Nothing Then
                lm = New LoginModel
                lm.ReturnUrl = returnUrl
            Else
                lm = TempData("LoginData")
            End If
            Return View(lm)
        End Function

        <OutputCache(Duration:=0)>
        <AllowAnonymous>
        Function LoginUrl() As ActionResult
            Return View()
        End Function

        <OutputCache(Duration:=0)>
        <AllowAnonymous>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Index(lm As LoginModel) As ActionResult
            If Request.IsAuthenticated Then

            End If
            Dim returnurl As String = String.Empty
            If TempData("returnurl") IsNot Nothing Then
                returnurl = TempData("returnurl")
                If returnurl.ToLower.Contains("logout") Then returnurl = "Home"
            Else
                returnurl = lm.ReturnUrl
            End If

            If Not ModelState.IsValid Then
                ModelState.AddModelError("", "Username and Password is required.")
                lm.ReturnUrl = returnurl
                Return View(lm)
            End If

            lm.Username = lm.Username.Trim.ToUpper

            Dim Accounts As New Accounts
            Dim Account As Accounts.Account = Accounts.SelectUserByUsername(lm.Username)

            If Account.UserName Is Nothing Then
                ModelState.AddModelError("", "Username does not exist.")
                lm.ReturnUrl = returnurl
                Return View(lm)
            End If
            Dim s As String = PRMs.GetMD5Hash(lm.Password)

            If PRMs.GetMD5Hash(lm.Password) <> Account.Password.ToLower Then
                ModelState.AddModelError("", "Password is incorrect.")
                lm.ReturnUrl = returnurl
                Return View(lm)
            Else
                Dim serializer As New Script.Serialization.JavaScriptSerializer
                Dim data As String = serializer.Serialize(Account)
                Dim ticket As FormsAuthenticationTicket
                ticket = New FormsAuthenticationTicket(1, lm.Username, Now, Now.AddMinutes(2), True, data, FormsAuthentication.FormsCookiePath)
                Dim encryptedticket As String = FormsAuthentication.Encrypt(ticket)
                Dim ticketcookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedticket)
                Dim x As Accounts.Account = serializer.Deserialize(Of Accounts.Account)(data)
                Response.Cookies.Add(ticketcookie)
                Parameters.UserID = Account.ID
                Parameters.UserName = Account.Name
                Parameters.UsedUsername = Account.UserName
                Parameters.Password = lm.Password
                Return Redirect(Url.Action("Index", "RequestTable"))
            End If
            Return RedirectToAction("Index")
        End Function
        <OutputCache(Duration:=0)>
        Function Logout(returnUrl As String) As ActionResult
            Try
                If returnUrl = String.Empty Then
                    Session.Abandon()
                End If
                Session.Clear()

                FormsAuthentication.SignOut()
                Return Redirect(Url.Action("Index", "Login", New With {.returnUrl = returnUrl}))
            Catch ex As Exception
                Return Redirect(Url.Action("Index", "Login", New With {.returnUrl = returnUrl}))
            End Try
        End Function

        Function Pseudoupdate() As ActionResult

            If Request.IsAuthenticated Then
                Return Content("ok")
            Else
                Return Nothing
            End If

        End Function
    End Class
End Namespace