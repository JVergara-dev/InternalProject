Imports System.Web.Mvc
Imports eRequestWebInternal.Models
Imports Support
Imports System.Data.SqlClient
Imports System.Net
Imports MessagingToolkit.QRCode.Codec
Imports System.Drawing
Imports System.IO
Imports HDB.Support
Namespace Controllers
    Public Class MainController
        Inherits Controller
        Dim count As Integer = 0
        <Authorize>
        <OutputCache(Duration:=0)>
        Function index(id? As Integer) As ActionResult
            If id Is Nothing Then Return Redirect(Url.Action("Index", "RequestTable"))

            Dim Code As String = ""
            Dim conn As New SqlConnection(Parameters.eRequestConnectionString)
            conn.Open()
            Dim cmd As New SqlCommand("sp_CheckPSCQueuepPending", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ID", id)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            While rdr.Read
                Code = rdr("Code")
            End While
            conn.Close()
            cmd.Dispose()
            If Code = "" Then
                Return Redirect(Url.Action("ViewOnly", "Main", New With {.id = id}))
                'Else
                '    Return Redirect(Url.Action("ViewOnly", "Main", New With {.id = id}))
            End If

#Region "SelectListItem"
            Dim m As New MainModel
            m.GenderList = New List(Of SelectListItem)
            m.GenderList.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.GenderList.Add(New SelectListItem With {.Value = "Male", .Text = "MALE"})
            m.GenderList.Add(New SelectListItem With {.Value = "Female", .Text = "FEMALE"})

            Dim HMO As New HMOs
            Dim HMOs As List(Of HMOs.HMO) = HMO.SelectHMO
            m.HMOList = New List(Of SelectListItem)
            m.SHMOList = New List(Of SelectListItem)
            m.HMOList.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.SHMOList.Add(New SelectListItem With {.Value = "", .Text = ""})
            For Each item In HMOs
                m.HMOList.Add(New SelectListItem With {.Value = item.Code, .Text = item.Name})
                m.SHMOList.Add(New SelectListItem With {.Value = item.Code, .Text = item.Name})
            Next

            m.RelationshipList = New List(Of SelectListItem)
            m.RelationshipList.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.RelationshipList.Add(New SelectListItem With {.Value = "Self", .Text = "SELF"})
            m.RelationshipList.Add(New SelectListItem With {.Value = "Spouse", .Text = "SPOUSE"})
            m.RelationshipList.Add(New SelectListItem With {.Value = "Child", .Text = "CHILD"})
            m.RelationshipList.Add(New SelectListItem With {.Value = "Other", .Text = "OTHER"})

            m.SRelationshipList = New List(Of SelectListItem)
            m.SRelationshipList.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.SRelationshipList.Add(New SelectListItem With {.Value = "Self", .Text = "SELF"})
            m.SRelationshipList.Add(New SelectListItem With {.Value = "Spouse", .Text = "SPOUSE"})
            m.SRelationshipList.Add(New SelectListItem With {.Value = "Child", .Text = "CHILD"})
            m.SRelationshipList.Add(New SelectListItem With {.Value = "Other", .Text = "OTHER"})

            m.PBilltype = New List(Of SelectListItem)
            m.PBilltype.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.PBilltype.Add(New SelectListItem With {.Value = "Client", .Text = "CLIENT"})
            m.PBilltype.Add(New SelectListItem With {.Value = "Medicare", .Text = "MEDICARE"})
            m.PBilltype.Add(New SelectListItem With {.Value = "Medicaid", .Text = "MEDICAID"})
            m.PBilltype.Add(New SelectListItem With {.Value = "Insurance", .Text = "INSURANCE"})
            m.PBilltype.Add(New SelectListItem With {.Value = "HMO", .Text = "HMO"})
            m.PBilltype.Add(New SelectListItem With {.Value = "Other", .Text = "OTHER"})

            m.SSBilltype = New List(Of SelectListItem)
            m.SSBilltype.Add(New SelectListItem With {.Value = "", .Text = ""})
            'm.SSBilltype.Add(New SelectListItem With {.Value = "Client", .Text = "CLIENT"})
            m.SSBilltype.Add(New SelectListItem With {.Value = "Medicare", .Text = "MEDICARE"})
            m.SSBilltype.Add(New SelectListItem With {.Value = "Medicaid", .Text = "MEDICAID"})
            m.SSBilltype.Add(New SelectListItem With {.Value = "Insurance", .Text = "INSURANCE"})
            m.SSBilltype.Add(New SelectListItem With {.Value = "HMO", .Text = "HMO"})
            m.SSBilltype.Add(New SelectListItem With {.Value = "Other", .Text = "OTHER"})

            Dim Insurances As New Insurances
            Dim Insurance As List(Of Insurances.Insurance) = Insurances.SelectInsurance
            m.InsuranceCompanyList = New List(Of SelectListItem)
            m.SInsuranceCompanyList = New List(Of SelectListItem)
            m.InsuranceCompanyList.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.SInsuranceCompanyList.Add(New SelectListItem With {.Value = "", .Text = ""})
            For Each item In Insurance
                m.InsuranceCompanyList.Add(New SelectListItem With {.Value = item.Code, .Text = item.Name})
                m.SInsuranceCompanyList.Add(New SelectListItem With {.Value = item.Code, .Text = item.Name})
            Next
            m.PMedicaidIns = New List(Of SelectListItem)
            m.PMedicaidIns.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.PMedicaidIns.Add(New SelectListItem With {.Value = "MC", .Text = "MEDICAL"})
            m.PMedicaidIns.Add(New SelectListItem With {.Value = "PE", .Text = "PRESUMPTIVE ELEGIBILITY"})
            m.SMedicaidIns = New List(Of SelectListItem)
            m.SMedicaidIns.Add(New SelectListItem With {.Value = "", .Text = ""})
            m.SMedicaidIns.Add(New SelectListItem With {.Value = "MC", .Text = "MEDICAL"})
            m.SMedicaidIns.Add(New SelectListItem With {.Value = "PE", .Text = "PRESUMPTIVE ELEGIBILITY"})

#End Region
#Region "Old Data List"
            Dim RequestHeaders As New RequestHeaders
            Dim RequestHeader As RequestHeaders.RequestHeader = RequestHeaders.SelectRequestHeader(id)
            m.ID = RequestHeader.ID
            m.DraftCode = RequestHeader.DraftCode
            m.RequestRequestor = RequestHeader.RequestRequestor
            m.RequestLabTestRemarks = RequestHeader.RequestLabTestRemarks
            m.RequestPLName = RequestHeader.RequestPLName
            m.RequestPFName = RequestHeader.RequestPFName
            m.RequestPMName = RequestHeader.RequestPMName
            m.RequestPContact = RequestHeader.RequestPContact
            m.MRN = RequestHeader.MRN
            m.RequestPDOB = RequestHeader.RequestPDOB
            m.RequestPGender = RequestHeader.RequestPGender
            m.RequestPAddress = RequestHeader.RequestPAddress
            m.City = RequestHeader.City
            m.State = RequestHeader.State
            m.ZipCode = RequestHeader.ZipCode
            m.RequestType = RequestHeader.RequestType
            m.Medicare = RequestHeader.Medicare
            m.MedicaID = RequestHeader.MedicaID
            m.InsuranceName = RequestHeader.InsuranceName
            m.InsuranceCode = RequestHeader.InsuranceCode
            m.HMOName = RequestHeader.HMOName
            m.HMOCode = RequestHeader.HMOCode
            m.HMOID = RequestHeader.HMOID
            m.GroupNo = RequestHeader.GroupNo
            m.InsuredID = RequestHeader.InsuredID
            m.Relationship = RequestHeader.Relationship
            m.SSN = RequestHeader.SSN
            m.InsuredLN = RequestHeader.InsuredLN
            m.InsuredMN = RequestHeader.InsuredMN
            m.InsuredFN = RequestHeader.InsuredFN
            m.InsuredDOB = RequestHeader.InsuredDOB
            m.InsuranceAddress = RequestHeader.InsuranceAddress
            m.InsuranceName = RequestHeader.InsuranceName
            m.InsuranceCity = RequestHeader.InsuranceCity
            m.InsuranceState = RequestHeader.InsuranceState
            m.InsuranceZip = RequestHeader.InsuranceZip
            m.InsurancePhoneNO = RequestHeader.InsurancePhoneNO
            m.Employer = RequestHeader.Employer
            m.EmployerAddress = RequestHeader.EmployerAddress
            m.EmployerCity = RequestHeader.EmployerCity
            m.EmployerState = RequestHeader.EmployerState
            m.EmployerZip = RequestHeader.EmployerZip
            m.EmployerPhone = RequestHeader.EmployerPhone
            m.SBilltype = RequestHeader.SBilltype
            m.SMedicareNo = RequestHeader.SMedicareNo
            m.SMedicaID = RequestHeader.SMedicaID
            m.SInsuranceCompanyName = RequestHeader.SInsuranceCompanyName
            m.SHMO = RequestHeader.SHMO
            m.SHMOName = RequestHeader.SHMOName
            m.SGroupNo = RequestHeader.SGroupNo
            m.SInsuredID = RequestHeader.SInsuredID
            m.SRelationship = RequestHeader.SRelationship
            m.SSSN = RequestHeader.SSSN
            m.SInsuredLName = RequestHeader.SInsuredLName
            m.SInsuredMName = RequestHeader.SInsuredMName
            m.SInsuredFName = RequestHeader.SInsuredFName
            m.SInsuredDOB = RequestHeader.SInsuredDBO
            m.SInsuranceAddress = RequestHeader.SInsuranceAdress
            m.SInsuranceCity = RequestHeader.SInsuranceCity
            m.SInsuranceState = RequestHeader.SInsuranceState
            m.SInsuranceZip = RequestHeader.SInsuranceZip
            m.SInsurancePhoneNo = RequestHeader.SInsurancePhoneNo
            m.SEmployer = RequestHeader.SEmployer
            m.SEmployerAddress = RequestHeader.SEmployerAdress
            m.SEmployerCity = RequestHeader.SEmployerCity
            m.SEmployerState = RequestHeader.SEmployerState
            m.SEmployerZip = RequestHeader.SEmployerZip
            m.SEmployerPhone = RequestHeader.SEmployerPhone
            m.RequestLabTest = RequestHeader.RequestLabTest
            m.RequestCollectedBy = RequestHeader.RequestCollectedBy
            m.RequestCollectedTS = IIf(IsDBNull(RequestHeader.RequestCollectedTS), "1900-01-01", RequestHeader.RequestCollectedTS)
            m.RequestSpecimen = RequestHeader.RequestSpecimen
            m.Secondary = RequestHeader.Secondary
            m.RequestClient = RequestHeader.RequestClient
            m.DoctorCode = RequestHeader.DoctorCode
            m.DoctorFName = RequestHeader.DoctorFName
            m.DoctorLName = RequestHeader.DoctorLName
            m.DoctorMName = RequestHeader.DoctorMName
            m.RequestLab = RequestHeader.RequestLab
            m.ShowPreAuth = RequestHeader.ShowPreAuth
            m.RequestLabTestRemarks = RequestHeader.RequestLabTestRemarks
            m.BillToClient = RequestHeader.BillToClient
            m.RequestCollectedTS = DateTime.Today
            m.Specimens = New List(Of MainModel.Specimen)
            For Each item In m.RequestSpecimen.Split(Chr(23))
                If item.Split(Chr(4))(0) <> "" Then
                    Dim MSpec As New MainModel.Specimen
                    MSpec.SpecCode = item.Split(Chr(4))(0)
                    MSpec.SpecDesc = item.Split(Chr(4))(1)
                    MSpec.SpecQuantity = item.Split(Chr(4))(2)
                    m.Specimens.Add(MSpec)
                End If
            Next

            m.Tests = New List(Of MainModel.Test)
            For Each item In m.RequestLabTest.Split(Chr(23))
                Dim MTest As New MainModel.Test
                MTest.TestCode = item.Split(Chr(4))(0)
                MTest.LabelTest = item.Split(Chr(4))(1)
                MTest.TestName = item.Split(Chr(4))(2)
                MTest.TestZero = item.Split(Chr(4))(3)
                MTest.TestQuestion = item.Split(Chr(4))(4)
                MTest.TestAnswer = item.Split(Chr(4))(5)
                MTest.ICD1 = item.Split(Chr(4))(6)
                MTest.ICD2 = item.Split(Chr(4))(7)
                MTest.ICD3 = item.Split(Chr(4))(8)
                MTest.ICD4 = item.Split(Chr(4))(9)
                MTest.TestSite = item.Split(Chr(4))(10)
                m.Tests.Add(MTest)
            Next

            m.AddTestList = New List(Of MainModel.AdditionalTest)
            If RequestHeader.Additional IsNot Nothing And RequestHeader.Additional <> "" Then
                For Each item In RequestHeader.Additional.Split(Chr(23))
                    Dim MATest As New MainModel.AdditionalTest
                    MATest.AddCode = item.Split(Chr(4))(0)
                    MATest.AddDesc = item.Split(Chr(4))(1)
                    m.AddTestList.Add(MATest)
                Next
            End If

            HDB.Support.PRMs.ConnString = Parameters.eRequestPRMS
            Dim ds As New DOCSETs
            Dim d As DOCSETs.DOCSET = ds.GetByID(m.RequestRequestor)
            If d Is Nothing Then
                m.NPI = String.Empty
            Else
                m.NPI = d.NPI
            End If
#End Region
            If count = 0 Then
                TempData("oldLogs") = m
            End If
            TempData("oldData") = m
            Return View(m)
        End Function
        <Authorize>
        <HttpPost>
        <OutputCache(Duration:=0)>
        Function index(PostModel As MainModel, Optional RequestCollectedTS As String = "") As ActionResult
            Dim CurrentData As MainModel = TempData("oldData")
            Dim id As String = TempData("oldData").ID
            ModelState.Clear()
#Region "Checker"
            If PostModel.RequestType = String.Empty Then
                ModelState.AddModelError("RequestType", "BILL TYPE IS REQUIRED")
            End If
            If PostModel.RequestCollectedBy = String.Empty Then
                ModelState.AddModelError("RequestCollectedBy", "COLLECTOR IS REQUIRED")
            End If
            If PostModel.RequestPLName = String.Empty Then
                ModelState.AddModelError("RequestPLName", "LAST NAME IS REQUIRED")
            End If
            If PostModel.RequestPFName = String.Empty Then
                ModelState.AddModelError("RequestPFName", "FIRST NAME IS REQUIRED")
            End If
            'If PostModel.RequestPMName = String.Empty Then
            '    ModelState.AddModelError("RequestPMName", "MIDDLE INITIAL IS REQUIRED")
            'End If
            If PostModel.RequestPGender = String.Empty Then
                ModelState.AddModelError("RequestPGender", "GENDER IS REQUIRED")
            End If
            If PostModel.RequestPAddress = String.Empty Then
                ModelState.AddModelError("RequestPAddress", "ADDRESS IS REQUIRED")
            End If
            If PostModel.City = String.Empty Then
                ModelState.AddModelError("City", "CITY IS REQUIRED")
            End If
            If PostModel.State = String.Empty Then
                ModelState.AddModelError("State", "STATE IS REQUIRED")
            End If
            If PostModel.ZipCode = String.Empty Then
                ModelState.AddModelError("ZipCode", "ZIPCODE IS REQUIRED")
            End If
            If PostModel.RequestPContact = String.Empty Then
                ModelState.AddModelError("RequestPContact", "PHONE # IS REQUIRED")
            End If

            Select Case PostModel.RequestType
                Case "Other"
#Region "Other"
                    If PostModel.InsuredID = String.Empty Then
                        ModelState.AddModelError("InsuredID", "INSURED ID IS INVALID")
                    End If
                    If PostModel.GroupNo = String.Empty Then
                        ModelState.AddModelError("GroupNo", "GROUP # IS INVALID")
                    End If
                    If PostModel.Relationship = String.Empty Then
                        ModelState.AddModelError("Relationship", "RELATIONSHIP IS INVALID")
                    End If

                    If PostModel.Relationship <> String.Empty And PostModel.Relationship <> "Self" Then
                        If PostModel.InsuredLN = String.Empty Then
                            ModelState.AddModelError("InsuredLN", "INSURED LAST NAME IS INVALID")
                        End If
                        If PostModel.InsuredFN = String.Empty Then
                            ModelState.AddModelError("InsuredFN", "INSURED FIRST NAME IS INVALID")
                        End If
                        Dim sd As Date = Date.Parse("0001-01-01")
                        If PostModel.InsuredDOB IsNot Nothing Then
                            If Not PostModel.InsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                sd = Date.Parse(PostModel.InsuredDOB)
                            End If
                        End If

                        If PostModel.InsuranceAddress = String.Empty Then
                            ModelState.AddModelError("InsuranceAddress", "ADDRESS IS REQUIRED")
                        End If
                        If PostModel.InsuranceCity = String.Empty Then
                            ModelState.AddModelError("InsuranceCity", "CITY IS REQUIRED")
                        End If
                        If PostModel.InsuranceState = String.Empty Then
                            ModelState.AddModelError("InsuranceState", "STATE IS REQUIRED")
                        End If
                        If PostModel.InsuranceZip = String.Empty Then
                            ModelState.AddModelError("InsuranceZip", "ZIPCODE IS REQUIRED")
                        End If
                        If PostModel.InsurancePhoneNO = String.Empty Then
                            ModelState.AddModelError("InsurancePhoneNO", "PHONE # IS REQUIRED")
                        End If
                    End If
                    Select Case PostModel.SBilltype
                        Case "Medicare"
                            If PostModel.SMedicareNo = String.Empty Then
                                ModelState.AddModelError("SMedicareNo", "SECONDARY MEDICARE NO IS INVALID")
                            End If
                            If PostModel.SRelationship <> "Self" Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                        Case "Medicaid"
                            If PostModel.SMedicaID = String.Empty Then
                                ModelState.AddModelError("SMedicaID", "MEDICAID NUMBER IS INVALID")
                            End If

                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If

                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "RELATIONSHIP IS INVALID")
                            End If

                            If PostModel.SRelationship <> "Self" And PostModel.SRelationship <> String.Empty Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If

                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Insurance"
                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.InsurancePhoneNO = String.Empty Then
                                    ModelState.AddModelError("InsurancePhoneNO", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Other"
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "HMO"
                            If PostModel.SHMOName = String.Empty Then
                                ModelState.AddModelError("SHMOName", "SECONDARY HMO IS INVALID")
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                    End Select
#End Region
                Case "Medicare"
#Region "Medicare"
                    If PostModel.Medicare = String.Empty Then
                        ModelState.AddModelError("Medicare", "MEDICARE NUMBER IS INVALID")
                    End If
                    If PostModel.Relationship <> "Self" Then
                        ModelState.AddModelError("Relationship", "RELATIONSHIP IS INVALID")
                    End If
                    Select Case PostModel.SBilltype
                        Case "Medicare"
                            ModelState.AddModelError("SBilltype", "SECONDARY BILL TO IS INVALID")
                        Case "Medicaid"
                            If PostModel.SMedicareNo Is Nothing Then PostModel.SMedicareNo = ""

                            If PostModel.SMedicaID = String.Empty Then
                                ModelState.AddModelError("SMedicaID", "MEDICAID NUMBER IS INVALID")
                            End If

                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If

                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "RELATIONSHIP IS INVALID")
                            End If

                            If PostModel.SRelationship <> "Self" And PostModel.SRelationship <> String.Empty Then

                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If

                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If

                        Case "Insurance"
                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Other"
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "HMO"
                            If PostModel.SHMOName = String.Empty Then
                                ModelState.AddModelError("SHMOName", "SECONDARY HMO IS INVALID")
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case Else

                    End Select
