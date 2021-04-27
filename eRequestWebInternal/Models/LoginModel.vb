Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class LoginModel
        <Required>
        <DisplayFormat(ConvertEmptyStringToNull:=False)>
        <Display(Name:="Username")>
        Property Username As String

        <Required>
        <DisplayFormat(ConvertEmptyStringToNull:=False)>
        <DataType(DataType.Password)>
        <Display(Name:="Password")>
        Property Password As String

        Property ReturnUrl As String
    End Class
End Namespace

