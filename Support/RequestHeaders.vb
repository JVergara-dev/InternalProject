Imports Support
Imports System.Data.SqlClient
Public Class RequestHeaders
    Public Class RequestHeader
        Property ID As Integer
        Property DraftCode As String
        Property RequestLab As String
        Property LabCode As String
        Property LabDesc As String
        Property RequestType As String
        Property RequestPSC As String
        Property RequestDate As Date?
        Property RequestTime As String
        Property RequestRequestor As String
        Property RequestClient As String
        Property RequestCollectedTS As DateTime?
        Property RequestCollectedBy As String
        Property RequestPLName As String
        Property RequestPMName As String
        Property RequestPFName As String
        Property RequestPDOB As Date?
        Property RequestPGender As String
        Property RequestPContact As String
        Property RequestPEmail As String
        Property RequestPAddress As String
        Property RequestPDiagnosis As String
        Property RequestPDiagnosis1 As String
        Property RequestPDiagnosis2 As String
        Property RequestPDiagnosis3 As String
        Property RequestAntiAgingRemarks As String
        Property RequestLabTestRemarks As String
        Property RequestAntiAging As String
        Property RequestAntiAgingCount As String
        Property RequestAntiAgingAmount As String
        Property RequestQuestionnaire As String
        Property RequestLabTest As String
        Property RequestLabTestCount As String
        Property RequestLabTestAmount As String
        Property RequestTransactionFee As String
        Property RequestCurrency As String
        Property RequestAmount As String
        Property RequestStripe As String
        Property RequestDiscountType As String
        Property RequestDiscountPercent As String
        Property RequestDiscountFixed As String
        Property RequestTotalDiscount As String
        Property RequestLocation As String
        Property RequestHomeServiceFee As String
        Property RequestPCardNumber As String
        Property RequestSpecimen As String
        Property SpecimenCode As New List(Of String)
        Property SpecimenName As New List(Of String)
        Property SpecimenQuant As New List(Of String)
        Property ShippingType As String
        Property ShippingRemarks As String
        Property ReferringDoctor As String
        Property DoctorSpecialization As String
        Property DoctorCode As String
        Property DoctorFName As String
        Property DoctorMName As String
        Property DoctorLName As String
        Property City As String
        Property State As String
        Property ZipCode As String
        Property Language As String
        Property SSN As String
        Property MRN As String
        Property Medicare As String
        Property MedicaID As String
        Property MissueDT As Date
        Property InsuredLN As String
        Property InsuredFN As String
        Property InsuredMN As String
        Property Relationship As String
        Property InsuranceCode As String
        Property InsuranceName As String
        Property HMOID As String
        Property HMOCode As String
        Property HMOName As String
        Property GroupNo As String
        Property InsuranceAddress As String
        Property InsuranceCity As String
        Property InsuranceState As String
        Property InsuranceZip As String
        Property InsuredID As String
        Property InsuredDOB As Date?
        Property InsurancePhoneNO As String
        Property PhlebID As String
        Property MNVPassed As String
        Property Template As String
        Property Show_ABN As String
        Property Employer As String
        Property EmployerAddress As String
        Property EmployerCity As String
        Property EmployerState As String
        Property EmployerZip As String
        Property EmployerPhone As String
        Property Secondary As String
        Property Additional As String
        Property Request_Header_Labtest As String
        'secondary
#Region "Secondary"
        Property SBilltype As String
        Property SMedicareNo As String
        Property SMedicaID As String
        Property SInsuranceCompany As String
        Property SInsuranceCompanyName As String
        Property SHMO As String
        Property SHMOName As String
        Property SGroupNo As String
        Property SInsuredID As String
        Property SRelationship As String
        Property SSSN As String
        Property SInsuredLName As String
        Property SInsuredFName As String
        Property SInsuredMName As String
        Property SInsuredDBO As Date?
        Property SInsuranceAdress As String
        Property SInsuranceCity As String
        Property SInsuranceState As String
        Property SInsuranceZip As String
        Property SInsurancePhoneNo As String
        Property SEmployer As String
        Property SEmployerAdress As String
        Property SEmployerCity As String
        Property SEmployerState As String
        Property SEmployerZip As String
        Property SEmployerPhone As String
        Property ShowPreAuth As String
        Property EmailSent As String
        Property BillToClient As String
        Property Terms As String
        Property MedicaidInsurance As String
        Property MedicaidInsuranceName As String
        Property SMedicaidInsurance As String
        Property SMedicaidInsuranceName As String
