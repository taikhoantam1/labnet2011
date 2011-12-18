<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.PanelSearchViewModel>" %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.PanelStrings.PanelSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.PanelStrings.PanelSearch_Name%></label>
                </div>
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.PanelName, new { Class = "textInput filterText" })%>
                </div>
                <div class="Colum">
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
            var filterText = $(".filterText").val();
            $.ajax({
                url: "/Panel/Search",
                type: "POST",
                data: {
                    filterText: filterText
                },
                success: function (data) {
                    $(".ResultTable").html(data);
                }
            });
        });
    });
</script>