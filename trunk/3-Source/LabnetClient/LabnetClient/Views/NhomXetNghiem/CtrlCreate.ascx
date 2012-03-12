<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.TestSectionViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript">

    $(function () {
        $('.autoNumeric').autoNumeric({ aSep: ',', aDec: '.', vMin: '0.00', aPad: false, wEmpty: 'empty' });

        $("#TestCostView").blur(function () {
            var cost = $("#TestCostView").val();
            cost = cost.replace(/,/gi, "");
            if (cost == "") {
                cost = 0;
            }
            $("#TestSection_Cost").val(cost);
            //alert(cost)
        });
    });
</script>

<%=Html.ValidationSummary() %>
<%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
  {%>
<% Html.BeginForm("Create", "NhomXetNghiem");%>
<%}
  else
  { %>
<% Html.BeginForm("Edit", "NhomXetNghiem"); %>
<%} %>
<%= Html.HiddenFor(m => m.TestSection.Id) %>

<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
              {%>
            <%=Resources.TestSectionStrings.TestSectionCreate_Title%>
            <%}
              else
              { %>
            <%=Resources.TestSectionStrings.TestSectionEdit_Title%>
            <%} %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestSectionStrings.TestSection_Name%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSection.Name, new { Class = "textInput220" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestSectionStrings.TestSection_Description%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSection.Description, new { Class = "textInput220" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle" for="TestSection_IsActive">
                            <%=Resources.TestSectionStrings.TestSection_IsActive %></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m => m.TestSection.IsActive)%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestSectionStrings.TestSection_SortOrder%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSection.SortOrder, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestSectionStrings.TestSection_Cost%></label>
                    </div>
                    
                    <div class="Column">
                        <input type="text" class="textInput autoNumeric" id="TestCostView" value="<%=Model.TestSection.Cost %>" />
                        <%=Html.HiddenFor(m => m.TestSection.Cost)%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <%=Html.CheckBoxFor(m => m.TestSection.UseCostForAssociateTest)%>
                    </div>
                    <div class="Column">
                        <label class="lbTitle" for="TestSection_UseCostForAssociateTest" style="width: 400px; margin-left: 5px;">
                            <%=Resources.TestSectionStrings.VMTestSection_UseCostForAssociateTest%></label>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        <div class="Row" align="center">
            <input type="submit" value="<%=Resources.TestSectionStrings.TestSectionCreate_Save%>" id="save" />
            <input type="button" value="<%=Resources.TestSectionStrings.TestSectionCreate_New%>" id="reloadPage" />
        </div>
    </div>
</div>
<% Html.EndForm(); %>