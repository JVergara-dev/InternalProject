<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>eRequest</title>
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    @Styles.Render("~/Content/smartadmin")
    @*<link rel="shortcut icon" href="~/content/img/favicon/favicon.ico" type="image/x-icon">*@
    @*<link rel="icon" href="~/content/img/favicon/favicon.ico" type="image/x-icon">*@
    <link rel="icon" href="~/Content/img/favicon_x.png" type="image/png">
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">
    <link rel="apple-touch-icon" href="~/content/img/splash/sptouch-icon-iphone.png">
    <link rel="apple-touch-icon" sizes="76x76" href="~/content/img/splash/touch-icon-ipad.png">
    <link rel="apple-touch-icon" sizes="120x120" href="~/content/img/splash/touch-icon-iphone-retina.png">
    <link rel="apple-touch-icon" sizes="152x152" href="~/content/img/splash/touch-icon-ipad-retina.png">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <link rel="apple-touch-startup-image" href="~/content/img/splash/ipad-.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)">
    <link rel="apple-touch-startup-image" href="~/content/img/splash/ipad-portrlandscapeait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)">
    <link rel="apple-touch-startup-image" href="~/content/img/splash/iphone.png" media="screen and (max-device-width: 320px)">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="~/Scripts/jquery.mask.js" type="text/javascript" charset="UTF-8"></script>
    <script> if (!window.jQuery) { document.write('<script src="/scripts/libs/jquery-2.1.1.min.js"><\/script>'); } </script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
    <script> if (!window.jQuery.ui) { document.write('<script src="/scripts/libs/jquery-ui-1.10.3.min.js"><\/script>'); } </script>
    <script>if (!window.jQuery) { document.write('<script src="/Scripts/plugins/jquery.maskedinput.min.js"><\/script>'); }</script>
    <script src="~/Scripts/DYMO.Label.Framework.2.0.2.js"></script>
    <script src="~/Scripts/DYMO.Label.Framework.latest.js" type="text/javascript" charset="UTF-8"></script>
    
    @Scripts.Render("~/scripts/smartadmin")
    @Scripts.Render("~/scripts/smart-chat")
    @Scripts.Render("~/scripts/datatables")
    @Scripts.Render("~/scripts/jq-grid")
    @Scripts.Render("~/scripts/forms")
    @Scripts.Render("~/scripts/charts")
    @Scripts.Render("~/scripts/vector-map")
    @Scripts.Render("~/scripts/full-calendar")
    @RenderSection("scripts", required:=False)
    <style>
        input, textarea {
            text-transform: uppercase;
        }

        .body-content {
            padding-top: 60px;
        }

        input[type="text"]:disabled {
            border-color: #ffc9c9;
            background: #ffc9c9;
        }

        .SmallBox a {
            color: #ccc;
            font-weight: bold;
            transition: all 0.2s;
        }

            .SmallBox a:hover {
                color: #fff;
            }

        .jarviswidget .btn {
            margin-top: 5px;
            margin-bottom: 8px;
        }

        table {
            cursor: pointer;
        }

        .table-responsive {
            margin-bottom: 5px !important;
        }

        .widget-body {
            padding-bottom: 5px !important;
        }

        .pagination {
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .page-footer {
            background: #eee;
            border-top: 1px #ccc solid;
        }

        .loading {
            width: 100%;
            height: 100%;
            position: fixed;
            background: rgba(0, 0, 0, 0.25);
            z-index: 110000 !important;
            display: block;
            opacity: 1;
        }

        .spinnerwrapper {
            margin: auto;
            width: 50px;
            height: 50px;
            position: relative;
            top: 50%;
            transform: translateY(-50%);
            text-align: center;
        }

        .spinner {
            border-radius: 50%;
            border: 5px #55606E solid;
            border-top: 5px #0091d9 solid;
            /*border-top: 5px #a90329 solid;*/
            width: 50px;
            height: 50px;
            animation: spin 1s linear infinite;
        }

        @@keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        @@media print {
            body {
                margin-top: -50px;
            }

            .no-print, .no-print * {
                display: none !important;
            }

            .has-print {
                display: block !important;
            }

            .pre-scrollable {
                overflow: visible !important;
                max-height: 100% !important;
            }
        }

        .bootstrapWizard li .step {
            padding: 4px 10px !important;
            border-radius: 50% !important;
        }

        #ad {
            position: fixed;
            z-index: 2;
            bottom: 0;
            left: 0;
            height: 50px;
            width: 100%;
            background: #57889c;
            background: url(~/content/img/mybg.png) #fff;
        }

        #hide {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 3;
            font-size: 10px;
            bottom: 0;
        }

        marquee {
            font-size: 15px;
            font-weight: bold;
            line-height: 50px;
        }

        .blink_text {
            animation: 1s blinker linear infinite;
            -webkit-animation: 1s blinker linear infinite;
            -moz-animation: 1s blinker linear infinite;
        }

        @@-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.5;
            }

            100% {
                opacity: 1.0;
            }
        }

        @@-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.5;
            }

            100% {
                opacity: 1.0;
            }
        }

        @@keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.5;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>
    <script>
        $(function () {
            $("form").submit(function () {
                $(".loading").css("display", "block");
                $(".loading").animate({ opacity: 1 }, 500);
            });
            $(".erequest").click(function () {
                $(".loading").css("display", "block");
                $(".loading").animate({ opacity: 1 }, 500);
            });
        });
    </script>
