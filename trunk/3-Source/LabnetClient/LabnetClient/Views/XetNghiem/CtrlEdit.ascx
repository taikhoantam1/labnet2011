<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.TestViewModel>" %>


<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript">

    $(function () {

        $('input.autoNumeric').autoNumeric({ aSep: ',', aDec: '.', vMin: '0.00', aPad: false, wEmpty: 'empty' });

        $("#TestCostView").blur(function () {
            var cost = $("#TestCostView").val();
            cost = cost.replace(",", "");
            $("#Test_Cost").val(cost);
        });

        $("#LowIndexView").blur(function () {
            var cost = $("#LowIndexView").val();
            cost = cost.replace(",", "");
            $("#Test_LowIndex").val(cost);
        });

        $("#HighIndexView").blur(function () {
            var cost = $("#HighIndexView").val();
            cost = cost.replace(",", "");
            $("#Test_HighIndex").val(cost);
        });

        var lowIndex = $("#Test_LowIndex").val();
        lowIndex = lowIndex.replace(",", ".");
        $("#LowIndexView").val(lowIndex);

        var highIndex = $("#Test_HighIndex").val();
        highIndex = highIndex.replace(",", ".");
        $("#HighIndexView").val(highIndex);

        var testCost = $("#Test_Cost").val();
        testCost = testCost.replace(",", ".");
        $("#TestCostView").val(testCost);
    });
</script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm("Edit", "XetNghiem"); %>
<%= Html.HiddenFor(m=>m.Test.Id) %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
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
                        <% Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
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
                        <input type="text" class="textInput2 autoNumeric" id="LowIndexView" />
                         <%=Html.HiddenFor(m => m.Test.LowIndex)%>
                    </div>
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestCreate_HighIndex%></label>
                    </div>
                    <div class="Column">
                        <input type="text" class="textInput2 autoNumeric" id="HighIndexView" />
                         <%=Html.HiddenFor(m => m.Test.HighIndex)%>
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
                        <input type="text" class="textInput autoNumeric" id="TestCostView" />
                         <%=Html.HiddenFor(m => m.Test.Cost)%>
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
            <input type="submit" value="<%=Resources.TestStrings.TestCreate_Save%>" />
        </div>
    </div>
</div>
<% Html.EndForm(); %>

<script type="text/javascript">
    $(function () {
        var lowIndexHidden = $("#Test_LowIndex").val();
        lowIndexHidden = lowIndexHidden.replace(',', '.');
        $("#Test_LowIndex").val(lowIndexHidden);

        var highIndexHidden = $("#Test_HighIndex").val();
        highIndexHidden = highIndexHidden.replace(',', '.');
        $("#Test_HighIndex").val(highIndexHidden);

        var costHidden = $("#Test_Cost").val();
        costHidden = costHidden.replace(',', '.');
        $("#Test_Cost").val(costHidden);
    });
</script>