#End Region
                Case "Medicaid"
#Region "Medicaid"
                    If PostModel.MedicaID = String.Empty Then
                        ModelState.AddModelError("MedicaID", "MEDICAID NUMBER IS INVALID")
                    End If
                    If PostModel.RequestType = "Medicaid" Then
                        If PostModel.SMedicaidInsIndex = String.Empty Then
                            ModelState.AddModelError("PMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                        End If
                    ElseIf PostModel.RequestType <> "" Or PostModel.RequestType IsNot Nothing Then
                        If PostModel.InsuranceCode = String.Empty Then
                            ModelState.AddModelError("InsuranceCode", "INSURANCE COMPANY IS INVALID")
                        End If
                    End If

                    If PostModel.Relationship = String.Empty Then
                        ModelState.AddModelError("Relationship", "RELATIONSHIP IS INVALID")
                    End If

                    If PostModel.Relationship <> "Self" And PostModel.Relationship <> String.Empty Then
                        If PostModel.InsuredLN = String.Empty Then
                            ModelState.AddModelError("InsuredLN", "INSURED LAST NAME IS INVALID")
                        End If
                        If PostModel.InsuredFN = String.Empty Then
                            ModelState.AddModelError("InsuredFN", "INSURED FIRST NAME IS INVALID")
                        End If
                        Dim sd As Date = Date.Parse("0001-01-01")
                        If PostModel.InsuredDOB IsNot Nothing Then
                            If Not PostModel.InsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                sd = Date.Parse(PostModel.InsuredDOB)
                            End If
                        End If
                        If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                            ModelState.AddModelError("InsuredDOB", "BIRTH DATE IS INVALID")
                        End If
                        If PostModel.InsuranceAddress = String.Empty Then
                            ModelState.AddModelError("InsuranceAddress", "ADDRESS IS REQUIRED")
                        End If
                        If PostModel.InsuranceCity = String.Empty Then
                            ModelState.AddModelError("InsuranceCity", "CITY IS REQUIRED")
                        End If
                        If PostModel.InsuranceState = String.Empty Then
                            ModelState.AddModelError("InsuranceState", "STATE IS REQUIRED")
                        End If
                        If PostModel.InsuranceZip = String.Empty Then
                            ModelState.AddModelError("InsuranceZip", "ZIPCODE IS REQUIRED")
                        End If
                        If PostModel.InsurancePhoneNO = String.Empty Then
                            ModelState.AddModelError("InsurancePhoneNO", "PHONE # IS REQUIRED")
                        End If
                    End If

                    'check secondary insurance
                    Select Case PostModel.SBilltype
                        Case "Medicaid"
                            ModelState.AddModelError("SBilltype", "SECONDARY BILL TO IS INVALID")
                        Case "Medicare"
                            If PostModel.SMedicareNo = String.Empty Then
                                ModelState.AddModelError("SMedicareNo", "SECONDARY MEDICARE NO IS INVALID")
                            End If
                            If PostModel.SRelationship <> "Self" Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                                ModelState.AddModelError("SRelationship1", "EXCLUDE" & Chr(4) & "MEDICARE ONLY ACCEPTS RELATIONSHIP AS SELF")
                            End If
                                        'ModelState.AddModelError("SBilltype", "SECONDARY BILL TO IS INVALID")
                                        'ModelState.AddModelError("Medicaid1", "EXCLUDE" & Chr(4) & "MEDICARE CANNOT BE USED AS A SECONDARY BILL TO IF MEDICAID IS USED AS PRIMARY")
                        Case "Medicaid"
                            ModelState.AddModelError("SBilltype", "SECONDARY BILL TO IS INVALID")
                            ModelState.AddModelError("Medicaid1", "EXCLUDE" & Chr(4) & "MEDICAID CANNOT BE USED AS A SECONDARY BILL TO IF MEDICAID IS USED AS PRIMARY")
                        Case "Insurance"
                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                'If postmodel.SecondaryInsuredMN = String.Empty Then
                                'Else
                                '    postmodel.SecondaryInsuredMN = postmodel.SecondaryInsuredMN
                                'End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Other"
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                'If postmodel.SecondaryInsuredMN = String.Empty Then
                                'Else
                                '    postmodel.SecondaryInsuredMN = postmodel.SecondaryInsuredMN
                                'End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "HMO"
                            If PostModel.SHMOName = String.Empty Then
                                ModelState.AddModelError("SHMOName", "SECONDARY HMO IS INVALID")
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case Else
                    End Select
#End Region
                Case "Insurance"
#Region "Insurance"
                    If PostModel.RequestType = "Medicaid" Then
                        If PostModel.PMedicaidInsIndex = String.Empty Then
                            ModelState.AddModelError("PMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                        End If
                    Else
                        If PostModel.InsuranceCode = String.Empty Then
                            ModelState.AddModelError("InsuranceCode", "INSURANCE COMPANY IS INVALID")
                        End If
                    End If
                    'If PostModel.InsuranceCode = String.Empty Then
                    '    ModelState.AddModelError("InsuranceCode", "INSURANCE COMPANY IS INVALID")
                    'End If

                    If PostModel.InsuredID = String.Empty Then
                        ModelState.AddModelError("InsuredID", "INSURED ID IS INVALID")
                    End If
                    If PostModel.GroupNo = String.Empty Then
                        ModelState.AddModelError("GroupNo", "GROUP # IS INVALID")
                    End If
                    If PostModel.Relationship = String.Empty Then
                        ModelState.AddModelError("Relationship", "RELATIONSHIP IS INVALID")
                    End If

                    If PostModel.Relationship <> String.Empty And PostModel.Relationship <> "Self" Then
                        If PostModel.InsuredLN = String.Empty Then
                            ModelState.AddModelError("InsuredLN", "INSURED LAST NAME IS INVALID")
                        End If
                        If PostModel.InsuredFN = String.Empty Then
                            ModelState.AddModelError("InsuredFN", "INSURED FIRST NAME IS INVALID")
                        Else
                            PostModel.InsuredFN = PostModel.InsuredFN
                        End If
                        'If postmodel.InsuredMN = String.Empty Then
                        'Else
                        '    postmodel.InsuredMN = postmodel.InsuredMN
                        'End If
                        Dim sd As Date = Date.Parse("0001-01-01")
                        If PostModel.InsuredDOB IsNot Nothing Then
                            If Not PostModel.InsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                sd = Date.Parse(PostModel.InsuredDOB)
                            End If
                        End If
                        Dim a As String = Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date
                        Dim b As String = Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year
                        Dim c As String = sd.Date.Year
                        If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                            ModelState.AddModelError("InsuredDOB", "BIRTH DATE IS INVALID")
                        End If
                        If PostModel.InsuranceAddress = String.Empty Then
                            ModelState.AddModelError("InsuranceAddress", "ADDRESS IS REQUIRED")
                        End If
                        If PostModel.InsuranceCity = String.Empty Then
                            ModelState.AddModelError("InsuranceCity", "CITY IS REQUIRED")
                        End If
                        If PostModel.InsuranceState = String.Empty Then
                            ModelState.AddModelError("InsuranceState", "STATE IS REQUIRED")
                        End If
                        If PostModel.InsuranceZip = String.Empty Then
                            ModelState.AddModelError("InsuranceZip", "ZIPCODE IS REQUIRED")
                        End If
                        If PostModel.InsurancePhoneNO = String.Empty Then
                            ModelState.AddModelError("InsurancePhoneNO", "PHONE # IS REQUIRED")
                        End If
                    End If

                    'check secondary insurance
                    Select Case PostModel.SBilltype
                        Case "Insurance"
                            ModelState.AddModelError("SBilltype", "SECONDARY BILL TO IS INVALID")
                        Case "Medicare"
                            If PostModel.SMedicareNo = String.Empty Then
                                ModelState.AddModelError("SMedicareNo", "SECONDARY MEDICARE NO IS INVALID")
                            End If
                            If PostModel.SRelationship <> "Self" Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                        Case "Medicaid"
                            If PostModel.SMedicaID Is Nothing Then PostModel.SMedicaID = String.Empty
                            PostModel.SMedicaID = PostModel.SMedicaID.Trim

                            If PostModel.SMedicaID = String.Empty Then
                                ModelState.AddModelError("SMedicaID", "MEDICAID NUMBER IS INVALID")
                            End If

                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If

                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "RELATIONSHIP IS INVALID")
                            End If

                            If PostModel.SRelationship <> "Self" And PostModel.SRelationship <> String.Empty Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If

                            End If
                        Case "Insurance"
                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Other"
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "HMO"
                            If PostModel.SHMOName = String.Empty Then
                                ModelState.AddModelError("SHMOName", "SECONDARY HMO IS INVALID")
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case Else
                    End Select
#End Region
                Case "HMO"
#Region "HMO"
                    If PostModel.HMOCode = String.Empty Then
                        ModelState.AddModelError("HMOCode", "HMO IS INVALID")
                    End If
                    If PostModel.InsuredID = String.Empty Then
                        ModelState.AddModelError("InsuredID", "INSURED ID IS INVALID")
                    End If
                    If PostModel.GroupNo = String.Empty Then
                        ModelState.AddModelError("GroupNo", "GROUP # IS INVALID")
                    End If
                    If PostModel.Relationship = String.Empty Then
                        ModelState.AddModelError("Relationship", "RELATIONSHIP IS INVALID")
                    End If

                    If PostModel.Relationship <> String.Empty And PostModel.Relationship <> "Self" Then
                        If PostModel.InsuredLN = String.Empty Then
                            ModelState.AddModelError("InsuredLN", "INSURED LAST NAME IS INVALID")
                        End If
                        If PostModel.InsuredFN = String.Empty Then
                            ModelState.AddModelError("InsuredFN", "INSURED FIRST NAME IS INVALID")
                        End If
                        'If postmodel.InsuredMN = String.Empty Then
                        'Else
                        '    postmodel.InsuredMN = postmodel.InsuredMN
                        'End If
                        Dim sd As Date = Date.Parse("0001-01-01")
                        If PostModel.InsuredDOB IsNot Nothing Then
                            If Not PostModel.InsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                sd = Date.Parse(PostModel.InsuredDOB)
                            End If
                        End If

                        If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                            ModelState.AddModelError("InsuredDOB", "BIRTH DATE IS INVALID")
                        End If
                        If PostModel.InsuranceAddress = String.Empty Then
                            ModelState.AddModelError("InsuranceAddress", "ADDRESS IS REQUIRED")
                        End If
                        If PostModel.InsuranceCity = String.Empty Then
                            ModelState.AddModelError("InsuranceCity", "CITY IS REQUIRED")
                        End If
                        If PostModel.InsuranceState = String.Empty Then
                            ModelState.AddModelError("InsuranceState", "STATE IS REQUIRED")
                        End If
                        If PostModel.InsuranceZip = String.Empty Then
                            ModelState.AddModelError("InsuranceZip", "ZIPCODE IS REQUIRED")
                        End If
                        If PostModel.InsurancePhoneNO = String.Empty Then
                            ModelState.AddModelError("InsurancePhoneNO", "PHONE # IS REQUIRED")
                        End If
                    End If

                    'check secondary insurance
                    Select Case PostModel.SBilltype
                        Case "HMO"
                            ModelState.AddModelError("SBilltype", "SECONDARY BILL TO IS INVALID")
                        Case "Medicare"
                            If PostModel.SMedicareNo = String.Empty Then
                                ModelState.AddModelError("SMedicareNo", "SECONDARY MEDICARE NO IS INVALID")
                            End If
                            If PostModel.SRelationship <> "Self" Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                        Case "Medicaid"
                            If PostModel.SMedicaID Is Nothing Then PostModel.SMedicaID = String.Empty
                            PostModel.SMedicaID = PostModel.SMedicaID.Trim

                            If PostModel.SMedicaID = String.Empty Then
                                ModelState.AddModelError("SMedicaID", "MEDICAID NUMBER IS INVALID")
                            End If

                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If

                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "RELATIONSHIP IS INVALID")
                            End If

                            If PostModel.SRelationship <> "Self" And PostModel.SRelationship <> String.Empty Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Insurance"
                            If PostModel.SBilltype = "Medicaid" Then
                                If PostModel.SMedicaidInsIndex = String.Empty Then
                                    ModelState.AddModelError("SMedicaidInsIndex", "INSURANCE COMPANY IS INVALID")
                                End If
                            ElseIf PostModel.SBilltype <> "" Or PostModel.SBilltype IsNot Nothing Then
                                If PostModel.SInsuranceCompanyName = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCompanyName", "INSURANCE COMPANY IS INVALID")
                                End If
                            End If

                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "Other"

                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case "HMO"
                            If PostModel.SHMOName = String.Empty Then
                                ModelState.AddModelError("SHMOName", "SECONDARY HMO IS INVALID")
                            End If
                            If PostModel.SInsuredID = String.Empty Then
                                ModelState.AddModelError("SInsuredID", "SECONDARY INSURED ID IS INVALID")
                            End If
                            If PostModel.SGroupNo = String.Empty Then
                                ModelState.AddModelError("SGroupNo", "SECONDARY GROUP # IS INVALID")
                            End If
                            If PostModel.SRelationship = String.Empty Then
                                ModelState.AddModelError("SRelationship", "SECONDARY RELATIONSHIP IS INVALID")
                            End If
                            If PostModel.SRelationship <> String.Empty And PostModel.SRelationship <> "Self" Then
                                If PostModel.SInsuredLName = String.Empty Then
                                    ModelState.AddModelError("SInsuredLName", "SECONDARY INSURED LAST NAME IS INVALID")
                                End If
                                If PostModel.SInsuredFName = String.Empty Then
                                    ModelState.AddModelError("SInsuredFName", "SECONDARY INSURED FIRST NAME IS INVALID")
                                End If
                                Dim sd As Date = Date.Parse("0001-01-01")
                                If PostModel.SInsuredDOB IsNot Nothing Then
                                    If Not PostModel.SInsuredDOB.ToString.Substring(0, 8) = "1/1/0001" Then
                                        sd = Date.Parse(PostModel.SInsuredDOB)
                                    End If
                                Else
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DAY IS INVALID")
                                End If
                                If sd.Date > Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Date OrElse Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0)).Year - sd.Date.Year > 123 Then
                                    ModelState.AddModelError("SInsuredDOB", "BIRTH DATE IS INVALID")
                                End If
                                If PostModel.SInsuranceAddress = String.Empty Then
                                    ModelState.AddModelError("SInsuranceAddress", "ADDRESS IS REQUIRED")
                                End If
                                If PostModel.SInsuranceCity = String.Empty Then
                                    ModelState.AddModelError("SInsuranceCity", "CITY IS REQUIRED")
                                End If
                                If PostModel.SInsuranceState = String.Empty Then
                                    ModelState.AddModelError("SInsuranceState", "STATE IS REQUIRED")
                                End If
                                If PostModel.SInsuranceZip = String.Empty Then
                                    ModelState.AddModelError("SInsuranceZip", "ZIPCODE IS REQUIRED")
                                End If
                                If PostModel.SInsurancePhoneNo = String.Empty Then
                                    ModelState.AddModelError("SInsurancePhoneNo", "PHONE # IS REQUIRED")
                                End If
                            End If
                        Case Else
                    End Select
