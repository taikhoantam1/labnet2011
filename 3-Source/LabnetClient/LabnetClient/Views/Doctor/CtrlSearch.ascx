<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.DoctorSearchViewModel>" %>

<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm();%>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.DoctorStrings.DoctorSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.DoctorStrings.DoctorSearch_Name%></label>
                </div>
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.DoctorSearch.Name, new { Class = "textInput" })%> 
                </div>
                <div class="Colum">
                    <input type="submit" value="<%=Resources.DoctorStrings.DoctorSearch_ButtonSearch%>" />
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</div>
<div>
<table width="765px">
    <tr valign="middle">
        <th class="textSearch150" align="center">
            <%=Resources.DoctorStrings.DoctorSearch_GridDoctorName%>
        </th>
        <th class="textSearch125" align="center"></th>
    </tr>
    <%foreach (DomainModel.DoctorSearchObject doctor in ViewData.Model.DoctorSearch.ListSearchResult)
      { %>
         <tr valign="middle">
            <th class="textSearch150" align="center">
                <%=doctor.DoctorName%>
            </th>             
             <th class="textSearch125" align="center">
                    <%= Html.ActionLink("Cập nhật", "Edit", "Doctor", new { Id = doctor.Id }, new { Class = "ActionLink" })%>
            </th>
         </tr>   
    <%} %>
</table>
</div>
<% Html.EndForm(); %>