<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PartnerViewModel>" %>
<script type="text/javascript">
function browseImage()
{
    var windowURL = "/Content/Lib/FileManager/Default.aspx";
    var popupWindow = window.open(windowURL, "popupWindow", "height=550,width=965,scrollbars=1,top=50,left=50");
    var watchClose = setInterval(function() {
        if (popupWindow.closed) {
            var path = getCookie("c_FileInsertPath");
            $("#Partner_Logo").val(path);
            cleanCookie("c_FileInsertPath");
            $("#logoImg").attr("src",$("#Partner_Logo").val());
            //Do something here...
            clearInterval(watchClose);
        }
    }, 200);
}
$(document).ready(function () {
    //Todo: Kiểm tra 
    //1:Không chọn test mà nhấn thêm
    //2:Chọn test đã tồn tại trong list
    //3: Chọn test mà không điền giá (kiểm tra thêm sử kiện onchange của textbox giá)
    $('#btnAddTest').attr('disabled', true);
    $('#btnAddTestSection').attr('disabled', true);

    $("#autocompleteSelectTest .autoComplete").blur(function () {
        var tags = $("#autocompleteSelectTest .autoCompleteTag").val().split(",");
        $("#txtCost").val(tags[1]);
        var testName = $("#autocompleteSelectTest .autoComplete").val();

        var allInputs = DataTableGetArrayDataSource_<%:Model.JQGrid.TableId %>();

        for (var i = 0; i < allInputs.length; i++) {
            var testTable = allInputs[i].TestName;
            if (testName == testTable) {
                $('#btnAddTest').attr('disabled', true);
                alert("Xét nghiệm " + testName + " đã tồn tại");
            }
        }
    });

    $("#autocompleteSelectTestSection .autoComplete").blur(function () {
        var tag = $("#autocompleteSelectTestSection .autoCompleteTag").val();
        $("#txtCostTestSection").val(tag);
        var testSectionName = $("#autocompleteSelectTestSection .autoComplete").val();

        var allInputs = DataTableGetArrayDataSource_<%:Model.JQGridTestSection.TableId %>();

        for (var i = 0; i < allInputs.length; i++) {
            var testTable = allInputs[i].TestSectionName;
            if (testSectionName == testTable) {
                $('#btnAddTestSection').attr('disabled', true);
                alert("Nhóm Xét nghiệm " + testSectionName + " đã tồn tại");
            }
        }
    });

    $("#autocompleteSelectTest .autoComplete").keypress(function () {
        $('#btnAddTest').attr('disabled', false);
    });

    $("#autocompleteSelectTestSection .autoComplete").keypress(function () {
        $('#btnAddTestSection').attr('disabled', false);
    });

    $("#txtCost").keyup(function () {
        var testName = $("#autocompleteSelectTest .autoComplete").val();
        var cost = $("#txtCost").val();
        //alert(cost);
        if (testName != "" && cost != "") {
            $('#btnAddTest').attr('disabled', false);
        }
        else {
            $('#btnAddTest').attr('disabled', true);
        }

        var allInputs = DataTableGetArrayDataSource_<%:Model.JQGrid.TableId %>();

        for (var i = 0; i < allInputs.length; i++) {
            var testTable = allInputs[i].TestName;
            if (testName == testTable) {
                $('#btnAddTest').attr('disabled', true);
            }
        }
    });

    $("#txtCostTestSection").keyup(function () {
        var testSectionName = $("#autocompleteSelectTestSection .autoComplete").val();
        var cost = $("#txtCostTestSection").val();
        //alert(cost);
        if (testSectionName != "" && cost != "") {
            $('#btnAddTestSection').attr('disabled', false);
        }
        else {
            $('#btnAddTestSection').attr('disabled', true);
        }

        var allInputs = DataTableGetArrayDataSource_<%:Model.JQGridTestSection.TableId %>();

        for (var i = 0; i < allInputs.length; i++) {
            var testTable = allInputs[i].TestSectionName;
            if (testSectionName == testTable) {
                $('#btnAddTestSection').attr('disabled', true);
            }
        }
    });

    $("#btnAddTest").click(function () {
        //alert("add");
        var tags = $("#autocompleteSelectTest .autoCompleteTag").val().split(",");
        var testName = $("#autocompleteSelectTest .autoComplete").val();
        var testSection = tags[0];
        var cost = tags[1];
        var costEnter = $("#txtCost").val();
        var testId = $("#autocompleteSelectTest .autoCompleteValue").val();

        var dataObject = new Object();
        dataObject.TestName = testName;
        dataObject.TestSectionName = testSection;
        dataObject.Cost = costEnter;
        dataObject.TestId = testId;
        dataObject.IsEnable = true;
        var array = $("#<%:Model.JQGrid.TableId %>").jqGrid().getRowData();
        jQuery("#<%:Model.JQGrid.TableId %>").jqGrid('addRowData', array.length, dataObject);

        $("#autocompleteSelectTest .autoComplete").val("");
        $("#autocompleteSelectTest .autoCompleteValue").val(null);
        $("#autocompleteSelectTest .autoCompleteTag").val(null);
        $("#txtCost").val("");
        $('#btnAddTest').attr('disabled', true);
    });

    $("#btnAddTestSection").click(function () {
        //alert("add");
        var tag = $("#autocompleteSelectTestSection .autoCompleteTag").val().split(",");
        //alert(tag);
        var testSectionName = $("#autocompleteSelectTestSection .autoComplete").val();
        //alert(testSectionName);
        var cost = tag;
        var costEnter = $("#txtCostTestSection").val();
        var testSectionId = $("#autocompleteSelectTestSection .autoCompleteValue").val();

        var dataObject = new Object();
        dataObject.TestSectionName = testSectionName;
        dataObject.TestSectionId = testSectionId;
        dataObject.TestSectionCost = costEnter;
        dataObject.IsEnableTestSection = true;
        var array = $("#<%:Model.JQGridTestSection.TableId %>").jqGrid().getRowData();
        //alert(array);
        jQuery("#<%:Model.JQGridTestSection.TableId %>").jqGrid('addRowData', array.length, dataObject);

        $("#autocompleteSelectTestSection .autoComplete").val("");
        $("#autocompleteSelectTestSection .autoCompleteValue").val(null);
        $("#autocompleteSelectTestSection .autoCompleteTag").val(null);
        $("#txtCostTestSection").val("");
        $('#btnAddTestSection').attr('disabled', true);
    });

    $("#btnSavePanel").click(function (event) {
        event.preventDefault();
        //var data = DataTableGetDataSource();
        $("#DataTableSaveButton_<%:Model.JQGrid.TableId %>").click();
        $("#DataTableSaveButton_<%:Model.JQGridTestSection.TableId %>").click();
        $("form").submit();
    });


    $(".btnCreateConnectionCode").click(function () {
        var clientLabId = $("#Partner_Id").val();
        $.ajax({
            url: "/DoiTac/CreateConnectionCode",
            data: {
                clientLabId: clientLabId
            },
            type: "POST",
            dataType: "json",
            success: function (data) {
                if (data.IsSuccess == true) {
                    $(".btnCreateConnectionCode").hide();
                    $(".btnCreateConnectionCode").after($("<label class='Red Bold Large'> " + data.ResponseData + "</label>"));
                    $("#Partner_ConnectionCode").val(data.ResponseData);
                }
                else {
                    alert(data.ErrorMessage);
                }
            }
        });
    });
           
    $("#btnRemoveConnection").click(function () {
        var clientLabId = $("#Partner_Id").val();
        if (clientLabId != 0) {
            $.ajax({
                url: "/DoiTac/RemoveConnection",
                data: {
                    clientLabId: clientLabId
                },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess == true) {
                        $("#btnRemoveConnection").attr("disabled", "disabled");
                        $("#Partner_IsConnected").attr("checked", false);
                        $("#lbConnectionCode").remove();
                        $("#Partner_ConnectionCode").val("");
                        $(".btnCreateConnectionCode").removeClass("hide");
                    }
                }
            });
        }
    });
});
    
