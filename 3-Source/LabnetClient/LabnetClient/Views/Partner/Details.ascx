<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PartnerViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
        function ListArticleCommand(com, grid) {   
         
        }
        $("#autocompleteSelectTest .autoCompleteText").blur(function(){
          
             $("#txtCost").val($("#autocompleteSelectTest .autoCompleteTag").val());
        });
    })

     var index = 1;
     $("#btnAddTest").click(function () {
         var newTr = $("#tblPartnerCostHiden tr").clone();
         var fieldA = index;
         var fieldB = index;
         var fieldC = index;
         var fieldD = index;
         $(newTr).find(".TestNameField").html(fieldA);
         $(newTr).find(".TestCostField").html(fieldB);
         $(newTr).find(".btnDelTest").val("Xóa");
         $(newTr).find(".btnDelTest").click(function () {
             alert($(this).val());
         })
         $("#tblPartnerCost").append(newTr);
         index++;

     });
</script>
<% string display = ViewData.ModelState.IsValid ? "none" : "block"; %>
<div class="errorbox" id="validationSummary" style="display: <%=display%>">
    <span class='errorimage'><span class='errorhead'>Looks like we have a small problem...</span></span>
    <%= Html.ValidationSummary() %>
</div>
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
                        
                    </th>
                </tr>
            </table>
        <div>
            <table class="hiden" id="tblPartnerCostHiden">
                <tr>
                    <td><label class="TestNameField"></label></td>
                    <td><label class="TestCostField"></label></td>
                    <td><input type="button" class="btnDelTest" value="Xóa"/> </td>
                </tr>
            </table>
        </div>
        <div>
            <input type="submit" value="save" />
        </div>
    </div>
</div>
<% Html.EndForm(); %>