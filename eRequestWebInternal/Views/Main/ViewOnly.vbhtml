@ModelType Models.MainModel
@Code
    ViewData("Title") = "ViewOnly"
    Layout = "~/Views/Shared/_Layout.vbhtml"

    @*@<a href="@Url.Action("Index", "RequestTable")" id="submitter" class="btn btn-lg btn-circle btn-danger" style="right: 17px;"><i class="fa fa-chevron-left"></i></a>*@

    Dim mname As String = String.Empty
    If Model.InsuredMN <> String.Empty Then mname = Model.InsuredMN.Substring(0, 1)
    Dim age As Integer = Support.Helpers.LocalizeDateTime(Now, Model.RequestLab.Substring(0, 4)).Year - Date.Parse(Model.InsuredDOB).Year
    If Integer.Parse(Support.Helpers.LocalizeDateTime(Now, Model.RequestLab.Substring(0, 4)).ToString("MMdd")) < Integer.Parse(Date.Parse(Model.InsuredDOB).ToString("MMdd")) Then age -= 1
    Dim agestring As String = age
    If age > 1 Then
        agestring &= " YEARS OLD"
    Else
        agestring &= " YEAR OLD"
    End If

    Dim Page As Integer = 0
    Dim PatAge As String
    If Integer.Parse(Now.ToString("MMdd")) < Integer.Parse(Model.RequestPDOB.Value.ToString("MMdd")) Then Page = 1
    Page = Now.Year - Model.RequestPDOB.Value.Year - Page
    If Page > 1 Then
        PatAge = Page & " YEARS OLD"
    Else
        PatAge = Page & " YEAR OLD"
    End If

    Dim printablebilling As String = String.Empty
    Dim printablebilling2 As String = String.Empty
    Dim printableicd As String = String.Empty
    Dim printablecount As String = String.Empty
    If Model.RequestLabTest.Split(Chr(23)).Count > 1 Then
        printablecount = Model.RequestLabTest.Split(Chr(23)).Count & " TESTS/PROFILES"
    Else
        printablecount = Model.RequestLabTest.Split(Chr(23)).Count & " TEST/PROFILE"
    End If
    Dim printabletests As String = String.Empty
    Dim printablespecimens As String = String.Empty
    For Each s In Model.Specimens
        printablespecimens &= s.SpecQuantity & " - " & s.SpecDesc & "<br />"
    Next
    If printablespecimens <> String.Empty Then
        printablespecimens = printablespecimens.Substring(0, printablespecimens.Length - 6)
    End If

    Dim employer As String = Model.Employer
    Dim empaddress As String = Model.EmployerAddress
    Dim empcity As String = Model.EmployerCity
    Dim empstate As String = Model.EmployerState
    Dim empzip As String = Model.EmployerZip
    Dim empphone As String = Model.EmployerPhone
    Dim sbilltype As String = Model.SBilltype
    Dim smedicareno As String = Model.SMedicareNo
    Dim smedicaidno As String = Model.SMedicaID
    Dim sinsurance As String = Model.SInsuranceCompany
    Dim sinsurancename As String = Model.SInsuranceCompanyName
    Dim shmo As String = Model.SHMO
    Dim shmoname As String = Model.SHMOName
    Dim sgroupno As String = Model.SGroupNo
    Dim sinsuredid As String = Model.SInsuredID
    Dim srelationship As String = Model.SRelationship
    Dim sssn As String = Model.SSSN
    Dim sinsuredln As String = Model.SInsuredLName
    Dim sinsuredfn As String = Model.SInsuredFName
    Dim sinsuredmn As String = Model.SInsuredMName
    Dim sinsureddob As String = Model.SInsuredDOB
    Dim sinsuredaddress As String = Model.SInsuranceAddress
    Dim sinsuredcity As String = Model.SInsuranceCity
    Dim sinsuredstate As String = Model.SInsuranceState
    Dim sinsuredzip As String = Model.SInsuranceZip
    Dim sinsuredphoneno As String = Model.SInsurancePhoneNo
    Dim semployer As String = Model.SEmployer
    Dim sempaddress As String = Model.SEmployerAddress
    Dim sempcity As String = Model.SEmployerCity
    Dim sempstate As String = Model.SEmployerState
    Dim sempzip As String = Model.SEmployerZip
    Dim sempphone As String = Model.SEmployerPhone
    Dim showpreauth As Boolean = Model.ShowPreAuth
    Dim emailsent As Boolean = Model.EmailSent
    Dim billedtoclient As Boolean = Model.BillToClient
    Dim terms As String = Model.Terms
    Dim medicaidins As String = Model.MedicaidInsurance
    Dim medicaidinsname As String = Model.MedicaidInsuranceName
    Dim secmedicaidins As String = Model.SMedicaidInsurance
    Dim secmedicaidinsname As String = Model.SMedicaidInsuranceName
    Dim sage As Integer = 0
    Dim sagestring As String = sage

    Try
        sage = Support.Helpers.LocalizeDateTime(Now, Model.RequestLab.Substring(0, 4)).Year - Date.Parse(sinsureddob).Year
        If Integer.Parse(Support.Helpers.LocalizeDateTime(Now, Model.RequestLab.Substring(0, 4)).ToString("MMdd")) < Integer.Parse(Date.Parse(sinsureddob).ToString("MMdd")) Then sage -= 1
        sagestring = sage
        If sage > 1 Then
            sagestring &= " YEARS OLD"
        Else
            sagestring &= " YEAR OLD"
        End If
    Catch ex As Exception
    End Try

    Dim tln As String = String.Format("{0}   ", Model.RequestPLName)
    Dim tfn As String = String.Format("{0}   ", Model.RequestPFName)
    Dim tmn As String = String.Format("{0}   ", Model.RequestPMName)
    Dim mrn As String = String.Empty
    mrn = String.Format("{0}{1}{2}", tln.Substring(0, 3).Trim.ToUpper, tfn.Substring(0, 3).Trim.ToUpper, Model.RequestPDOB.Value.ToString("MMddyyyy"))

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
<div class="jarviswidget jarviswidget-color-red">
    <header>
        <span class="widget-icon"><i class="fa fa-file"></i></span>
        <h2><strong>&nbsp;&nbsp;REQUEST @Model.DraftCode</strong></h2>
    </header>
    <div class="widget-body">
        <div class="row no-padding">
            <div class="col-xs-12">
                <a href="@Url.Action("Index", "RequestTable")" class="btn btn-sm btn-primary erequestlink" title="BACK"><strong><i class="fa fa-chevron-left"></i></strong></a>
                @*<a href="javascript:void(0);" class="btn btn-sm btn-primary" onclick="frameworkInitShim()"*@ @*id="barcode" title="BARCODE"><strong><i class="fa fa-qrcode"></i></strong></a>*@
                <button id="barcode" name="barcode" class="btn btn-sm btn-primary"><strong><i class="fa fa-barcode">&nbsp;BAR CODE</i></strong></button>
                <a href="javascript:void(0);" class="btn btn-sm btn-primary" title="PRINT" id="print"><strong><i class="fa fa-print">&nbsp;PRINT</i></strong></a>
                <table class="pull-right">
                    <tr>
                        <td style="text-align: left; vertical-align: top; padding-right: 10px;"><h1><i class="fa fa-hospital-o"></i></h1></td>
                        <td><h1><strong>@Model.RequestLab.Split(Chr(4))(1)</strong></h1></td>
                    </tr>
                </table>
                <br />
                <br />
            </div>
            <div class="col-xs-12">
                <div class="table-responsive">
                    <table class="table table-condensed table-bordered">
                        <thead>
                            <tr>
                                <th colspan="4"><strong><i class="fa fa-user">&nbsp;&nbsp;</i>PATIENT INFO</strong></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><span class="note">Name</span></td>
                                <td><strong>@Model.RequestPFName.ToUpper @Model.RequestPMName.ToUpper @Model.RequestPLName.ToUpper</strong></td>
                                <td><span class="note">ALSO KNOWN AS (AKA)</span></td>
                                <td><strong>@Model.MRN.ToUpper</strong></td>
                            </tr>
                            <tr>
                                <td><span class="note">Date of Birth</span></td>
                                <td><strong>@Model.RequestPDOB.Value.ToString("MMMM d, yyyy").ToUpper (@PatAge)</strong></td>
                                <td><span class="note">Address</span></td>
                                <td><strong>@Model.RequestPAddress.ToUpper @Model.City.ToUpper @Model.State.ToUpper @Model.ZipCode.ToUpper</strong></td>
                            </tr>
                            <tr>
                                <td><span class="note">Gender</span></td>
                                <td><strong>@Model.RequestPGender.ToUpper</strong></td>
                                <td><span class="note">Phone #</span></td>
                                <td><strong>@Model.RequestPContact.ToUpper</strong></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="table-responsive">
                            <table class="table table-condensed table-bordered">
                                <thead>
                                    <tr>
                                        <th colspan="2"><strong><i class="fa fa-money">&nbsp;&nbsp;</i>PRIMARY BILLING INFO</strong></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><span class="note">Billed To</span></td>
                                        <td><strong>@Model.RequestType.ToUpper</strong></td>
                                    </tr>
                                    @If Model.RequestType = "Medicare" Then
                                        printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & Model.RequestType.ToUpper & "</strong></td><td><span class='small'>Medicare #</span></td><td><strong>" & Model.Medicare.ToUpper & "</strong></td></tr>"
                                        @<tr>
                                            <td><span class="note">Medicare #</span></td>
                                            <td><strong>@Model.Medicare.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Name</span></td>
                                            @If Model.InsuredLN = String.Empty Then
                                                @<td></td>
                                            Else
                                                printablebilling &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td></tr>"
                                                @<td><strong>@Model.InsuredFN.ToUpper @mname.ToUpper @Model.InsuredLN.ToUpper (@Model.Relationship.ToUpper)</strong></td>
                                            End If
                                        </tr>
                                        @*@<tr>
                                                <td><span class="note">Issue Date</span></td>
                                                <td><strong>@Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper</strong></td>
                                            </tr>*@
                                    ElseIf Model.RequestType = "Medicaid" Then
                                        'printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td colspan='3'><strong>" & Model.Request.Type.ToUpper & "</strong></td><tr><td><span class='small'>Medicaid #</span></td><td><strong>" & Model.Request.Medicaid & "</strong></td><td><span class='small'>Issue Date</span></td><td><strong>" & Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper & "</strong></td></tr>"
                                        printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & Model.RequestType.ToUpper & " (" & medicaidinsname.ToUpper & ")</strong></td><td><span class='small'>Medicaid #</span></td><td><strong>" & Model.MedicaID.ToUpper & "</strong></td></tr>"
                                        @<tr>
                                            <td><span class="note">Medicaid #</span></td>
                                            <td><strong>@Model.MedicaID.ToUpper (@medicaidinsname.ToUpper)</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Name</span></td>
                                            @If Model.InsuredLN = String.Empty Then
                                                @<td></td>
                                            Else
                                                If Model.Relationship = "Self" Then
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td></tr>"
                                                Else
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td>"
                                                End If
                                                @<td><strong>@Model.InsuredFN.ToUpper @mname.ToUpper @Model.InsuredLN.ToUpper (@Model.Relationship.ToUpper)</strong></td>
                                            End If
                                        </tr>
                                        @If Model.Relationship <> "Self" Then
                                            printablebilling &= "<td><span class='small'>SSN #</span></td><td><strong>" & Model.SSN.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & Model.InsurancePhoneNO.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & Model.InsuranceAddress.ToUpper & " " & Model.InsuranceCity.ToUpper & " " & Model.InsuranceState.ToUpper & " " & Model.InsuranceZip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & employer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & empphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & empaddress.ToUpper & " " & empcity & " " & empstate & " " & empzip & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Date of Birth</span></td>
                                                <td><strong>@Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper (@agestring)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">SSN #</span></td>
                                                <td><strong>@Model.SSN.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@Model.InsuranceAddress.ToUpper @Model.InsuranceCity.ToUpper @Model.InsuranceState.ToUpper @Model.InsuranceZip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@Model.InsurancePhoneNO.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Employer</span></td>
                                                <td><strong>@employer.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@empaddress.ToUpper @empcity.ToUpper @empstate.ToUpper @empzip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@empphone.ToUpper</strong></td>
                                            </tr>
                                        End If
                                        @*@<tr>
                                                <td><span class="note">Issue Date</span></td>
                                                <td><strong>@Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper</strong></td>
                                            </tr>*@
                                    ElseIf Model.RequestType = "Insurance" Then
                                        'printablebilling &= "<tr><td style='min-width: 140px !important; max-width: 140px !important; width: 140px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.Request.Type.ToUpper & "</strong></td><td style='min-width: 130px !important; max-width: 130px !important; width: 130px !important;'><span class='small'>Insurance</span></td><td><strong>" & Model.Request.InsuranceName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Name</span></td><td><strong>" & Model.Request.InsuredFN.ToUpper & " " & mname.ToUpper & " " & Model.Request.InsuredLN.ToUpper & " (" & Model.Request.Relationship.ToUpper & ")</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.Request.InsuredID.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Date of Birth</span></td><td><strong>" & Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Group #</span></td><td><strong>" & Model.Request.GroupNo.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td style='max-width: 500px;'><strong>" & Model.Request.InsuranceAddress.ToUpper & " " & Model.Request.InsuranceCity.ToUpper & " " & Model.Request.InsuranceState.ToUpper & " " & Model.Request.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Insurance Phone #</span></td><td><strong>" & Model.Request.InsurancePhoneNo.ToUpper & "</strong></td></tr>"
                                        printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & Model.RequestType.ToUpper & "</strong></td><td><span class='small'>Insurance</span></td><td><strong>" & Model.InsuranceName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Group #</span></td><td><strong>" & Model.GroupNo.ToUpper & "</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.InsuredID.ToUpper & "</strong></td></tr>"
                                        @<tr>
                                            <td><span class="note">Insurance</span></td>
                                            <td><strong>@Model.InsuranceName.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Group #</span></td>
                                            <td><strong>@Model.GroupNo.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Insured ID</span></td>
                                            <td><strong>@Model.InsuredID.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Name</span></td>
                                            @If Model.InsuredLN = String.Empty Then
                                                @<td></td>
                                            Else
                                                If Model.Relationship = "Self" Then
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td></tr>"
                                                Else
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td>"
                                                End If
                                                @<td><strong>@Model.InsuredFN.ToUpper @mname.ToUpper @Model.InsuredLN.ToUpper (@Model.Relationship.ToUpper)</strong></td>
                                            End If
                                        </tr>
                                        @If Model.Relationship <> "Self" Then
                                            printablebilling &= "<td><span class='small'>SSN #</span></td><td><strong>" & Model.SSN.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & Model.InsurancePhoneNO.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & Model.InsuranceAddress.ToUpper & " " & Model.InsuranceCity.ToUpper & " " & Model.InsuranceState.ToUpper & " " & Model.InsuranceZip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & employer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & empphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & empaddress.ToUpper & " " & empcity & " " & empstate & " " & empzip & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Date of Birth</span></td>
                                                <td><strong>@Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper (@agestring)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">SSN #</span></td>
                                                <td><strong>@Model.SSN.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@Model.InsuranceAddress.ToUpper @Model.InsuranceCity.ToUpper @Model.InsuranceState.ToUpper @Model.InsuranceZip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@Model.InsurancePhoneNO.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Employer</span></td>
                                                <td><strong>@employer.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@empaddress.ToUpper @empcity.ToUpper @empstate.ToUpper @empzip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@empphone.ToUpper</strong></td>
                                            </tr>
                                        End If
                                        @*@<tr>
                                                <td><span class="note">Name</span></td>
                                                <td><strong>@Model.Request.InsuredFN.ToUpper @mname.ToUpper @Model.Request.InsuredLN.ToUpper (@Model.Request.Relationship.ToUpper)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Date of Birth</span></td>
                                                <td><strong>@Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper (@agestring)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@Model.Request.InsuranceAddress.ToUpper @Model.Request.InsuranceCity.ToUpper @Model.Request.InsuranceState.ToUpper @Model.Request.InsuranceZip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@Model.Request.InsurancePhoneNo.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Employer</span></td>
                                                <td><strong>@employer.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@empaddress.ToUpper @empcity.ToUpper @empstate.ToUpper @empzip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@empphone.ToUpper</strong></td>
                                            </tr>*@
                                    ElseIf Model.RequestType = "Other" Then
                                        'printablebilling &= "<tr><td style='min-width: 140px !important; max-width: 140px !important; width: 140px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.Request.Type.ToUpper & "</strong></td><td style='min-width: 130px !important; max-width: 130px !important; width: 130px !important;'><span class='small'>Insurance</span></td><td><strong>" & Model.Request.InsuranceName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Name</span></td><td><strong>" & Model.Request.InsuredFN.ToUpper & " " & mname.ToUpper & " " & Model.Request.InsuredLN.ToUpper & " (" & Model.Request.Relationship.ToUpper & ")</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.Request.InsuredID.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Date of Birth</span></td><td><strong>" & Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Group #</span></td><td><strong>" & Model.Request.GroupNo.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td style='max-width: 500px;'><strong>" & Model.Request.InsuranceAddress.ToUpper & " " & Model.Request.InsuranceCity.ToUpper & " " & Model.Request.InsuranceState.ToUpper & " " & Model.Request.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Insurance Phone #</span></td><td><strong>" & Model.Request.InsurancePhoneNo.ToUpper & "</strong></td></tr>"
                                        printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td colspan='3'><strong>" & Model.RequestType.ToUpper & "</strong></td></tr><tr><td><span class='small'>Group #</span></td><td><strong>" & Model.GroupNo.ToUpper & "</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.InsuredID.ToUpper & "</strong></td></tr>"
                                        @<tr>
                                            <td><span class="note">Group #</span></td>
                                            <td><strong>@Model.GroupNo.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Insured ID</span></td>
                                            <td><strong>@Model.InsuredID.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Name</span></td>
                                            @If Model.InsuredLN = String.Empty Then
                                                @<td></td>
                                            Else
                                                If Model.Relationship = "Self" Then
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td></tr>"
                                                Else
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td>"
                                                End If
                                                @<td><strong>@Model.InsuredFN.ToUpper @mname.ToUpper @Model.InsuredLN.ToUpper (@Model.Relationship.ToUpper)</strong></td>
                                            End If
                                        </tr>
                                        @If Model.Relationship <> "Self" Then
                                            printablebilling &= "<td><span class='small'>SSN #</span></td><td><strong>" & Model.SSN.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & Model.InsurancePhoneNO.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & Model.InsuranceAddress.ToUpper & " " & Model.InsuranceCity.ToUpper & " " & Model.InsuranceState.ToUpper & " " & Model.InsuranceZip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & employer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & empphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & empaddress.ToUpper & " " & empcity & " " & empstate & " " & empzip & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Date of Birth</span></td>
                                                <td><strong>@Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper (@agestring)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">SSN #</span></td>
                                                <td><strong>@Model.SSN.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@Model.InsuranceAddress.ToUpper @Model.InsuranceCity.ToUpper @Model.InsuranceState.ToUpper @Model.InsuranceZip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@Model.InsurancePhoneNO.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Employer</span></td>
                                                <td><strong>@employer.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@empaddress.ToUpper @empcity.ToUpper @empstate.ToUpper @empzip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@empphone.ToUpper</strong></td>
                                            </tr>
                                        End If
                                    ElseIf Model.RequestType = "HMO" Then
                                        'printablebilling &= "<tr><td style='min-width: 140px !important; max-width: 140px !important; width: 140px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.Request.Type.ToUpper & "</strong></td><td style='min-width: 130px !important; max-width: 130px !important; width: 130px !important;'><span class='small'>HMO</span></td><td><strong>" & Model.Request.HMOName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Name</span></td><td><strong>" & Model.Request.InsuredFN.ToUpper & " " & mname.ToUpper & " " & Model.Request.InsuredLN.ToUpper & " (" & Model.Request.Relationship.ToUpper & ")</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.Request.InsuredID.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Date of Birth</span></td><td><strong>" & Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Group #</span></td><td><strong>" & Model.Request.GroupNo.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td><strong>" & Model.Request.InsuranceAddress.ToUpper & " " & Model.Request.InsuranceCity.ToUpper & " " & Model.Request.InsuranceState.ToUpper & " " & Model.Request.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Insurance Phone #</span></td><td><strong>" & Model.Request.InsurancePhoneNo.ToUpper & "</strong></td></tr>"
                                        printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & Model.RequestType.ToUpper & "</strong></td><td><span class='small'>Insurance</span></td><td><strong>" & Model.HMOName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Group #</span></td><td><strong>" & Model.GroupNo.ToUpper & "</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.InsuredID.ToUpper & "</strong></td></tr>"
                                        @<tr>
                                            <td><span class="note">HMO</span></td>
                                            <td><strong>@Model.HMOName.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Group #</span></td>
                                            <td><strong>@Model.GroupNo.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Insured ID</span></td>
                                            <td><strong>@Model.InsuredID.ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Name</span></td>
                                            @if Model.insuredln = String.Empty Then
                                                @<td></td>
                                            Else
                                                @If Model.Relationship = "Self" Then
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td></tr>"
                                                @<td>   </td>
                                                Else
                                                    printablebilling &= "<tr><td><span class='small'>Name</span></td><td><strong>" & Model.InsuredFN.ToUpper & " " & Model.InsuredMN.ToUpper & " " & Model.InsuredLN.ToUpper & " (" & Model.Relationship.ToUpper & ")</strong></td>"
                                                @<td><strong>@Model.InsuredFN.ToUpper @mname.ToUpper @Model.InsuredLN.ToUpper (@Model.Relationship.ToUpper)</strong></td>
                                                End If
                                            End If
                                            
                                        </tr>
                                        @If Model.Relationship <> "Self" Then
                                            printablebilling &= "<td><span class='small'>SSN #</span></td><td><strong>" & Model.SSN.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Model.InsuranceAddress.ToUpper & " " & Model.InsuranceCity.ToUpper & " " & Model.InsuranceState.ToUpper & " " & Model.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & Model.InsurancePhoneNO.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & employer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & empphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & empaddress.ToUpper & " " & empcity & " " & empstate & " " & empzip & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Date of Birth</span></td>
                                                <td><strong>@Date.Parse(Model.InsuredDOB).ToString("MMMM d, yyyy").ToUpper (@agestring)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">SSN #</span></td>
                                                <td><strong>@Model.SSN.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@Model.InsuranceAddress.ToUpper @Model.InsuranceCity.ToUpper @Model.InsuranceState.ToUpper @Model.InsuranceZip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@Model.InsurancePhoneNO.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Employer</span></td>
                                                <td><strong>@employer.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Address</span></td>
                                                <td><strong>@empaddress.ToUpper @empcity.ToUpper @empstate.ToUpper @empzip.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Phone #</span></td>
                                                <td><strong>@empphone.ToUpper</strong></td>
                                            </tr>
                                        End If
                                    ElseIf Model.RequestType = "Patient" Then
                                        @<tr></tr>
                                    Else
                                        printablebilling &= "<tr><td style='min-width: 100px !important; max-width: 100px !important; width: 100px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.RequestType.ToUpper & "</strong></td><td style='min-width: 70px !important; max-width: 70px !important; width: 70px !important;'><span class='small'>Client #</span></td><td><strong>" & Model.RequestClient.ToUpper & "</strong></td></tr>"
                                        @<tr>
                                            <td><span class="note">Client #</span></td>
                                            <td><strong>@Model.RequestClient</strong></td>
                                        </tr>
                                    End If
                                </tbody>
                            </table>
                        </div>
                        <br />
                        @If sbilltype <> String.Empty Then
                            @<div class="table-responsive">
                                <table class="table table-condensed table-bordered">
                                    <thead>
                                        <tr>
                                            <th colspan="2"><strong><i class="fa fa-money">&nbsp;&nbsp;</i>SECONDARY BILLING INFO</strong></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><span class="note">Billed To</span></td>
                                            <td><strong>@sbilltype.ToUpper</strong></td>
                                        </tr>
                                        @If sbilltype = "Medicare" Then
                                            'printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td colspan='3'><strong>" & sbilltype.ToUpper & "</strong></td><tr><td><span class='small'>Medicare #</span></td><td><strong>" & smedicareno & "</strong></td><td><span class='small'>Issue Date</span></td><td><strong>" & Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper & "</strong></td></tr>"
                                            printablebilling2 &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & sbilltype.ToUpper & "</strong></td><td><span class='small'>Medicare #</span></td><td><strong>" & smedicareno.ToUpper & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Medicare #</span></td>
                                                <td><strong>@smedicareno.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Name</span></td>
                                                @If sinsuredln = String.Empty Then
                                                   @<td></td>
                                                Else
                                                    printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td></tr>"
                                                    @<td><strong>@sinsuredfn.ToUpper @sinsuredmn.ToUpper @sinsuredln.ToUpper (@srelationship.ToUpper)</strong></td>
                                                End If
                                            </tr>
                                            @*@<tr>
                                                    <td><span class="note">Issue Date</span></td>
                                                    <td><strong>@Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper</strong></td>
                                                </tr>*@
                                        ElseIf sbilltype = "Medicaid" Then
                                            'printablebilling &= "<tr><td><span class='small'>Billed To</span></td><td colspan='3'><strong>" & sbilltype.ToUpper & "</strong></td><tr><td><span class='small'>Medicaid #</span></td><td><strong>" & Model.Request.Medicaid & "</strong></td><td><span class='small'>Issue Date</span></td><td><strong>" & Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper & "</strong></td></tr>"
                                            printablebilling2 &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & sbilltype.ToUpper & " (" & secmedicaidinsname.ToUpper & ")</strong></td><td><span class='small'>Medicaid #</span></td><td><strong>" & smedicaidno.ToUpper & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Medicaid #</span></td>
                                                <td><strong>@smedicaidno.ToUpper (@secmedicaidinsname.ToUpper)</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Name</span></td>
                                                @If sinsuredln = String.Empty Then
                                                    @<td></td>
                                                Else
                                                    If srelationship = "Self" Then
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td></tr>"
                                                    Else
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td>"
                                                    End If
                                                    @<td><strong>@sinsuredfn.ToUpper @sinsuredmn.ToUpper @sinsuredln.ToUpper (@srelationship.ToUpper)</strong></td>
                                                End If
                                            </tr>
                                            @If srelationship <> "Self" Then
                                                printablebilling2 &= "<td><span class='small'>SSN #</span></td><td><strong>" & sssn.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper & " (" & sagestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sinsuredphoneno.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sinsuredaddress.ToUpper & " " & sinsuredcity.ToUpper & " " & sinsuredstate.ToUpper & " " & sinsuredzip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & semployer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sempphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sempaddress.ToUpper & " " & sempcity & " " & sempstate & " " & sempzip & "</strong></td></tr>"
                                                @<tr>
                                                    <td><span class="note">Date of Birth</span></td>
                                                    <td><strong>@Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper (@sagestring)</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">SSN #</span></td>
                                                    <td><strong>@sssn.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sinsuredaddress.ToUpper @sinsuredcity.ToUpper @sinsuredstate.ToUpper @sinsuredzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sinsuredphoneno.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Employer</span></td>
                                                    <td><strong>@semployer.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sempaddress.ToUpper @sempcity.ToUpper @sempstate.ToUpper @sempzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sempphone.ToUpper</strong></td>
                                                </tr>
                                            End If
                                            @*@<tr>
                                                    <td><span class="note">Issue Date</span></td>
                                                    <td><strong>@Date.Parse(Model.Request.MIssuedDT).ToString("MMMM d, yyyy").ToUpper</strong></td>
                                                </tr>*@
                                        ElseIf sbilltype = "Insurance" Then
                                            'printablebilling &= "<tr><td style='min-width: 140px !important; max-width: 140px !important; width: 140px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.Request.Type.ToUpper & "</strong></td><td style='min-width: 130px !important; max-width: 130px !important; width: 130px !important;'><span class='small'>Insurance</span></td><td><strong>" & Model.Request.InsuranceName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Name</span></td><td><strong>" & Model.Request.InsuredFN.ToUpper & " " & mname.ToUpper & " " & Model.Request.InsuredLN.ToUpper & " (" & Model.Request.Relationship.ToUpper & ")</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.Request.InsuredID.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Date of Birth</span></td><td><strong>" & Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Group #</span></td><td><strong>" & Model.Request.GroupNo.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td style='max-width: 500px;'><strong>" & Model.Request.InsuranceAddress.ToUpper & " " & Model.Request.InsuranceCity.ToUpper & " " & Model.Request.InsuranceState.ToUpper & " " & Model.Request.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Insurance Phone #</span></td><td><strong>" & Model.Request.InsurancePhoneNo.ToUpper & "</strong></td></tr>"
                                            printablebilling2 &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & sbilltype.ToUpper & "</strong></td><td><span class='small'>Insurance</span></td><td><strong>" & sinsurancename.ToUpper & "</strong></td></tr><tr><td><span class='small'>Group #</span></td><td><strong>" & sgroupno.ToUpper & "</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & sinsuredid.ToUpper & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Insurance</span></td>
                                                <td><strong>@sinsurancename.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Group #</span></td>
                                                <td><strong>@sgroupno.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Insured ID</span></td>
                                                <td><strong>@sinsuredid.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Name</span></td>
                                                @If sinsuredln = String.Empty Then
                                                    @<td></td>
                                                Else
                                                    If srelationship = "Self" Then
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td></tr>"
                                                    Else
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td>"
                                                    End If
                                                    @<td><strong>@sinsuredfn.ToUpper @sinsuredmn.ToUpper @sinsuredln.ToUpper (@srelationship.ToUpper)</strong></td>
                                                End If
                                            </tr>
                                            @If srelationship <> "Self" Then
                                                printablebilling2 &= "<td><span class='small'>SSN #</span></td><td><strong>" & sssn.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper & " (" & sagestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sinsuredphoneno.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sinsuredaddress.ToUpper & " " & sinsuredcity.ToUpper & " " & sinsuredstate.ToUpper & " " & sinsuredzip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & semployer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sempphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sempaddress.ToUpper & " " & sempcity & " " & sempstate & " " & sempzip & "</strong></td></tr>"
                                                @<tr>
                                                    <td><span class="note">Date of Birth</span></td>
                                                    <td><strong>@Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper (@sagestring)</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">SSN #</span></td>
                                                    <td><strong>@sssn.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sinsuredaddress.ToUpper @sinsuredcity.ToUpper @sinsuredstate.ToUpper @sinsuredzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sinsuredphoneno.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Employer</span></td>
                                                    <td><strong>@semployer.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sempaddress.ToUpper @sempcity.ToUpper @sempstate.ToUpper @sempzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sempphone.ToUpper</strong></td>
                                                </tr>
                                            End If
                                            @*@<tr>
                                                    <td><span class="note">Name</span></td>
                                                    <td><strong>@Model.Request.InsuredFN.ToUpper @mname.ToUpper @Model.Request.InsuredLN.ToUpper (@Model.Request.Relationship.ToUpper)</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Date of Birth</span></td>
                                                    <td><strong>@Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper (@agestring)</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@Model.Request.InsuranceAddress.ToUpper @Model.Request.InsuranceCity.ToUpper @Model.Request.InsuranceState.ToUpper @Model.Request.InsuranceZip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@Model.Request.InsurancePhoneNo.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Employer</span></td>
                                                    <td><strong>@employer.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@empaddress.ToUpper @empcity.ToUpper @empstate.ToUpper @empzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@empphone.ToUpper</strong></td>
                                                </tr>*@
                                        ElseIf sbilltype = "Other" Then
                                            'printablebilling &= "<tr><td style='min-width: 140px !important; max-width: 140px !important; width: 140px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.Request.Type.ToUpper & "</strong></td><td style='min-width: 130px !important; max-width: 130px !important; width: 130px !important;'><span class='small'>Insurance</span></td><td><strong>" & Model.Request.InsuranceName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Name</span></td><td><strong>" & Model.Request.InsuredFN.ToUpper & " " & mname.ToUpper & " " & Model.Request.InsuredLN.ToUpper & " (" & Model.Request.Relationship.ToUpper & ")</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.Request.InsuredID.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Date of Birth</span></td><td><strong>" & Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Group #</span></td><td><strong>" & Model.Request.GroupNo.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td style='max-width: 500px;'><strong>" & Model.Request.InsuranceAddress.ToUpper & " " & Model.Request.InsuranceCity.ToUpper & " " & Model.Request.InsuranceState.ToUpper & " " & Model.Request.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Insurance Phone #</span></td><td><strong>" & Model.Request.InsurancePhoneNo.ToUpper & "</strong></td></tr>"
                                            printablebilling2 &= "<tr><td><span class='small'>Billed To</span></td><td colspan='3'><strong>" & sbilltype.ToUpper & "</strong></td></tr><tr><td><span class='small'>Group #</span></td><td><strong>" & sgroupno.ToUpper & "</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & sinsuredid.ToUpper & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">Group #</span></td>
                                                <td><strong>@sgroupno.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Insured ID</span></td>
                                                <td><strong>@sinsuredid.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Name</span></td>
                                                @If sinsuredln = String.Empty Then
                                                    @<td></td>
                                                Else
                                                    If srelationship = "Self" Then
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td></tr>"
                                                    Else
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td>"
                                                    End If
                                                    @<td><strong>@sinsuredfn.ToUpper @sinsuredmn.ToUpper @sinsuredln.ToUpper (@srelationship.ToUpper)</strong></td>
                                                End If
                                            </tr>
                                            @If srelationship <> "Self" Then
                                                printablebilling2 &= "<td><span class='small'>SSN #</span></td><td><strong>" & sssn.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper & " (" & sagestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sinsuredphoneno.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sinsuredaddress.ToUpper & " " & sinsuredcity.ToUpper & " " & sinsuredstate.ToUpper & " " & sinsuredzip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & semployer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sempphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sempaddress.ToUpper & " " & sempcity & " " & sempstate & " " & sempzip & "</strong></td></tr>"
                                                @<tr>
                                                    <td><span class="note">Date of Birth</span></td>
                                                    <td><strong>@Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper (@sagestring)</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">SSN #</span></td>
                                                    <td><strong>@sssn.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sinsuredaddress.ToUpper @sinsuredcity.ToUpper @sinsuredstate.ToUpper @sinsuredzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sinsuredphoneno.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Employer</span></td>
                                                    <td><strong>@semployer.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sempaddress.ToUpper @sempcity.ToUpper @sempstate.ToUpper @sempzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sempphone.ToUpper</strong></td>
                                                </tr>
                                            End If
                                        ElseIf sbilltype = "HMO" Then
                                            'printablebilling &= "<tr><td style='min-width: 140px !important; max-width: 140px !important; width: 140px !important;'><span class='small'>Billed To</span></td><td><strong>" & Model.Request.Type.ToUpper & "</strong></td><td style='min-width: 130px !important; max-width: 130px !important; width: 130px !important;'><span class='small'>HMO</span></td><td><strong>" & Model.Request.HMOName.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Name</span></td><td><strong>" & Model.Request.InsuredFN.ToUpper & " " & mname.ToUpper & " " & Model.Request.InsuredLN.ToUpper & " (" & Model.Request.Relationship.ToUpper & ")</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & Model.Request.InsuredID.ToUpper & "</strong></td></tr><tr><td><span class='small'>Insured Date of Birth</span></td><td><strong>" & Date.Parse(Model.Request.InsuredDOB).ToString("MMMM d, yyyy").ToUpper & " (" & agestring & ")</strong></td><td><span class='small'>Group #</span></td><td><strong>" & Model.Request.GroupNo.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td><strong>" & Model.Request.InsuranceAddress.ToUpper & " " & Model.Request.InsuranceCity.ToUpper & " " & Model.Request.InsuranceState.ToUpper & " " & Model.Request.InsuranceZip.ToUpper & "</strong></td><td><span class='small'>Insurance Phone #</span></td><td><strong>" & Model.Request.InsurancePhoneNo.ToUpper & "</strong></td></tr>"
                                            printablebilling2 &= "<tr><td><span class='small'>Billed To</span></td><td><strong>" & sbilltype.ToUpper & "</strong></td><td><span class='small'>HMO</span></td><td><strong>" & shmoname.ToUpper & "</strong></td></tr><tr><td><span class='small'>Group #</span></td><td><strong>" & sgroupno.ToUpper & "</strong></td><td><span class='small'>Insured ID</span></td><td><strong>" & sinsuredid.ToUpper & "</strong></td></tr>"
                                            @<tr>
                                                <td><span class="note">HMO</span></td>
                                                <td><strong>@shmoname.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Group #</span></td>
                                                <td><strong>@sgroupno.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Insured ID</span></td>
                                                <td><strong>@sinsuredid.ToUpper</strong></td>
                                            </tr>
                                            @<tr>
                                                <td><span class="note">Name</span></td>
                                                @If sinsuredln = String.Empty Then
                                                    @<td></td>
                                                Else
                                                    If srelationship = "Self" Then
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td colspan='3'><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td></tr>"
                                                    Else
                                                        printablebilling2 &= "<tr><td><span class='small'>Name</span></td><td><strong>" & sinsuredfn.ToUpper & " " & sinsuredmn.ToUpper & " " & sinsuredln.ToUpper & " (" & srelationship.ToUpper & ")</strong></td>"
                                                    End If
                                                    @<td><strong>@sinsuredfn.ToUpper @sinsuredmn.ToUpper @sinsuredln.ToUpper (@srelationship.ToUpper)</strong></td>
                                                End If
                                            </tr>
                                            @If srelationship <> "Self" Then
                                                printablebilling2 &= "<td><span class='small'>SSN #</span></td><td><strong>" & sssn.ToUpper & "</strong></td></tr><tr><td><span class='small'>Date of Birth</span></td><td><strong>" & Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper & " (" & sagestring & ")</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sinsuredphoneno.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sinsuredaddress.ToUpper & " " & sinsuredcity.ToUpper & " " & sinsuredstate.ToUpper & " " & sinsuredzip.ToUpper & "</strong></td></tr><tr><td><span class='small'>Employer</span></td><td><strong>" & semployer & "</strong></td><td><span class='small'>Phone #</span></td><td><strong>" & sempphone.ToUpper & "</strong></td></tr><tr><td><span class='small'>Address</span></td><td colspan='3'><strong>" & sempaddress.ToUpper & " " & sempcity & " " & sempstate & " " & sempzip & "</strong></td></tr>"
                                                @<tr>
                                                    <td><span class="note">Date of Birth</span></td>
                                                    <td><strong>@Date.Parse(sinsureddob).ToString("MMMM d, yyyy").ToUpper (@sagestring)</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">SSN #</span></td>
                                                    <td><strong>@sssn.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sinsuredaddress.ToUpper @sinsuredcity.ToUpper @sinsuredstate.ToUpper @sinsuredzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sinsuredphoneno.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Employer</span></td>
                                                    <td><strong>@semployer.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Address</span></td>
                                                    <td><strong>@sempaddress.ToUpper @sempcity.ToUpper @sempstate.ToUpper @sempzip.ToUpper</strong></td>
                                                </tr>
                                                @<tr>
                                                    <td><span class="note">Phone #</span></td>
                                                    <td><strong>@sempphone.ToUpper</strong></td>
                                                </tr>
                                            End If
                                        End If
                                    </tbody>
                                </table>
                            </div>
                        End If
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div class="table-responsive">
                    <table class="table table-condensed table-bordered">
                        <thead>
                            <tr>
                                <th>
                                    <strong> <i class="fa fa-flask">&nbsp;&nbsp;</i>TESTS/PROFILES</strong>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            @For Each ts In Model.Request_Header_LabTest.Split(Chr(23))
                                Dim t As String() = ts.Split(Chr(4))
                                Dim fa As String = String.Empty
                                'If Model.Request.Type = "HMO" And t(10) = "FOR APPROVAL" Then
                                '    fa = "&nbsp; <span class='label label-warning'>FOR APPROVAL</span>"
                                'End If
                                printabletests &= "<tr><td><strong>" & t(0) & "</strong></td><td><strong>" & t(2) & "</strong></td><td><strong>"
                                @<tr>
                                    <td>
                                        <strong>@t(2) @Html.Raw(fa)</strong>
                                        @If t(6) <> String.Empty Or t(7) <> String.Empty Or t(8) <> String.Empty Or t(9) <> String.Empty Then
                                            @<br />
                                        End If
                                        @If t(6) <> String.Empty Then
                                            printabletests &= t(6).ToUpper & ", "
                                            @<span class="label label-primary">@t(6).ToUpper</span>
                                        End If
                                        @If t(7) <> String.Empty Then
                                            printabletests &= t(7).ToUpper & ", "
                                            @<span class="label label-primary">@t(7).ToUpper</span>
                                        End If
                                        @If t(8) <> String.Empty Then
                                            printabletests &= t(8).ToUpper & ", "
                                            @<span class="label label-primary">@t(8).ToUpper</span>
                                        End If
                                        @If t(9) <> String.Empty Then
                                            printabletests &= t(9).ToUpper & ", "
                                            @<span class="label label-primary">@t(9).ToUpper</span>
                                        End If
                                        @If t(4) <> String.Empty Then
                                            @<br />
                                            @<span class="note">@t(4) &nbsp;<strong>@t(5)</strong></span>
                                        End If
                                    </td>
                                </tr>
                                printabletests = printabletests.Substring(0, printabletests.Length - 2)
                                printabletests &= "</strong></td><td><span class='note'>" & t(4) & "</span><br /><strong>" & t(5) & "</strong></td></tr>"
                            Next
                            @If showpreauth And Model.PreAuthTest <> String.Empty Then
                                printabletests &= "<tr><td colspan='2'><span class='small'>Tests for Pre-authorization</span></td><td colspan='2'><strong>" & Model.PreAuthTest & "</strong><br />"
                                @<tr>
                                    <td>
                                        <span class="note">THE FOLLOWING TESTS NEEDS PRE AUTHORIZATION:</span><br />
                                        <strong>@Model.PreAuthTest</strong>
                                        <br />
                                        @If emailsent Then
                                            printabletests &= "<span class='small'>YOU CHOSE TO GET PRE-AUTHORIZATION E-MAIL</span>"
                                            @<span class="note">YOU CHOSE TO GET PRE-AUTHORIZATION E-MAIL</span>
                                        ElseIf billedtoclient Then
                                            printabletests &= "<span class='small'>YOU CHOSE FOR THE TESTS ABOVE TO BE BILLED TO CLIENT</span>"
                                            @<span class="note">YOU CHOSE FOR THE TESTS ABOVE TO BE BILLED TO CLIENT</span>
                                        End If
                                    </td>
                                </tr>
                                printabletests &= "</strong></td></tr>"
                            End If
                        </tbody>
                    </table>
                </div>
                <br />
                <div Class="table-responsive">
                    <Table Class="table table-condensed table-bordered">
                        <thead>
                            <tr>
                                <th> <strong> <i Class="fa fa-flask">&nbsp;&nbsp;</i>SPECIMEN</strong></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <strong>@Html.Raw(printablespecimens)</strong>
                                </td>
                            </tr>
                        </tbody>
                    </Table>
                </div>
                <br />
                <div class="table-responsive">
                    <table class="table table-condensed table-bordered">
                        <thead>
                            <tr>
                                <th colspan="2"><strong><i class="fa fa-flask">&nbsp;&nbsp;</i>ADDITIONAL INFO</strong></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><span class="note">Remarks</span></td>
                                <td style="text-align: justify;"><strong>@Model.RequestLabTestRemarks.ToUpper</strong></td>
                            </tr>
                            @If Model.RequestType = "Patient" Then

                            Else
                                If Model.PSCIsProcessed Then
                                    @<tr>
                                        <td><span class="note">Collection</span></td>
                                        <td><strong>ABC LAB PATIENT SERVICE CENTER (PSC)</strong></td>
                                    </tr>
                                    @<tr>
                                        <td><span class="note">Processed</span></td>
                                        @If Model.PSCIsProcessed Then
                                            @<td><strong><span class="label label-primary">PROCESSED</span>&nbsp;&nbsp;@Model.PSCProcessedTS.ToString("MMMM d, yyyy H:mm").ToUpper</strong></td>
                                        Else
                                            @<td><span class="label label-warning">NOT YET PROCESSED</span></td>
                                        End If
                                    </tr>
                                    If Model.PSCIsProcessed Then
                                        @<tr>
                                            <td><span class="note">Collection Timestamp</span></td>
                                            <td><strong>@Date.Parse(Model.RequestCollectedTS).ToString("MMMM d, yyyy H:mm").ToUpper</strong></td>
                                        </tr>
                                        @<tr>
                                            <td><span class="note">Collector</span></td>
                                            <td><strong>@Model.RequestCollectedBy.ToUpper</strong></td>
                                        </tr>
                                    End If
                                Else
                                    @<tr>
                                        <td><span class="note">Collection Timestamp</span></td>
                                        <td><strong>@Date.Parse(Model.RequestCollectedTS).ToString("MMMM d, yyyy h:mm tt").ToUpper</strong></td>
                                    </tr>
                                    @<tr>
                                        <td><span class="note">Collector</span></td>
                                        <td><strong>@Model.RequestCollectedBy.ToUpper</strong></td>
                                    </tr>
                                End If
                            End If
                            <tr>
                                <td><span class="note">Referring Doctor</span></td>
                                <td><strong>@String.Format("{0} {1} {2} {3}", Model.DoctorCode.ToUpper, Model.DoctorFName.ToUpper, Model.DoctorMName.ToUpper, Model.DoctorLName.ToUpper)</strong></td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                </div>
            </div>
        </div>
    </div>
