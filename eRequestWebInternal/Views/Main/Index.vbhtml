@ModelType Models.MainModel
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim haserror As Boolean = False
    Dim errormessage As String = String.Empty

    For Each vmsv In ViewData.ModelState.Values
        If vmsv.Errors.Count > 0 Then
            haserror = True
            Exit For
        End If
    Next
    If haserror Then
        errormessage = "SOME FIELDS ARE INVALID"
    End If

    @<a href="javascript:void(0);" onclick="$('#mainform').submit();" id="submitter" class="btn btn-lg btn-circle btn-danger" style="right: 17px;"><i class="fa fa-chevron-right"></i></a>
End Code

<style>
    #submitter, #saver, #discarder {
        position: fixed;
        z-index: 100000 !important;
        bottom: 17px;
    }

    #saver {
        bottom: 44px;
    }

    #submitter:active {
        top: auto !important;
        left: auto !important;
    }

    #saver:active, #discarder:active {
        top: auto !important;
        left: 17px;
    }

    .table-noborder {
        border-bottom: 0px !important;
    }

        .table-noborder td {
            border: 1px !important;
        }

    /*.ui-menu-item a {
        max-width: 400px;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
    }*/

    .ui-autocomplete {
        width: auto;
        z-index: 10000001 !important;
    }

    .haserror {
        border-color: #A90329;
        background: #fff0f0;
    }
</style>

