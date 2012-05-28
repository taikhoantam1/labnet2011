<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetServer.Models.KQXNModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="Title">
        Xem kết quả xét nghiệm</h2>
    <%Html.BeginForm(); %>
    <div class="PageContent">
        <div class="LeftCol">
            <%Html.RenderPartial("LabMenu"); %>
        </div>
        <div class="RightCol">
            <div class="Row">
                <% Html.RenderPartial("KQSN",Model); %>
            </div>
        </div>
        <div class="clear"></div>
    </div>
</asp:Content>