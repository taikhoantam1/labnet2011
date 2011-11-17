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
    }
    $(document).ready(function () {
        
        CheckAllowAddTest();
        var index =<%=Model.PanelTestList.Count %>;
        BindCheckBoxDeleteTest();
        $("#autocompleteSelectTest .autoCompleteText").blur(function(){
             $("#txtCost").val($("#autocompleteSelectTest .autoCompleteTag").val());
            
         });

        $("#btnAddTest").click(function () {
            //alert("add");
            var newTr = $("#tblPanelItemHiden tr").clone();
            var testName=$("#autocompleteSelectTest .autoCompleteText").val();
            var testSection=$("#autocompleteSelectTest .autoCompleteTag").val();
            var testId=$("#autocompleteSelectTest .autoCompleteValue").val();

            $(newTr).find(".TestNameField").html(testName);
            $(newTr).find(".TestSectionField").html(testSection);

            $(newTr).find(".TestName").val(testName).attr("name","PanelTestList["+index+"].TestName");
            $(newTr).find(".TestId").val(testId).attr("name","PanelTestList["+index+"].TesstId");
            $(newTr).find(".TestSectionName").val(testSection).attr("name","PanelTestList["+index+"].TestSectionName");
            $(newTr).find(".IsDelete").attr("name","PanelTestList["+index+"].IsDelete");
            

            $("#tblPanelItem").append(newTr);
            BindCheckBoxDeleteTest();
            index++;
            $("#autocompleteSelectTest .autoCompleteText").val("");
            $("#autocompleteSelectTest .autoCompleteValue").val(null);
            $("#autocompleteSelectTest .autoCompleteTag").val(null);
            $('#btnAddTest').attr('disabled', true);
        });
        function BindCheckBoxDeleteTest()
        {
        
            $(".btnDelTest").unbind("click").click(function(){
                var trParent=$(this).parents("tr.trPanelItemDetail");
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
            <%if (Model.ViewMode == LabnetClient.Constant.ViewMode.Create)
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
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PanelStrings.PanelInsert_Name%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Panel.Name, new  {Class="textInput" })%>
                    </div>
                    <div class="clear">
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
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PanelStrings.PanelInsert_IsActive%></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m => m.Panel.IsActive)%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row clear">
                </div>
            </div>
        </div>
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
                <div class="Colum">
                    <input type="button" id="btnAddTest" value=" <%=Resources.PanelStrings.PanelInsert_ButtonAdd%>" />
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div>
        </div>
            <table id="tblPanelItem" width="765px">
                <tr>
                    <th class="textSearch150" align="center">
                        <%=Resources.PanelStrings.PanelInsert_GridTestName%>
                    </th>
                    <th class="textSearch150" align="center">
                        <%=Resources.PanelStrings.PanelInsert_GridTestSection%>
                    </th>
                    <th class="textSearch150" align="center">
                        <%=Resources.PanelStrings.PanelInsert_GridRemove%>
                    </th>
                </tr>
                <% for(int i=0;i< Model.PanelTestList.Count;i++)
                   { %>
                        <tr class="trPanelItemDetail">
                            <td class="textSearch150" align="center"><label class="TestNameField"><%= Model.PanelTestList[i].TestName%></label>
                                <%= Html.HiddenFor(p => p.PanelTestList[i].TestName, new { @class = "TestName" })%>
                                <%= Html.HiddenFor(p => p.PanelTestList[i].TesstId, new { @class = "TestId" })%>
                                <%= Html.HiddenFor(p => p.PanelTestList[i].TestSectionName, new { @class = "TestSectionName" })%>
                                <%= Html.HiddenFor(p => p.PanelTestList[i].IsDelete, new { @class = "IsDelete" })%>
                            </td>
                            <td class="textSearch150" align="center"><label class="TestSectionField"> <%= Model.PanelTestList[i].TestSectionName%></label></td>
                            <td class="textSearch150" align="center"><input type="checkbox" class="btnDelTest" value="Xóa"/></td>
                        </tr>
                <%} %>
            </table>
        <div>
            <table class="hide" id="tblPanelItemHiden">
                <tr class="trPanelItemDetail">
                    <td class="textSearch150" align="center"><label class="TestNameField"></label>
                        <input type="hidden" class="TestName"/>
                        <input type="hidden" class="TestId"/>
                        <input type="hidden" class="TestSectionName"/>
                        <input type="hidden" class="IsDelete" value="False"/>
                    </td>
                    <td class="textSearch150" align="center"><label class="TestSectionField"></label></td>
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