#End Region

        'additional

#Region "Comments"
        ''Property Laboratory As SITEs.SITE

        ''Property Requestor As ACTs.ACT

        'Property RequestorNPI As String

        ''Property Patient As ACTs.ACT

        'Property Contact As String

        'Property Address As String

        'Property Diagnosis As String

        'Property OrderAntiAging As Boolean

        ''Property AntiAgingTests As List(Of TESTS.TESTSELECTION)

        ''Property AntiAgingProfiles As List(Of PROFILES.PROFILESELECTION)

        'Property AntiAgingRemarks As String

        'Property AnswerQuestionnaire As Boolean

        'Property Questionnaire As String

        'Property Instructions As String

        ''Property Questions As List(Of QUESTIONNAIRES.QUESTION)

        'Property OrderLab As Boolean

        ''Property LaboratoryTests As List(Of TESTS.TESTSELECTION)

        ''Property LaboratoryProfiles As List(Of PROFILES.PROFILESELECTION)

        'Property LaboratoryRemarks As String

        ''Property Appointment As APPOINTMENTS.APPOINTMENTSELECTION

        'Property Currency As String

        'Property TransactionFee As Decimal

        'Property Location As String

        'Property CollectionTS As Date?

        'Property CollectedBy As String

        'Property DiscountPercent As List(Of Decimal)

        'Property DiscountFixed As List(Of Decimal)

        'Property DiscountType As List(Of String)

        'Property TotalDiscount As Decimal

        'Property HomeServiceFee As Decimal

        'Property FixedFee As Decimal

        'Property SpecimenCollectionLocation As String

        'Property Expiry As Date

        ''Property SpecimenList As List(Of SPECIMENS.SPECIMEN)

        'Property MedicareNumber As String

        'Property MedicaidNumber As String

        'Property Insurance As String

        'Property InsuranceName As String

        'Property InsuranceGroupNumber As String

        'Property InsurancePhoneNumber As String

        'Property HMO As HMOs.HMO

        'Property HMOGroupNumber As String

        'Property InsuredID As String

        'Property Relationship As String

        'Property ShippingType As String

        'Property ShippingRemarks As String

        'Property ReferringDoctor As String

        'Property ViewMode As String

        'Property DoctorSpecialization As String

        'Property DoctorCode As String

        'Property DoctorFName As String

        'Property DoctorMName As String

        'Property DoctorLName As String

        'Property PatientFound As Boolean

        'Property PatientUsed As Boolean

        ''Property PatientLastTransaction As REQUEST_HEADERS.REQUEST_HEADER

        'Property City As String

        'Property State As String

        'Property ZipCode As String

        'Property Distance As Double

        'Property Language As String

        'Property Channel As String

        ''Property Voucher As VOUCHERS.VOUCHER

        'Property VoucherDiscount As Decimal

        ''Property Template As List(Of REQUEST_FORM_TEMPLATES.REQUEST_FORM_TEMPLATE)

        ''Property TemplateList As List(Of SelectListItem)

        ''Property TemplateSelected As REQUEST_FORM_TEMPLATES.REQUEST_FORM_TEMPLATE

        'Property SSN As String

        'Property MRN As String

        'Property InsuranceIssueDT As Date

        'Property InsuranceDOB As Date

        ''Property BillTypeList As List(Of SelectListItem)

        ''Property InsuranceCompanyList As List(Of SelectListItem)

        ''Property MedicaidList As List(Of SelectListItem)

        'Property MedicaidInsurance As String

        'Property MedicaidInsuranceName As String

        ''Property HMOList As List(Of SelectListItem)

        'Property InsuredLN As String

        'Property InsuredFN As String

        'Property InsuredMN As String

        'Property InsuranceAddress As String

        'Property InsuranceCity As String

        'Property InsuranceState As String

        'Property InsuranceZip As String

        'Property PhlebID As String

        'Property Additional As String

        ''Property CurrentTests As List(Of TESTS.TESTSELECTION)

        ''Property CurrentProfiles As List(Of PROFILES.PROFILESELECTION)

        ''Property CustomTests As List(Of TESTS.TESTSELECTION)

        ''Property CustomProfiles As List(Of PROFILES.PROFILESELECTION)

        'Property ExchangeRate As Decimal

        'Property NeedReferral As Boolean

        ''Property DoctorSpecializationList As List(Of SelectListItem)

        'Property OldBillType As String

        'Property OldHMO As String

        'Property OldInsurance As String

        'Property SecondaryBillType As String

        ''Property SecondaryBillTypeList As List(Of SelectListItem)

        'Property SecondaryMedicareNo As String

        'Property SecondaryMedicaidNo As String

        'Property SecondaryIssuedDT As Date?

        'Property SecondaryInsuredFN As String

        'Property SecondaryInsuredLN As String

        'Property SecondaryInsuredMN As String

        'Property SecondaryRelationship As String

        ''Property SecondaryRelationshipList As List(Of SelectListItem)

        'Property SecondaryHMO As String

        ''Property SecondaryHMOList As List(Of SelectListItem)

        'Property SecondaryInsuranceCompany As String
        'Property SecondaryInsuranceCompanyName As String

        ''Property SecondaryInsuranceCompanyList As List(Of SelectListItem)

        ''Property SecondaryMedicaidList As List(Of SelectListItem)

        'Property SecondaryMedicaidInsurance As String

        'Property SecondaryMedicaidInsuranceName As String

        'Property SecondaryGroupNo As String

        'Property SecondaryInsuranceAddress As String

        'Property SecondaryInsuranceCity As String

        'Property SecondaryInsuranceState As String

        'Property SecondaryInsuranceZip As String

        'Property SecondaryInsuredID As String

        'Property SecondaryInsuredDOB As Date?

        'Property SecondaryInsurancePhoneNo As String

        'Property Employer As String
        'Property EmpAddress As String
        'Property EmpCity As String
        'Property EmpState As String
        'Property EmpZip As String
        'Property EmpPhone As String

        'Property SecondaryEmployer As String

        'Property SecondaryEmpAddress As String

        'Property SecondaryEmpCity As String

        'Property SecondaryEmpState As String

        'Property SecondaryEmpZip As String

        'Property SecondaryEmpPhone As String

        'Property SecondarySSN As String

        'Property SecondaryHMOName As String

        'Property HasAdditional As Boolean

        'Property AdditionalCodes As String

        'Property ShowABN As Boolean

        'Property ShowPreAuth As Boolean

        'Property EmailSent As Boolean

        'Property BilledToClient As Boolean

        ''Property GlucoseTests As List(Of TESTS.TESTSELECTION)

        ''Property ReferringDoctors As List(Of ReferringDoctors.ReferringDoctor)