#End Region
                Case "Client"
#Region "Client"
                    If PostModel.SBilltype = "Client" Then
                        ModelState.AddModelError("SBilltype", "SECONDARY BILL TYPE IS INVALID")
                    End If
                    PostModel.Medicare = String.Empty
                    PostModel.MedicaID = String.Empty

                    'PostModel.MedicaidInsurance = String.Empty

                    PostModel.InsuranceCode = String.Empty
                    PostModel.PMedicaidInsIndex = String.Empty
                    PostModel.HMOCode = String.Empty
                    PostModel.HMOID = String.Empty
                    PostModel.HMOName = String.Empty
                    PostModel.GroupNo = String.Empty
                    PostModel.Relationship = String.Empty
                    PostModel.InsuredLN = String.Empty
                    PostModel.InsuredFN = String.Empty
                    PostModel.InsuredMN = String.Empty
                    PostModel.InsuredID = String.Empty
                    'PostModel.InsuredDOB = Date.Parse("01-01-0001")
                    PostModel.InsurancePhoneNO = String.Empty
                    PostModel.InsuranceAddress = String.Empty
                    PostModel.InsuranceCity = String.Empty
                    PostModel.InsuranceState = String.Empty
                    PostModel.InsuranceZip = String.Empty
                    PostModel.Employer = String.Empty
                    PostModel.EmployerAddress = String.Empty
                    PostModel.EmployerCity = String.Empty
                    PostModel.EmployerState = String.Empty
                    PostModel.EmployerZip = String.Empty
                    PostModel.EmployerPhone = String.Empty

                    PostModel.SBilltype = String.Empty
                    PostModel.SMedicareNo = String.Empty
                    PostModel.SMedicaID = String.Empty

                    'PostModel.SecondaryMedicaidInsurance = String.Empty

                    PostModel.SInsuranceCompanyName = String.Empty
                    PostModel.SMedicaidInsuranceName = String.Empty
                    PostModel.shmoname = Nothing
                    PostModel.SGroupNo = String.Empty
                    PostModel.SRelationship = String.Empty
                    PostModel.SInsuredFName = String.Empty
                    PostModel.SInsuredLName = String.Empty
                    PostModel.SInsuredMName = String.Empty
                    PostModel.SInsuredID = String.Empty
                    'PostModel.SInsuredDOB = Date.Parse("01-01-0001")
                    PostModel.SInsurancePhoneNo = String.Empty
                    PostModel.SInsuranceAddress = String.Empty
                    PostModel.SInsuranceCity = String.Empty
                    PostModel.SInsuranceState = String.Empty
                    PostModel.SInsuranceZip = String.Empty
                    PostModel.SEmployerAddress = String.Empty
                    PostModel.SEmployerCity = String.Empty
                    PostModel.SEmployerState = String.Empty
                    PostModel.SEmployerZip = String.Empty
                    PostModel.SEmployerPhone = String.Empty

#End Region
            End Select


