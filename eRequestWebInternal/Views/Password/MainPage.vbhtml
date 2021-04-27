@ModelType Models.PasswordModel
@Code
    ViewData("Title") = "MainPage"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row">
    <div class="col-md-offset-3 col-md-6">
        <div Class="well no-padding">
            @Using Html.BeginForm("MainPage", "Password", FormMethod.Post, New With {.class = "smart-form client-form"})
                @Html.AntiForgeryToken
                @<header><span class="fa fa-lock txt-color-red"></span>&nbsp;&nbsp;Change Password</header>
                @if not ViewData.ModelState.IsValid Then
                    @<div class="alert alert-danger alert-block">
                        @Html.Raw(HttpUtility.HtmlDecode(Html.ValidationMessage("pw").ToHtmlString))
                    </div>
                End If
                @<fieldset>
                    @*@If Model.First Then
                        @<section>
                            <h1 Class="txt-color-red">Attention</h1>
                            <p> We detected that your transaction password has not been set. You can set your transaction password later but we highly recommend for it to be set now.</p>
                        </section>
                        @Html.HiddenFor(Function(m) m.OldPassword)
                    End If*@
                    @*<div class="alert alert-block alert-warning">
                        <h4 class="alert-heading">Why do I need this?</h4>
                        Transaction password protects you from fraudulent transactions. Before finalizing your requests, the system will require you to input this password. You can also leave this blank though but we highly recommend you set this password for your security.
                    </div>*@
                    <section>
                        <Label Class="label">New Password</Label>
                        <Label Class="input">
                            @Html.PasswordFor(Function(m) m.NewPassword, New With {.id = "new", .maxlength = "20", .onkeyup = "CN(); KU();"})
                            <b Class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-red"></i>&nbsp;&nbsp;Please input new password.</b>
                            <span class="note"><strong>8-20 characters</strong></span>
                        </Label>
                    </section>
                    <section>
                        <Label Class="label">Confirm Password</Label>
                        <Label Class="input">
                            @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.id = "confirm", .maxlength = "20", .onkeyup = "KU();"})
                            <b Class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-red"></i>&nbsp;&nbsp;Please reinput new password.</b>
                        </Label>
                    </section>
                    @*@If Not Model.First Then*@
                        <section>
                            <Label Class="label">Old Password</Label>
                            <Label Class="input">
                                @Html.PasswordFor(Function(m) m.OldPassword, New With {.maxlength = "20"})
                                <b Class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-red"></i>&nbsp;&nbsp;Please input old password.</b>
                            </Label>
                        </section>
                    @*End If*@
                </fieldset>
                @<footer>
                    <Button type="submit" Class="btn btn-danger"><span class="glyphicon glyphicon-floppy-disk"></span>&nbsp;&nbsp;Save</Button>
                </footer>
            End Using
        </div>
    </div>
    <div Class="col-md-6">
    </div>
</div>

@*@If Model.First Then*@
    @*<div id="firstmodal" Class="modal fade" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Transaction Password</h4>
                </div>
                <div class="modal-body">
                    <h4 class="txt-color-red">Attention</h4>
                    <br />
                    <p>We detected that your TRANSACTION PASSWORD has not been set. You can set this password later but we highly recommend that you set this now. Please leave the OLD PASSWORD field blank.</p>
                    <p>This warning will show up for every login until you set a password.</p>
                    <div class="alert alert-block alert-warning">
                        <h4 class="alert-heading">Why do I need this?</h4>
                        Transaction password protects you from fraudulent transactions. Before finalizing your requests, the system will require you to input this password. You can also leave this blank though but we highly recommend you set this password for your security.
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>*@
@*End If*@

