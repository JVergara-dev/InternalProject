Imports Support
Namespace Models
    Public Class RequestTableModel
        Property Keyword As String = ""
        Property PageSizePending As Integer = 10
        Property PageSizeProcess As Integer = 10
        Property CurrentPagePending As Integer = 1
        Property CurrentPageProcess As Integer = 1

        Property NumberOfPagesPending As Integer
        Property NumberOfPagesProcess As Integer
        Property PendingLIst As New List(Of RequestHeaders.RequestHeader)
        Property ProcessedList As New List(Of RequestHeaders.RequestHeader)
    End Class
End Namespace

