﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PatientViewModel>" %>
<%= Html.ValidationSummary() %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
              {%>
          
             <%=Resources.PatientStrings.PatientCreate_Title%>
            <%}
              else
              { %>
           
             <%=Resources.PatientStrings.PatientEdit_Title %>
            <%} %>
        </h3>
    </div>
    <%Html.BeginForm(); %>
    <%= Html.HiddenFor(p=>p.LabExamination.Id) %>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientSTT %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.LabExamination.OrderNumber, new { Class = "textInput50 number readonly" })%>
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
                            <%=Resources.PatientStrings.PatientInsert_ReceivedDate%></label>
                    </div>
                    <div class="Column">
                    <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Edit)
                      { %>
                        <%=Html.TextBox("LabExamination.ReceivedDate", Model.LabExamination.ReceivedDate.Value.ToString("d"), new { ID = "LabExamination_ReceivedDate", Class = "textInput100 readonly" })%>
                    <%}
                      else
                      {%>
                        <%=Html.TextBox("LabExamination.ReceivedDate", Model.LabExamination.ReceivedDate.Value.ToString("d"), new { ID = "LabExamination_ReceivedDate", Class = "textInput100 date" })%>
                    <%} %>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="line">
        </div>
        <div class="ContentTop">
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
                            <%=Resources.PatientStrings.PatientBirthday%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.BirthDate, new  {Class="textInput" })%>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientPhone%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.Phone, new  {Class="textInput number" })%>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientEmail%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.Email, new  {Class="textInput" })%>
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientGender%></label>
                    </div>
                    <div class="Column">
                       <select name="Patient.Gender" id="Patient_Gender">
                        <option value="true">
                            Nam
                        </option>
                        <option value="false">
                            Nữ
                        </option>
                       </select>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientIDNumber%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.IndentifierNumber, new { Class = "textInput number" })%>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientAddress%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.Address, new  {Class="textInput" })%>
                    </div>
                </div>
            </div>
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PatientStrings.PatientDiagnosis%></label>
                </div>
                <div class="Column">
                    <%=Html.TextAreaFor(m => m.LabExamination.Diagnosis, new { cols = 75, rows = 3 })%>
                </div>
            </div>
         <% Html.EndForm(); %>
        </div>
        <div class="clear">
        </div>
        <div class="line">
        </div>
        <div class="ContentBottom">
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle Width150 MarginT5">
                        <%=Resources.PatientStrings.PatientInsert_Panel %></label>
                </div>
                <div class="Column" id="PanelComplete">
                    <%Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
                </div>
                <div class="clear">
                </div>
            </div>
            <div id="PatientTestTable">
                <%Html.RenderPartial("DataTable", Model.JQGrid); %>
            </div>
        </div>
        <div align="center">
            <input type="button" value="<%=Resources.PatientStrings.PatientInsert_Button_Save%>"
                id="btnSavePatientInfo" />
        </div>
    </div>
</div>
<script type="text/javascript">
    function AutocompleSelect(id, label, tag) {
        $.ajax({
            url: "/BenhNhan/GetPanelTests",
            data: {
                Id: id
            },
            type: "POST",
            async: false,
            dataType: "Json",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var array = $("#list").jqGrid().getRowData();
                    jQuery("#list").jqGrid('addRowData', array.length, data[i]);
                } 
            }
        });
    }

    $(document).ready(function () {
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
                        alert(data);
                        if (data.trim().toLowerCase().indexOf("false") != -1)
                            alert("Số thứ tự bạn tìm kiếm không tồn tại");
                        else {
                            window.location = "/BenhNhan/Edit/" + data + "?OrderNumber=" + $("#LabExamination_OrderNumber").val() + "&ReceivedDate=" + date;
                        }
                    }

                });
            }
        });

        $("#btnSavePatientInfo").click(function (event) {
            event.preventDefault();
            $("#DataTableSaveButton").click();
            $("form").submit();
        });
        
//        <%if(Model.ViewMode == DomainModel.Constant.ViewMode.Edit){ %>
//            $("#list tr td :checkbox").attr("disabled","disabled");
//        <%} %>
    });
    
</script>