</head>
<body>
    <div class="loading">
        <div class="spinnerwrapper">
            <div class="spinner"></div>
            <p class="label">Loading</p>
        </div>
    </div>
    <div Class="navbar navbar-default navbar-fixed-top">
        <div Class="container-fluid">
            <div Class="navbar-header">
                <Button type="button" Class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span Class="icon-bar"></span>
                    <span Class="icon-bar"></span>
                    <span Class="icon-bar"></span>
                </Button>
                <a href="@Url.Action("Index", "RequestTable")" Class="navbar-brand"><img style="margin-top: -8px; height: 36px;" src="~/Content/img/logo.png" /></a>
                @*<span class="navbar-brand hidden-sm hidden-xs" style="font-size: 13px; font-weight: bold;">New online requisition system. Order your laboratory test whenever, wherever.</span>*@
                @*@If Request.IsAuthenticated Then
                        Dim jsserializer As New Script.Serialization.JavaScriptSerializer
                        Dim account As HDB.Support.ACTs.ACT = jsserializer.Deserialize(Of HDB.Support.ACTs.ACT)(eRequest_Support.HELPERS.GetFormsAuthenticationCookie)
                        @<span class="note">
                            @If account.CompanyCode Is Nothing Then
                                @eRequest_Support.HELPERS.LocalizeDateTime(Now, account.CompanyCode).ToString("dddd, MMMM d, yyyy h:mm tt - LOS ANGELES, CALIFORNIA, U.S.A.")
                            Else
                                If account.CompanyCode = "0001" Then
                                    @eRequest_Support.HELPERS.LocalizeDateTime(Now, account.CompanyCode).ToString("dddd, MMMM d, yyyy h:mm tt - LOS ANGELES, CALIFORNIA, U.S.A.")
                                ElseIf account.CompanyCode = "0002" Then
                                    @eRequest_Support.HELPERS.LocalizeDateTime(Now, account.CompanyCode).ToString("dddd, MMMM d, yyyy h:mm tt - JAKARTA, INDONESIA")
                                ElseIf account.CompanyCode = "0002" Then
                                    @eRequest_Support.HELPERS.LocalizeDateTime(Now, account.CompanyCode).ToString("dddd, MMMM d, yyyy h:mm tt - MANILA, PHILIPPINES")
                                End If
                            End If
                        </span>
                    End If*@
            </div>
            <div Class="navbar-collapse collapse">
                <ul Class="nav navbar-nav navbar-right">
                    @Html.Partial("_Navigation")
                </ul>
            </div>
        </div>
    </div>
    <div class="container-fluid body-content">
        @*<div class="container">*@
        @RenderBody()
        @*</div>*@
    </div>
    <br />
    <br />
    <br />
    <div class="page-footer no-print" style="padding-left: 15px !important;">
        <div class="row">
            @*<div class="col-xs-12 hidden-sm hidden-md hidden-lg text-center">Copyright &copy; @Now.Year <a href="https://www.hcdbi.com" target="_blank"><img src="/content/img/HDI_Logo_small3-1-e1455154196454.png" style="width: 50px;"></a> All rights reserved.</div>
                <div class="col-sm-8 hidden-xs">Copyright &copy; @Now.Year <a href="https://www.hcdbi.com" target="_blank">Healthcare Databank Inc.</a> All rights reserved.</div>
                <div class="col-sm-4 text-right hidden-xs">Partnered with <img src="/content/img/HDI_Logo_small3-1-e1455154196454.png" style="width: 50px;"></div>*@
            @*<div class="col-xs-12 hidden-sm hidden-md hidden-lg text-center">Copyright &copy; @Now.Year <a href="https://www.hcdbi.com" target="_blank"><img src="/erequest_beta/content/img/HDI_Logo_small3-1-e1455154196454.png" style="width: 50px;"></a> All rights reserved.</div>
                <div class="col-sm-8 hidden-xs">Copyright &copy; @Now.Year <a href="https://www.hcdbi.com" target="_blank">Healthcare Databank Inc.</a> All rights reserved.</div>
                <div class="col-sm-4 text-right hidden-xs">Partnered with <img src="/erequest_beta/content/img/HDI_Logo_small3-1-e1455154196454.png" style="width: 50px;"></div>*@
            @*<div class="col-xs-12 hidden-sm hidden-md hidden-lg text-center">Copyright &copy; @Now.Year <a href="https://www.hcdbi.com" target="_blank"><img src="/erequest/content/img/HDI_Logo_small3-1-e1455154196454.png" style="width: 50px;"></a> All rights reserved.</div>
                <div class="col-sm-8 hidden-xs">Copyright &copy; @Now.Year <a href="https://www.hcdbi.com" target="_blank">Healthcare Databank Inc.</a> All rights reserved.</div>*@
            <div class="col-sm-8"></div>
            <div class="col-sm-4 text-right hidden-xs"><span class="note">Version 1.0</span> Partnered with <img src="/eRequestPSC_Beta/content/img/HDI_Logo_small3-1-e1455154196454.png" style="width: 50px;"></div>
        </div>
    </div>