#End Region


    End Class
    Public Class PSCQueue
        Property ID As Integer
        Property Code As String
        Property IsProcessed As Boolean
        Property ProcessedTS As DateTime
        Property Personnel_Name As String
        Property Personnel_UserName As String
        Property PSC_Code As String
        Property PSC_Name As String
    End Class
    Function SelectPSCQueueByCode(Code As String) As PSCQueue
        Dim retObj As New PSCQueue
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_selectPSCQueueByCode", con)
        cmd.Parameters.AddWithValue("@Code", Code)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    retObj.ID = rdr("ID")
                    retObj.Code = rdr("Code")
                    retObj.IsProcessed = rdr("IsProcessed")
                    retObj.ProcessedTS = rdr("ProcessedTS")
                    retObj.Personnel_Name = IIf(IsDBNull(rdr("Personnel_Name")), "", rdr("Personnel_Name"))
                    retObj.Personnel_UserName = IIf(IsDBNull(rdr("Personnel_UserName")), "", rdr("Personnel_UserName"))
                    retObj.PSC_Code = IIf(IsDBNull(rdr("PSC_Code")), "", rdr("PSC_Code"))
                    retObj.PSC_Name = IIf(IsDBNull(rdr("PSC_Name")), "", rdr("PSC_Name"))
                End While
            End If
        Catch ex As Exception
            retObj = New PSCQueue
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
    Function SelectRequestHeader(ID As Integer) As RequestHeader
        Dim retObj As New RequestHeader
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_SelectRequestHeaderByID", con)
        cmd.Parameters.AddWithValue("@ID", ID)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
