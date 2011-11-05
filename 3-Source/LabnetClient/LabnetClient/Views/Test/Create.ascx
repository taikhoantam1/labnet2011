<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.TestViewModel>" %>
<link href="../../Content/Lib/jquery/jquery-ui.css" rel="stylesheet" type="text/css"/>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript" src="../../Content/Lib/jquery/jquery.min.js"></script>
<script type="text/javascript" src="../../Content/Lib/jquery/jquery-ui.min.js"></script>
<script type="text/javascript">

     $(function () {
         $("input#Test_TestSectionName").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: "/Test/FindTestSectionNames", type: "POST", dataType: "json",
                     data: { searchText: request.term },
                     success: function (data) {
                         response($.map(data, function (item) {
                             return { value: item.Name, id: item.Id }
                         }))
                     }
                 })
             },
             select: function (event, ui) {
                 $("input#Test_TestSectionId").val(ui.item.id);
             }
         });
     });
  </script>
<%if (Model.ViewMode == LabnetClient.Constant.ViewMode.Create)
  {%>
<% Html.BeginForm("Create", "Test");%>
<%}
  else
  { %>
<% Html.BeginForm("Edit", "Test"); %>
<%} %>
<%= Html.HiddenFor(m=>m.Test.Id) %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == LabnetClient.Constant.ViewMode.Create)
              {%>
                    <%=Resources.TestStrings.TestCreate_Title %>
            <%}
              else
              { %>
                     <%=Resources.TestStrings.TestEdit_Title%>
            <%} %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_Name%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.Name, new { Class = "textInput220" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_TestSection%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.TestSectionName, new { id="Test_TestSectionName", Class = "textInput220" })%>
                        <%=Html.HiddenFor(m => m.Test.TestSectionId) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_Range%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.Range, new { Class = "textInput220" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_LowIndex%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.LowIndex, new { Class = "textInput2" })%>
                    </div>

                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_HighIndex%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.HighIndex, new { Class = "textInput2" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_IsActive %></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m=>m.Test.IsActive) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_SortOrder %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.SortOrder, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_IsBold%></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m => m.Test.IsBold) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_Unit%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.Unit, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                 <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_Cost%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Test.Cost, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.TestStrings.TestCreate_Description%></label>
                </div>
                <div class="Column">
                    <%=Html.TextAreaFor(m => m.Test.Description, new { cols=76, rows=3})%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        
        <div align="center"> 
            <input type="submit" value="<%=Resources.TestStrings.TestCreate_Save%>"/>
            <input type="submit" value="<%=Resources.TestStrings.TestCreate_New%>" />
        </div>
    </div>
</div>

<% Html.EndForm(); %>