</body>
</html>

<script>
        var load = true;
        var r;
        $(function () {
            $(".loading").css("display", "none");
            $(".loading").css("opacity", "0");

            $('html').on('mousemove keypress', function () {
                if (load) {
                    load = false;
                    var t = setTimeout("load = true;", 10000);
                    //alert([location.protocol, '//', location.host, location.pathname].join('').toLowerCase().indexOf("login"));
                    if ([location.protocol, '//', location.host, location.pathname].join('').toLowerCase().indexOf("login") == -1) {
                        clearTimeout(r);
                        //alert('inside');
                        //r = setTimeout("$('.loading').css('display', 'block');$('.loading').animate({ opacity: 1 }, 500);alert('Account will be automatically logged out due to inactivity.');location.href='/account/logout?returnurl=" + location.href + "';", 110000);
                        //r = setTimeout("ForceLogout();", 100000);
                        $.ajax({
                            url: "@Url.Action("Pseudoupdate", "Login")",
                            success: function (data) {
                                if (data != "ok") {
                                    r = setTimeout("ForceLogout();", 100);
                                }
                            }
                    });
                }
            }
            });
        });
    function ForceLogout() {
        $.SmartMessageBox({
            title: "<i class='fa fa-refresh txt-color-yellow'></i>&nbsp;&nbsp;Inactivity Detected",
            content: "Your account will be automatically logged out due to inactivity.",
            buttons: "[OK]"
        },
        function (ButtonPressed) {
            if (ButtonPressed === "OK") {
                $(".loading").css("display", "block");
                $(".loading").animate({ opacity: 1 }, 500);
                location.href = "@Url.Action("logout", "Login")";
            }
        });
    }
</script>