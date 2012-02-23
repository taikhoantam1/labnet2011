<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PanelViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript">
 

    function CheckAllowAddTest()
    {
        //Todo: Kiểm tra 
        //1:Không chọn test mà nhấn thêm
        //2:Chọn test đã tồn tại trong list
        //3: Chọn test mà không điền giá (kiểm tra thêm sử kiện onchange của textbox giá)
        $('#btnAddTest').attr('disabled', true);

        $("#autocompleteSelectTest .autoComplete").blur(function () {

            var testName = $("#autocompleteSelectTest .autoComplete").val();
            var testId = $("#autocompleteSelectTest .autoCompleteValue").val();
            if (testName != "") {
                $('#btnAddTest').attr('disabled', false);
            }
            else {
                $('#btnAddTest').attr('disabled', true);
            }

            if (testId == "") {
                $('#btnAddTest').attr('disabled', true);
            }

            var allInputs = DataTableGetArrayDataSource();

            for (var i = 0; i < allInputs.length; i++) {
                var testTable = allInputs[i].TestName;
                if (testName == testTable) {
                    $('#btnAddTest').attr('disabled', true);
                    alert("Xét nghiệm " + testName + " đã tồn tại");
                }
            }
        });

        $("#autocompleteSelectTest .autoComplete").keypress(function () {
            $('#btnAddTest').attr('disabled', false);
        });

        $("#autocompleteSelectTest .autoComplete").keyup(function () {
            if (event.keyCode == '13') {
                var isExist = false;
                var testName = $("#autocompleteSelectTest .autoComplete").val();
                var testId = $("#autocompleteSelectTest .autoCompleteValue").val();
                var allInputs = DataTableGetArrayDataSource();
                for (var i = 0; i < allInputs.length; i++) {
                    var testTable = allInputs[i].TestName;
                    if (testTable == testName) {
                        isExist = true;
                        break;
                    }
                }
                if (isExist == false && testId != "") {
                    $("#btnAddTest").click();
                }
                if (isExist == true && testId != "") {
                    alert("Xét nghiệm " + testName + " đã tồn tại");
                }
            }
        });
    }
    $(document).ready(function () {

        CheckAllowAddTest();

        $("#btnAddTest").click(function () {

            var tags = $("#autocompleteSelectTest .autoCompleteTag").val().split(",");
            var testName = $("#autocompleteSelectTest .autoComplete").val();
            var testSection = tags[0];
            var cost = tags[1];
            var testId = $("#autocompleteSelectTest .autoCompleteValue").val();
            var dataObject = new Object();
            dataObject.TestName = testName;
            dataObject.TestSectionName = testSection;
            dataObject.Cost = cost;
            dataObject.TestId = testId;
            dataObject.IsEnable = true;
            var array = $("#list").jqGrid().getRowData();
            jQuery("#list").jqGrid('addRowData', array.length, dataObject);

            $("#autocompleteSelectTest .autoComplete").val("");
            $("#autocompleteSelectTest .autoCompleteValue").val(null);
            $("#autocompleteSelectTest .autoCompleteTag").val(null);
            $('#btnAddTest').attr('disabled', true);
        });

        $("#btnSavePanel").click(function (event) {
            event.preventDefault();
            $("#DataTableSaveButton").click();
            $("form").submit();
        });

    });
    
</script>
<%= Html.ValidationSummary() %>
<%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
  {%>
<% Html.BeginForm("Create", "Panel");%>
<%}
  else
  { %>
<% Html.BeginForm("Edit", "Panel"); %>
<%} %>
<%= Html.HiddenFor(m=>m.Panel.Id) %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
              {%>
                    <%=Resources.PanelStrings.PanelInsert_Title %>
            <%}
              else
              { %>
                     <%=Resources.PanelStrings.PanelUpdate_Title%>
            <%} %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="BothCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PanelStrings.PanelInsert_Name%></label>
                    </div>
                    <div class="Column ">
                        <%=Html.TextBoxFor(m => m.Panel.Name, new  {Class="textInput" })%>
                    </div>
                    <div class="Column" style="padding-left:20px;">
                        <%=Html.CheckBoxFor(m => m.Panel.IsActive)%>
                    </div>
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PanelStrings.PanelInsert_IsActive%></label>
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PanelStrings.PanelInsert_Description%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Panel.Description, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>

<% Html.EndForm(); %>
        <div class="ContentBottom">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PanelStrings.PanelInsert_TestName%></label>
                </div>
                <div class="Column">
                    <div id="autocompleteSelectTest">
                        <% Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
                    </div>
                </div>
                <div class="Column">
                    <input type="button" id="btnAddTest" value=" <%=Resources.PanelStrings.PanelInsert_ButtonAdd%>" />
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div>
            <% Html.RenderPartial("DataTable", Model.JQGrid); %>
        </div>
        <div align="center">

            <input type="button" value="<%=Resources.PanelStrings.PanelInsert_ButtonSave%>" id="btnSavePanel"/>
            <input type="button" value="<%=Resources.PanelStrings.PanelInsert_ButtonCanel%>" id="btnCanvel" />
        </div>
    </div>
</div>