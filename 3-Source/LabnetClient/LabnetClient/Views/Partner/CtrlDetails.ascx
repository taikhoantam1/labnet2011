<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PartnerViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript">

    function CheckAllowAddTest()
    {
        //Todo: Kiểm tra 
        //1:Không chọn test mà nhấn thêm
        //2:Chọn test đã tồn tại trong list
        //3: Chọn test mà không điền giá (kiểm tra thêm sử kiện onchange của textbox giá)
        $('#btnAddTest').attr('disabled', true);

        $("#autocompleteSelectTest .autoCompleteText").blur(function(){
            var testName = $("#autocompleteSelectTest .autoCompleteText").val();
            var cost = $("#txtCost").val();
            //alert(testName);
            if(testName != "" && cost != ""){
                $('#btnAddTest').attr('disabled', false);
            }
            else{
                $('#btnAddTest').attr('disabled', true);
            }
           
            var allInputs = $(".TestName");

            for (var i = 0; i < allInputs.length; i++) {
                var testTable = allInputs[i].value;
                if(testName == testTable){
                    $('#btnAddTest').attr('disabled', true);
                }
            }
         });

         $("#txtCost").keyup(function(){
            var testName = $("#autocompleteSelectTest .autoCompleteText").val();
            var cost = $("#txtCost").val();
            //alert(cost);
            if(testName != "" && cost != ""){
                $('#btnAddTest').attr('disabled', false);
            }
            else{
                $('#btnAddTest').attr('disabled', true);
            }

            var allInputs = $(".TestName");

            for (var i = 0; i < allInputs.length; i++) {
                var testTable = allInputs[i].value;
                if(testName == testTable){
                    $('#btnAddTest').attr('disabled', true);
                }
            }
         });
    }
    $(document).ready(function () {
        CheckAllowAddTest();
        var index =<%=Model.PartnerTestList.Count %>;
        BindCheckBoxDeleteTest();
        $("#autocompleteSelectTest .autoCompleteText").blur(function(){
             $("#txtCost").val($("#autocompleteSelectTest .autoCompleteTag").val());
            
         });

        $("#btnAddTest").click(function () {
            //alert("add");
            var newTr = $("#tblPartnerCostHiden tr").clone();
            var testName=$("#autocompleteSelectTest .autoCompleteText").val();
            var cost=$("#autocompleteSelectTest .autoCompleteTag").val();
            var testId=$("#autocompleteSelectTest .autoCompleteValue").val();
            var costEnter = $("#txtCost").val();

            $(newTr).find(".TestNameField").html(testName);
            $(newTr).find(".TestCostField").html(costEnter);

            $(newTr).find(".TestName").val(testName).attr("name","PartnerTestList["+index+"].TestName");
            $(newTr).find(".TestId").val(testId).attr("name","PartnerTestList["+index+"].TestId");
            $(newTr).find(".Cost").val(costEnter).attr("name","PartnerTestList["+index+"].Cost");
            $(newTr).find(".IsDelete").attr("name","PartnerTestList["+index+"].IsDelete");
            

            $("#tblPartnerCost").append(newTr);
            BindCheckBoxDeleteTest();
            index++;
            $("#autocompleteSelectTest .autoCompleteText").val("");
            $("#autocompleteSelectTest .autoCompleteValue").val(null);
            $("#autocompleteSelectTest .autoCompleteTag").val(null);
            $("#txtCost").val("");
            $('#btnAddTest').attr('disabled', true);
        });
        function BindCheckBoxDeleteTest()
        {
        
            $(".btnDelTest").unbind("click").click(function(){
                var trParent=$(this).parents("tr.trPartnerCost");
                if($(this).is(":checked"))
                {
                   $(trParent).find(".IsDelete").val("True");
                }
                else
                {
                    $(trParent).find(".IsDelete").val("False");
                }
            });
        }

        $('#reloadPage').click(function () {
            //alert('Handler for .click() called.');
            var allInputs = $(":input");
            for (var i = 0; i < allInputs.length; i++) {

                if (allInputs[i].id == "Partner_IsActive" || allInputs[i].id == "save" 
                    || allInputs[i].id == "reloadPage" || allInputs[i].id == "btnAddTest") {
                    //do nothing
                }
                else {
                    //alert(allInputs[i].value);
                    allInputs[i].value = "";
                }
            }

            $('.trPartnerCost').remove();
        });
    });
    
