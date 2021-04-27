Imports System.Data.SqlClient
Imports Support
Public Class HMOs
    Public Class HMO
        Property ID As Integer
        Property Lab As Integer
        Property Code As String
        Property Name As String
        Property Contact As String
        Property Active As Boolean
        Property Template As Integer
        Property Email As String
        Property PreAuth As String
    End Class

    Function SelectHMO() As List(Of HMO)
        Dim retObj As New List(Of HMO)
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_SelectHMO", con)
        cmd.CommandType = CommandType.StoredProcedure
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    Dim temp As New HMO
                    temp.ID = rdr("ID")
                    temp.Lab = rdr("Lab")
                    temp.Code = rdr("Code")
                    temp.Name = rdr("Name")
                    temp.Contact = rdr("Contact")
                    temp.Active = rdr("Active")
                    temp.Template = rdr("Template")
                    temp.Email = rdr("Email")
                    temp.PreAuth = rdr("PreAuth")
                    retObj.Add(temp)
                End While
            End If
        Catch ex As Exception
            retObj = New List(Of HMO)
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
End Class
