<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.PatientTestViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <% Html.RenderPartial("PatientResult", Model); %>

</asp:Content>