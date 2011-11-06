<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.TestSearctViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm();%>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.TestStrings.TestSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestSearch_TestName%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSearch.TestName, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestSearch_PanelName%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSearch.PanelName, new {Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestSearch_TestSectionName %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSearch.TestSectionName, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div align="center" class="Row clear">
                <input type="submit" value="<%=Resources.TestStrings.TestSearch_Search%>" />
            </div> 
        </div>
    </div>
</div>
<div>
<table width="765px">
    <tr valign="middle">
        <th class="textSearch150" align="center">
            <%=Resources.TestStrings.TestSearch_TestName %>
        </th>
        <th class="textSearch125" align="center">
            <%=Resources.TestStrings.TestSearch_TestSectionName %>
        </th>
        <th class="textSearch125" align="center">
            <%=Resources.TestStrings.TestSearch_PanelName %>
        </th>
        <th class="textSearch125" align="center">
            <%=Resources.TestStrings.TestSearch_TestRange %>
        </th>
        <th class="textSearch125" align="center">
            <%=Resources.TestStrings.TestSearch_TestUnit %>
        </th>
        <th class="textSearch125" align="center"></th>
    </tr>
    <%foreach (DomainModel.TestSearchObject test in ViewData.Model.TestSearch.ObjSearchResult)
      { %>
         <tr valign="middle">
            <th class="textSearch150" align="center">
                <%=test.TestName %>
            </th>
             <th class="textSearch125" align="center">
                <%=test.TestSectionName %>
            </th>
             <th class="textSearch125" align="center">
                
            </th>
             <th class="textSearch125" align="center">
                <%=test.TestRange %>
            </th>
             <th class="textSearch125" align="center">
                <%=test.TestUnit %>
            </th>
             <th class="textSearch125" align="center">
                    <%= Html.ActionLink("Cập nhật", "Edit/" + test.TestId, "Test")%>
            </th>
         </tr>   
    <%} %>
</table>
</div>
<% Html.EndForm(); %>