#Region "Primary"
                    retObj.ID = rdr("ID")
                    retObj.DraftCode = rdr("REQUEST_HEADER_DRAFT")
                    retObj.RequestLab = rdr("REQUEST_HEADER_LAB")
                    retObj.Request_Header_Labtest = rdr("REQUEST_HEADER_LABTEST")
                    Dim ReqLab As String() = retObj.RequestLab.Split(Chr(4))
                    retObj.LabCode = ReqLab(0)
                    retObj.LabDesc = ReqLab(1)
                    retObj.RequestType = rdr("REQUEST_HEADER_TYPE")
                    retObj.RequestPSC = rdr("REQUEST_HEADER_PSC")
                    retObj.RequestDate = rdr("REQUEST_HEADER_DATE")
                    retObj.RequestTime = rdr("REQUEST_HEADER_TIME")
                    retObj.RequestRequestor = rdr("REQUEST_HEADER_REQUESTOR")
                    retObj.RequestClient = rdr("REQUEST_HEADER_CLIENT")
                    retObj.RequestCollectedTS = rdr("REQUEST_HEADER_COLLECTEDTS")
                    retObj.RequestCollectedBy = rdr("REQUEST_HEADER_COLLECTEDBY")
                    retObj.RequestPLName = rdr("REQUEST_HEADER_PLNAME")
                    retObj.RequestPMName = rdr("REQUEST_HEADER_PMNAME")
                    retObj.RequestPFName = rdr("REQUEST_HEADER_PFNAME")
                    retObj.RequestPDOB = rdr("REQUEST_HEADER_PDOB")
                    retObj.RequestPGender = rdr("REQUEST_HEADER_PGENDER")
                    retObj.RequestPContact = rdr("REQUEST_HEADER_PCONTACT")
                    retObj.RequestPEmail = rdr("REQUEST_HEADER_PEMAIL")
                    retObj.RequestPAddress = rdr("REQUEST_HEADER_PADDRESS")
                    retObj.RequestPDiagnosis = rdr("REQUEST_HEADER_PDIAGNOSIS")
                    Dim ReqDiag As String() = retObj.RequestPDiagnosis.Split(Chr(4))
                    retObj.RequestPDiagnosis1 = ReqDiag(0)
                    retObj.RequestPDiagnosis2 = ReqDiag(1)
                    retObj.RequestPDiagnosis3 = ReqDiag(2)
                    retObj.RequestAntiAgingRemarks = rdr("REQUEST_HEADER_ANTIAGINGREMARKS")
                    retObj.RequestLabTestRemarks = rdr("REQUEST_HEADER_LABTESTREMARKS")
                    retObj.RequestAntiAging = rdr("REQUEST_HEADER_ANTIAGING")
                    retObj.RequestAntiAgingCount = rdr("REQUEST_HEADER_ANTIAGINGCOUNT")
                    retObj.RequestAntiAgingAmount = rdr("REQUEST_HEADER_ANTIAGINGAMOUNT")
                    retObj.RequestQuestionnaire = rdr("REQUEST_HEADER_QUESTIONNAIRE")
                    retObj.RequestLabTest = rdr("REQUEST_HEADER_LABTEST")
                    retObj.RequestLabTestCount = rdr("REQUEST_HEADER_LABTESTCOUNT")
                    retObj.RequestLabTestAmount = rdr("REQUEST_HEADER_LABTESTAMOUNT")
                    retObj.RequestTransactionFee = rdr("REQUEST_HEADER_TRANSACTIONFEE")
                    retObj.RequestCurrency = rdr("REQUEST_HEADER_CURRENCY")
                    retObj.RequestAmount = rdr("REQUEST_HEADER_AMOUNT")
                    retObj.RequestStripe = rdr("REQUEST_HEADER_STRIPE")
                    retObj.RequestDiscountType = rdr("REQUEST_HEADER_DISCOUNTTYPE")
                    retObj.RequestDiscountPercent = rdr("REQUEST_HEADER_DISCOUNTPERCENT")
                    retObj.RequestDiscountFixed = rdr("REQUEST_HEADER_DISCOUNTFIXED")
                    retObj.RequestTotalDiscount = rdr("REQUEST_HEADER_TOTALDISCOUNT")
                    retObj.RequestLocation = rdr("REQUEST_HEADER_LOCATION")
                    retObj.RequestHomeServiceFee = rdr("REQUEST_HEADER_HOMESERVICEFEE")
                    retObj.RequestPCardNumber = rdr("REQUEST_HEADER_PCARDNUMBER")
                    retObj.RequestSpecimen = rdr("REQUEST_HEADER_SPECIMEN")
                    'Dim Specimen As String() = retObj.RequestSpecimen.Split(Chr(23))
                    'For Each item In Specimen
                    '    If item = "" Then Exit For
                    '    Dim QItem As String() = item.Split(Chr(4))
                    '    retObj.SpecimenCode.Add(QItem(0).ToString)
                    '    retObj.SpecimenName.Add(QItem(1).ToString)
                    '    retObj.SpecimenQuant.Add(QItem(2).ToString)
                    'Next
                    retObj.ShippingType = rdr("SHIPPING_TYPE")
                    retObj.ReferringDoctor = rdr("REFERRING_DOCTOR")
                    retObj.DoctorSpecialization = rdr("DOCTOR_SPECIALIZATION")
                    retObj.DoctorCode = rdr("DOCTOR_CODE")
                    retObj.DoctorFName = rdr("DOCTOR_FNAME")
                    retObj.DoctorMName = rdr("DOCTOR_MNAME")
                    retObj.DoctorLName = rdr("DOCTOR_LNAME")
                    retObj.City = rdr("CITY")
                    retObj.State = rdr("STATE")
                    retObj.ZipCode = rdr("ZIPCODE")
                    retObj.Language = rdr("LANGUAGE")
                    retObj.SSN = rdr("SSN")
                    retObj.MRN = rdr("MRN")
                    retObj.Medicare = rdr("MEDICARE")
                    retObj.MedicaID = rdr("MEDICAID")
                    'retObj.MissueDT = IIf(rdr("MISSUEDT") Is Nothing, rdr("MISSUEDT"), "")
                    retObj.InsuredLN = rdr("INSUREDLN")
                    retObj.InsuredFN = rdr("INSUREDFN")
                    retObj.InsuredMN = rdr("INSUREDMN")
                    retObj.Relationship = rdr("RELATIONSHIP")
                    retObj.InsuranceCode = rdr("INSURANCECODE")
                    retObj.InsuranceName = rdr("INSURANCENAME")
                    retObj.HMOID = rdr("HMOID")
                    retObj.HMOCode = rdr("HMOCODE")
                    retObj.HMOName = rdr("HMONAME")
                    retObj.GroupNo = rdr("GROUPNO")
                    retObj.InsuranceAddress = rdr("INSURANCEADDRESS")
                    retObj.InsuranceCity = rdr("INSURANCECITY")
                    retObj.InsuranceState = rdr("INSURANCESTATE")
                    retObj.InsuranceZip = rdr("INSURANCEZIP")
                    retObj.InsuredID = rdr("INSUREDID")
                    retObj.InsuredDOB = rdr("INSUREDDOB")
                    retObj.InsurancePhoneNO = rdr("INSURANCEPHONENO")
                    retObj.PhlebID = rdr("PHLEBID")
                    retObj.MNVPassed = rdr("TEMPLATE")
                    retObj.Additional = rdr("ADDITIONAL")