</script>

    <%= Html.ValidationSummary() %>
<%if (Model.ViewMode == LabnetClient.Constant.ViewMode.Create)
  {%>
<% Html.BeginForm("Create", "Partner");%>
<%}
  else
  { %>
<% Html.BeginForm("Edit", "Partner"); %>
<%} %>
<%= Html.HiddenFor(m=>m.Partner.id) %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == LabnetClient.Constant.ViewMode.Create)
              {%>
                    <%=Resources.PartnerStrings.PartnerInsert_Title %>
            <%}
              else
              { %>
                     <%=Resources.PartnerStrings.PartnerEdit_Title %>
            <%} %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Name %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Partner.Name, new  {Class="textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Address %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Partner.Address, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Phone %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Partner.Phone, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Active %></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m=>m.Partner.IsActive) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Owner %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Partner.Owner, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Email %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Partner.Email, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_Fax %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Partner.Fax, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PartnerStrings.PartnerInsert_Note %></label>
                </div>
                <div class="Column">
                    <%=Html.TextAreaFor(m => m.Partner.Note, new { cols=75, rows=3})%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="ContentBottom">
            <div class="Row MarginT20">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PartnerStrings.PartnerInsert_Test %></label>
                </div>
                <div class="Column">
                    <div id="autocompleteSelectTest">
                        <% Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
                    </div>
                </div>
                <div class="Column MarginL65">
                    <label class="lbTitle">
                        <%=Resources.PartnerStrings.PartnerInsert_TestPrice%></label>
                </div>
                <div class="Column">
                    <input type="text" id="txtCost" class="textInput130 number" />
                </div>
                <div class="Colum">
                    <input type="button" id="btnAddTest" value=" <%=Resources.PartnerStrings.PartnerInsert_Button_Add%>" />
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row MarginAuto MarginT20">
                <table id="ListTest">
                </table>
            </div>
        </div>
        <div>
        </div>
            <table id="tblPartnerCost" width="765px">
                <tr>
                    <th class="textSearch150" align="center">
                        <%=Resources.PartnerStrings.PartnerInsert_GridColumn_TestName%>
                    </th>
                    <th class="textSearch150" align="center">
                        <%=Resources.PartnerStrings.PartnerInsert_GridColumn_Price%>
                    </th>
                    <th class="textSearch150" align="center">
                        Xóa
                    </th>
                </tr>
                <% for(int i=0;i< Model.PartnerTestList.Count;i++)
                   { %>
                        <tr class="trPartnerCost">
                            <td class="textSearch150" align="center"><label class="TestNameField"><%= Model.PartnerTestList[i].TestName %></label>
                                <%= Html.HiddenFor(p => p.PartnerTestList[i].TestName, new  {@class="TestName" })%>
                                <%= Html.HiddenFor(p => p.PartnerTestList[i].TestId, new { @class = "TestId" })%>
                                <%= Html.HiddenFor(p => p.PartnerTestList[i].Cost, new { @class = "Cost" })%>
                                <%= Html.HiddenFor(p => p.PartnerTestList[i].IsDelete, new { @class = "IsDelete" })%>
                            </td>
                            <td class="textSearch150" align="center"><label class="TestCostField"> <%= Model.PartnerTestList[i].Cost %></label></td>
                            <td class="textSearch150" align="center"><input type="checkbox" class="btnDelTest" value="Xóa"/></td>
                        </tr>
                <%} %>
            </table>
        <div>
            <table class="hide" id="tblPartnerCostHiden">
                <tr class="trPartnerCost">
                    <td class="textSearch150" align="center"><label class="TestNameField"></label>
                        <input type="hidden" class="TestName"/>
                        <input type="hidden" class="TestId"/>
                        <input type="hidden" class="Cost"/>
                        <input type="hidden" class="IsDelete" value="False"/>
                    </td>
                    <td class="textSearch150" align="center"><label class="TestCostField"></label></td>
                    <td class="textSearch150" align="center"><input type="checkbox" class="btnDelTest" value="Xóa"/> </td>
                </tr>
            </table>
        </div>
        <div align="center">
            <input type="submit" value="<%=Resources.PartnerStrings.PartnerInsert_Button_Save%>" id="save"/>
            
        </div>
    </div>
</div>
<% Html.EndForm(); %>