</div>
<div id="qrModal" Class="modal fade" role="dialog">
    <div Class="modal-dialog modal-sm">
        <div Class="modal-content">
            <div Class="modal-header">
                <Button type="button" Class="close" data-dismiss="modal">&times;</Button>
                <h4 Class="modal-title" style="margin-left: 10px; text-align: center;"><strong>@Model.DraftCode</strong></h4>
            </div>
            <div class="modal-body">
                <img class="img-responsive" src="@Model.QRCode" style="margin:0 auto;" />
            </div>
            <div class="modal-footer">
                <a href="@Url.Action("QRCode", "Main", New With {.id = Model.ID})" class="btn btn-primary"><strong><i class="fa fa-download"></i></strong></a>
            </div>
        </div>
    </div>
</div>
<script>
    //function GetDYMOPrinters()
    //{   var printers = dymo.label.framework.getPrinters();
    //    if (printers.length == 0)
    //        throw ("No DYMO printers are installed. Install DYMO printers.");
    //    dymo.label.framework.getPrintersAsync().then(function (printers) {
    //        // Successful
    //        console.log(printers);
    //    }).thenCatch(function (error) {
    //        // Error
    //    });
    //    var labelXml = '<?xml version="1.0" encoding="utf-8"?>\
    //                   <DieCutLabel Version="8.0" Units="twips">\
    //                    <PaperOrientation>Landscape</PaperOrientation>\
    //                    <Id>Address</Id>\
    //                    <PaperName>30252 Address</PaperName>\
    //                    <DrawCommands>\
    //                        <RoundRectangle X="0" Y="0" Width="1581" Height="5040" Rx="270" Ry="270" />\
    //                    </DrawCommands>\
    //                    <ObjectInfo>\
    //                    <BarcodeObject>\
    //                        <Name>BARCODE</Name>\
    //                        <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
    //                        <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
    //                        <LinkedObjectName></LinkedObjectName>\
    //                        <Rotation>Rotation0</Rotation>\
    //                        <IsMirrored>False</IsMirrored>\
    //                        <IsVariable>True</IsVariable>\
    //                        <Text></Text>\
    //                        <Type>Code39</Type>\
    //                        <Size>Medium</Size>\
    //                        <TextPosition>Bottom</TextPosition>\
    //                        <TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
    //                        <CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
    //                        <TextEmbedding>None</TextEmbedding>\
    //                        <ECLevel>0</ECLevel>\
    //                        <HorizontalAlignment>Center</HorizontalAlignment>\
    //                        <QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
    //                    </BarcodeObject>\
    //                    <Bounds X="331" Y="178" Width="4260" Height="420" />\
    //                </ObjectInfo>\
    //            </DieCutLabel>';
    //    var label = dymo.label.framework.openLabelXml(labelXml);
    //    label.setObjectText("BARCODE", '000220200');
    //    label.print("DYMO LabelWriter 450");
    //}
    //function frameworkInitShim() {
    //    dymo.label.framework.trace = 1;
    //    dymo.label.framework.init(GetDYMOPrinters);
    //}
    //window.onload = frameworkInitShim;

    @*$(function () {
        function onload() {
            barcode.onclick = function () {
                try
                {
                    var labelXml = '<?xml version="1.0" encoding="utf-8"?>\
                <DieCutLabel Version="8.0" Units="twips">\
                <PaperOrientation>Landscape</PaperOrientation>\
                <Id>Address</Id>\
                <PaperName>30252 Address</PaperName>\
                <DrawCommands/>\
                <ObjectInfo>\
                <TextObject>\
                    <Name>Name</Name>\
                    <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                    <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
                    <LinkedObjectName></LinkedObjectName>\
                    <Rotation>Rotation0</Rotation>\
                    <IsMirrored>False</IsMirrored>\
                    <IsVariable>True</IsVariable>\
                    <HorizontalAlignment>Left</HorizontalAlignment>\
                    <VerticalAlignment>Middle</VerticalAlignment>\
                    <TextFitMode>ShrinkToFit</TextFitMode>\
                    <UseFullFontHeight>True</UseFullFontHeight>\
                    <Verticalized>False</Verticalized>\
                    <StyledText/>\
                </TextObject>\
                <Bounds X="332" Y="150" Width="4455" Height="1260" />\
            </ObjectInfo>\
            <ObjectInfo>\
             <BarcodeObject>\
                 <Name>Barcode</Name>\
                 <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                 <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
                 <LinkedObjectName>BarcodeText</LinkedObjectName>\
                 <Rotation>Rotation0</Rotation>\
                 <IsMirrored>False</IsMirrored>\
                 <IsVariable>True</IsVariable>\
                 <Text>barCode</Text>\
                 <Type>Code128Auto</Type>\
                 <Size>Medium</Size>\
                 <TextPosition>Bottom</TextPosition>\
                 <TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                 <CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                 <TextEmbedding>None</TextEmbedding>\
                 <ECLevel>0</ECLevel>\
                 <HorizontalAlignment>Center</HorizontalAlignment>\
                 <QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
             </BarcodeObject>\
             <Bounds X="324" Y="950" Width="3150" Height="520" />\
         </ObjectInfo>\
        </DieCutLabel>';
                    var label = dymo.label.framework.openLabelXml(labelXml);
                    label.setObjectText('Name', @Model.RequestPLName & ", " & @Model.RequestPFName & ", " & @Model.RequestPMName);
                    label.setObjectText('Barcode', @Model.DraftCode);

                    var printers = dymo.label.framework.getPrinters();
                    if (printers.length == 0)
                        throw alert("No DYMO printers are installed. Install DYMO printers.");

                    var printerName = "";
                    for (var i = 0; i < printers.length; ++i)
                    {
                        var printer = printers[i];
                        if (printer.printerType == "LabelWriterPrinter")
                        {
                            printerName = printer.name;
                            break;
                        }
                    }

                    if (printerName == "")
                        throw alert("No LabelWriter printers found. Install LabelWriter printer");

                    label.print(printerName);
                }
                catch(e)
                {alert(e.message || e);}
            }
        };
        if (window.addEventListener)
            window.addEventListener("load", onload, false);
        else if (window.attachEvent)
            window.attachEvent("onload", onload);
        else
            window.onload = onload;
    });*@

    $(function () {
        $(function onload() {
            $("#barcode").on("click", function () {
                var labelXml = '<?xml version="1.0" encoding="utf-8"?>\
                <DieCutLabel Version="8.0" Units="twips">\
                <PaperOrientation>Landscape</PaperOrientation>\
                <Id>Address</Id>\
                <PaperName>30252 Address</PaperName>\
                <DrawCommands/>\
                <ObjectInfo>\
                <TextObject>\
                    <Name>Name</Name>\
                    <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                    <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
                    <LinkedObjectName></LinkedObjectName>\
                    <Rotation>Rotation0</Rotation>\
                    <IsMirrored>False</IsMirrored>\
                    <IsVariable>True</IsVariable>\
                    <HorizontalAlignment>Left</HorizontalAlignment>\
                    <VerticalAlignment>Middle</VerticalAlignment>\
                    <TextFitMode>ShrinkToFit</TextFitMode>\
                    <UseFullFontHeight>True</UseFullFontHeight>\
                    <Verticalized>False</Verticalized>\
                    <StyledText/>\
                </TextObject>\
                <Bounds X="10" Y="150" Width="1296" Height="566" />\
            </ObjectInfo>\
            <ObjectInfo>\
                <TextObject>\
                    <Name>Birthday</Name>\
                    <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                    <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
                    <LinkedObjectName></LinkedObjectName>\
                    <Rotation>Rotation0</Rotation>\
                    <IsMirrored>False</IsMirrored>\
                    <IsVariable>True</IsVariable>\
                    <HorizontalAlignment>Left</HorizontalAlignment>\
                    <VerticalAlignment>Middle</VerticalAlignment>\
                    <TextFitMode>ShrinkToFit</TextFitMode>\
                    <UseFullFontHeight>True</UseFullFontHeight>\
                    <Verticalized>False</Verticalized>\
                    <StyledText/>\
                </TextObject>\
                <Bounds X="90" Y="150" Width="1296" Height="566" />\
            </ObjectInfo>\
            <ObjectInfo>\
                <TextObject>\
                    <Name>CollectionDateTime</Name>\
                    <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                    <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
                    <LinkedObjectName></LinkedObjectName>\
                    <Rotation>Rotation0</Rotation>\
                    <IsMirrored>False</IsMirrored>\
                    <IsVariable>True</IsVariable>\
                    <HorizontalAlignment>Left</HorizontalAlignment>\
                    <VerticalAlignment>Middle</VerticalAlignment>\
                    <TextFitMode>ShrinkToFit</TextFitMode>\
                    <UseFullFontHeight>True</UseFullFontHeight>\
                    <Verticalized>False</Verticalized>\
                    <StyledText/>\
                </TextObject>\
                <Bounds X="180" Y="150" Width="1296" Height="566" />\
            </ObjectInfo>\
            <ObjectInfo>\
             <BarcodeObject>\
                 <Name>Barcode</Name>\
                 <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
                 <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
                 <LinkedObjectName>BarcodeText</LinkedObjectName>\
                 <Rotation>Rotation0</Rotation>\
                 <IsMirrored>False</IsMirrored>\
                 <IsVariable>True</IsVariable>\
                 <Text>barCode</Text>\
                 <Type>Code128Auto</Type>\
                 <Size>Medium</Size>\
                 <TextPosition>Bottom</TextPosition>\
                 <TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                 <CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
                 <TextEmbedding>None</TextEmbedding>\
                 <ECLevel>0</ECLevel>\
                 <HorizontalAlignment>Center</HorizontalAlignment>\
                 <QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
             </BarcodeObject>\
             <Bounds X="270" Y="150" Width="1296" Height="566" />\
         </ObjectInfo>\
        </DieCutLabel>';
                var name = "Name: " + "@tln" + "," + "@tmn" + "," + "@tfn";
                var DOB = "Birth Day: "+@Model.RequestPDOB.Value.ToString("MM/dd/yyyy");
                var CollectionTS ="Collection Stamp: "+"@Model.RequestCollectedTS.Value.ToString("MM/dd/yyyy HH:mm")";
                var label = dymo.label.framework.openLabelXml(labelXml);
                label.setObjectText('Name', name);
                label.setObjectText('Birthday', DOB)
                label.setObjectText('CollectionDateTime', CollectionTS)
                label.setObjectText('Barcode', "@Model.DraftCode");
                var printers = dymo.label.framework.getPrinters();
                if (printers.length == 0)
                    throw alert("No DYMO printers are installed. Install DYMO printers.");

                var printerName = "";
                for (var i = 0; i < printers.length; ++i) {
                    var printer = printers[i];
                    if (printer.printerType == "LabelWriterPrinter") {
                        printerName = printer.name;
                        break;
                    }
                }

                if (printerName == "")
                    throw alert("No LabelWriter printers found. Install LabelWriter printer");

                label.print(printerName);
            });
            if (window.addEventListener)
                window.addEventListener("load", onload, false);
            else if (window.attachEvent)
                window.attachEvent("onload", onload);
            else
                window.onload = onload;
        });





        $("#print").on("click", function () {
            var w = 600;
            var h = 400;
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;
            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;
            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var mywindow = window.open("", "PRINT", "width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            mywindow.document.open();
            var s = "";
            @If printablebilling2 <> String.Empty Then
            @:s = "<table class='table table-bordered table-condensed'><thead><th colspan='4'><i class='fa fa-money'></i></i>&nbsp;&nbsp;SECONDARY BILLING INFO</td></thead><tbody>@Html.Raw(printablebilling2)</tbody></table>"
                        End If
            mywindow.document.write(""
                + "<!doctype html>"
                + "<html>"
                + "<head><title>@Model.DraftCode</title></head>"
                + "<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css' />"
                + "<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css' />"
                + "<style>"
                + "@@media print { th { background: #ccc; } }"
                + ".table td { padding: 5px 10px !important; }"
                + "</style>"
                + "<body onload='window.print();window.close();'>"
                + "<div class='pull-left'>"
                    + "<h4><strong>@Model.DraftCode.ToUpper</strong></h4>"
                    + "<p>"
                        + "<i class='fa fa-hospital-o'></i></i>&nbsp;&nbsp;@Model.RequestLab.Split(Chr(4))(1).ToUpper<br />"
                        + "<i class='fa fa-user-md'></i></i>&nbsp;&nbsp;@Model.ReferringDoctor<br />"
                        + "<i class='fa fa-clock-o'></i></i>&nbsp;&nbsp;@Model.RequestedTimestamp.ToString("MMMM d, yyyy H:mm").ToUpper<br>"
                        + "<i class='fa fa-flask'></i></i>&nbsp;&nbsp;@printablecount"
                    + "</p>"
                + "</div>"
                + "<img src='@Model.QRCode' class='pull-right' style='margin-bottom: 10px;' />"
                + "<table class='table table-bordered table-condensed'>"
                    + "<thead>"
                        + "<tr><th colspan='4'><i class='fa fa-user'></i></i>&nbsp;&nbsp;PATIENT INFO</th></tr>"
                    + "</thead>"
                    + "<tbody>"
                        + "<tr>"
                            + "<td style='min-width: 100px !important; max-width: 100px !important; width: 100px !important;'><span class='small'>Name</span></td>"
                            + "<td><strong>@Model.RequestPFName.ToUpper @Model.RequestPMName.ToUpper @Model.RequestPLName.ToUpper</strong><br /><span class='small'>MRN: @mrn</span></td>"
                            + "<td style='min-width: 70px !important; max-width: 70px !important; width: 70px !important;'><span class='small'>A.K.A.</span></td>"
                            + "<td><strong>@Model.MRN.ToUpper</strong></td>"
                        + "</tr>"
                        + "<tr>"
                            + "<td><span class='small'>Date of Birth</span></td>"
                            + "<td><strong>@Model.RequestPDOB.Value.ToString("MMMM d, yyyy").ToUpper (@PatAge)</strong></td>"
                            + "<td><span class='small'>Address</span></td>"
                            + "<td style='max-width: 500px;'><strong>@Model.RequestPAddress.ToUpper @Model.City.ToUpper @Model.State.ToUpper @Model.ZipCode.ToUpper</strong></td>"
                        + "</tr>"
                        + "<tr>"
                            + "<td><span class='small'>Gender</span></td>"
                            + "<td><strong>@Model.RequestPGender.ToUpper</strong></td>"
                            + "<td><span class='small'>Phone #</span></td>"
                            + "<td><strong>@Model.RequestPContact.ToUpper</strong></td>"
                        + "</tr>"
                    + "</tbody>"
                + "</table>"
                + "<table class='table table-bordered table-condensed'>"
                    + "<thead>"
                        + "<th colspan='4'><i class='fa fa-money'></i></i>&nbsp;&nbsp;PRIMARY BILLING INFO</td>"
                    + "</thead>"
                    + "<tbody>@Html.Raw(printablebilling)"
                    + "</tbody>"
                + "</table>"
                + s
                + "<table class='table table-bordered table-condensed'>"
                    + "<thead>"
                        + "<tr><th colspan='4'><i class='fa fa-flask'></i></i>&nbsp;&nbsp;TESTS/PROFILES</th></tr>"
                    + "</thead>"
                    + "<tbody>"
                        + "<tr>"
                            + "<td style='min-width: 100px !important; max-width: 100px !important; width: 100px !important;'><span class='small'>Code</span></td>"
                            + "<td style='min-width: 300px !important; max-width: 300px !important; width: 300px !important;'><span class='small'>Name</span></td>"
                            + "<td style='min-width: 300px !important; max-width: 300px !important; width: 300px !important;'><span class='small'>ICD 10 Codes</span></td>"
                            + "<td><span class='small'>Q & A</span></td>"
                        + "</tr>"
                        + "@Html.Raw(printabletests)"
                    + "</tbody>"
                + "</table>"
                + "<table class='table table-bordered table-condensed'>"
                    + "<thead>"
                        + "<tr><th><i class='fa fa-flask'></i></i>&nbsp;&nbsp;SPECIMEN</th></tr>"
                    + "</thead>"
                    + "<tbody>"
                        + "<tr><td><strong>@Html.Raw(printablespecimens)</strong></td></tr>"
                    + "</tbody>"
                + "</table>"
                + "<table class='table table-bordered table-condensed'>"
                    + "<thead>"
                        + "<tr><th colspan='4'><i class='fa fa-flask'></i>&nbsp;&nbsp;ADDITIONAL INFO</th></tr>"
                    + "</thead>"
                    + "<tbody>"
                        + "<tr>"
                            @*+ "<td><span class='small'>ICD for this Request</span></td>"
                            + "<td><strong>@printableicd</strong></td>"*@
                            + "<td style='min-width: 170px !important; max-width: 170px !important; width: 170px !important;'><span class='small'>Remarks</span></td>"
                            + "<td colspan='3' style='text-align: justify;'><strong>@Model.RequestLabTestRemarks.ToUpper</strong></td>"
                        + "</tr>"
                        + "<tr>"
                            + "<td><span class='small'>Collection Timestamp</span></td>"
            //+ "<td><strong>@Date.Parse(Model.RequestCollectedTS).ToString("MMMM d, yyyy h:mm tt").ToUpper</strong></td>"
                            + "<td><strong>@Date.Parse(Model.RequestCollectedTS).ToString("MMMM d, yyyy H:mm").ToUpper</strong></td>"
                            + "<td style='min-width: 100px !important; max-width: 100px !important; width: 100px !important;'><span class='small'>Collector</span></td>"
                            + "<td><strong>@Model.RequestCollectedBy.ToUpper</strong></td>"
                        + "</tr>"
                        + "<tr>"
                            + "<td style='min-width: 170px !important; max-width: 170px !important; width: 170px !important;'><span class='small'>Referring Doctor</span></td>"
                            + "<td colspan='3' style='text-align: justify;'><strong>@Model.DoctorCode.ToUpper @Model.DoctorFName.ToUpper @Model.DoctorMName.ToUpper @Model.DoctorLName.ToUpper</strong></td>"
                        + "</tr>"
                    + "</tbody>"
                + "</table>"
                + "<table class='table table-bordered table-condensed'>"
                    + "<tbody>"
                        + "<tr><td><br><br><center><strong>PATIENT NAME/SIGNATURE</strong></center></td></tr>"
                    + "</tbody>"
                + "</table>"
                + "<span class='small'>*@terms</span>"
                + "</body>"
                + "</html>"
                + "");
            mywindow.document.close();
            mywindow.focus();
        });
    });
</script>

  