#End Region
#Region "Current Data List"
            'CurrentData.NPI = PostModel.NPI
            Try
                CurrentData.RequestCollectedTS = Convert.ToDateTime(RequestCollectedTS)
            Catch ex As Exception
                ModelState.AddModelError("RequestCollectedTS", "Invalid Date")
            End Try
            CurrentData.PMedicaidInsIndex = PostModel.PMedicaidInsIndex
            CurrentData.SMedicaidInsIndex = PostModel.SMedicaidInsIndex
            CurrentData.RequestType = PostModel.RequestType
            CurrentData.SBilltype = IIf(PostModel.SBilltype Is Nothing, "", PostModel.SBilltype)
            CurrentData.RequestLabTestRemarks = PostModel.RequestLabTestRemarks
            CurrentData.InsuranceName = PostModel.InsuranceName
            CurrentData.RequestPLName = PostModel.RequestPLName
            CurrentData.RequestPFName = PostModel.RequestPFName
            CurrentData.RequestPMName = IIf(PostModel.RequestPMName Is Nothing, "", PostModel.RequestPMName)
            CurrentData.MRN = PostModel.MRN
            CurrentData.RequestPDOB = PostModel.RequestPDOB
            CurrentData.RequestPGender = PostModel.RequestPGender
            CurrentData.RequestPAddress = PostModel.RequestPAddress
            CurrentData.City = PostModel.City
            CurrentData.State = PostModel.State
            CurrentData.ZipCode = PostModel.ZipCode
            CurrentData.RequestPContact = PostModel.RequestPContact
            CurrentData.Medicare = PostModel.Medicare
            CurrentData.MedicaID = PostModel.MedicaID
            If PostModel.InsuranceCode Is Nothing Then
                If PostModel.PMedicaidInsIndex <> String.Empty Then
                    CurrentData.InsuranceCode = PostModel.PMedicaidInsIndex
                Else
                    CurrentData.InsuranceCode = PostModel.InsuranceCode
                End If
            Else
                CurrentData.InsuranceCode = PostModel.InsuranceCode
            End If
            'CurrentData.InsuranceCode = IIf(PostModel.InsuranceCode Is Nothing, "", PostModel.InsuranceCode)
            CurrentData.HMOName = PostModel.HMOName
            CurrentData.HMOCode = IIf(PostModel.HMOCode Is Nothing, "", PostModel.HMOCode)
            CurrentData.GroupNo = PostModel.GroupNo
            CurrentData.InsuredID = PostModel.InsuredID
            CurrentData.Relationship = IIf(PostModel.Relationship Is Nothing, "", PostModel.Relationship)
            CurrentData.SSN = PostModel.SSN
            CurrentData.InsuredLN = PostModel.InsuredLN
            CurrentData.InsuredFN = PostModel.InsuredFN
            CurrentData.InsuredMN = IIf(PostModel.InsuredMN Is Nothing, "", PostModel.InsuredMN)
            CurrentData.InsuredDOB = PostModel.InsuredDOB
            CurrentData.InsuranceAddress = PostModel.InsuranceAddress
            CurrentData.InsuranceCity = PostModel.InsuranceCity
            CurrentData.InsuranceState = PostModel.InsuranceState
            CurrentData.InsuranceZip = PostModel.InsuranceZip
            CurrentData.InsurancePhoneNO = PostModel.InsurancePhoneNO
            CurrentData.Employer = PostModel.Employer
            CurrentData.EmployerAddress = PostModel.EmployerAddress
            CurrentData.EmployerCity = PostModel.EmployerCity
            CurrentData.EmployerState = PostModel.EmployerState
            CurrentData.EmployerZip = PostModel.EmployerZip
            CurrentData.EmployerPhone = PostModel.EmployerPhone
            CurrentData.SMedicareNo = PostModel.SMedicareNo
            CurrentData.SMedicaID = IIf(PostModel.SMedicaID Is Nothing, "", PostModel.SMedicaID)
            'CurrentData.SInsuranceCompanyName = PostModel.SInsuranceCompanyName

            If PostModel.SInsuranceCompanyName Is Nothing Then
                If PostModel.SMedicaidInsIndex <> String.Empty Then
                    CurrentData.SInsuranceCompanyName = PostModel.SMedicaidInsIndex
                Else
                    CurrentData.SInsuranceCompanyName = PostModel.SInsuranceCompanyName
                End If
            Else
                CurrentData.SInsuranceCompanyName = PostModel.SInsuranceCompanyName
            End If
            CurrentData.SHMOName = IIf(PostModel.SHMOName Is Nothing, "", PostModel.SHMOName)
            CurrentData.SGroupNo = PostModel.SGroupNo
            CurrentData.SInsuredID = PostModel.SInsuredID
            CurrentData.SRelationship = IIf(PostModel.SRelationship Is Nothing, "", PostModel.SRelationship)
            CurrentData.SSSN = PostModel.SSSN
            CurrentData.SInsuredLName = PostModel.SInsuredLName
            CurrentData.SInsuredFName = PostModel.SInsuredFName
            CurrentData.SInsuredMName = IIf(PostModel.SInsuredMName Is Nothing, "", PostModel.SInsuredMName)
            CurrentData.SInsuredDOB = PostModel.SInsuredDOB
            CurrentData.SInsuranceAddress = PostModel.SInsuranceAddress
            CurrentData.SInsuranceCity = PostModel.SInsuranceCity
            CurrentData.SInsuranceState = PostModel.SInsuranceState
            CurrentData.SInsuranceZip = PostModel.SInsuranceZip
            CurrentData.SInsurancePhoneNo = PostModel.SInsurancePhoneNo
            CurrentData.SEmployer = PostModel.SEmployer
            CurrentData.SEmployerAddress = PostModel.SEmployerAddress
            CurrentData.SEmployerCity = PostModel.SEmployerCity
            CurrentData.SEmployerState = PostModel.SEmployerState
            CurrentData.SEmployerZip = PostModel.SEmployerZip
            CurrentData.SEmployerPhone = PostModel.SEmployerPhone
            CurrentData.RequestCollectedBy = PostModel.RequestCollectedBy

            For x As Integer = 0 To CurrentData.Specimens.Count - 1
                CurrentData.Specimens(x).SpecQuantity = PostModel.Specimens(x).SpecQuantity
                If CurrentData.Specimens(x).SpecQuantity = 0 Then
                    ModelState.AddModelError("Specimens_" & x & "__SpecQuantity", "No Value")
                End If
            Next

            If ModelState.IsValid Then
                Dim conn As New SqlConnection(Parameters.eRequestConnectionString)
                conn.Open()
                'Dim NPISql As String
                'NPISql = "SELECT * FROM ReferringDoctor where code = '" & CurrentData.DoctorCode & "'"
                'Dim NPIcmd As New SqlCommand(NPISql, conn)
                'Dim NPIrdr As SqlDataReader = NPIcmd.ExecuteReader
                'If conn.State <> ConnectionState.Open Then Throw New Exception
                'If NPIrdr.HasRows Then
                '    While NPIrdr.Read
                '        CurrentData.NPI = NPIrdr("NPI")
                '    End While
                'End If
                'NPIrdr.Close()

                Dim strSpec As String = ""
                For Each item In CurrentData.Specimens
                    strSpec &= item.SpecCode & Chr(4) & item.SpecDesc & Chr(4) & item.SpecQuantity & Chr(23)
                Next
                If strSpec.Length > 0 Then
                    strSpec = strSpec.Substring(0, strSpec.Length - 1)
                End If

                Dim strSecondary As String = ""
                strSecondary &=
                    CurrentData.Employer & Chr(4) &
                    CurrentData.EmployerAddress & Chr(4) &
                    CurrentData.EmployerCity & Chr(4) &
                    CurrentData.EmployerState & Chr(4) &
                    CurrentData.EmployerZip & Chr(4) &
                    CurrentData.EmployerPhone & Chr(4)
                strSecondary &= CurrentData.SBilltype & Chr(4)

                strSecondary &= CurrentData.SMedicareNo & Chr(4) &
                                CurrentData.SMedicaID & Chr(4)
                Dim sql As String
                Dim con As New SqlConnection(Parameters.eRequestConnectionString)
                Dim cmd As New SqlCommand
                Dim CompanyCode As String = CurrentData.SInsuranceCompanyName

                If CurrentData.SBilltype = "Medicaid" Then
                    strSecondary &= CurrentData.SMedicaidInsIndex & Chr(4)
                    strSecondary &= CurrentData.SMedicaidInsIndex & Chr(4)
                ElseIf CurrentData.SBilltype IsNot Nothing Or CurrentData.SBilltype <> "" Then
                    strSecondary &= CurrentData.SInsuranceCompanyName & Chr(4)
                    sql = "SELECT NAME FROM INSURANCE WHERE CODE = " & IIf(CurrentData.SInsuranceCompanyName Is Nothing, "''", "'" & CurrentData.SInsuranceCompanyName & "'")
                    cmd = New SqlCommand(sql, con)
                    con.Open()
                    Dim rdr As SqlDataReader = cmd.ExecuteReader
                    If con.State <> ConnectionState.Open Then Throw New Exception
                    If rdr.HasRows Then
                        While rdr.Read
                            CurrentData.SInsuranceCompanyName = rdr("NAME")
                        End While
                    End If
                    con.Close()
                    rdr.Close()
                    strSecondary &= CurrentData.SInsuranceCompanyName & Chr(4)
                ElseIf CurrentData.SBilltype Is Nothing Or CurrentData.SBilltype = "" Then
                    strSecondary &= Chr(4) & Chr(4)
                End If


                strSecondary &= CurrentData.SHMOName & Chr(4)
                Dim SHMOCode As String = CurrentData.SHMOName
                sql = "SELECT NAME FROM HMO WHERE CODE =" & IIf(CurrentData.SHMOName Is Nothing, "''", "'" & CurrentData.SHMOName & "'")
                con.Open()
                cmd = New SqlCommand(sql, con)
                Dim rdr1 As SqlDataReader = cmd.ExecuteReader
                If rdr1.HasRows Then
                    While rdr1.Read
                        CurrentData.SHMOName = rdr1("NAME")
                    End While
                End If
                con.Close()
                rdr1.Close()



                sql = "SELECT * FROM INSURANCE WHERE CODE =" & IIf(CurrentData.InsuranceCode Is Nothing, "''", "'" & CurrentData.InsuranceCode & "'")
                con.Open()
                cmd = New SqlCommand(sql, con)
                Dim rdrPIns As SqlDataReader = cmd.ExecuteReader
                If rdrPIns.HasRows Then
                    While rdrPIns.Read
                        CurrentData.InsuranceName = rdrPIns("Name")
                        CurrentData.InsuranceCode = rdrPIns("Code")
                    End While
                End If
                con.Close()
                rdrPIns.Close()
#End Region
#Region "Secondary"
                'strSecondary &= IIf(IsNothing(CurrentData.SHMOName), "", CurrentData.SHMOName.ToString) & Chr(4)
                If CurrentData.SHMOName Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SHMOName.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SGroupNo.ToString Is Nothing, "" & Chr(4), CurrentData.SGroupNo.ToString & Chr(4))
                If CurrentData.SGroupNo Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SGroupNo.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsuredID.ToString Is Nothing, "", CurrentData.SInsuredID.ToString) & Chr(4)
                If CurrentData.SInsuredID Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuredID.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SRelationship.ToString Is Nothing, "", CurrentData.SRelationship.ToString) & Chr(4)
                If CurrentData.SRelationship Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SRelationship.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SSSN.ToString Is Nothing, "", CurrentData.SSSN.ToString) & Chr(4)
                If CurrentData.SSSN Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SSSN.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.InsuredLN Is Nothing, "", CurrentData.InsuredLN) & Chr(4)
                If CurrentData.SInsuredLName Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuredLName & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.InsuredFN Is Nothing, "", CurrentData.InsuredFN) & Chr(4)
                If CurrentData.SInsuredFName Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuredFName & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.InsuredMN Is Nothing, "", CurrentData.InsuredMN) & Chr(4)
                If CurrentData.SInsuredMName Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuredMName & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsuredDOB.ToString Is Nothing, "", CurrentData.SInsuredDOB.ToString) & Chr(4)
                If CurrentData.SInsuredDOB Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuredDOB.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsuranceAddress Is Nothing, "", CurrentData.SInsuranceAddress) & Chr(4)
                If CurrentData.SInsuranceAddress Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuranceAddress & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsuranceCity Is Nothing, "", CurrentData.SInsuranceCity) & Chr(4)
                If CurrentData.SInsuranceCity Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuranceCity & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsuranceState Is Nothing, "", CurrentData.SInsuranceState) & Chr(4)
                If CurrentData.SInsuranceState Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuranceState & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsuranceZip.ToString Is Nothing, "", CurrentData.SInsuranceZip.ToString) & Chr(4)
                If CurrentData.SInsuranceZip Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsuranceZip.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SInsurancePhoneNo Is Nothing, "", CurrentData.SInsurancePhoneNo) & Chr(4)
                If CurrentData.SInsurancePhoneNo Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SInsurancePhoneNo & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SEmployer Is Nothing, "", CurrentData.SEmployer) & Chr(4)
                If CurrentData.SEmployer Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SEmployer & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SEmployerAddress Is Nothing, "", CurrentData.SEmployerAddress) & Chr(4)
                If CurrentData.SEmployerAddress Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SEmployerAddress & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SEmployerCity Is Nothing, "", CurrentData.SEmployerCity) & Chr(4)
                If CurrentData.SEmployerCity Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SEmployerCity & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SEmployerState Is Nothing, "", CurrentData.SEmployerState) & Chr(4)
                If CurrentData.SEmployerState Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SEmployerState & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SEmployerZip.ToString Is Nothing, "", CurrentData.SEmployerZip.ToString) & Chr(4)
                If CurrentData.SEmployerZip Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SEmployerZip.ToString & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.SEmployerPhone Is Nothing, "", CurrentData.SEmployerPhone) & Chr(4)
                If CurrentData.SEmployerPhone Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.SEmployerPhone & Chr(4)
                End If
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(32).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(32).ToString) & Chr(4)
                'show pre auth
                If CurrentData.Secondary.Split(Chr(4))(32).ToString Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(32).ToString & Chr(4)
                End If
                'email sent
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(33).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(33).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(33) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(33).ToString & Chr(4)
                End If
                'bill to client
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(34).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(34).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(34) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(34).ToString & Chr(4)
                End If
                'terms
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(35).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(35).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(35) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(35).ToString & Chr(4)
                End If
                'medicaid insurance
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(36).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(36).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(36) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(36).ToString & Chr(4)
                End If
                'MedicaidInsuranceName
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(37).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(37).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(37) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(37).ToString & Chr(4)
                End If
                'SecondaryMedicaidInsurance
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(38).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(38).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(38) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(38).ToString & Chr(4)
                End If
                'SecondaryMedicaidInsuranceName
                'strSecondary &= IIf(CurrentData.RequestLabTest.Split(Chr(4))(39).ToString Is Nothing, "", CurrentData.RequestLabTest.Split(Chr(4))(39).ToString) & Chr(4)
                If CurrentData.Secondary.Split(Chr(4))(39) Is Nothing Then
                    strSecondary &= "" & Chr(4)
                Else
                    strSecondary &= CurrentData.Secondary.Split(Chr(4))(39).ToString & Chr(4)
                End If
