Imports System.Web
Imports System.Web.Security
Imports System.Security.Cryptography
Public Class Helpers
    Public Shared Function GetFormsAuthenticationCookie() As String

        Dim cookie As HttpCookie = HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)
        Return FormsAuthentication.Decrypt(cookie.Value).UserData

    End Function
    Public Shared Function GetSessionValue() As String
        Dim jsserializer As New Script.Serialization.JavaScriptSerializer
        Dim account As Accounts.Account = jsserializer.Deserialize(Of Accounts.Account)(GetFormsAuthenticationCookie)
        If account Is Nothing Then Return "error_trying_session"
        Return account.UserName
    End Function

    Public Shared Function LocalizeDateTime(dt As Date, lab As String) As Date

        Dim dls As Integer = 0
        If Not dt.IsDaylightSavingTime Then dls = 1
        If dt.Date = Date.Parse("1-1-1980").Date Then
            Dim x As String
            x = String.Empty
        End If

        Select Case Parameters.ServerCode
            Case "0001"
                'la server
                Select Case lab
                    Case "0002"
                        Return dt.AddHours(14 + dls)
                    Case "0003"
                        Return dt.AddHours(15 + dls)
                End Select
                Return dt
            Case "0002"
                'jkt server
                Select Case lab
                    Case "0001"
                        Return dt.AddHours(-14)
                    Case "0003"
                        Return dt.AddHours(1)
                End Select
                Return dt
            Case "0003"
                'mnl server
                Select Case lab
                    Case "0001"
                        Return dt.AddHours(-15)
                    Case "0002"
                        Return dt.AddHours(-1)
                End Select
                Return dt
            Case Else
                'la server
                Select Case lab
                    Case "0002"
                        Return dt.AddHours(14 + dls)
                    Case "0003"
                        Return dt.AddHours(15 + dls)
                End Select
                Return dt
        End Select
    End Function
    Public NotInheritable Class SECURITY
        Private TripleDes As New TripleDESCryptoServiceProvider

        Sub New(ByVal key As String)

            TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)

        End Sub

        Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

            Dim sha1 As New SHA1CryptoServiceProvider
            Dim keyBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(key)
            Dim hash() As Byte = sha1.ComputeHash(keyBytes)
            ReDim Preserve hash(length - 1)
            Return hash

        End Function

        Public Function EncryptData(ByVal plaintext As String) As String

            Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(plaintext)
            Dim ms As New System.IO.MemoryStream
            Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
            encStream.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray)

        End Function
    End Class
End Class
