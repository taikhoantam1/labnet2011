<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PartnerViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
        function ListArticleCommand(com, grid) {   
         
        }
    
        $("#ListTest").flexigrid
			        (
			            {
			                url: <% Response.Write("'/Ajax/GetListTest?PartnerId=1'"); %>,
			                dataType: 'json',
			                colModel: [
                            { display: '<%=Resources.PartnerStrings.PartnerInsert_GridColumn_TestName %>', name: 'TestName', width: 200, sortable: true, align: 'center', hide: false },
                            { display: '<%=Resources.PartnerStrings.PartnerInsert_GridColumn_Price %>', name: 'TestPrice', width: 200, sortable: true, align: 'center', hide: false },
                            { display: '<%=Resources.PartnerStrings.PartnerInsert_GridColumn_Delete %>', name: '', width: 100, sortable: true, align: 'center', hide: false },
                            
                            ],
			                sortname: "TestName",
			                sortorder: "desc",
			                usepager: true,
			                title: '<%=Resources.PartnerStrings.PartnerInsert_GridColumn_Title %>',
			                useRp: true,
			                rp: 10,
			                showTableToggleBtn: true,
			                width:600,
			                height:300,
			                singleSelect: true
			            }
			        );

                     $('b.top').click
	                    (
		                    function() {
		                        $(this).parent().toggleClass('fh');
		                    }
	                    );
    })
   
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
                        <% Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
                </div>
                <div class="Column MarginL15">
                    <label class="lbTitle Width120">
                        <%=Resources.PartnerStrings.PartnerInsert_TestPrice%></label>
                </div>
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Partner.PartnerCostDetails[0].Cost)%>
                </div>
                <div class="Colum">
                    <input type="button" value=" <%=Resources.PartnerStrings.PartnerInsert_Button_Add%>" />
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
            <input type="submit" value="save" />
        </div>
    </div>
</div>
<% Html.EndForm(); %>