#End Region
#Region "Update"
                cmd = New SqlCommand("sp_UpdateRequestHeader", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Remarks", CurrentData.RequestLabTestRemarks)
                cmd.Parameters.AddWithValue("@PBilltype", CurrentData.RequestType)
                cmd.Parameters.AddWithValue("@ID", CurrentData.ID)
                cmd.Parameters.AddWithValue("@PLName", CurrentData.RequestPLName)
                cmd.Parameters.AddWithValue("@PFName", CurrentData.RequestPFName)
                cmd.Parameters.AddWithValue("@PMName", IIf(CurrentData.RequestPMName Is Nothing, "", CurrentData.RequestPMName))
                cmd.Parameters.AddWithValue("@MRN", CurrentData.MRN)
                cmd.Parameters.AddWithValue("@PDOB", CurrentData.RequestPDOB)
                cmd.Parameters.AddWithValue("@PGender", CurrentData.RequestPGender)
                cmd.Parameters.AddWithValue("@PAddress", CurrentData.RequestPAddress)
                cmd.Parameters.AddWithValue("@PCity", CurrentData.City)
                cmd.Parameters.AddWithValue("@PState", CurrentData.State)
                cmd.Parameters.AddWithValue("@PZipCode", CurrentData.ZipCode)
                cmd.Parameters.AddWithValue("@PContactNo", CurrentData.RequestPContact)
                cmd.Parameters.AddWithValue("@MedicareNo", IIf(CurrentData.Medicare Is Nothing, "", CurrentData.Medicare))
                cmd.Parameters.AddWithValue("@MedicaID", CurrentData.MedicaID)
                If CurrentData.RequestType <> "Medicaid" Then
                    cmd.Parameters.AddWithValue("@InsuranceCode", CurrentData.InsuranceCode)
                    cmd.Parameters.AddWithValue("@InsuranceCompany", CurrentData.InsuranceName)
                Else
                    cmd.Parameters.AddWithValue("@InsuranceCode", CurrentData.PMedicaidInsIndex)
                    cmd.Parameters.AddWithValue("@InsuranceCompany", CurrentData.PMedicaidInsIndex)
                End If
                cmd.Parameters.AddWithValue("@HMOCode", CurrentData.HMOCode)
                cmd.Parameters.AddWithValue("@GroupNo", CurrentData.GroupNo)
                cmd.Parameters.AddWithValue("@InsuredID", CurrentData.InsuredID)
                cmd.Parameters.AddWithValue("@RelationShip", CurrentData.Relationship)
                cmd.Parameters.AddWithValue("@SSN", CurrentData.SSN)
                cmd.Parameters.AddWithValue("@InsureLName", CurrentData.InsuredLN)
                cmd.Parameters.AddWithValue("@InsureFName", CurrentData.InsuredFN)
                cmd.Parameters.AddWithValue("@InsureMName", CurrentData.InsuredMN)
                cmd.Parameters.AddWithValue("@InsureDOB", IIf(CurrentData.InsuredDOB Is Nothing, "1/1/0001", CurrentData.InsuredDOB))
                cmd.Parameters.AddWithValue("@InsureAddress", CurrentData.InsuranceAddress)
                cmd.Parameters.AddWithValue("@InsureCity", CurrentData.InsuranceCity)
                cmd.Parameters.AddWithValue("@InsureState", CurrentData.InsuranceState)
                cmd.Parameters.AddWithValue("@InsureZipCode", CurrentData.InsuranceZip)
                cmd.Parameters.AddWithValue("@InsureContactNo", CurrentData.InsurancePhoneNO)
                cmd.Parameters.AddWithValue("@Secondary", strSecondary)
                cmd.Parameters.AddWithValue("@Specimen", strSpec)
                cmd.Parameters.AddWithValue("@CollectedBy", CurrentData.RequestCollectedBy)
                cmd.Parameters.AddWithValue("@CollectedTS", CurrentData.RequestCollectedTS)

                Try
                    con.Open()
                    If con.State <> ConnectionState.Open Then Throw New Exception
                    Dim rdr2 As SqlDataReader = cmd.ExecuteReader
                    If rdr2.HasRows Then
                        'retObj = True
                    End If
                    rdr2.Close()
#End Region
#Region "Hl7 Merge"

                    'hl7 separation start
                    Dim n As Date = Helpers.LocalizeDateTime(Now, CurrentData.RequestLab.Split(Chr(4))(0))
                    Dim mergedhl7handler As New HL7Handler.MergedHL7
                    Dim mergedhl7 As String = String.Empty

                    If CurrentData.ShowPreAuth And CurrentData.BillToClient Then
                        'make hl7 for additional which is bill to client
                        'store to temp list
                        'Dim temptests As New List(Of TESTS.TESTINPUT)
                        'temptests.AddRange(postmodel.InputTests)
                        'temptests.AddRange(postmodel.NoCurrentDataalTests)
                        'segregate additional from valid
                        'Dim additionaltests As New List(Of TESTS.TESTINPUT)
                        'For Each item In CurrentData.RequestLabTest.Split(Chr(23))
                        '    Dim additional As TESTS.TESTINPUT = temptests.SingleOrDefault(Function(x) x.Test.Code = item.Split(Chr(4))(0))
                        '    additionaltests.Add(additional)
                        '    temptests.Remove(additional)
                        '    Nextx
                        '    segregate micros from lab in valid
                        'temptests first

                        Dim additionaltest As New List(Of MainModel.Test)
                        For Each item In CurrentData.AddTestList
                            Dim additional As MainModel.Test = CurrentData.Tests.SingleOrDefault(Function(x) x.TestCode = item.AddCode)
                            additionaltest.Add(additional)
                            CurrentData.Tests.Remove(additional)
                        Next

                        Dim microtests As List(Of MainModel.Test) = CurrentData.Tests.Where(Function(x) x.LabelTest.ToUpper = "MICROBIOLOGY").ToList
                        Dim labtests As List(Of MainModel.Test) = CurrentData.Tests.Where(Function(x) x.LabelTest.ToUpper <> "MICROBIOLOGY").ToList
                        If microtests.Count > 0 Then
                            'add the urine micro In labtests, else add the first from the list
                            Dim urinetest As MainModel.Test = microtests.Where(Function(y) y.TestName.ToUpper.Contains("URINE")).FirstOrDefault
                            If urinetest IsNot Nothing Then
                                labtests.Add(urinetest)
                                microtests.Remove(urinetest)
                            Else
                                labtests.Add(microtests.First)
                                microtests.RemoveAt(0)
                            End If
                            '    create hl7 for microtests
                            'check If there are still micros
                            If microtests.Count > 0 Then
                                '6.14.18
                                For Each item In microtests
                                    Dim set_ As Integer = 1
                                    mergedhl7 &= mergedhl7handler.MSH(n, CurrentData.DraftCode)
                                    mergedhl7 &= mergedhl7handler.PID(CurrentData.RequestPFName, CurrentData.RequestPMName, CurrentData.RequestPLName, CurrentData.RequestPDOB, CurrentData.RequestPGender, CurrentData.RequestPAddress, CurrentData.City, CurrentData.State, CurrentData.ZipCode, CurrentData.RequestPContact, CurrentData.MRN, CurrentData.RequestClient, CurrentData.DoctorCode)
                                    mergedhl7 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, "HMO", CurrentData.RequestClient, CurrentData.DoctorCode)
                                    If CurrentData.Relationship = "Self" Then
                                        mergedhl7 &= mergedhl7handler.SelfIN1(CurrentData.HMOCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID)
                                    Else
                                        mergedhl7 &= mergedhl7handler.IN1(CurrentData.HMOCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID, CurrentData.InsuredDOB, CurrentData.InsuranceAddress, CurrentData.InsuranceCity, CurrentData.InsuranceState, CurrentData.InsuranceZip, CurrentData.Employer, CurrentData.EmployerAddress, CurrentData.EmployerCity, CurrentData.EmployerState, CurrentData.EmployerZip, CurrentData.EmployerPhone, CurrentData.SSN)
                                    End If
                                    If CurrentData.SBilltype <> String.Empty Then
                                        mergedhl7 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, CurrentData.SBilltype, CurrentData.RequestClient, CurrentData.DoctorCode)
                                        Select Case CurrentData.SBilltype
                                            Case "Medicare"
                                                mergedhl7 &= mergedhl7handler.IN2(CurrentData.SMedicareNo, "")
                                            Case "Medicaid"
                                                If CurrentData.SRelationship = "Self" Then
                                                    mergedhl7 &= mergedhl7handler.SelfIN1(CurrentData.SMedicaidInsurance, "", "", CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SMedicaID)
                                                Else
                                                    mergedhl7 &= mergedhl7handler.IN1(CurrentData.SMedicaidInsIndex, "", "", CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SMedicaID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                                End If
                                                mergedhl7 &= mergedhl7handler.IN2("", CurrentData.MedicaID)
                                            Case "HMO"
                                                If CurrentData.SRelationship = "Self" Then
                                                    mergedhl7 &= mergedhl7handler.SelfIN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                                Else
                                                    mergedhl7 &= mergedhl7handler.IN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                                End If
                                            Case "Insurance"
                                                If CurrentData.SRelationship = "Self" Then
                                                    mergedhl7 &= mergedhl7handler.SelfIN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                                Else
                                                    mergedhl7 &= mergedhl7handler.IN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                                End If
                                            Case "Other"
                                                If CurrentData.SRelationship = "Self" Then
                                                    mergedhl7 &= mergedhl7handler.SelfIN1("Other", CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                                Else
                                                    mergedhl7 &= mergedhl7handler.IN1("Other", CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                                End If
                                        End Select
                                    End If
                                    mergedhl7 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, n)
                                    mergedhl7 &= mergedhl7handler.OBR(set_, CurrentData.DraftCode, item.TestCode, CurrentData.RequestCollectedTS, "O", n, item.TestSite, Date.Parse("1-1-1900"), item.TestSite, CurrentData.DoctorCode, "R", CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName)
                                    set_ += 1
                                    If item.TestQuestion IsNot Nothing AndAlso item.TestQuestion <> String.Empty Then mergedhl7 &= mergedhl7handler.OBX(item.TestQuestion, item.TestAnswer)
                                    If item.ICD1 IsNot Nothing AndAlso item.ICD1 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD1)
                                    If item.ICD2 IsNot Nothing AndAlso item.ICD2 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD2)
                                    If item.ICD3 IsNot Nothing AndAlso item.ICD3 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD3)
                                    If item.ICD4 IsNot Nothing AndAlso item.ICD4 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD4)
                                    If CurrentData.RequestLabTestRemarks IsNot Nothing AndAlso CurrentData.RequestLabTestRemarks <> String.Empty Then mergedhl7 &= mergedhl7handler.NTE(CurrentData.RequestLabTestRemarks)
                                Next
                            End If
                        End If
                        'create hl7 for labtests
                        If labtests.Count > 0 Then
                            Dim set_ As Integer = 1
                            mergedhl7 &= mergedhl7handler.MSH(n, CurrentData.DraftCode)
                            mergedhl7 &= mergedhl7handler.PID(CurrentData.RequestPFName, CurrentData.RequestPMName, CurrentData.RequestPLName, CurrentData.RequestPDOB, CurrentData.RequestPGender, CurrentData.RequestPGender, CurrentData.City, CurrentData.State, CurrentData.ZipCode, CurrentData.RequestPContact, CurrentData.MRN, CurrentData.RequestClient, CurrentData.DoctorCode)
                            mergedhl7 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, "HMO", CurrentData.RequestClient, CurrentData.DoctorCode)
                            If CurrentData.Relationship = "Self" Then
                                mergedhl7 &= mergedhl7handler.SelfIN1(CurrentData.HMOCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID)
                            Else
                                mergedhl7 &= mergedhl7handler.IN1(CurrentData.HMOCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID, CurrentData.InsuredDOB, CurrentData.InsuranceAddress, CurrentData.InsuranceCity, CurrentData.InsuranceState, CurrentData.InsuranceZip, CurrentData.Employer, CurrentData.EmployerAddress, CurrentData.EmployerCity, CurrentData.EmployerState, CurrentData.EmployerZip, CurrentData.EmployerPhone, CurrentData.SSN)
                            End If
                            If CurrentData.SBilltype <> String.Empty Then
                                mergedhl7 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, CurrentData.SBilltype, CurrentData.RequestClient, CurrentData.DoctorCode)
                                Select Case CurrentData.SBilltype
                                    Case "Medicare"
                                        mergedhl7 &= mergedhl7handler.IN2(CurrentData.SMedicareNo, "")
                                    Case "Medicaid"
                                        If CurrentData.SRelationship Then
                                            mergedhl7 &= mergedhl7handler.SelfIN1(CurrentData.SMedicaidInsurance, "", "", CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SMedicaID)
                                        Else
                                            mergedhl7 &= mergedhl7handler.IN1(CurrentData.SMedicaidInsIndex, "", "", CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SMedicaID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                        End If
                                        mergedhl7 &= mergedhl7handler.IN2("", CurrentData.SMedicaID)
                                    Case "HMO"
                                        If CurrentData.SRelationship = "Self" Then
                                            mergedhl7 &= mergedhl7handler.SelfIN1(SHMOCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                        Else
                                            mergedhl7 &= mergedhl7handler.IN1(SHMOCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                        End If
                                    Case "Insurance"
                                        If CurrentData.SRelationship = "Self" Then
                                            mergedhl7 &= mergedhl7handler.SelfIN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                        Else
                                            mergedhl7 &= mergedhl7handler.IN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                        End If
                                    Case "Other"
                                        If CurrentData.SRelationship = "Self" Then
                                            mergedhl7 &= mergedhl7handler.SelfIN1("Other", CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                        Else
                                            mergedhl7 &= mergedhl7handler.IN1("Other", CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                        End If
                                End Select
                            End If
                            For Each item In labtests
                                mergedhl7 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, n)
                                mergedhl7 &= mergedhl7handler.OBR(set_, CurrentData.DraftCode, item.TestCode, CurrentData.RequestCollectedTS, "O", n, item.TestSite, Date.Parse("1-1-1900"), item.TestSite, CurrentData.DoctorCode, "R", CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName)
                                set_ += 1
                                If item.TestQuestion IsNot Nothing AndAlso item.TestQuestion <> String.Empty Then mergedhl7 &= mergedhl7handler.OBX(item.TestQuestion, item.TestAnswer)
                                If item.ICD1 IsNot Nothing AndAlso item.ICD1 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD1)
                                If item.ICD2 IsNot Nothing AndAlso item.ICD2 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD2)
                                If item.ICD3 IsNot Nothing AndAlso item.ICD3 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD3)
                                If item.ICD4 IsNot Nothing AndAlso item.ICD4 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD4)
                            Next
                            If CurrentData.RequestLabTestRemarks IsNot Nothing AndAlso CurrentData.RequestLabTestRemarks <> String.Empty Then mergedhl7 &= mergedhl7handler.NTE(CurrentData.RequestLabTestRemarks)
                        End If
                        'additional next
                        labtests = additionaltest
                        microtests = CurrentData.Tests.Where(Function(x) x.LabelTest.ToUpper = "MICROBIOLOGY").ToList
                        labtests = CurrentData.Tests.Where(Function(x) x.LabelTest.ToUpper <> "MICROBIOLOGY").ToList
                        If microtests.Count > 0 Then
                            'add the urine micro In labtests, else add the first from the list
                            Dim urinetest As MainModel.Test = microtests.Where(Function(y) y.TestName.ToUpper.Contains("URINE")).FirstOrDefault
                            If urinetest IsNot Nothing Then
                                labtests.Add(urinetest)
                                microtests.Remove(urinetest)
                            Else
                                labtests.Add(microtests.First)
                                microtests.RemoveAt(0)
                            End If
                            'create hl7 for microtests
                            'check If there are still micros
                            If microtests.Count > 0 Then
                                For Each item In microtests
                                    Dim set_ As Integer = 1
                                    mergedhl7 &= mergedhl7handler.MSH(n, CurrentData.DraftCode)
                                    mergedhl7 &= mergedhl7handler.PID(CurrentData.RequestPFName, CurrentData.RequestPMName, CurrentData.RequestPLName, CurrentData.RequestPDOB, CurrentData.RequestPGender, CurrentData.RequestPAddress, CurrentData.City, CurrentData.State, CurrentData.ZipCode, CurrentData.RequestPContact, CurrentData.MRN, CurrentData.RequestClient, CurrentData.DoctorCode)
                                    mergedhl7 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, "Client", CurrentData.RequestClient, CurrentData.DoctorCode)
                                    mergedhl7 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, n)
                                    mergedhl7 &= mergedhl7handler.OBR(set_, CurrentData.DraftCode, item.TestCode, CurrentData.RequestCollectedTS, "O", n, item.TestSite, Date.Parse("1-1-1900"), item.TestSite, CurrentData.DoctorCode, "R", CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName)
                                    set_ += 1
                                    If item.TestQuestion IsNot Nothing AndAlso item.TestQuestion <> String.Empty Then mergedhl7 &= mergedhl7handler.OBX(item.TestQuestion, item.TestAnswer)
                                    If item.ICD1 IsNot Nothing AndAlso item.ICD1 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD1)
                                    If item.ICD2 IsNot Nothing AndAlso item.ICD2 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD2)
                                    If item.ICD3 IsNot Nothing AndAlso item.ICD3 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD3)
                                    If item.ICD4 IsNot Nothing AndAlso item.ICD4 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD4)
                                    If CurrentData.RequestLabTestRemarks IsNot Nothing AndAlso CurrentData.RequestLabTestRemarks <> String.Empty Then mergedhl7 &= mergedhl7handler.NTE(CurrentData.RequestLabTestRemarks)
                                Next
                            End If
                        End If
                        'create hl7 for labtests
                        If labtests.Count > 0 Then
                            Dim set_ As Integer = 1
                            mergedhl7 &= mergedhl7handler.MSH(n, CurrentData.DraftCode)
                            mergedhl7 &= mergedhl7handler.PID(CurrentData.RequestPFName, CurrentData.RequestPMName, CurrentData.RequestPLName, CurrentData.RequestPDOB, CurrentData.RequestPGender, CurrentData.RequestPAddress, CurrentData.City, CurrentData.State, CurrentData.ZipCode, CurrentData.RequestPContact, CurrentData.MRN, CurrentData.RequestClient, CurrentData.DoctorCode)
                            mergedhl7 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, "Client", CurrentData.RequestClient, CurrentData.DoctorCode)
                            For Each item In labtests
                                mergedhl7 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, n)
                                mergedhl7 &= mergedhl7handler.OBR(set_, CurrentData.DraftCode, item.TestCode, CurrentData.RequestCollectedTS, "O", n, item.TestSite, Date.Parse("1-1-1900"), item.TestSite, CurrentData.DoctorCode, "R", CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName)
                                set_ += 1
                                If item.TestQuestion IsNot Nothing AndAlso item.TestQuestion <> String.Empty Then mergedhl7 &= mergedhl7handler.OBX(item.TestQuestion, item.TestAnswer)
                                If item.ICD1 IsNot Nothing AndAlso item.ICD1 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD1)
                                If item.ICD2 IsNot Nothing AndAlso item.ICD2 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD2)
                                If item.ICD3 IsNot Nothing AndAlso item.ICD3 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD3)
                                If item.ICD4 IsNot Nothing AndAlso item.ICD4 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD4)
                            Next
                            If CurrentData.RequestLabTestRemarks IsNot Nothing AndAlso CurrentData.RequestLabTestRemarks <> String.Empty Then mergedhl7 &= mergedhl7handler.NTE(CurrentData.RequestLabTestRemarks)
                        End If
                    Else
                        'Not pre auth
                        'divide hl7 to minimize code
                        Dim mergedhl7header1 As String = String.Empty
                        Dim mergedhl7headerbilling1 As String = String.Empty
                        Dim mergedhl7headerbilling2 As String = String.Empty
                        Dim mergedhl7header2 As String = String.Empty
                        Dim mergedhl7tests As String = String.Empty
                        Dim mergedhl7footer As String = String.Empty
                        Dim set_ As Integer = 1
                        mergedhl7header1 &= mergedhl7handler.MSH(n, CurrentData.DraftCode)
                        mergedhl7header1 &= mergedhl7handler.PID(CurrentData.RequestPFName, CurrentData.RequestPMName, CurrentData.RequestPLName, CurrentData.RequestPDOB, CurrentData.RequestPGender, CurrentData.RequestPAddress, CurrentData.City, CurrentData.State, CurrentData.ZipCode, CurrentData.RequestPContact, CurrentData.MRN, CurrentData.RequestClient, CurrentData.DoctorCode)
                        mergedhl7header1 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, CurrentData.RequestType, CurrentData.RequestClient, CurrentData.DoctorCode)
                        Select Case CurrentData.RequestType
                            Case "Medicare"
                                mergedhl7headerbilling1 &= mergedhl7handler.IN2(CurrentData.Medicare, "")
                            Case "Medicaid"
                                If CurrentData.Relationship = "Self" Then
                                    mergedhl7headerbilling1 &= mergedhl7handler.SelfIN1(CurrentData.InsuranceCode, "", "", CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.MedicaID)
                                Else
                                    mergedhl7headerbilling1 &= mergedhl7handler.IN1(CurrentData.PMedicaidInsIndex, "", "", CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.MedicaID, CurrentData.InsuredDOB, CurrentData.InsuranceAddress, CurrentData.InsuranceCity, CurrentData.InsuranceState, CurrentData.InsuranceZip, CurrentData.Employer, CurrentData.EmployerAddress, CurrentData.EmployerCity, CurrentData.EmployerState, CurrentData.EmployerZip, CurrentData.EmployerPhone, CurrentData.SSN)
                                End If
                                mergedhl7headerbilling1 &= mergedhl7handler.IN2("", CurrentData.MedicaID)
                            Case "HMO"
                                If CurrentData.Relationship = "Self" Then
                                    mergedhl7headerbilling1 &= mergedhl7handler.SelfIN1(CurrentData.HMOCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID)
                                Else
                                    mergedhl7headerbilling1 &= mergedhl7handler.IN1(CurrentData.HMOCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID, CurrentData.InsuredDOB, CurrentData.InsuranceAddress, CurrentData.InsuranceCity, CurrentData.InsuranceState, CurrentData.InsuranceZip, CurrentData.Employer, CurrentData.EmployerAddress, CurrentData.EmployerCity, CurrentData.EmployerState, CurrentData.EmployerZip, CurrentData.EmployerPhone, CurrentData.SSN)
                                End If
                            Case "Insurance"
                                If CurrentData.Relationship = "Self" Then
                                    mergedhl7headerbilling1 &= mergedhl7handler.SelfIN1(CurrentData.InsuranceCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID)
                                Else
                                    mergedhl7headerbilling1 &= mergedhl7handler.IN1(CurrentData.InsuranceCode, CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID, CurrentData.InsuredDOB, CurrentData.InsuranceAddress, CurrentData.InsuranceCity, CurrentData.InsuranceState, CurrentData.InsuranceZip, CurrentData.Employer, CurrentData.EmployerAddress, CurrentData.EmployerCity, CurrentData.EmployerState, CurrentData.EmployerZip, CurrentData.EmployerPhone, CurrentData.SSN)
                                End If
                            Case "Other"
                                If CurrentData.Relationship = "Self" Then
                                    mergedhl7headerbilling1 &= mergedhl7handler.SelfIN1("Other", CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID)
                                Else
                                    mergedhl7headerbilling1 &= mergedhl7handler.IN1("Other", CurrentData.InsurancePhoneNO, CurrentData.GroupNo, CurrentData.InsuredFN, CurrentData.InsuredMN, CurrentData.InsuredLN, CurrentData.Relationship, CurrentData.InsuredID, CurrentData.InsuredDOB, CurrentData.InsuranceAddress, CurrentData.InsuranceCity, CurrentData.InsuranceState, CurrentData.InsuranceZip, CurrentData.Employer, CurrentData.EmployerAddress, CurrentData.EmployerCity, CurrentData.EmployerState, CurrentData.EmployerZip, CurrentData.EmployerPhone, CurrentData.SSN)
                                End If
                        End Select
                        mergedhl7header1 &= mergedhl7headerbilling1
                        If CurrentData.SBilltype <> String.Empty Then
                            mergedhl7header1 &= mergedhl7handler.PV1(CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, String.Empty, CurrentData.SBilltype, CurrentData.RequestClient, CurrentData.DoctorCode)
                            Select Case CurrentData.SBilltype
                                Case "Medicare"
                                    mergedhl7headerbilling2 &= mergedhl7handler.IN2(CurrentData.SMedicareNo, "")
                                Case "Medicaid"
                                    If CurrentData.SRelationship = "Self" Then
                                        mergedhl7headerbilling2 &= mergedhl7handler.SelfIN1(CurrentData.SMedicaidInsurance, "", "", CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SMedicaID)
                                    Else
                                        mergedhl7headerbilling2 &= mergedhl7handler.IN1(CurrentData.SMedicaidInsIndex, "", "", CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SMedicaID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                    End If
                                    mergedhl7headerbilling2 &= mergedhl7handler.IN2("", CurrentData.SMedicaID)
                                Case "HMO"
                                    If CurrentData.SRelationship = "Self" Then
                                        mergedhl7headerbilling2 &= mergedhl7handler.SelfIN1(SHMOCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                    Else
                                        mergedhl7headerbilling2 &= mergedhl7handler.IN1(SHMOCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                    End If
                                Case "Insurance"
                                    If CurrentData.SRelationship = "Self" Then
                                        mergedhl7headerbilling2 &= mergedhl7handler.SelfIN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                    Else
                                        mergedhl7headerbilling2 &= mergedhl7handler.IN1(CompanyCode, CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                    End If
                                Case "Other"
                                    If CurrentData.SRelationship = "Self" Then
                                        mergedhl7headerbilling2 &= mergedhl7handler.SelfIN1("Other", CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID)
                                    Else
                                        mergedhl7headerbilling2 &= mergedhl7handler.IN1("Other", CurrentData.SInsurancePhoneNo, CurrentData.SGroupNo, CurrentData.SInsuredFName, CurrentData.SInsuredMName, CurrentData.SInsuredLName, CurrentData.SRelationship, CurrentData.SInsuredID, CurrentData.SInsuredDOB, CurrentData.SInsuranceAddress, CurrentData.SInsuranceCity, CurrentData.SInsuranceState, CurrentData.SInsuranceZip, CurrentData.SEmployer, CurrentData.SEmployerAddress, CurrentData.SEmployerCity, CurrentData.SEmployerState, CurrentData.SEmployerZip, CurrentData.SEmployerPhone, CurrentData.SSSN)
                                    End If
                            End Select
                            mergedhl7header1 &= mergedhl7headerbilling2
                        End If
                        'mergedhl7header2 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.requestclient)
                        If CurrentData.RequestLabTestRemarks IsNot Nothing AndAlso CurrentData.RequestLabTestRemarks <> String.Empty Then mergedhl7footer &= mergedhl7handler.NTE(CurrentData.RequestLabTestRemarks)
                        'Dim temptests As New List(Of MainModel.Test)
                        'temptests.AddRange(mergedhl7.)
                        'temptests.AddRange(postmodel.NoCurrentDataalTests)
                        Dim microtests As List(Of MainModel.Test) = CurrentData.Tests.Where(Function(x) x.LabelTest.ToUpper = "MICROBIOLOGY").ToList
                        Dim labtests As List(Of MainModel.Test) = CurrentData.Tests.Where(Function(x) x.LabelTest.ToUpper <> "MICROBIOLOGY").ToList
                        If microtests.Count > 0 Then
                            'add the urine micro In labtests, else add the first from the list
                            Dim urinetest As MainModel.Test = microtests.Where(Function(y) y.TestName.ToUpper.Contains("URINE")).FirstOrDefault
                            If urinetest IsNot Nothing Then
                                labtests.Add(urinetest)
                                microtests.Remove(urinetest)
                            Else
                                labtests.Add(microtests.First)
                                microtests.RemoveAt(0)
                            End If
                            'create hl7 for microtests
                            'check If there are still micros
                            If microtests.Count > 0 Then
                                For Each item In microtests
                                    set_ = 1
                                    mergedhl7 &= mergedhl7header1
                                    mergedhl7 &= mergedhl7header2
                                    mergedhl7 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, n)
                                    mergedhl7 &= mergedhl7handler.OBR(set_, CurrentData.DraftCode, item.TestCode, CurrentData.RequestCollectedTS, "O", n, item.TestSite, Date.Parse("1-1-1900"), item.TestSite, CurrentData.DoctorCode, "R", CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName)
                                    set_ += 1
                                    If item.TestQuestion IsNot Nothing AndAlso item.TestQuestion <> String.Empty Then mergedhl7 &= mergedhl7handler.OBX(item.TestQuestion, item.TestAnswer)
                                    If item.ICD1 IsNot Nothing AndAlso item.ICD1 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD1)
                                    If item.ICD2 IsNot Nothing AndAlso item.ICD2 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD2)
                                    If item.ICD3 IsNot Nothing AndAlso item.ICD3 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD3)
                                    If item.ICD4 IsNot Nothing AndAlso item.ICD4 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD4)
                                    mergedhl7 &= mergedhl7footer
                                Next
                            End If
                        End If
                        'create hl7 for labtests
                        If labtests.Count > 0 Then
                            set_ = 1
                            mergedhl7 &= mergedhl7header1
                            mergedhl7 &= mergedhl7header2
                            For Each item In labtests
                                mergedhl7 &= mergedhl7handler.ORC(CurrentData.DraftCode, CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName, n)
                                mergedhl7 &= mergedhl7handler.OBR(set_, CurrentData.DraftCode, item.TestCode, CurrentData.RequestCollectedTS, "O", n, item.TestSite, Date.Parse("1-1-1900"), item.TestSite, CurrentData.DoctorCode, "R", CurrentData.NPI, CurrentData.DoctorLName, CurrentData.DoctorFName, CurrentData.DoctorMName)
                                set_ += 1
                                If item.TestQuestion IsNot Nothing AndAlso item.TestQuestion <> String.Empty Then mergedhl7 &= mergedhl7handler.OBX(item.TestQuestion, item.TestAnswer)
                                If item.ICD1 IsNot Nothing AndAlso item.ICD1 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD1)
                                If item.ICD2 IsNot Nothing AndAlso item.ICD2 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD2)
                                If item.ICD3 IsNot Nothing AndAlso item.ICD3 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD3)
                                If item.ICD4 IsNot Nothing AndAlso item.ICD4 <> String.Empty Then mergedhl7 &= mergedhl7handler.DG1(item.ICD4)
                            Next
                            mergedhl7 &= mergedhl7footer
                        End If
                    End If
                    'hl7 separation end
