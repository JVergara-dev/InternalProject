Public Class HL7Handler
    Sub New()



    End Sub

    Public Function MSH(dt As Date) As String

        Dim retObj As String = String.Empty
        retObj = String.Format("MSH|^~\&|||||{0}||ORM^O01|000000000000001|{1}", dt.ToString("yyyyMMddhhmmss"), vbNewLine)
        Return retObj

    End Function

    Public Function PID(ln As String, fn As String, dob As Date) As String

        Dim retObj As String = String.Empty
        ln &= "   "
        ln = ln.Substring(0, 3).Trim.ToUpper
        fn &= "   "
        fn = fn.Substring(0, 3).Trim.ToUpper
        Dim mrn As String = String.Format("{0}{1}{2}", ln, fn, dob.ToString("MMddyyyy"))
        retObj = String.Format("PID|||{0}||||||||||||||||{1}", mrn, vbNewLine)
        Return retObj

    End Function

    Public Function PV1(bt As String) As String

        Dim retObj As String = String.Empty
        Dim code As String = String.Empty
        Select Case bt
            Case "Client"
                code = "A"
            Case "Medicare"
                code = "B"
            Case "Medicaid"
                code = "C"
            Case "HMO"
                code = "D"
            Case "Insurance"
                code = "E"
            Case "Patient"
                code = "F"
        End Select
        retObj = String.Format("PV1||||||||||||||||||||{0}|{1}", code, vbNewLine)
        Return retObj

    End Function

    Public Function ORC(p As String) As String

        Dim retObj As String = String.Empty
        retObj = String.Format("ORC|NW|{0}|||||^^^^^^|||||||||||||{1}", p, vbNewLine)
        Return retObj

    End Function

    Public Function OBR(p As String, t As String, cdt As Date, ss As String, lmp As Date, cc As String) As String

        'add doctor code
        Dim retObj As String = String.Empty
        retObj = String.Format("OBR||{0}||{1}|||{2}||||O|||[DATE_TIME_RECEIVED_HANDLER]|{3}^^{4}^^^|{5}||LAB|||||||||^^^{2}^^R||||{6}", p, t, cdt.ToString("yyyyMMddhhmmss"), ss, lmp.ToString("yyyyMMdd"), cc, vbNewLine)
        Return retObj

    End Function

    Public Function DG1(icd As String) As String

        Dim retObj As String = String.Empty
        retObj = String.Format("DG1|||{0}|{1}", icd, vbNewLine)
        Return retObj

    End Function

    Public Function NTE(c As String) As String

        Dim retObj As String = String.Empty
        Dim cl As Integer = c.Length
        Dim cd As Integer = Math.Ceiling(cl / 50.0)
        For i = 1 To cd
            Try
                retObj &= String.Format("NTE|||{0}|{1}", c.Substring((i - 1) * 50, 50), vbNewLine)
            Catch ex As Exception
                retObj &= String.Format("NTE|||{0}|{1}", c.Substring((i - 1) * 50, cl Mod 50), vbNewLine)
            End Try
        Next
        'retObj = String.Format("NTE|||{0}|{1}", c, vbNewLine)
        Return retObj

    End Function

    Public Function OBX(q As String, a As String) As String

        Dim retObj As String = String.Empty
        retObj = String.Format("OBX|||^{0}||{1}|{2}", q, a, vbNewLine)
        Return retObj

    End Function

    Public Class Demographics

        Public Function MSH(dt7 As Date, s14 As String) As String

            Dim retObj As String = String.Empty
            'MSH|^~\&|||||20040605101530||ADT|000000000000001|
            retObj &= "MSH"
            retObj &= "|"
            retObj &= "^~\&"
            retObj &= "|||||"
            retObj &= dt7.ToString("yyyyMMddHHmmss")
            retObj &= "||"
            retObj &= "ADT^A01"
            retObj &= "|"
            retObj &= s14
            retObj &= "|"
            Return retObj

        End Function

        Public Function PID(s3 As String, s5_1 As String, s5_2 As String, s5_3 As String, dt7 As Date, s8 As String, s11_1 As String, s11_2 As String, s11_3 As String, s11_4 As String, s13 As String, s18 As String, s19 As String) As String

            Dim retObj As String = String.Empty
            'PID|||MRN001||PRESLEY^ELVIS^A||19350108|M||C|1134 ROCKY ROAD^^MEMPHIS^TN^38116-5716||(321)555-6789|||||ADM001|111223333|
            retObj &= "PID"
            retObj &= "|||"
            retObj &= s3
            retObj &= "||"
            retObj &= String.Format("{0}^{1}^{2}", s5_1, s5_2, s5_3)
            retObj &= "||"
            retObj &= dt7.ToString("yyyyMMdd")
            retObj &= "|"
            retObj &= s8
            retObj &= "|||"
            retObj &= String.Format("{0}^{1}^{2}^{3}", s11_1, s11_2, s11_3, s11_4)
            retObj &= "||"
            retObj &= s13
            retObj &= "|||||"
            retObj &= s18
            retObj &= "|"
            retObj &= s19
            retObj &= "|"
            Return retObj

        End Function

    End Class

    Public Class Orders

    End Class

    Public Class MergedHL7

        Public Function MSH(dt As Date, draft As String) As String

            Dim retObj As String = String.Empty
            'retObj = String.Format("MSH|^~\&|||||{0}|||{1}|{2}", dt.ToString("yyyyMMddHHmmss"), draft, vbNewLine)

            retObj = String.Format("MSH|^~\&|||||{0}||ORM^001|{1}|{2}", dt.ToString("yyyyMMddHHmmss"), draft, vbNewLine)

            '6.14.18
            retObj = String.Format("MSH|^~\&|||ABCLAB|LAB|{0}||ORM^001|{1}|P|2.3|||NE|NE{2}", dt.ToString("yyyyMMddHHmmss"), draft, vbNewLine)

            Return retObj

        End Function

        Public Function PID(fn As String, mn As String, ln As String, dob As Date, gender As String, address As String, city As String, state As String, zip As String, phone As String, aka As String, clientno As String, doccode As String) As String

            Dim retObj As String = String.Empty
            Dim tln As String = String.Format("{0}   ", ln)
            Dim tfn As String = String.Format("{0}   ", fn)
            Dim mrn As String = String.Empty
            mrn = String.Format("{0}{1}{2}", tln.Substring(0, 3).Trim.ToUpper, tfn.Substring(0, 3).Trim.ToUpper, dob.ToString("MMddyyyy"))
            retObj = String.Format("PID|{14}||{0}||{1}^{2}^{3}||{4}|{5}|{12}||{6}^^{7}^{8}^{9}||{10}|||||{13}||{11}", mrn.ToUpper, ln.Trim.ToUpper, fn.Trim.ToUpper, mn.Trim.ToUpper, dob.ToString("yyyyMMdd"), gender.Substring(0, 1).ToUpper, address.ToUpper, city.ToUpper, state.ToUpper, zip.ToUpper, phone.ToUpper, vbNewLine, aka, clientno, doccode)
            ''6.14.18
            'retObj = String.Format("PID|{14}||{0}||{1}^{2}^{3}||{4}|{5}|{12}||{6}^^{7}^{8}^{9}||{10}|||||{14}||{11}", mrn.ToUpper, ln.Trim.ToUpper, fn.Trim.ToUpper, mn.Trim.ToUpper, dob.ToString("yyyyMMdd"), gender.Substring(0, 1).ToUpper, address.ToUpper, city.ToUpper, state.ToUpper, zip.ToUpper, phone.ToUpper, vbNewLine, aka, clientno, doccode)
            'deprecated
            Return retObj

        End Function

        Public Function PV1(npi As String, ln As String, fn As String, mn As String, title As String, bt As String, clientno As String, doccode As String) As String

            Dim retObj As String = String.Empty
            Dim code As String = String.Empty
            Select Case bt
                Case "Client"
                    code = "A"
                Case "Medicare"
                    code = "B"
                Case "Medicaid"
                    code = "C"
                Case "HMO"
                    code = "D"
                Case "Insurance"
                    code = "E"
                Case "Patient"
                    code = "F"
                Case "Other"
                    code = "G"
            End Select
            'retObj = String.Format("PV1||||||||||||||||||||{0}|{1}", code.ToUpper, vbNewLine)
            'retObj = String.Format("PV1|||||||{0}^{1}^{2}^{3}^^^^{4}|||||||||||||{5}|{6}", _npi, _ln, _fn, _mn, _title, code.ToUpper, vbNewLine)
            retObj = String.Format("PV1|{7}|{8}|||||{0}^{1}^{2}|||||||||||||{5}|{6}", npi, ln, fn, mn, title, code.ToUpper, vbNewLine, clientno, doccode)
            Return retObj

        End Function

        Public Function IN1(ins As String, phone As String, groupno As String, fn As String, mn As String, ln As String, relationship As String, insuredid As String, insureddob As Date, insuranceaddress As String, insurancecity As String, insurancestate As String, insurancezip As String, employer As String, employeraddress As String, employercity As String, employerstate As String, employerzip As String, employerphone As String, ssn As String) As String

            Dim retObj As String = String.Empty
            retObj = String.Format("IN1|||{0}||||{1}|{2}||||||||{3}^{4}^{5}|{6}|{9}|{10}^{11}^{12}^{13}|{14}|{15}^{16}^{17}^{18}|{19}|{20}|||||||||||||{7}|{8}", ins.ToUpper, phone.ToUpper, groupno.ToUpper, ln.ToUpper, fn.ToUpper, mn.ToUpper, relationship.ToUpper, insuredid.ToUpper, vbNewLine, insureddob.ToString("yyyyMMdd"), insuranceaddress, insurancecity, insurancestate, insurancezip, employer, employeraddress, employercity, employerstate, employerzip, employerphone, ssn)
            Return retObj

        End Function

        Public Function SelfIN1(ins As String, phone As String, groupno As String, fn As String, mn As String, ln As String, relationship As String, insuredid As String) As String

            Dim retObj As String = String.Empty
            'retObj = String.Format("IN1|||{0}||||{1}|{2}||||||||{3}^{4}^{5}|{6}|||||||||||||||||||{7}|{8}", ins.ToUpper, phone.ToUpper, groupno.ToUpper, ln.ToUpper, fn.ToUpper, mn.ToUpper, relationship.ToUpper, insuredid.ToUpper, vbNewLine)
            retObj = String.Format("IN1|||{0}||||{1}|{2}||||||||{3}^{4}^{5}|{6}|||||||||||||||||||{7}|{8}", ins.ToUpper, String.Empty, groupno.ToUpper, String.Empty, String.Empty, String.Empty, relationship.ToUpper, insuredid.ToUpper, vbNewLine)
            Return retObj

        End Function

        Public Function IN2(medicare As String, medicaid As String) As String

            Dim retObj As String = String.Empty
            retObj = String.Format("IN2||||||{0}||{1}|{2}", medicare.ToUpper, medicaid.ToUpper, vbNewLine)
            Return retObj

        End Function

        'Public Function ORC(draft As String, loc As String) As String

        '    Dim retObj As String = String.Empty
        '    retObj = String.Format("ORC|NW|{0}|||||^^^^^^||||||{1}|||||||{2}", draft.ToUpper, loc.ToUpper, vbNewLine)
        '    Return retObj

        'End Function

        'orc using npi
        'Public Function ORC(draft As String, npi As String, ln As String, fn As String, mn As String) As String

        '    Dim retObj As String = String.Empty
        '    retObj = String.Format("ORC|NW|{0}|||||^^^^^^||||||{1}^{2}^{3}|||||||{5}", draft.ToUpper, npi, ln, fn, mn, vbNewLine)
        '    '6.14.18
        '    retObj = String.Format("ORC|NW|{0}|||||||{6}|||{1}^{2}^{3}||||||||{5}", draft.ToUpper, npi, ln, fn, mn, vbNewLine)
        '    Return retObj

        'End Function
        Public Function ORC(draft As String, npi As String, ln As String, fn As String, mn As String, dt As Date) As String

            Dim retObj As String = String.Empty
            '6.14.18
            retObj = String.Format("ORC|NW|{0}|||||||{6}|||{1}^{2}^{3}||||||||{5}", draft.ToUpper.Substring(4, draft.Length - 4), npi, ln, fn, mn, vbNewLine, dt.ToString("yyyyMMddHHmmss"))
            Return retObj

        End Function

        'tests
        'Public Function OBR(draft As String, code As String, collectionts As Date, collectionstatus As String, receivedts As Date, source As String, lmp As Date, specimensource As String, doctor As String, timing As String) As String

        '    Dim retObj As String = String.Empty
        '    Dim segment5 As String = String.Empty
        '    If source IsNot Nothing AndAlso source <> String.Empty Then
        '        segment5 = source
        '    Else
        '        If lmp.Year <> 1900 Then
        '            segment5 = lmp.ToString("yyyyMMdd")
        '        Else
        '            If specimensource IsNot Nothing AndAlso specimensource <> String.Empty Then
        '                segment5 = specimensource
        '            End If
        '        End If
        '    End If
        '    retObj = String.Format("OBR||{0}||{1}|||{2}||||{3}|||{4}|{5}|{6}||LAB|||||||||^^^{2}^^{7}||||{8}", draft.ToUpper, code.ToUpper, collectionts.ToString("yyyyMMddHHmmss"), collectionstatus.ToUpper, receivedts.ToString("yyyyMMddHHMMss"), segment5.ToUpper, doctor.ToUpper, timing.ToUpper, vbNewLine)
        '    Return retObj

        'End Function

        'obr using npi
        Public Function OBR(set_ As Integer, draft As String, code As String, collectionts As Date, collectionstatus As String, receivedts As Date, source As String, lmp As Date, specimensource As String, doctor As String, timing As String, npi As String, ln As String, fn As String, mn As String) As String

            Dim retObj As String = String.Empty
            Dim segment5 As String = String.Empty
            If source IsNot Nothing AndAlso source <> String.Empty Then
                segment5 = source
            Else
                If lmp.Year <> 1900 Then
                    segment5 = lmp.ToString("yyyyMMdd")
                Else
                    If specimensource IsNot Nothing AndAlso specimensource <> String.Empty Then
                        segment5 = specimensource
                    End If
                End If
            End If
            'retObj = String.Format("OBR|{13}|{0}||{1}|||{2}||||{3}|||{4}|{5}|{9}^{10}^{11}||LAB|||||||||^^^{2}^^{7}||||{8}", draft.ToUpper.Substring(4, draft.Length - 4), code.ToUpper, collectionts.ToString("yyyyMMddHHmmss"), collectionstatus.ToUpper, receivedts.ToString("yyyyMMddHHMMss"), segment5.ToUpper, doctor.ToUpper, timing.ToUpper, vbNewLine, npi, ln, fn, mn, set_)
            retObj = String.Format("OBR|{0}|{1}||{2}|R|{3}|{4}|||||||||{5}^{6}^{7}|||||||||||^^^^^R^^N||||{8}", set_, draft.ToUpper.Substring(4, draft.Length - 4), code, collectionts.ToString("yyyyMMddHHmmss"), collectionts.AddSeconds(1).ToString("yyyyMMddHHmmss"), npi, ln, fn, vbNewLine)
            Return retObj

        End Function
        'Public Function OBR(set As Integer, draft As String, code As String, collectionts As Date, collectionstatus As String, receivedts As Date, source As String, lmp As Date, specimensource As String, doctor As String, timing As String, npi As String, ln As String, fn As String, mn As String) As String

        '    Dim retObj As String = String.Empty
        '    Dim segment5 As String = String.Empty
        '    If source IsNot Nothing AndAlso source <> String.Empty Then
        '        segment5 = source
        '    Else
        '        If lmp.Year <> 1900 Then
        '            segment5 = lmp.ToString("yyyyMMdd")
        '        Else
        '            If specimensource IsNot Nothing AndAlso specimensource <> String.Empty Then
        '                segment5 = specimensource
        '            End If
        '        End If
        '    End If
        '    retObj = String.Format("OBR||{0}||{1}|||{2}||||{3}|||{4}|{5}|{9}^{10}^{11}||LAB|||||||||^^^{2}^^{7}||||{8}", draft.ToUpper, code.ToUpper, collectionts.ToString("yyyyMMddHHmmss"), collectionstatus.ToUpper, receivedts.ToString("yyyyMMddHHMMss"), segment5.ToUpper, doctor.ToUpper, timing.ToUpper, vbNewLine, npi, ln, fn, mn)
        '    '6.14.18
        '    retObj = String.Format("OBR|{12}|{0}||{1}|R||{2}|||9997|{3}|||{4}|{5}|{9}^{10}^{11}||LAB|||||||||^^^{2}^^{7}||||{8}", draft.ToUpper.Substring(6, draft.Length - 6), code.ToUpper, collectionts.ToString("yyyyMMddHHmmss"), collectionstatus.ToUpper, receivedts.ToString("yyyyMMddHHMMss"), segment5.ToUpper, doctor.ToUpper, timing.ToUpper, vbNewLine, npi, ln, fn, mn)
        '    'OBR()|2|8214729||3879|R|20180606202849|20180606202850|||9997|JUAN|||||1982553790^DELA CRUZ^JUAN|||||||||||^^^^^R^^N||||
        '    Return retObj

        'End Function

        Public Function OBX(q As String, a As String) As String

            Dim retObj As String = String.Empty
            retObj = String.Format("OBX|||^{0}||{1}|{2}", q.ToUpper, a.ToUpper, vbNewLine)
            Return retObj

        End Function

        Public Function DG1(icd As String) As String

            Dim retObj As String = String.Empty
            retObj = String.Format("DG1|||{0}|{1}", icd.ToUpper, vbNewLine)
            Return retObj

        End Function

        Public Function NTE(c As String) As String

            Dim retObj As String = String.Empty
            Dim cl As Integer = c.Length
            Dim cd As Integer = Math.Ceiling(cl / 50.0)
            For i = 1 To cd
                Try
                    retObj &= String.Format("NTE|||{0}|{1}", c.Substring((i - 1) * 50, 50).ToUpper, vbNewLine)
                Catch ex As Exception
                    retObj &= String.Format("NTE|||{0}|{1}", c.Substring((i - 1) * 50, cl Mod 50).ToUpper, vbNewLine)
                End Try
            Next
            Return retObj

        End Function

    End Class
End Class
