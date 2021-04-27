@If Request.IsAuthenticated Then
    Dim Group As String = Support.Helpers.GetSessionValue
    If Group = "error_trying_session" Then
        Response.Redirect(Url.Action("Index", "Login"))
    End If
    Dim jsserializer As New Script.Serialization.JavaScriptSerializer
    Dim authcookie As HttpCookie = Request.Cookies(".eRequestPSCCK")
    Dim account As Support.Accounts.Account = jsserializer.Deserialize(Of Support.Accounts.Account)(Support.Helpers.GetFormsAuthenticationCookie)

    'If Group = "ADMINISTRATOR" Then
        @*@<li Class="dropdown"><a href="@Url.Action("Index", "Home")" class="erequestlink"><span class="fa fa-home txt-color-red" style="font-size: 18px;"></span></a></li>
        @<li class="dropdown"><a href="@Url.Action("Index", "User")" class="erequestlink"><span class="fa fa-user txt-color-red"></span>&nbsp;&nbsp;User Maintenance&nbsp;&nbsp;</a></li>*@

    'End If
    @<li Class="dropdown"><a href="@Url.Action("Index", "RequestTable")" class="erequestlink"><span class="fa fa-home txt-color-red" style="font-size: 18px;"></span></a></li>
    @<li class="dropdown">
         <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
         <span class="glyphicon glyphicon-user txt-color-red"></span>&nbsp;&nbsp;Account&nbsp;&nbsp;
         <span class="caret"></span>
         </a>
         <ul class="dropdown-menu" role="menu">
             @*<li><a href="" class="erequestlink"><span class="glyphicon glyphicon-th-list txt-color-red"></span>&nbsp;&nbsp;View Processed</a></li>
             <li class="divider"></li>*@
             <li><a href="@Url.Action("MainPage", "Password")" class="erequestlink"><span class="glyphicon glyphicon-lock txt-color-red"></span>&nbsp;&nbsp;Change Password</a></li>
             <li class="divider"></li>

             <li><a href="#" id="logout" onclick="Logout(this)"><span Class="glyphicon glyphicon-log-out txt-color-red"></span>&nbsp;&nbsp;Logout</a></li>
         </ul>
    </li>

    @<script>
        function Logout(e) {
            $.SmartMessageBox({
                title: "<i class='fa fa-sign-out txt-color-red'></i>&nbsp;&nbsp;Logout",
                content: "Unsaved changes will be discarded, are you sure you want to logout?",
                buttons: '[Yes][No]'
            }, function (ButtonPressed) {
                if (ButtonPressed === "Yes") {
                    window.location.href = "@Url.Action("Logout", "Login")"
                }
            });
            e.preventDefault();
        }
    </script>
End If

