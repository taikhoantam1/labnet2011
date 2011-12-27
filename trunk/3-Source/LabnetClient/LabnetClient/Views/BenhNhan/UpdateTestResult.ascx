<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PatientTestViewModel>" %>


<%= Html.ValidationSummary() %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
             <%=Resources.PatientStrings.PatientTestUpdate_Title%>
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
                            <%=Resources.PatientStrings.PatientSTT %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.LabExamination.OrderNumber, new  {Class="textInput" })%>
                    </div>
                </div>
            </div>
            <div class="RightCol">
               <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientInsert_ReceivedDate%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBox("LabExamination.ReceivedDate",Model.LabExamination.ReceivedDate.Value.ToString("d"), new {ID="LabExamination_ReceivedDate" ,Class = "textInput100 date" })%>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="line">
        </div>
        <div class="ContentTop PatientInfo">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientName%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.FirstName, new  {Class="textInput" })%>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientInsert_Partner %></label>
                    </div>
                    <div class="Column">
                        <%= Html.DropDownListFor(p => p.LabExamination.PartnerId, Model.SelectListPartner) %>
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientBirthday%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.Age, new  {Class="textInput" })%>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientGender%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBox("Patient.Gender", !Model.Patient.Gender?"Nữ":"Nam", new { Class = "textInput" })%>
                    </div>
                </div>
            </div>
        </div>
         <% Html.EndForm(); %>
         
        <div class="Row ResultTable">
                 <% Html.RenderPartial("DataTable", Model.JQGrid); %>
        </div>
        
    </div>
    <input type="button" id="btnSaveTestResult"  value="Lưu"/>
    <input type="button" id="btnTestResultApproved"  value="Xác Minh"/>
    <input type="button" id="btnCancel"  value="Nhập Lại"/>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("input,textarea,select", ".PatientInfo").attr("disabled", "disabled");
        $("#LabExamination_OrderNumber").keyup(function (event) {
            if (event.keyCode == 13) {
                var date = $("#LabExamination_ReceivedDate").val();
                $.ajax({
                    url: "/BenhNhan/SearchByOrderNumber",
                    data: {
                        OrderNumber: $(this).val(),
                        ReceivedDate: date
                    },
                    type: "POST",
                    success: function (data) {
                        if (data.trim().toLowerCase().indexOf("false") != -1)
                            alert("Số thứ tự bạn tìm kiếm không tồn tại");
                        else {
                            window.location = "/BenhNhan/PatientTestResult/" + data + "?OrderNumber=" + $("#LabExamination_OrderNumber").val() + "&ReceivedDate=" + date;
                        }
                    }

                });
            }
        });
        $("#btnSaveTestResult").click(function () {
            $("#DataTableSaveButton").click();
            window.location.reload();
        });
        $("#btnTestResultApproved").click(function () {
            $.ajax({
                url: "/BenhNhan/TestResultApproved",
                data: {
                    LabExaminationId: $("#LabExamination_Id").val()
                },
                type: "POST",
                asyn: false,
                success: function () {
                    window.location.reload();
                }
            });
        });
        $("#btnCancel").click(function () {
            window.location.reload();
        });
    });
</script>