</script>
<%= Html.ValidationSummary() %>
<%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
  {%>
<% Html.BeginForm("Create", "DoiTac");%>
<%}
  else
  { %>
<% Html.BeginForm("Edit", "DoiTac"); %>
<%} %>
<%= Html.HiddenFor(m=>m.Partner.Id) %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
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
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            Banner
                        </label>
                    </div>
                    <div class="Column">
                        <%= Html.HiddenFor(p=>p.Partner.Logo) %>
                        <img id="logoImg" src="<%=string.IsNullOrWhiteSpace(Model.Partner.Logo)?"":Model.Partner.Logo%>" width="120" height="60"/>
                        <input type="button" onclick="browseImage()" value="Browse..." />
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
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_ConnectionCode%></label>
                    </div>
                    <div class="Column">
                        <%if (string.IsNullOrEmpty(Model.Partner.ConnectionCode) && Model.ViewMode != DomainModel.Constant.ViewMode.Create)
                          { %>
                        <input type="button" class="btnCreateConnectionCode" value="Tạo Mã Liên Kết" />
                        <%}
                          else
                          { %>
                        <label class="Red Bold Large" id="lbConnectionCode">
                            <%= Model.Partner.ConnectionCode %></label>
                        <input type="button" value="Tạo Mã Liên Kết" class="hide btnCreateConnectionCode" />
                        <%} %>
                        <%= Html.HiddenFor(p=>p.Partner.ConnectionCode) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PartnerStrings.PartnerInsert_IsConnected%></label>
                    </div>
                    <div class="Column">
                        <%= Html.CheckBoxFor(m => m.Partner.IsConnected, new { @class = "disabled" })%>
                    </div>
                    <div class="Column">
                        <input type="button" id="btnRemoveConnection" value="Hũy Kết Nối" <%= Model.Partner.IsConnected && !string.IsNullOrEmpty(Model.Partner.ConnectionCode)? "": "disabled='disabled'" %> />
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
                <div class="Column PaddingL65 ">
                    <label class="lbTitle Width80">
                        <%=Resources.PartnerStrings.PartnerInsert_TestPrice%></label>
                </div>
                <div class="Column ">
                    <input type="text" id="txtCost" class="textInput130 number" />
                </div>
                <div class="Column">
                    <input type="button" id="btnAddTest" value=" <%=Resources.PartnerStrings.PartnerInsert_Button_Add%>" />
                </div>
            </div>
            <div class="Row MarginAuto MarginT20">
                <table id="ListTest">
                </table>
            </div>
            <% Html.EndForm(); %>
        </div>
        <div>
            <%Html.RenderPartial("DataTable", Model.JQGrid); %>
        </div>
        <div class="clear">
        </div>
        <div class="Row MarginT20">
            <div class="Column">
                <label class="lbTitle">
                    <%=Resources.PartnerStrings.PartnerInsert_TestSection %></label>
            </div>
            <div class="Column">
                <div id="autocompleteSelectTestSection">
                    <% Html.RenderPartial("Autocomplete", Model.TestSectionAutocomplete); %>
                </div>
            </div>
            <div class="Column PaddingL65 ">
                <label class="lbTitle Width80">
                    <%=Resources.PartnerStrings.PartnerInsert_TestSectionPrice%></label>
            </div>
            <div class="Column ">
                <input type="text" id="txtCostTestSection" class="textInput130 number" />
            </div>
            <div class="Column">
                <input type="button" id="btnAddTestSection" value=" <%=Resources.PartnerStrings.PartnerInsert_Button_Add_TestSection%>" />
            </div>
            <div class="clear">
            </div>
        </div>
        <div>
            <%Html.RenderPartial("DataTable", Model.JQGridTestSection); %>
        </div>
        <%--<table id="tblPartnerCost" width="765px">
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
            </table>--%>
        <div align="center">
            <input type="button" value="<%=Resources.PartnerStrings.PartnerInsert_Button_Save%>"
                id="btnSavePanel" />
        </div>
    </div>
</div>
