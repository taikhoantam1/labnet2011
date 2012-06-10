<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.TestSectionSearchModel>" %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.TestSectionStrings.TestSectionSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.TestSectionStrings.TestSectionSearch_IsActive%></label>
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
                        <%=Resources.TestSectionStrings.TestSectionSearch_Name%></label>
                </div>
                <div class="Column">
                    <div id="autocompleteSelectPanel">
                        <% Html.RenderPartial("Autocomplete", Model.Autocomplete); %>
                    </div>
                </div>
                <div class="Column">
                    <input type="button" value="<%=Resources.PanelStrings.PanelSearch_Search%>" id="btnSearchFilter"/>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row ResultTable">
              
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnSearchFilter").click(function () {
            var filterText = $("#autocompleteSelectPanel .autoComplete").val();
            var isActive = $("#IsActive").is(":checked");
            $.ajax({
                url: "/NhomXetNghiem/Search",
                type: "POST",
                data: {
                    filterText: filterText,
                    isActive: isActive
                },
                success: function (data) {
                    $(".ResultTable").html(data);
                }
            });
        });
    });
</script>