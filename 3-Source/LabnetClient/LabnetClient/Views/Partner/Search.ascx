<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PartnerSearchViewModel>" %>

<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm();%>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.PartnerStrings.PartnerEdit_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PartnerStrings.PartnerSearch_Name%></label>
                </div>
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.PartnerSearch.Name, new { Class = "textInput" })%> 
                </div>
                <div class="Colum">
                    <input type="submit" value="<%=Resources.PartnerStrings.PanelSearch_Search%>" />
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
            <%=Resources.PartnerStrings.PartnerSearch_GridPartnerName%>
        </th>
        <th class="textSearch125" align="center"></th>
    </tr>
    <%foreach (DomainModel.PartnerSearchObject partner in ViewData.Model.PartnerSearch.ListSearchResult)
      { %>
         <tr valign="middle">
            <th class="textSearch150" align="center">
                <%=partner.PartnerName%>
            </th>             
             <th class="textSearch125" align="center">
                    <%= Html.ActionLink("Cập nhật", "Edit", "Partner", new { Id = partner.Id }, new { Class = "ActionLink" })%>
            </th>
         </tr>   
    <%} %>
</table>
</div>
<% Html.EndForm(); %>