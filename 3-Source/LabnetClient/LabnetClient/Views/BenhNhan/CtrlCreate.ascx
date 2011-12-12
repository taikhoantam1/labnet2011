<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PatientViewModel>" %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == LabnetClient.Constant.ViewMode.Create)
              {%>
            <%=Resources.PatientStrings.PatientInsert_Title %>
            <%}
              else
              { %>
            <%=Resources.PatientStrings.PatientEdit_Title %>
            <%} %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientSTT %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.LabManagement.OrderNumber, new  {Class="textInput" })%>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientInsert_Partner %></label>
                    </div>
                    <div class="Column">
                        <%= Html.DropDownListFor(p => p.LabManagement.PartnerId, Model.SelectListPartner) %>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="line"></div>
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
                            <%=Resources.PatientStrings.PatientGender%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Patient.Gender, new  {Class="textInput" })%>
                    </div>
                </div>
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
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PatientStrings.PatientDiagnosis%></label>
                </div>
                <div class="Column">
                    <%=Html.TextAreaFor(m => m.LabManagement.Diagnosis, new { cols = 75, rows = 3 })%>
                </div>
            </div>
            <% Html.EndForm(); %>
        </div>
        
        <div class="clear"></div>
        <div class="line"></div>
         <div class="ContentBottom">
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle Width150 MarginT5">
                            <%=Resources.PatientStrings.PatientInsert_Panel %></label>
                </div>
                <div class="Column">
                    <%Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
                </div>
                <div class="clear"></div>
            </div>
            <div id="PatientTestTable">
                <%Html.RenderPartial("DataTable", Model.JQGrid); %>
            </div>
        </div>
        <div align="center">
            <input type="button" value="<%=Resources.PatientStrings.PatientInsert_Button_Save%>"
                id="btnSavePanel" />
        </div>
    </div>
</div>
