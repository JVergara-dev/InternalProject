Imports System.Security.Cryptography
Imports System.Text
Public Class PRMs
    Shared Function GetMD5Hash(ByVal value As String) As String
        Dim md5Hasher As MD5 = MD5.Create()
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.[Default].GetBytes(value))
        Dim sBuilder As New StringBuilder()
        For i As Integer = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next
        Return sBuilder.ToString()
    End Function
End Class