#End Region
#Region "Insert Hl7 File"
                    'insert hl7
                    Dim hl7cmd As New SqlCommand("sp_inserthl7", con)
                    hl7cmd.CommandType = CommandType.StoredProcedure
                    hl7cmd.Parameters.AddWithValue("@code", CurrentData.DraftCode)
                    hl7cmd.Parameters.AddWithValue("@content", mergedhl7)
                    hl7cmd.Parameters.AddWithValue("@specimnid", String.Empty)
                    hl7cmd.Parameters.AddWithValue("@accessionno", String.Empty)
                    hl7cmd.Parameters.AddWithValue("@sent", n)
                    hl7cmd.Parameters.AddWithValue("@lab", CurrentData.RequestLab.Split(Chr(4))(0))
                    hl7cmd.Parameters.AddWithValue("@type", CurrentData.RequestType)
                    hl7cmd.Parameters.AddWithValue("@collection", "client")
                    hl7cmd.Parameters.AddWithValue("@active", True)
                    hl7cmd.Parameters.AddWithValue("@parsed", False)
                    hl7cmd.Parameters.AddWithValue("@accessionnosentts", n)
                    hl7cmd.Parameters.AddWithValue("@collectionsent", True)
                    Dim hl7rdr As SqlDataReader = hl7cmd.ExecuteReader
                    rdr2.Close()
                    hl7rdr.Close()
