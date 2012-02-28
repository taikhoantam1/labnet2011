<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PatientTestViewModel>" %>
<%= Html.ValidationSummary() %>

<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.PatientStrings.PatientResultReport_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <%Html.BeginForm(); %>
        <%=Html.HiddenFor(p => p.LabExamination.Id)%>
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientResultReport_Number%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.LabExamination.OrderNumber, new { Class = "textInput" , Style="width:125px"})%>
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientResultReport_ReceivedDate%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBox("LabExamination.ReceivedDate",Model.LabExamination.ReceivedDate.Value.ToString("d"), new {ID="LabExamination_ReceivedDate" ,Class = "textInput100 date" })%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div align="center">
        <input type="button" id="btnPrintPreview" value="In Kết Quả" />  
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnPrintPreview").click(function () {
            window.location = "/Report/PatientResultReport?OrderNumber=" + $("#LabExamination_OrderNumber").val() + "&ReceivedDate=" + $("#LabExamination_ReceivedDate").val();
        });
    });
</script>
