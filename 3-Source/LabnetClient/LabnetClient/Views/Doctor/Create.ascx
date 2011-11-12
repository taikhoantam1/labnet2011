<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.DoctorViewModel>" %>

<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<% string display = ViewData.ModelState.IsValid ? "none" : "block"; %>
<div class="errorbox" id="validationSummary" style="display: <%=display%>">
    <span class='errorimage'><span class='errorhead'>Looks like we have a small problem...</span></span>
    <%= Html.ValidationSummary() %>
</div>

<% Html.BeginForm("Create", "Doctor");%>

<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.DoctorStrings.DoctorInsert_Title %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Name%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Name, new  {Class="textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Address%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Address, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Phone%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Phone, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_IsActive%></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m=>m.Doctor.IsActive) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Email%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Email, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Commission%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Commission, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_BankAccount%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.BankAccountNumber, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.DoctorStrings.DoctorInsert_Note%></label>
                </div>
                <div class="Column">
                    <%=Html.TextAreaFor(m => m.Doctor.Other, new { cols=75, rows=3})%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div align="center">
            <input type="submit" value="<%=Resources.DoctorStrings.DoctorInsert_ButtonSave%>" id="save"/>
            <input type="button" value="<%=Resources.DoctorStrings.DoctorInsert_ButtonNew%>" id="Submit1"/>
        </div>
    </div>
</div>
<% Html.EndForm(); %>