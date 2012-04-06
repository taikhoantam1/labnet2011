<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.TestSectionViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <% Html.RenderPartial("CtrlCreate", Model); %>
</asp:Content>

