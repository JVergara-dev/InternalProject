@ModelType Models.RequestTableModel
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="jarviswidget jarviswidget-color-red" id="wid-id-0" data-widget-editbutton="false">
    <header><span class="widget-icon"><i class="fa fa-table"></i></span>&nbsp;&nbsp;<h2>Patient List</h2></header>
    <div>
        <div class="widget-body">
            <p>Below are the list of patients.</p>
            <form action="@Url.Action("index", "requesttable")" method="get">
                <input type="hidden" name="PendingPI" value="@Model.CurrentPagePending" />
                <input type="hidden" name="ProcessPI" value="@Model.CurrentPageProcess" />
                <div class="icon-addon">
                <input type="text" name="key" value="@Model.Keyword" placeholder="Search Here" class="form-control" autofocus />
                    <label for="keyword" class="glyphicon glyphicon-search txt-color-red" rel="tooltip" title="" data-original-title="Search an item using a keyword"></label>
                </div>
            </form>
            <br />
            <div class="table-responsive; col-md-6">
                <span>PENDING REQUEST</span>
                <table class="table table-bordered table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>Draft Code</th>
                            <th>Client #</th>
                            <th>Doctor Name</th>
                            <th>Patient Name</th>
                        </tr>
                    </thead>
                    <tbody>
                          @for Each item As Support.RequestHeaders.RequestHeader In Model.PendingLIst
                              Dim DoctorName As String
                              Dim PatientName As String
                              DoctorName = item.DoctorLName & ", " & item.DoctorFName & ", " & item.DoctorMName
                              PatientName = item.RequestPLName & ", " & item.RequestPFName & ", " & item.RequestPMName
                              @<tr data-href="@Url.Action("Index", "Main", New With {.id = item.ID})">
                                    <td>@item.DraftCode</td>
                                    <td>@item.RequestClient</td>
                                    <td>@DoctorName</td>
                                    <td>@PatientName</td>
                               </tr>
                          Next
                    </tbody>
                </table>
                <ul Class="pagination pagination-xs">
                    @If Model.CurrentPagePending - 3 > 1 Then
                        @<li><a href="@Url.Action("Index", "RequestTable", New With {.key = Model.Keyword, .PendingPI = "1", .ProcessPI = Model.CurrentPageProcess})">&laquo;</a></li>
                    End If
                    @For i = Model.CurrentPagePending - 3 To Model.CurrentPagePending + 3
                        If i > 0 And i < Model.NumberOfPagesPending + 1 Then
                            If i = Model.CurrentPagePending Then
                                @<li class="active"><a href="#">@i</a></li>
                            Else
                                @<li><a href="@Url.Action("Index", "RequestTable", New With {.key = Model.Keyword, .PendingPI = i, .ProcessPI = Model.CurrentPageProcess})">@i</a></li>
                            End If
                        End If
                    Next
                    @If Model.CurrentPagePending + 3 < Model.NumberOfPagesPending Then
                        @<li><a href="@Url.Action("Index", "RequestTable", New With {.key = Model.Keyword, .PendingPI = Model.NumberOfPagesPending})">&raquo;</a></li>
                    End If
                </ul>
            </div>

            <div class="table-responsive; col-md-6">
                <span>ONPROCESS REQUEST</span>
                <table class="table table-bordered table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>Draft Code</th>
                            <th>Client #</th>
                            <th>Doctor Name</th>
                            <th>Patient Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        @for Each item As Support.RequestHeaders.RequestHeader In Model.ProcessedList
                            Dim DoctorName As String
                            Dim PatientName As String
                            DoctorName = item.DoctorLName & ", " & item.DoctorFName & ", " & item.DoctorMName
                            PatientName = item.RequestPLName & ", " & item.RequestPFName & ", " & item.RequestPMName
                            @<tr data-href="@Url.Action("ViewOnly", "Main", New With {.id = item.ID})">
                                <td>@item.DraftCode</td>
                                <td>@item.RequestClient</td>
                                <td>@DoctorName</td>
                                <td>@PatientName</td>
                            </tr>
                        Next
                    </tbody>
                </table>
                <ul Class="pagination pagination-xs">
                    @If Model.CurrentPageProcess - 3 > 1 Then
                        @<li><a href="@Url.Action("Index", "RequestTable", New With {.key = Model.Keyword, .ProcessPI = "1", .PendingPI = Model.CurrentPagePending})">&laquo;</a></li>
                    End If
                    @For i = Model.CurrentPageProcess - 3 To Model.CurrentPageProcess + 3
                        If i > 0 And i < Model.NumberOfPagesProcess + 1 Then
                            If i = Model.CurrentPageProcess Then
                                @<li class="active"><a href="#">@i</a></li>
                            Else
                                @<li><a href="@Url.Action("Index", "RequestTable", New With {.key = Model.Keyword, .ProcessPI = i, .PENDINGPI = Model.CurrentPagePending})">@i</a></li>
                            End If
                        End If
                    Next
                    @If Model.CurrentPageProcess + 3 < Model.NumberOfPagesProcess Then
                        @<li><a href="@Url.Action("Index", "RequestTable", New With {.key = Model.Keyword, .ProcessPI = Model.NumberOfPagesProcess})">&raquo;</a></li>
                    End If
                </ul>
            </div>

        </div>
    </div>
</div>
<script>
    $("tr[data-href]").on("click", function () { document.location = $(this).data("href"); })
</script>