#End Region
#Region "Update PSCQueue TBL"
                    Dim PSCQcmd As New SqlCommand("sp_updatePSCQueue", con)
                    PSCQcmd.CommandType = CommandType.StoredProcedure
                    PSCQcmd.Parameters.AddWithValue("@Code", CurrentData.DraftCode)
                    PSCQcmd.Parameters.AddWithValue("@PName", Parameters.UserName)
                    PSCQcmd.Parameters.AddWithValue("@PUserName", Parameters.UsedUsername)
                    PSCQcmd.Parameters.AddWithValue("@PSCCode", Parameters.PSCCode)
                    PSCQcmd.Parameters.AddWithValue("@PSCName", Parameters.PSCName)
                    Dim PSCQrdr As SqlDataReader = PSCQcmd.ExecuteReader
                    PSCQrdr.Close()
#End Region
#Region "Insert Logs"
#Region "Old Logs Values"
                    Dim oldLogs As MainModel = TempData("oldLogs")
                    Dim strOldLog As String = ""
                    strOldLog &= oldLogs.DraftCode & (Chr(4))
                    strOldLog &= oldLogs.RequestPLName & (Chr(4))
                    strOldLog &= oldLogs.RequestPFName & (Chr(4))
                    strOldLog &= oldLogs.RequestPMName & (Chr(4))
                    strOldLog &= oldLogs.MRN & (Chr(4))
                    strOldLog &= oldLogs.RequestPDOB.ToString & (Chr(4))
                    strOldLog &= oldLogs.RequestPGender & (Chr(4))
                    strOldLog &= oldLogs.RequestPAddress & (Chr(4))
                    strOldLog &= oldLogs.City & (Chr(4))
                    strOldLog &= oldLogs.State & (Chr(4))
                    strOldLog &= oldLogs.ZipCode & (Chr(4))
                    strOldLog &= oldLogs.RequestPContact & (Chr(4))
                    strOldLog &= oldLogs.RequestType & (Chr(4))
                    strOldLog &= oldLogs.Medicare & (Chr(4))
                    strOldLog &= oldLogs.MedicaID & (Chr(4))
                    strOldLog &= oldLogs.InsuranceName & (Chr(4))
                    strOldLog &= oldLogs.HMOCode & (Chr(4))
                    strOldLog &= oldLogs.HMOName & (Chr(4))
                    strOldLog &= oldLogs.GroupNo & (Chr(4))
                    strOldLog &= oldLogs.InsuredID & (Chr(4))
                    strOldLog &= oldLogs.Relationship & (Chr(4))
                    strOldLog &= oldLogs.SSN & (Chr(4))
                    strOldLog &= oldLogs.InsuredLN & (Chr(4))
                    strOldLog &= oldLogs.InsuredFN & (Chr(4))
                    strOldLog &= oldLogs.InsuredMN & (Chr(4))
                    strOldLog &= oldLogs.InsuredDOB & (Chr(4))
                    strOldLog &= oldLogs.InsuranceAddress & (Chr(4))
                    strOldLog &= oldLogs.InsuranceCity & (Chr(4))
                    strOldLog &= oldLogs.InsuranceState & (Chr(4))
                    strOldLog &= oldLogs.InsuranceZip & (Chr(4))
                    strOldLog &= oldLogs.InsurancePhoneNO & (Chr(4))
                    strOldLog &= oldLogs.Employer & (Chr(4))
                    strOldLog &= oldLogs.EmployerAddress & (Chr(4))
                    strOldLog &= oldLogs.EmployerCity & (Chr(4))
                    strOldLog &= oldLogs.EmployerState & (Chr(4))
                    strOldLog &= oldLogs.EmployerZip & (Chr(4))
                    strOldLog &= oldLogs.EmployerPhone & (Chr(4))
                    strOldLog &= oldLogs.SBilltype & (Chr(4))
                    strOldLog &= oldLogs.SMedicareNo & (Chr(4))
                    strOldLog &= oldLogs.SMedicaID & (Chr(4))
                    strOldLog &= oldLogs.SInsuranceCompanyName & (Chr(4))
                    strOldLog &= oldLogs.HMOName & (Chr(4))
                    strOldLog &= oldLogs.SGroupNo & (Chr(4))
                    strOldLog &= oldLogs.SInsuredID & (Chr(4))
                    strOldLog &= oldLogs.SRelationship & (Chr(4))
                    strOldLog &= oldLogs.SSSN & (Chr(4))
                    strOldLog &= oldLogs.SInsuredLName & (Chr(4))
                    strOldLog &= oldLogs.SInsuredFName & (Chr(4))
                    strOldLog &= oldLogs.SInsuredMName & (Chr(4))
                    strOldLog &= oldLogs.SInsuredDOB.ToString & (Chr(4))
                    strOldLog &= oldLogs.SInsuranceAddress & (Chr(4))
                    strOldLog &= oldLogs.SInsuranceCity & (Chr(4))
                    strOldLog &= oldLogs.SInsuranceState & (Chr(4))
                    strOldLog &= oldLogs.SInsuranceZip & (Chr(4))
                    strOldLog &= oldLogs.SInsurancePhoneNo & (Chr(4))
                    strOldLog &= oldLogs.SEmployer & (Chr(4))
                    strOldLog &= oldLogs.SEmployerAddress & (Chr(4))
                    strOldLog &= oldLogs.SEmployerCity & (Chr(4))
                    strOldLog &= oldLogs.SEmployerState & (Chr(4))
                    strOldLog &= oldLogs.SEmployerZip & (Chr(4))
                    strOldLog &= oldLogs.SEmployerPhone & (Chr(4))
                    strOldLog &= oldLogs.ShowPreAuth & (Chr(4))
                    strOldLog &= oldLogs.EmailSent & (Chr(4))
                    strOldLog &= oldLogs.BillToClient & (Chr(4))
                    strOldLog &= oldLogs.Terms & (Chr(4))
                    strOldLog &= oldLogs.MedicaidInsurance & (Chr(4))
                    strOldLog &= oldLogs.MedicaidInsuranceName & (Chr(4))
                    strOldLog &= oldLogs.SMedicaidInsurance & (Chr(4))
                    strOldLog &= oldLogs.SMedicaidInsuranceName & (Chr(4))
#End Region
#Region "Current Logs"
                    Dim strNewLog As String = ""
                    strNewLog &= CurrentData.DraftCode & (Chr(4))
                    strNewLog &= CurrentData.RequestPLName & (Chr(4))
                    strNewLog &= CurrentData.RequestPFName & (Chr(4))
                    strNewLog &= CurrentData.RequestPMName & (Chr(4))
                    strNewLog &= CurrentData.MRN & (Chr(4))
                    strNewLog &= CurrentData.RequestPDOB.ToString & (Chr(4))
                    strNewLog &= CurrentData.RequestPGender & (Chr(4))
                    strNewLog &= CurrentData.RequestPAddress & (Chr(4))
                    strNewLog &= CurrentData.City & (Chr(4))
                    strNewLog &= CurrentData.State & (Chr(4))
                    strNewLog &= CurrentData.ZipCode & (Chr(4))
                    strNewLog &= CurrentData.RequestPContact & (Chr(4))
                    strNewLog &= CurrentData.RequestType & (Chr(4))
                    strNewLog &= CurrentData.Medicare & (Chr(4))
                    strNewLog &= CurrentData.MedicaID & (Chr(4))
                    strNewLog &= CurrentData.InsuranceName & (Chr(4))
                    strNewLog &= CurrentData.HMOCode & (Chr(4))
                    strNewLog &= CurrentData.HMOName & (Chr(4))
                    strNewLog &= CurrentData.GroupNo & (Chr(4))
                    strNewLog &= CurrentData.InsuredID & (Chr(4))
                    strNewLog &= CurrentData.Relationship & (Chr(4))
                    strNewLog &= CurrentData.SSN & (Chr(4))
                    strNewLog &= CurrentData.InsuredLN & (Chr(4))
                    strNewLog &= CurrentData.InsuredFN & (Chr(4))
                    strNewLog &= CurrentData.InsuredMN & (Chr(4))
                    strNewLog &= CurrentData.InsuredDOB & (Chr(4))
                    strNewLog &= CurrentData.InsuranceAddress & (Chr(4))
                    strNewLog &= CurrentData.InsuranceCity & (Chr(4))
                    strNewLog &= CurrentData.InsuranceState & (Chr(4))
                    strNewLog &= CurrentData.InsuranceZip & (Chr(4))
                    strNewLog &= CurrentData.InsurancePhoneNO & (Chr(4))
                    strNewLog &= CurrentData.Employer & (Chr(4))
                    strNewLog &= CurrentData.EmployerAddress & (Chr(4))
                    strNewLog &= CurrentData.EmployerCity & (Chr(4))
                    strNewLog &= CurrentData.EmployerState & (Chr(4))
                    strNewLog &= CurrentData.EmployerZip & (Chr(4))
                    strNewLog &= CurrentData.EmployerPhone & (Chr(4))
                    strNewLog &= CurrentData.SBilltype & (Chr(4))
                    strNewLog &= CurrentData.SMedicareNo & (Chr(4))
                    strNewLog &= CurrentData.SMedicaID & (Chr(4))
                    strNewLog &= CurrentData.SInsuranceCompanyName & (Chr(4))
                    strNewLog &= CurrentData.HMOName & (Chr(4))
                    strNewLog &= CurrentData.SGroupNo & (Chr(4))
                    strNewLog &= CurrentData.SInsuredID & (Chr(4))
                    strNewLog &= CurrentData.SRelationship & (Chr(4))
                    strNewLog &= CurrentData.SSSN & (Chr(4))
                    strNewLog &= CurrentData.SInsuredLName & (Chr(4))
                    strNewLog &= CurrentData.SInsuredFName & (Chr(4))
                    strNewLog &= CurrentData.SInsuredMName & (Chr(4))
                    strNewLog &= CurrentData.SInsuredDOB.ToString & (Chr(4))
                    strNewLog &= CurrentData.SInsuranceAddress & (Chr(4))
                    strNewLog &= CurrentData.SInsuranceCity & (Chr(4))
                    strNewLog &= CurrentData.SInsuranceState & (Chr(4))
                    strNewLog &= CurrentData.SInsuranceZip & (Chr(4))
                    strNewLog &= CurrentData.SInsurancePhoneNo & (Chr(4))
                    strNewLog &= CurrentData.SEmployer & (Chr(4))
                    strNewLog &= CurrentData.SEmployerAddress & (Chr(4))
                    strNewLog &= CurrentData.SEmployerCity & (Chr(4))
                    strNewLog &= CurrentData.SEmployerState & (Chr(4))
                    strNewLog &= CurrentData.SEmployerZip & (Chr(4))
                    strNewLog &= CurrentData.SEmployerPhone & (Chr(4))
                    strNewLog &= CurrentData.ShowPreAuth & (Chr(4))
                    strNewLog &= CurrentData.EmailSent & (Chr(4))
                    strNewLog &= CurrentData.BillToClient & (Chr(4))
                    strNewLog &= CurrentData.Terms & (Chr(4))
                    strNewLog &= CurrentData.MedicaidInsurance & (Chr(4))
                    strNewLog &= CurrentData.MedicaidInsuranceName & (Chr(4))
                    strNewLog &= CurrentData.SMedicaidInsurance & (Chr(4))
                    strNewLog &= CurrentData.SMedicaidInsuranceName & (Chr(4))
#End Region
                    Dim LOGcmd As New SqlCommand("sp_insertLog", con)
                    LOGcmd.CommandType = CommandType.StoredProcedure
                    LOGcmd.Parameters.AddWithValue("@CN", CurrentData.DraftCode)
                    LOGcmd.Parameters.AddWithValue("@Type", "eRequestPSC")
                    LOGcmd.Parameters.AddWithValue("@Msg", String.Empty)
                    LOGcmd.Parameters.AddWithValue("@Old", strOldLog)
                    LOGcmd.Parameters.AddWithValue("@New", strNewLog)
                    Dim LOGrdr As SqlDataReader = LOGcmd.ExecuteReader
                    LOGrdr.Close()
