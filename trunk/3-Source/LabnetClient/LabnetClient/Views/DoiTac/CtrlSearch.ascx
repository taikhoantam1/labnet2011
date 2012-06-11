<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PartnerSearchViewModel>" %>

<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm();%>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.PartnerStrings.PartnerSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PartnerStrings.PartnerSearch_IsActive%></label>
                </div>
                <div class="Column">
                        <%=Html.CheckBoxFor(m => m.IsActive)%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PartnerStrings.PartnerSearch_Name%></label>
                </div>
                <div class="Column">
                    <div id="autocompleteSelectPartner">
                      <%=Html.TextBoxFor(m => m.PartnerName, new { Class = "textInput" })%>
                    </div>
                </div>
                <div class="Column">
                    <input type="button" id="btnSubmit" value="<%=Resources.PartnerStrings.PanelSearch_Search%>" />
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</div>
<% Html.EndForm(); %>
<div id="SearchResult">
</div>
<script type="text/javascript">
    $(document).ready(function () {
        
        $("#btnSubmit").click(function () {
            //var data = $("form").serialize();
            var filterText = $("#PartnerName").val();
            var isActive = $("#IsActive").is(":checked");
            $.ajax({
                url: "/DoiTac/SearchPartner",
                type: "POST",
                data: {
                    PartnerName: filterText,
                    IsActive: isActive
                },
                success: function (data) {
                    $("#SearchResult").html(data);
                }
            });
        });
        <%if(!string.IsNullOrWhiteSpace(Model.PartnerName)) {%>
            $("#btnSubmit").click();
        <%}%>
    });
</script>