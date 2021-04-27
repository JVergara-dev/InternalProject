
Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc
Imports Support

Namespace Models
    Public Class MainModel
        Property GenderList As List(Of SelectListItem)
        Property PBilltype As List(Of SelectListItem)
        Property SSBilltype As List(Of SelectListItem)
        Property RelationshipList As List(Of SelectListItem)
        Property HMOList As List(Of SelectListItem)
        Property SHMOList As List(Of SelectListItem)
        Property InsuranceCompanyList As List(Of SelectListItem)
        Property PMedicaidIns As List(Of SelectListItem)
        Property PMedicaidInsIndex As String
        Property SMedicaidIns As List(Of SelectListItem)
        Property SMedicaidInsIndex As String

        Property MedicaidList As List(Of SelectListItem)

        Property SRelationshipList As List(Of SelectListItem)

        Property SecondaryHMOList As List(Of SelectListItem)

        Property SInsuranceCompanyList As List(Of SelectListItem)
        Property BarCode As String
        Property RequestedTimestamp As Date
        Property PreAuthTest As String
        Property ID As Integer
        Property DraftCode As String
        Property RequestLab As String
        Property LabCode As String
        Property LabDesc As String
        Property RequestType As String
        Property RequestPSC As String
        Property RequestDate As Date
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
        Property RequestAntiAgingCount As Integer
        Property RequestAntiAgingAmount As String
        Property RequestQuestionnaire As String
        Property RequestLabTest As String
        Property RequestLabTestCount As Integer
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
        Property SpecimenCode As List(Of String)
        Property SpecimenName As List(Of String)
        Property SpecimenQuant As List(Of String)
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
        Property Specimens As List(Of Specimen)
        Property Tests As List(Of Test)
        Property NPI As String
        Property PSCID As Integer
        Property PSCCode As String
        Property PSCIsProcessed As Boolean
        Property PSCProcessedTS As DateTime
        Property PSCPersonnel_Name As String
        Property PSCPersonnel_UserName As String
        Property PSCPSC_Code As String
        Property PSCPSC_Name As String
        Property QRCode As String
        Property AddTest As String
        Property AddTestList As List(Of AdditionalTest)
        Property AddTestHL7 As List(Of AdditionalTestHL7)
        Property Request_Header_LabTest As String
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
        Property SInsuredDOB As Date?
        Property SInsuranceAddress As String
        Property SInsuranceCity As String
        Property SInsuranceState As String
        Property SInsuranceZip As String
        Property SInsurancePhoneNo As String
        Property SEmployer As String
        Property SEmployerAddress As String
        Property SEmployerCity As String
        Property SEmployerState As String
        Property SEmployerZip As String
        Property SEmployerPhone As String
        Property ShowPreAuth As Boolean
        Property EmailSent As String
        Property BillToClient As Boolean
        Property Terms As String
        Property MedicaidInsurance As String
        Property MedicaidInsuranceName As String
        Property SMedicaidInsurance As String
        Property SMedicaidInsuranceName As String
#End Region
        Public Class Specimen
            Property SpecCode As String
            Property SpecDesc As String
            Property SpecQuantity As Integer
        End Class
        Public Class Test
            Property TestCode As String
            Property LabelTest As String
            Property TestName As String
            Property TestZero As String
            Property TestQuestion As String
            Property TestAnswer As String
            Property ICD1 As String
            Property ICD2 As String
            Property ICD3 As String
            Property ICD4 As String
            Property TestSite As String
        End Class
        Public Class AdditionalTestHL7
            Property TestCode As String
            Property LabelTest As String
            Property TestName As String
            Property TestZero As String
            Property TestQuestion As String
            Property TestAnswer As String
            Property ICD1 As String
            Property ICD2 As String
            Property ICD3 As String
            Property ICD4 As String
            Property TestSite As String
        End Class
        Public Class AdditionalTest
            Property AddCode As String
            Property AddDesc As String
        End Class
    End Class

End Namespace

