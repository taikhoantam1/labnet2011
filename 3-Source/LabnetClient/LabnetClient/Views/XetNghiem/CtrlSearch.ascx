<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.TestSearctViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%=Html.ValidationSummary() %>
<% Html.BeginForm();%>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.TestStrings.TestSearch_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestSearch_TestName%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSearch.TestName, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestSearch_PanelName%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSearch.PanelName, new {Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <input type="button" id="btnSubmit" value="<%=Resources.TestStrings.TestSearch_Search%>" style="margin:0px"/>
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.TestStrings.TestSearch_TestSectionName %></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.TestSearch.TestSectionName, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<% Html.EndForm(); %>
<div id="SearchResult" class="Row">
   
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnSubmit").click(function () {
            var data = $("form").serialize();
            $.ajax({
                url: "/XetNghiem/SearchTest",
                data: data,
                type: "POST",
                success: function (data) {
                    $("#SearchResult").html(data);
                }
            });
        });
    });
</script>