#End Region

                Catch ex As Exception
                    'retObj = False
                Finally
                    con.Close()
                    con.Dispose()
                    cmd.Dispose()
                End Try

                TempData("oldData") = New MainModel
                count = 0
                Return RedirectToAction("ViewOnly", New With {.id = id})
            Else
                count = 1
                TempData("oldData") = CurrentData
                Return View(CurrentData)
            End If
        End Function
        <Authorize>
        <OutputCache(Duration:=0)>
        Function ViewOnly(id? As Integer) As ActionResult
            If id Is Nothing Then Return Redirect(Url.Action("Index", "RequestTable"))

            Dim Code As String = ""
            Dim conn As New SqlConnection(Parameters.eRequestConnectionString)
            conn.Open()
            Dim cmd As New SqlCommand("sp_CheckPSCQueuepProcess", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ID", id)
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            While rdr.Read
                Code = rdr("Code")
            End While
            conn.Close()
            cmd.Dispose()
            If Code = "" Then
                Return Redirect(Url.Action("Index", "RequestTable"))
                'Else
                '    Return Redirect(Url.Action("Index", "Main", New With {.id = id}))
            End If

#Region "Old Data List"
            Dim m As New MainModel
            Dim RequestHeaders As New RequestHeaders
            Dim RequestHeader As RequestHeaders.RequestHeader = RequestHeaders.SelectRequestHeader(id)
            m.EmailSent = RequestHeader.EmailSent
            m.ID = RequestHeader.ID
            m.Request_Header_LabTest = RequestHeader.Request_Header_Labtest
            m.DraftCode = RequestHeader.DraftCode
            m.RequestRequestor = RequestHeader.RequestRequestor
            m.RequestPLName = RequestHeader.RequestPLName
            m.RequestPFName = RequestHeader.RequestPFName
            m.RequestPMName = RequestHeader.RequestPMName
            m.RequestPContact = RequestHeader.RequestPContact
            m.SInsuranceCompany = RequestHeader.SInsuranceCompany
            m.MRN = RequestHeader.MRN
            m.RequestPDOB = RequestHeader.RequestPDOB
            m.RequestPGender = RequestHeader.RequestPGender
            m.RequestPAddress = RequestHeader.RequestPAddress
            m.City = RequestHeader.City
            m.State = RequestHeader.State
            m.ZipCode = RequestHeader.ZipCode
            m.RequestType = RequestHeader.RequestType
            m.Medicare = RequestHeader.Medicare
            m.MedicaID = RequestHeader.MedicaID
            m.InsuranceName = RequestHeader.InsuranceName
            m.InsuranceCode = RequestHeader.InsuranceCode
            m.HMOName = RequestHeader.HMOName
            m.HMOCode = RequestHeader.HMOCode
            m.HMOID = RequestHeader.HMOID
            m.GroupNo = RequestHeader.GroupNo
            m.InsuredID = RequestHeader.InsuredID
            m.Relationship = RequestHeader.Relationship
            m.SSN = RequestHeader.SSN
            m.InsuredLN = RequestHeader.InsuredLN
            m.InsuredMN = RequestHeader.InsuredMN
            m.InsuredFN = RequestHeader.InsuredFN
            m.InsuredDOB = RequestHeader.InsuredDOB
            m.InsuranceAddress = RequestHeader.InsuranceAddress
            m.InsuranceName = RequestHeader.InsuranceName
            m.InsuranceCity = RequestHeader.InsuranceCity
            m.InsuranceState = RequestHeader.InsuranceState
            m.InsuranceZip = RequestHeader.InsuranceZip
            m.InsurancePhoneNO = RequestHeader.InsurancePhoneNO
            m.Employer = RequestHeader.Employer
            m.EmployerAddress = RequestHeader.EmployerAddress
            m.EmployerCity = RequestHeader.EmployerCity
            m.EmployerState = RequestHeader.EmployerState
            m.EmployerZip = RequestHeader.EmployerZip
            m.EmployerPhone = RequestHeader.EmployerPhone
            m.SBilltype = RequestHeader.SBilltype
            m.SMedicareNo = RequestHeader.SMedicareNo
            m.SMedicaID = IIf(RequestHeader.SMedicaID Is Nothing, "", RequestHeader.SMedicaID)
            m.SInsuranceCompanyName = RequestHeader.SInsuranceCompanyName
            m.SHMO = RequestHeader.SHMO
            m.SHMOName = RequestHeader.SHMOName
            m.SGroupNo = RequestHeader.SGroupNo
            m.SInsuredID = RequestHeader.SInsuredID
            m.SRelationship = RequestHeader.SRelationship
            m.SSSN = RequestHeader.SSSN
            m.SInsuredLName = RequestHeader.SInsuredLName
            m.SInsuredMName = RequestHeader.SInsuredMName
            m.SInsuredFName = RequestHeader.SInsuredFName
            m.SInsuredDOB = RequestHeader.SInsuredDBO
            m.SInsuranceAddress = RequestHeader.SInsuranceAdress
            m.SInsuranceCity = RequestHeader.SInsuranceCity
            m.SInsuranceState = RequestHeader.SInsuranceState
            m.SInsuranceZip = RequestHeader.SInsuranceZip
            m.SInsurancePhoneNo = RequestHeader.SInsurancePhoneNo
            m.SEmployer = RequestHeader.SEmployer
            m.SEmployerAddress = RequestHeader.SEmployerAdress
            m.SEmployerCity = RequestHeader.SEmployerCity
            m.SEmployerState = RequestHeader.SEmployerState
            m.SEmployerZip = RequestHeader.SEmployerZip
            m.SEmployerPhone = RequestHeader.SEmployerPhone
            m.RequestLabTest = RequestHeader.RequestLabTest
            m.RequestCollectedBy = RequestHeader.RequestCollectedBy
            m.RequestCollectedTS = IIf(IsDBNull(RequestHeader.RequestCollectedTS), "1900-01-01", RequestHeader.RequestCollectedTS)
            m.RequestSpecimen = RequestHeader.RequestSpecimen
            m.Secondary = RequestHeader.Secondary
            m.RequestClient = RequestHeader.RequestClient
            m.DoctorCode = RequestHeader.DoctorCode
            m.DoctorFName = RequestHeader.DoctorFName
            m.DoctorLName = RequestHeader.DoctorLName
            m.DoctorMName = RequestHeader.DoctorMName
            m.RequestLab = RequestHeader.RequestLab
            m.ShowPreAuth = RequestHeader.ShowPreAuth
            m.RequestLabTestRemarks = RequestHeader.RequestLabTestRemarks
            m.BillToClient = RequestHeader.BillToClient
            m.AddTest = RequestHeader.Additional
            m.Terms = RequestHeader.Terms
            m.MedicaidInsurance = RequestHeader.MedicaidInsurance
            m.MedicaidInsuranceName = RequestHeader.MedicaidInsuranceName
            m.SMedicaidInsurance = RequestHeader.SMedicaidInsurance
            m.SMedicaidInsuranceName = RequestHeader.SMedicaidInsuranceName

            m.Specimens = New List(Of MainModel.Specimen)
            For Each item In m.RequestSpecimen.Split(Chr(23))
                If item.Split(Chr(4))(0) <> "" Then
                    Dim MSpec As New MainModel.Specimen
                    MSpec.SpecCode = item.Split(Chr(4))(0)
                    MSpec.SpecDesc = item.Split(Chr(4))(1)
                    MSpec.SpecQuantity = item.Split(Chr(4))(2)
                    m.Specimens.Add(MSpec)
                End If
            Next

            m.Tests = New List(Of MainModel.Test)
            For Each item In m.RequestLabTest.Split(Chr(23))
                Dim MTest As New MainModel.Test
                MTest.TestCode = item.Split(Chr(4))(0)
                MTest.LabelTest = item.Split(Chr(4))(1)
                MTest.TestName = item.Split(Chr(4))(2)
                MTest.TestZero = item.Split(Chr(4))(3)
                MTest.TestQuestion = item.Split(Chr(4))(4)
                MTest.TestAnswer = item.Split(Chr(4))(5)
                MTest.ICD1 = item.Split(Chr(4))(6)
                MTest.ICD2 = item.Split(Chr(4))(7)
                MTest.ICD3 = item.Split(Chr(4))(8)
                MTest.ICD4 = item.Split(Chr(4))(9)
                MTest.TestSite = item.Split(Chr(4))(10)
                m.Tests.Add(MTest)
            Next

            m.AddTestList = New List(Of MainModel.AdditionalTest)
            If m.AddTest IsNot Nothing And m.AddTest <> "" Then
                For Each item In m.AddTest.Split(Chr(23))
                    Dim MATest As New MainModel.AdditionalTest
                    MATest.AddCode = item.Split(Chr(4))(0)
                    MATest.AddDesc = item.Split(Chr(4))(1)
                    m.AddTestList.Add(MATest)
                Next
            End If



            Dim PSCQueue As RequestHeaders.PSCQueue = RequestHeaders.SelectPSCQueueByCode(Code)
            m.PSCID = PSCQueue.ID
            m.PSCCode = PSCQueue.Code
            m.PSCPSC_Name = PSCQueue.PSC_Name
            m.PSCPSC_Code = PSCQueue.PSC_Name
            m.PSCPersonnel_UserName = PSCQueue.Personnel_UserName
            m.PSCPersonnel_Name = PSCQueue.Personnel_Name
            m.PSCIsProcessed = PSCQueue.IsProcessed
            m.PSCProcessedTS = PSCQueue.ProcessedTS

            Dim qcode As String = m.DraftCode & Chr(4) & m.RequestClient
            Dim security As New Helpers.SECURITY("ereq_key_17")
            qcode = security.EncryptData(qcode)
            Dim enc As New QRCodeEncoder
            Dim qrcode As Bitmap = enc.Encode(Code)
            Dim image As Image
            Using g As Graphics = Graphics.FromImage(qrcode)
                image = Image.FromFile(Server.MapPath("~/App_Data/Images/") & m.RequestLab.Split(Chr(4))(0) & ".png")
                g.DrawImage(image, Single.Parse(qrcode.Width * 0.28), Single.Parse(qrcode.Height * 0.4), Single.Parse(qrcode.Width * 0.44), Single.Parse(qrcode.Height * 0.2))
            End Using
            Dim converter As New ImageConverter
            Dim base64 = Convert.ToBase64String(converter.ConvertTo(qrcode, GetType(Byte())))
            m.QRCode = "data: Image;base64," & base64

            Dim Page As Integer = 0
            Dim PatAge As String
            If Integer.Parse(Now.ToString("MMdd")) < Integer.Parse(m.RequestPDOB.Value.ToString("MMdd")) Then Page = 1
            Page = Now.Year - m.RequestPDOB.Value.Year - Page
            If Page > 1 Then
                PatAge = Page & " YEARS OLD"
            Else
                PatAge = Page & " YEAR OLD"
            End If

            Dim barcode As String = ""
            barcode &= "Name: " & m.RequestPFName & " " & m.RequestPMName & " " & m.RequestPLName & " " & Chr(10)
            barcode &= "Date of Birth: " & m.RequestPDOB & Chr(10)
            barcode &= "Age: " & PatAge
            barcode &= "Address: " & m.RequestPAddress




            If m.ShowPreAuth Then
                If m.AddTest <> String.Empty Then
                    For Each item In m.AddTest.Split(Chr(23))
                        m.PreAuthTest &= item.Split(Chr(4))(1) & ", "
                    Next
                    m.PreAuthTest = m.PreAuthTest.Substring(0, m.PreAuthTest.Length - 2)
                End If
            End If

            Dim sql2 As String = "Select TOP 1 LOG_DT FROM LOG WHERE LOG_TYPE = 'NEW REQUEST' AND LOG_NEW = @DRAFT"
            Dim con2 As New SqlConnection(Parameters.eRequestConnectionString)
            Dim cmd2 As New SqlCommand(sql2, con2)
            cmd2.Parameters.AddWithValue("@DRAFT", m.DraftCode)
            con2.Open()
            Dim dtr2 As SqlDataReader = cmd2.ExecuteReader
            While dtr2.Read
                m.RequestedTimestamp = Helpers.LocalizeDateTime(Date.Parse(dtr2("LOG_DT").ToString), m.RequestLab.Split(Chr(4))(0))
            End While
            con2.Close()
            con2.Dispose()
            cmd2.Dispose()
#End Region
            Return View(m)
        End Function
        <Authorize>
        Function QRCode(id? As Integer) As ActionResult

            If id Is Nothing Then Return RedirectToAction("Index")

            Dim jsserializer As New Script.Serialization.JavaScriptSerializer
            Dim authcookie As HttpCookie = Request.Cookies(".eRequestPSCCK")
            Dim accountCK As Accounts.Account = jsserializer.Deserialize(Of Accounts.Account)(Helpers.GetFormsAuthenticationCookie)

            Dim headerparser As New RequestHeaders
            Dim header As RequestHeaders.RequestHeader = headerparser.SelectRequestHeader(id)
            If header Is Nothing Then
                Return Redirect(Url.Action("Index", "RequestTable"))
            Else
                If header.ID <> id Then
                    Return Redirect(Url.Action("Index", "RequestTable"))
                Else
                    Try
                        'Dim actparser As New ACTs
                        Dim code As String
                        Try
                            code = header.DraftCode & Chr(4) & header.RequestClient
                        Catch ex As Exception
                            code = header.DraftCode & Chr(4) & "WALKIN"
                        End Try

                        Dim xsecurity As New Support.Helpers.SECURITY("ereq_key_17")
                        code = xsecurity.EncryptData(code)
                        Dim enc As New QRCodeEncoder
                        Dim qrcodex As Bitmap = enc.Encode(code)
                        Dim image As Image
                        Using g As Graphics = Drawing.Graphics.FromImage(qrcodex)
                            image = Drawing.Image.FromFile(Server.MapPath("~/App_Data/Images/") & header.RequestLab.Split(Chr(4))(0) & ".png")
                            g.DrawImage(image, Single.Parse(qrcodex.Width * 0.28), Single.Parse(qrcodex.Height * 0.4), Single.Parse(qrcodex.Width * 0.44), Single.Parse(qrcodex.Height * 0.2))
                        End Using
                        Dim ms As New MemoryStream
                        qrcodex.Save(ms, Imaging.ImageFormat.Png)
                        ms.Position = 0
                        Return File(ms, "image/png", header.DraftCode & ".png")
                    Catch ex As Exception
                        Return Content("File not found.")
                    End Try
                End If
            End If

        End Function
        <Authorize>
        Function BarCode(id? As Integer) As ActionResult
            If id Is Nothing Then Return RedirectToAction("Index")

            Dim jsserializer As New Script.Serialization.JavaScriptSerializer
            Dim authcookie As HttpCookie = Request.Cookies(".eRequestPSCCK")
            Dim accountCK As Accounts.Account = jsserializer.Deserialize(Of Accounts.Account)(Helpers.GetFormsAuthenticationCookie)

            Dim headerparser As New RequestHeaders
            Dim header As RequestHeaders.RequestHeader = headerparser.SelectRequestHeader(id)
            If header Is Nothing Then
                Return Redirect(Url.Action("Index", "RequestTable"))
            Else
                'Dim printers = Dymo.frame
            End If
        End Function
    End Class
End Namespace