@Using Html.BeginForm("Index", "Main", FormMethod.Post, New With {.id = "mainform"})
    @<div class="jarviswidget jarviswidget-color-red">
        <header>
            <span class="widget-icon"><i class="fa fa-file"></i></span>
            <h2><strong>&nbsp;&nbsp;REQUEST INFORMATION</strong></h2>
        </header>
        <div class="widget-body row">
            @If haserror Then
                @<div class="col-md-12 no-padding">
                    <div class="alert alert-danger fade in">
                        <strong>@Html.Raw(errormessage)</strong>
                    </div>
                </div>
            End If
            <div class="col-md-12 no-padding">
                <div class="alert alert-info fade in">
                    <strong>CLICK&nbsp;&nbsp;( <i class="fa fa-chevron-right"></i> )&nbsp;&nbsp;BUTTON BELOW TO SUBMIT</strong>
                </div>
            </div>
            <div class="col-xs-12 no-padding">
                <table class="table table-condensed">
                    <thead>
                        <tr>
                            <th>
                                <h6><strong>PATIENT INFORMATION</strong></h6>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-6">
                                        <div class="row">
                                            <section class="col-xs-5">
                                                <span class="note"><strong>LAST NAME</strong></span>
                                                @Html.TextBoxFor(Function(m) m.RequestPLName, New With {.class = "form-control autoc", .maxlength = "50", .autocomplete = "off"})
                                            </section>
                                            <section class="col-xs-5">
                                                <span class="note"><strong>FIRST NAME</strong></span>
                                                @Html.TextBoxFor(Function(m) m.RequestPFName, New With {.class = "form-control", .maxlength = "50"})
                                            </section>
                                            <section class="col-xs-2">
                                                <span class="note"><strong>MI</strong></span>
                                                @Html.TextBoxFor(Function(m) m.RequestPMName, New With {.class = "form-control", .maxlength = "1"})
                                            </section>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-6">
                                        <div class="row">
                                            <section class="col-xs-5">
                                                <span class="note"><strong>ALSO KNOWN AS (AKA)</strong></span>
                                                @Html.TextBoxFor(Function(m) m.MRN, New With {.class = "form-control", .maxlength = "50"})
                                            </section>
                                            <section class="col-xs-4">
                                                <span class="note"><strong>DATE OF BIRTH</strong></span>
                                                @If Not Model.RequestPDOB.HasValue OrElse Model.RequestPDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                                    @Html.TextBoxFor(Function(m) m.RequestPDOB, New With {.class = "form-control", .type = "date", .maxlength = "10"})
                                                Else
                                                    @Html.TextBoxFor(Function(m) m.RequestPDOB, "{0:yyyy-MM-dd}", New With {.class = "form-control", .type = "date", .maxlength = "10"})
                                                End If
                                            </section>
                                            <section class="col-xs-3">
                                                <span class="note"><strong>GENDER</strong></span>
                                                @Html.DropDownListFor(Function(m) m.RequestPGender, Model.GenderList, New With {.class = "form-control"})
                                            </section>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-6">
                                        <div class="row">
                                            <section class="col-xs-12">
                                                <span class="note"><strong>STREET ADDRESS</strong></span>
                                                @Html.TextBoxFor(Function(m) m.RequestPAddress, New With {.class = "form-control", .maxlength = "100"})
                                            </section>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-6">
                                        <div class="row">
                                            <section class="col-xs-3">
                                                <span class="note"><strong>CITY</strong></span>
                                                @Html.TextBoxFor(Function(m) m.City, New With {.class = "form-control autoc", .maxlength = "20"})
                                            </section>
                                            <section class="col-xs-3">
                                                <span class="note"><strong>STATE</strong></span>
                                                @Html.TextBoxFor(Function(m) m.State, New With {.class = "form-control autoc", .maxlength = "20"})
                                            </section>
                                            <section class="col-xs-3">
                                                <span class="note"><strong>ZIP CODE</strong></span>
                                                @Html.TextBoxFor(Function(m) m.ZipCode, New With {.class = "form-control", .maxlength = "20"})
                                            </section>
                                            <section class="col-xs-3">
                                                <span class="note"><strong>PHONE #</strong></span>
                                                @Html.TextBoxFor(Function(m) m.RequestPContact, New With {.class = "form-control", .maxlength = "12"})
                                            </section>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-xs-12" style="margin-top: 10px !important;">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 no-padding">
                        <table class="table table-condensed">
                            <thead>
                                <tr>
                                    <th>
                                        <h6><strong>PRIMARY BILL TYPE</strong></h6>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="row">
                                           <div class="col-md-12">
                                               <div class="row" >
                                                   <center><legend style="font-weight:bold;">BILLING INFORMATION</legend></center>
                                                   <div class="col-xs-12">
                                                       <span class="note"><strong>BILL TO</strong></span>
                                                       @Html.DropDownListFor(Function(m) m.RequestType, Model.PBilltype, New With {.Class = "form-control"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>MEDICARE #</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.Medicare, New With {.class = "form-control", .maxlength = "11"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>MEDICAID #</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.MedicaID, New With {.class = "form-control", .maxlength = "14"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>INSURANCE COMPANY</strong></span>
                                                       @Html.DropDownListFor(Function(m) m.InsuranceCode, Model.InsuranceCompanyList, New With {.class = "form-control"})
                                                       @Html.DropDownListFor(Function(m) m.PMedicaidInsIndex, Model.PMedicaidIns, New With {.class = "form-control"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>HMO</strong></span>
                                                       @Html.DropDownListFor(Function(m) m.HMOCode, Model.HMOList, New With {.class = "form-control"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>GROUP #</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.GroupNo, New With {.class = "form-control", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>INSURED ID</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuredID, New With {.class = "form-control", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>SSN #</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.SSN, New With {.class = "form-control", .maxlength = "9"})<br />
                                                   </div>
                                               </div><br />
                                           </div>
                                           
                                           <div class="col-md-12">
                                               <div class="row">
                                                   <center><legend style="font-weight:bold;">CARD HOLDER INFORMATION</legend></center>
                                                   <div class="col-xs-12">
                                                       <span class="note"><strong>RELATIONSHIP</strong></span>
                                                       @Html.DropDownListFor(Function(m) m.Relationship, Model.RelationshipList, New With {.class = "form-control"})
                                                   </div>
                                                   <div class="col-xs-5">
                                                       <span class="note"><strong>LAST NAME</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuredLN, New With {.class = "form-control", .maxlength = "100"})
                                                   </div>
                                                   <div class="col-xs-5">
                                                       <span class="note"><strong>FIRST NAME</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuredFN, New With {.class = "form-control", .maxlength = "100"})
                                                   </div>
                                                   <div class="col-xs-2">
                                                       <span class="note"><strong>MI</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuredMN, New With {.class = "form-control", .maxlength = "1"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>DATE OF BIRTH</strong></span>
                                                       @If Not Model.InsuredDOB.HasValue OrElse Model.InsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                                   @Html.TextBoxFor(Function(m) m.InsuredDOB, New With {.class = "form-control", .type = "date", .maxlength = "10"})
                                                       Else
                                                   @Html.TextBoxFor(Function(m) m.InsuredDOB, "{0:yyyy-MM-dd}", New With {.class = "form-control", .type = "date", .maxlength = "10"})
                                                       End If
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>STREET ADDRESS</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuranceAddress, New With {.class = "form-control", .maxlength = "50"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>CITY</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuranceCity, New With {.class = "form-control autoc", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>STATE</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuranceState, New With {.class = "form-control autoc", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>ZIP CODE</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsuranceZip, New With {.class = "form-control", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>PHONE #</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.InsurancePhoneNO, New With {.class = "form-control", .maxlength = "12"})<br />
                                                   </div>
                                               </div><br />
                                           </div>
                                           
                                           <div class="col-md-12">
                                               <div class="row">
                                                   <center><legend style="font-weight:bold;">EMPLOYER INFORMATION</legend></center>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>EMPLOYER</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.Employer, New With {.class = "form-control", .maxlength = "50"})
                                                   </div>
                                                   <div class="col-xs-6">
                                                       <span class="note"><strong>STREET ADDRESS</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.EmployerAddress, New With {.class = "form-control", .maxlength = "50"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>CITY</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.EmployerCity, New With {.class = "form-control autoc", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>STATE</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.EmployerState, New With {.class = "form-control autoc", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>ZIP CODE</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.EmployerZip, New With {.class = "form-control", .maxlength = "20"})
                                                   </div>
                                                   <div class="col-xs-3">
                                                       <span class="note"><strong>PHONE #</strong></span>
                                                       @Html.TextBoxFor(Function(m) m.EmployerPhone, New With {.class = "form-control", .maxlength = "12"})<br />
                                                   </div>
                                               </div><br />
                                           </div> 
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-6 no-padding">
                        <table class="table table-condensed">
                            <thead>
                                <tr>
                                    <th>
                                        <h6><strong>SECONDARY BILL TYPE</strong></h6>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <center><legend style="font-weight:bold;">BILLING INFORMATION</legend></center>
                                                    <div class="col-xs-12">
                                                        <span class="note"><strong>BILL TO</strong></span>
                                                        @Html.DropDownListFor(Function(m) m.SBilltype, Model.SSBilltype, New With {.class = "form-control"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>MEDICARE #</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SMedicareNo, New With {.class = "form-control", .maxlength = "11"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>MEDICAID #</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SMedicaID, New With {.class = "form-control", .maxlength = "14"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>INSURANCE COMPANY</strong></span>
                                                        @Html.DropDownListFor(Function(m) m.SInsuranceCompanyName, Model.SInsuranceCompanyList, New With {.class = "form-control"})
                                                        @Html.DropDownListFor(Function(m) m.SMedicaidInsIndex, Model.SMedicaidIns, New With {.class = "form-control"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>HMO</strong></span>
                                                        @Html.DropDownListFor(Function(m) m.SHMOName, Model.SHMOList, New With {.class = "form-control"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>GROUP #</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SGroupNo, New With {.class = "form-control", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>INSURED ID</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuredID, New With {.class = "form-control", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>SSN #</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SSSN, New With {.class = "form-control", .maxlength = "9"})<br />
                                                    </div>
                                                </div><br />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <center><legend style="font-weight:bold;">CARD HOLDER INFORMATION</legend></center>
                                                    <div class="col-xs-12">
                                                        <span class="note"><strong>RELATIONSHIP</strong></span>
                                                        @Html.DropDownListFor(Function(m) m.SRelationship, Model.SRelationshipList, New With {.class = "form-control"})
                                                    </div>
                                                    <div class="col-xs-5">
                                                        <span class="note"><strong>LAST NAME</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuredLName, New With {.class = "form-control", .maxlength = "100"})
                                                    </div>
                                                    <div class="col-xs-5">
                                                        <span class="note"><strong>FIRST NAME</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuredFName, New With {.class = "form-control", .maxlength = "100"})
                                                    </div>
                                                    <div class="col-xs-2">
                                                        <span class="note"><strong>MI</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuredMName, New With {.class = "form-control", .maxlength = "1"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>DATE OF BIRTH</strong></span>
                                                        @If Not Model.SInsuredDOB.HasValue OrElse Model.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                                            @Html.TextBoxFor(Function(m) m.SInsuredDOB, New With {.class = "form-control", .type = "date", .maxlength = "10"})
                                                        Else
                                                            @Html.TextBoxFor(Function(m) m.SInsuredDOB, "{0:yyyy-MM-dd}", New With {.class = "form-control", .type = "date", .maxlength = "10"})
                                                        End If
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>STREET ADDRESS</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuranceAddress, New With {.class = "form-control", .maxlength = "50"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>CITY</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuranceCity, New With {.class = "form-control autoc", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>STATE</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuranceState, New With {.class = "form-control autoc", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>ZIP CODE</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsuranceZip, New With {.class = "form-control", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>PHONE #</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SInsurancePhoneNo, New With {.class = "form-control", .maxlength = "12"})<br />
                                                    </div>
                                                </div><br />
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <center><legend style="font-weight:bold;">EMPLOYER INFORMATION</legend></center>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>EMPLOYER</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SEmployer, New With {.class = "form-control", .maxlength = "50"})
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <span class="note"><strong>STREET ADDRESS</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SEmployerAddress, New With {.class = "form-control", .maxlength = "50"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>CITY</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SEmployerCity, New With {.class = "form-control autoc", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>STATE</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SEmployerState, New With {.class = "form-control autoc", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>ZIP CODE</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SEmployerZip, New With {.class = "form-control", .maxlength = "20"})
                                                    </div>
                                                    <div class="col-xs-3">
                                                        <span class="note"><strong>PHONE #</strong></span>
                                                        @Html.TextBoxFor(Function(m) m.SEmployerPhone, New With {.class = "form-control", .maxlength = "12"})
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <div class="col-xs-12" style="margin-top: 10px !important;">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12 no-padding">
                        <table class="table table-condensed">
                            <thead>
                                <tr>
                                    <th>
                                        <h6><strong>SPECIMEN</strong></h6>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div class="col-xs-12 col-sm-12 col-md-6">
                                            <div class="row">
                                                <section class="col-xs-12">
                                                    <span class="note"><strong>Collected By</strong></span>
                                                    @Html.TextBoxFor(Function(m) m.RequestCollectedBy, New With {.class = "form-control"})
                                                </section>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-12 col-md-6">
                                            <div class="row">
                                                <section class="col-xs-12">
                                                    <span class="note"><strong>Collected Date</strong></span>
                                                    @If Not Model.RequestCollectedTS.HasValue OrElse Model.RequestCollectedTS.ToString.Substring(0, 8) = "1/1/0001" Then
                                                        @Html.TextBoxFor(Function(m) m.RequestCollectedTS, "{0:yyyy-MM-ddTHH:mm}", New With {.class = "form-control", .type = "datetime-local", .maxlength = "10"})
                                                    Else
                                                        @<input type="text" value="@Model.RequestCollectedTS.Value.ToString("MM/dd/yyyy HH:mm")" id="RequestCollectedTS" name="RequestCollectedTS" data-mask="99/99/9999" data-mask-placeholder="-" class="form-control" />
                                                        @<p class="note">
                                                            Data format: MM/dd/yyyy hh:mm
                                                        </p>
                                                        @*@Html.TextBoxFor(Function(m) m.RequestCollectedTS, "{0:yyyy-MM-ddTHH:mm}", New With {.class = "form-control", .type = "datetime-local", .maxlength = "10"})*@
                                                    End If
                                                </section>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row">
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Specimen Code</th>
                                                        <th>Specimen Description</th>
                                                        <th>Quantity</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    @for i = 0 To Model.Specimens.Count - 1
                                                        @*@Html.HiddenFor(Function(m) m.Specimens(i).SpecCode)*@
                                                        @<tr>
                                                            <td>@Html.DisplayFor(Function(m) m.Specimens(i).SpecCode)</td>
                                                            <td>@Html.DisplayFor(Function(m) m.Specimens(i).SpecDesc)</td>
                                                             <td>@Html.TextBoxFor(Function(m) m.Specimens(i).SpecQuantity, New With {.class = "form-control", .maxlength = "3", .onkeypress = "return isNumberKey(event);"})</td>
                                                        </tr>
                                                    Next
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <div Class="col-xs-12" style="margin-top: 10px !important;">
                <div Class="row">
                    <div Class="col-xs-12 col-sm-12 col-md-12 no-padding">
                        <Table Class="table table-condensed">
                            <thead>
                                <tr>
                                    <th>
                                        <h6> <strong> TEST LIST</strong></h6>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <div Class="row">
                                            <Table Class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th style="width:150px;"> Test Code</th>
                                                        <th>Test Description</th>
                                                        <th>Question</th>
                                                        <th>Answer</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    
                                                    @For X As Integer = 0 To Model.Tests.Count - 1
                                                        @<tr>
                                                            <td>@Model.Tests(X).TestCode</td>
                                                            <td>@Model.Tests(X).TestName</td>
                                                            <td>@Model.Tests(X).TestQuestion</td>
                                                            <td>@Model.Tests(X).TestAnswer</td>
                                                        </tr>
                                                    Next
                                                </tbody>
                                            </Table>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </Table>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <h6> <strong>TEST REMARKS</strong></h6>
                @Html.TextAreaFor(Function(x) x.RequestLabTestRemarks, New With {.Class = "form-control", .maxlength = "255", .rows = "5"})
            </div>
            <div Class="col-xs-12"><br /><input type="submit" style="display: none;" /></div>
        </div>
    </div>  End Using

<script>
    $(function() {
       @For Each vmsv In ViewData.ModelState.Keys
           Dim ms As ModelState = ViewData.ModelState(vmsv)
           If ms.Errors.Count > 0 Then
        @:$("#@vmsv").addClass("haserror");
                End If
       Next


        $("#RequestType").on("change", function () {
            ShowIns();
        });

        $("#SBilltype").on("change", function () {
            ShowSecIns();
        });

        

        $("#RequestCollectedTS").mask("99/99/9999 99:99");
    });

    ShowIns();
    ShowSecIns();
    function ShowIns() {
        if ($("#RequestType").val() == "Client") 
        {
            document.getElementById("InsuredDOB").value = null;
            $("#InsuranceCode").css("display", "none");
            $("#PMedicaidInsIndex").css("display", "block");    
            $("#Medicare").attr("readonly", "true");
            //document.getElementById('Medicare').value = '';

            $("#MedicaID").attr("readonly", "true");
            //document.getElementById('MedicaID').value = '';

            $("#InsuranceCode").attr("readonly", "true");
            //document.getElementById('InsuranceCode').value = '';

            $("#PMedicaidInsIndex").attr("readonly", "true");
            //document.getElementById('PMedicaidInsIndex').value = '';

            $("#HMOCode").attr("readonly", "true");
            //document.getElementById('HMOCode').value = '';

            $("#GroupNo").attr("readonly", "true");
            //document.getElementById('GroupNo').value = '';

            $("#InsuredID").attr("readonly", "true");
            //document.getElementById('InsuredID').value = '';

            $("#SSN").attr("readonly", "true");
            //document.getElementById('SSN').value = '';

            $("#Relationship").attr("readonly", "true");
            //document.getElementById('Relationship').value = '';

            $("#InsuredLN").attr("readonly", "true");
            //document.getElementById('InsuredLN').value = '';

            $("#InsuredFN").attr("readonly", "true");
            //document.getElementById('InsuredFN').value = '';

            $("#InsuredMN").attr("readonly", "true");
            //document.getElementById('InsuredMN').value = '';

            $("#InsuredDOB").attr("readonly", "true");
            //document.getElementById('InsuredDOB').value = '';

            $("#InsuranceAddress").attr("readonly", "true");
            //document.getElementById('InsuranceAddress').value = '';

            $("#InsuranceCity").attr("readonly", "true");
            //document.getElementById('InsuranceCity').value = '';

            $("#InsuranceState").attr("readonly", "true");
            //document.getElementById('InsuranceState').value = '';

            $("#InsuranceZip").attr("readonly", "true");
            //document.getElementById('InsuranceZip').value = '';

            $("#InsurancePhoneNO").attr("readonly", "true");
            //document.getElementById('InsurancePhoneNO').value = '';

            $("#Employer").attr("readonly", "true");
            //document.getElementById('Employer').value = '';

            $("#EmployerAddress").attr("readonly", "true");
            //document.getElementById('EmployerAddress').value = '';

            $("#EmployerCity").attr("readonly", "true");
            //document.getElementById('EmployerCity').value = '';

            $("#EmployerState").attr("readonly", "true");
            //document.getElementById('EmployerState').value = '';

            $("#EmployerZip").attr("readonly", "true");
            //document.getElementById('EmployerZip').value = '';

            $("#EmployerPhone").attr("readonly", "true");
            //document.getElementById('EmployerPhone').value = '';
        }
        else
        {
            $("#Medicare").attr("readonly", false);
            $("#MedicaID").attr("readonly", false);
            $("#InsuranceCode").attr("readonly", false);
            $("#PMedicaidInsIndex").attr("readonly", false);
            $("#HMOCode").attr("readonly", false);
            $("#GroupNo").attr("readonly", false);
            $("#InsuredID").attr("readonly", false);
            $("#SSN").attr("readonly", false);
            $("#Relationship").attr("readonly", false);
            $("#InsuredLN").attr("readonly", false);
            $("#InsuredFN").attr("readonly", false);
            $("#InsuredMN").attr("readonly", false);
            $("#InsuredDOB").attr("readonly", false);
            $("#InsuranceAddress").attr("readonly", false);
            $("#InsuranceCity").attr("readonly", false);
            $("#InsuranceState").attr("readonly", false);
            $("#InsuranceZip").attr("readonly", false);
            $("#InsurancePhoneNO").attr("readonly", false);
            $("#Employer").attr("readonly", false);
            $("#EmployerAddress").attr("readonly", false);
            $("#EmployerCity").attr("readonly", false);
            $("#EmployerState").attr("readonly", false);
            $("#EmployerZip").attr("readonly", false);
            $("#EmployerPhone").attr("readonly", false);
            
            if ($("#RequestType").val() == "Medicaid") {
                $("#InsuranceCode").css("display", "none");
                $("#PMedicaidInsIndex").css("display", "block");
            }
            else {
                $("#InsuranceCode").css("display", "block");
                $("#PMedicaidInsIndex").css("display", "none");
            }
        }
    }
    function ShowSecIns() {
        if ($("#SBilltype").val() == "Medicaid") {
            $("#SInsuranceCompanyName").css("display", "none");
            $("#SMedicaidInsIndex").css("display", "block");
        }
        else {
            $("#SInsuranceCompanyName").css("display", "block");
            $("#SMedicaidInsIndex").css("display", "none");
        }
    }
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

</script>