#End Region
#Region "Secondary"
                    retObj.Secondary = rdr("LA_Secondary")
                    Dim Secondarily As String = rdr("LA_Secondary")
                    retObj.Employer = Secondarily.Split(Chr(4))(0)
                    retObj.EmployerAddress = Secondarily.Split(Chr(4))(1)
                    retObj.EmployerCity = Secondarily.Split(Chr(4))(2)
                    retObj.EmployerState = Secondarily.Split(Chr(4))(3)
                    retObj.EmployerZip = Secondarily.Split(Chr(4))(4)
                    retObj.EmployerPhone = Secondarily.Split(Chr(4))(5)
                    retObj.SBilltype = Secondarily.Split(Chr(4))(6)
                    retObj.SMedicareNo = Secondarily.Split(Chr(4))(7)
                    retObj.SMedicaID = Secondarily.Split(Chr(4))(8)
                    retObj.SInsuranceCompanyName = Secondarily.Split(Chr(4))(9)
                    retObj.SInsuranceCompany = Secondarily.Split(Chr(4))(10)
                    retObj.SHMO = Secondarily.Split(Chr(4))(12)
                    retObj.SHMOName = Secondarily.Split(Chr(4))(11)
                    retObj.SGroupNo = Secondarily.Split(Chr(4))(13)
                    retObj.SInsuredID = Secondarily.Split(Chr(4))(14)
                    retObj.SRelationship = Secondarily.Split(Chr(4))(15)
                    retObj.SSSN = Secondarily.Split(Chr(4))(16)
                    retObj.SInsuredLName = Secondarily.Split(Chr(4))(17)
                    retObj.SInsuredFName = Secondarily.Split(Chr(4))(18)
                    retObj.SInsuredMName = Secondarily.Split(Chr(4))(19)
                    'retObj.SHMO = Secondarily.Split(Chr(4))(21)
                    'retObj.SInsuredDBO = IIf(Secondarily.Split(Chr(4))(20) = "", "01/01/0001", Secondarily.Split(Chr(4))(20))
                    If Secondarily.Split(Chr(4))(20) = "" Then
                        retObj.SInsuredDBO = "01/01/0001"
                    Else
                        retObj.SInsuredDBO = Secondarily.Split(Chr(4))(20)
                    End If
                    retObj.SInsuranceAdress = Secondarily.Split(Chr(4))(21)
                    retObj.SInsuranceCity = Secondarily.Split(Chr(4))(22)
                    retObj.SInsuranceState = Secondarily.Split(Chr(4))(23)
                    retObj.SInsuranceZip = Secondarily.Split(Chr(4))(24)
                    retObj.SInsurancePhoneNo = Secondarily.Split(Chr(4))(25)
                    retObj.SEmployer = Secondarily.Split(Chr(4))(26)
                    retObj.SEmployerAdress = Secondarily.Split(Chr(4))(27)
                    retObj.SEmployerCity = Secondarily.Split(Chr(4))(28)
                    retObj.SEmployerState = Secondarily.Split(Chr(4))(29)
                    retObj.SEmployerZip = Secondarily.Split(Chr(4))(30)
                    retObj.SEmployerPhone = Secondarily.Split(Chr(4))(31)
                    retObj.ShowPreAuth = IIf(Secondarily.Split(Chr(4))(32) = "", 0, Secondarily.Split(Chr(4))(32))
                    retObj.EmailSent = Secondarily.Split(Chr(4))(33)
                    retObj.BillToClient = Secondarily.Split(Chr(4))(34)
                    retObj.Terms = Secondarily.Split(Chr(4))(35)
                    retObj.MedicaidInsurance = Secondarily.Split(Chr(4))(36)
                    retObj.MedicaidInsuranceName = Secondarily.Split(Chr(4))(37)
                    retObj.SMedicaidInsurance = Secondarily.Split(Chr(4))(38)
                    retObj.SMedicaidInsuranceName = Secondarily.Split(Chr(4))(39)
