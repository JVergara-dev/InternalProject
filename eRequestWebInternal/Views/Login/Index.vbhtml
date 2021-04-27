@ModelType Models.LoginModel
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<style>
    html {
        background: url('/eRequestPSC_Beta/Content/img/jumbo3.png');
        /*background: url('../../Content/img/jumbo3.png');*/
        background-size: cover;
        background-position: center;
    }

    body {
        background: none;
    }

    .jumbotron {
        background: rgba(255,255,255,0.25);
    }
</style>
<div class="row">
    <div class="col-md-8 hidden-sm hidden-xs">
        <div class="jumbotron">
            <h1><strong>ABC eRequest PSC</strong></h1>
            <p>
               This is the login page for phlebotomist.
            </p>
        </div>
    </div>
    <div class="col-md-4 col-sm-6 col-sm-offset-3 col-md-offset-0 col-lg-offset-0">
        <div class="well no-padding">
            @Using Html.BeginForm("Index", "Login", FormMethod.Post, New With {.class = "smart-form client-form"})
                @Html.AntiForgeryToken
                @Html.HiddenFor(Function(m) m.ReturnUrl)
                @<header>PSC Login</header>
                @if not ViewData.ModelState.IsValid Then
                    @<div class="alert alert-danger alert-block">
                        @Html.ValidationMessage("")
                    </div>
                End If
                @<fieldset>
                    <section>
                        <label class="label">Username</label>
                        <label class="input">
                            <i class="icon-append fa fa-user"></i>
                            @Html.TextBoxFor(Function(m) m.Username, New With {.maxlength = "16", .placeholder = "Username"})
                            <b class="tooltip tooltip-top-right"><i class="fa fa-user txt-color-red"></i>&nbsp;&nbsp;Please input Username/Card Number.</b>
                        </label>
                    </section>
                    <section>
                        <Label Class="label">Password</Label>
                        <Label Class="input">
                            <i Class="icon-append fa fa-lock"></i>
                            @Html.PasswordFor(Function(m) m.Password, New With {.maxlength = "20", .placeholder = "Password"})
                            <b Class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-red"></i>&nbsp;&nbsp;Please input Password.</b>
                        </Label>
                    </section>
                </fieldset>

                @<footer>
                    <Button type="submit" Class="btn btn btn-default"><span class="glyphicon glyphicon-log-in"></span>&nbsp;&nbsp;Login</Button>
                </footer>
            End Using
        </div>
    </div>
</div>

