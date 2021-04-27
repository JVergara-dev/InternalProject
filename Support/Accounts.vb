Imports System.Data.SqlClient
Public Class Accounts
    Public Class Account
        Property ID As Integer
        Property Name As String
        Property UserName As String
        Property Password As String
        Property CreatedTS As DateTime
        Property CreatedBy_User_ID As Integer
        Property CreatedBy_User_Name As String
        Property UpdatedTS As DateTime
        Property UpdatedBy_User_ID As Integer
        Property UpdatedBy_User_Name As String
        Property IsActive As Boolean
    End Class
    Public Function SelectUserByUsername(Username As String) As Account
        Dim retObj As New Account
        Dim con As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As SqlCommand = New SqlCommand("sp_SelectPSC_UsersByUsername", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@Username", Username)
        Try
            con.Open()
            If con.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                While rdr.Read
                    retObj.ID = rdr("ID")
                    retObj.Name = rdr("Name")
                    retObj.UserName = rdr("Username")
                    retObj.Password = rdr("Password")
                    retObj.CreatedTS = rdr("CreatedTS")
                    retObj.CreatedBy_User_ID = rdr("CreatedBy_User_ID")
                    retObj.CreatedBy_User_Name = rdr("CreatedBy_User_Name")
                    retObj.UpdatedTS = rdr("UpdatedTS")
                    retObj.UpdatedBy_User_ID = rdr("UpdatedBy_User_ID")
                    retObj.UpdatedBy_User_Name = rdr("UpdatedBy_User_Name")
                    retObj.IsActive = rdr("IsActive")
                End While
            End If
        Catch ex As Exception
            retObj = New Account
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
    Function UpdatePassword(id As Integer, password As String) As Boolean
        Dim retObj As Boolean = False
        Dim conn As New SqlConnection(Parameters.eRequestConnectionString)
        Dim cmd As New SqlCommand("sp_UpdatePSCUserPassword", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@ID", id)
        cmd.Parameters.AddWithValue("@Password", PRMs.GetMD5Hash(password))
        Try
            conn.Open()
            If conn.State <> ConnectionState.Open Then Throw New Exception
            Dim rdr As SqlDataReader = cmd.ExecuteReader
            If rdr.HasRows Then
                retObj = True
            Else
                retObj = False
            End If
        Catch ex As Exception
            retObj = False
        Finally
            conn.Close()
            cmd.Dispose()
        End Try
        Return retObj
    End Function
End Class