#End Region
                End While
            End If
        Catch ex As Exception
            retObj = New RequestHeader
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
    Function SelectRequestHeaderTable(key As String, val As Integer) As List(Of RequestHeader)
        Dim retObj As New List(Of RequestHeader)
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_SelectRequestHeaderByPSCQueue", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@Top", val)
        cmd.Parameters.AddWithValue("@Key", "%" + key + "%")
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    Dim temp As New RequestHeader
                    temp.ID = rdr("ID")
                    temp.DraftCode = rdr("REQUEST_HEADER_DRAFT")
                    temp.RequestClient = IIf(rdr("REQUEST_HEADER_CLIENT") Is Nothing, "", rdr("REQUEST_HEADER_CLIENT"))
                    temp.DoctorFName = IIf(IsDBNull(rdr("DOCTOR_FNAME")), "", rdr("DOCTOR_FNAME"))
                    temp.DoctorLName = IIf(IsDBNull(rdr("DOCTOR_LNAME")), "", rdr("DOCTOR_LNAME"))
                    temp.DoctorMName = IIf(IsDBNull(rdr("DOCTOR_MNAME")), "", rdr("DOCTOR_MNAME"))
                    temp.RequestPFName = IIf(IsDBNull(rdr("REQUEST_HEADER_PFNAME")), "", rdr("REQUEST_HEADER_PFNAME"))
                    temp.RequestPMName = IIf(IsDBNull(rdr("REQUEST_HEADER_PMNAME")), "", rdr("REQUEST_HEADER_PMNAME"))
                    temp.RequestPLName = IIf(IsDBNull(rdr("REQUEST_HEADER_PLNAME")), "", rdr("REQUEST_HEADER_PLNAME"))
                    retObj.Add(temp)
                End While
            End If
        Catch ex As Exception
            retObj = New List(Of RequestHeader)
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function

    Function SelectRequestHeaderTableProcessed(key As String, val As Integer) As List(Of RequestHeader)
        Dim retObj As New List(Of RequestHeader)
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_SelectRequestHeaderByPSCQueueProcessed", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@Top", val)
        cmd.Parameters.AddWithValue("@Key", "%" + key + "%")
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    Dim temp As New RequestHeader
                    temp.ID = rdr("ID")
                    temp.DraftCode = rdr("REQUEST_HEADER_DRAFT")
                    temp.RequestClient = IIf(rdr("REQUEST_HEADER_CLIENT") Is Nothing, "", rdr("REQUEST_HEADER_CLIENT"))
                    temp.DoctorFName = IIf(IsDBNull(rdr("DOCTOR_FNAME")), "", rdr("DOCTOR_FNAME"))
                    temp.DoctorLName = IIf(IsDBNull(rdr("DOCTOR_LNAME")), "", rdr("DOCTOR_LNAME"))
                    temp.DoctorMName = IIf(IsDBNull(rdr("DOCTOR_MNAME")), "", rdr("DOCTOR_MNAME"))
                    temp.RequestPFName = IIf(IsDBNull(rdr("REQUEST_HEADER_PFNAME")), "", rdr("REQUEST_HEADER_PFNAME"))
                    temp.RequestPMName = IIf(IsDBNull(rdr("REQUEST_HEADER_PMNAME")), "", rdr("REQUEST_HEADER_PMNAME"))
                    temp.RequestPLName = IIf(IsDBNull(rdr("REQUEST_HEADER_PLNAME")), "", rdr("REQUEST_HEADER_PLNAME"))
                    retObj.Add(temp)
                End While
            End If
        Catch ex As Exception
            retObj = New List(Of RequestHeader)
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
    'Function UpdateRequestHeader(ID As Integer) As RequestHeader
    '    Dim retObj As Boolean = False
    '    Dim HeaderValues As New RequestHeader
    '    Dim con As New SqlConnection(Parameters.eRequestConnectionString)
    '    Dim cmd As New SqlCommand("sp_UpdateRequestHeader", con)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.AddWithValue("@ID", ID)
    '    cmd.Parameters.AddWithValue("@PLName", HeaderValues.RequestPLName)
    '    cmd.Parameters.AddWithValue("@PFName", HeaderValues.RequestPFName)
    '    cmd.Parameters.AddWithValue("@PMName", HeaderValues.RequestPMName)
    '    cmd.Parameters.AddWithValue("@MRN", HeaderValues.MRN)
    '    cmd.Parameters.AddWithValue("@PDOB", HeaderValues.RequestPDOB)
    '    cmd.Parameters.AddWithValue("@PGender", HeaderValues.RequestPGender)
    '    cmd.Parameters.AddWithValue("@PAddress", HeaderValues.RequestPAddress)
    '    cmd.Parameters.AddWithValue("@PCity", HeaderValues.City)
    '    cmd.Parameters.AddWithValue("@PState", HeaderValues.State)
    '    cmd.Parameters.AddWithValue("@PZipCode", HeaderValues.ZipCode)
    '    cmd.Parameters.AddWithValue("@PContactNo", HeaderValues.RequestPContact)
    '    cmd.Parameters.AddWithValue("@PMedicareNo", HeaderValues.Medicare)
    '    cmd.Parameters.AddWithValue("@MedicaID", HeaderValues.MedicaID)
    '    cmd.Parameters.AddWithValue("@InsuranceCode", HeaderValues.InsuranceName)
    '    cmd.Parameters.AddWithValue("@HMOCode", HeaderValues.HMOName)
    '    cmd.Parameters.AddWithValue("@GroupNo", HeaderValues.GroupNo)
    '    cmd.Parameters.AddWithValue("@InsuredID", HeaderValues.InsuredID)
    '    cmd.Parameters.AddWithValue("@RelationShip", HeaderValues.Relationship)
    '    cmd.Parameters.AddWithValue("@SSN", HeaderValues.SSN)
    '    cmd.Parameters.AddWithValue("@InsureLName", HeaderValues.InsuredLN)
    '    cmd.Parameters.AddWithValue("@InsureFName", HeaderValues.InsuredFN)
    '    cmd.Parameters.AddWithValue("@InsureMName", HeaderValues.InsuredMN)
    '    cmd.Parameters.AddWithValue("@InsureDOB", HeaderValues.InsuredDOB)
    '    cmd.Parameters.AddWithValue("@InsureAddress", HeaderValues.InsuranceAddress)
    '    cmd.Parameters.AddWithValue("@InsureCity", HeaderValues.InsuranceCity)
    '    cmd.Parameters.AddWithValue("@InsureState", HeaderValues.InsuranceState)
    '    cmd.Parameters.AddWithValue("@InsureZipCode", HeaderValues.InsuranceZip)
    '    cmd.Parameters.AddWithValue("@InsureContactNo", HeaderValues.InsuranceZip)
    '    cmd.Parameters.AddWithValue("@Secondary", HeaderValues.InsuranceZip)
    '    cmd.Parameters.AddWithValue("@Specimen", HeaderValues.InsuranceZip)
    '    Try
    '        con.Open()
    '        If con.State <> ConnectionState.Open Then Throw New Exception
    '        Dim rdr As SqlDataReader = cmd.ExecuteReader
    '        If rdr.HasRows Then
    '            retObj = True
    '        Else
    '            retObj = False
    '        End If
    '    Catch ex As Exception
    '        retObj = False
    '    Finally
    '        con.Close()
    '        con.Dispose()
    '        cmd.Dispose()
    '    End Try
    'End Function
End Class
