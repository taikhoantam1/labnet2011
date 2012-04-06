<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.TestSectionListViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Module">
        <div class="ModuleTitle">
            <h3 class="Title">
                <%=Resources.TestSectionStrings.TestSection_ListTitle%>
            </h3>
        </div>
        <div class="Row">
            <div class="Column">
                <% Html.RenderPartial("DataTable", Model.TestSectionList); %>
            </div>
        </div>
    </div>
</asp:Content>
