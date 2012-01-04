<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PatientViewModel>" %>

<%= Html.ValidationSummary() %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
             <%=Resources.PatientStrings.PatientSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
    
        <%Html.BeginForm(); %>
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
                        <%=Html.TextBox("LabExamination.ReceivedDate",Model.LabExamination.ReceivedDate.Value.ToString("d"), new {ID="LabExamination_ReceivedDate" ,Class = "textInput100 date" })%>
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
                            <%=Resources.PatientStrings.PatientPhone%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.Phone, new  {Class="textInput" })%>
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
                            <%=Resources.PatientStrings.PatientIDNumber%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.IndentifierNumber, new  {Class="textInput" })%>
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
        </div>
         <% Html.EndForm(); %>
         
        <div class="Row ResultTable">
              
        </div>
        
    </div>
    <input type=button id="btnSearchFilter"  value="Tìm"/>
<script type="text/javascript">
    $(document).ready(function () {
        $(".date").datepicker();
        $("#btnSearchFilter").click(function () {
            var data = $(".ModuleContent form").serialize();
            $.ajax({
                url: "/BenhNhan/Search",
                type: "POST",
                data: data,
                success: function (data) {
                    $(".ResultTable").html(data);
                }
            });
        });
    });
</script>