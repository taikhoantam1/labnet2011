<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PanelSearchViewModel>" %>

<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm();%>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.PanelStrings.PanelSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PanelStrings.PanelSearch_Name%></label>
                </div>
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.PanelSearch.Name, new { Class = "textInput" })%> 
                </div>
                <div class="Colum">
                    <input type="submit" value="<%=Resources.PanelStrings.PanelSearch_Search%>" />
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
            <%=Resources.PanelStrings.PanelSearch_GridPanelName%>
        </th>
        <th class="textSearch125" align="center"></th>
    </tr>
    <%foreach (DomainModel.PanelSearchObject panel in ViewData.Model.PanelSearch.ListSearchResult)
      { %>
         <tr valign="middle">
            <th class="textSearch150" align="center">
                <%=panel.PanelName %>
            </th>             
             <th class="textSearch125" align="center">
                    <%= Html.ActionLink("Cập nhật", "Create", "Panel", new { Id = panel.Id }, new { Class = "ActionLink" })%>
            </th>
         </tr>   
    <%} %>
</table>
</div>
<% Html.EndForm(); %>