Imports System.Data.SqlClient
Imports Support
Public Class Insurances
    Public Class Insurance
        Property ID As Integer
        Property Lab As Integer
        Property Code As String
        Property Name As String
        Property Active As Boolean
    End Class
    Public Function SelectInsurance() As List(Of Insurance)
        Dim retObj As New List(Of Insurance)
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_SelectInsuranceaasdasd", con)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    Dim temp As New Insurance
                    temp.ID = rdr("ID")
                    temp.Lab = rdr("Lab")
                    temp.Code = rdr("Code")
                    temp.Name = rdr("Name")
                    temp.Active = rdr("Active")
                    retObj.Add(temp)
                End While
            End If
        Catch ex As Exception
            retObj = New List(Of Insurance)
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
End Class

