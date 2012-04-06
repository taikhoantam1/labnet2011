<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.QuanLyTaiChinhViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    QuanLyTaiChinh
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.ValidationSummary() %>
    <div class="Module">
        <div class="ModuleTitle">
            <h3 class="Title">
                <%=Resources.ReportStrings.QuanLyTaiChinh_Title%>
            </h3>
        </div>
        <div class="ModuleContent">
            <%Html.BeginForm(); %>
            <div class="ContentTop">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.ReportStrings.QuanLyTaiChinh_Partner%></label>
                    </div>
                    <div class="Column">
                        <%=Html.DropDownListFor(m => m.PartnerId,Model.DropDownListPartner, new { Style = "width:200px" })%>
                    </div>
                </div>
                <div class="LeftCol">
                    <div class="Row">
                        <div class="Column">
                            <label class="lbTitle">
                                <%=Resources.ReportStrings.QuanLyTaiChinh_StartDate%></label>
                        </div>
                        <div class="Column">
                            <%=Html.TextBoxFor(m => m.StartDate, new { Class = "textInput100 date", Style = "width:125px" })%>
                        </div>
                    </div>
                </div>
                <div class="RightCol">
                    <div class="Row">
                        <div class="Column">
                            <label class="lbTitle">
                                <%=Resources.ReportStrings.QuanLyTaiChinh_EndDate%></label>
                        </div>
                        <div class="Column">
                            <%=Html.TextBoxFor(m => m.EndDate, new { Class = "textInput100 date" , Style="width:125px"})%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
        <div align="center">
            <input type="submit" id="btnPrintPreview" value="In Kết Quả" />
        </div>
    </div>
    <%if (Model.ReportModel != null)
      {%>
            <div id="ReportContainer">
                <% Html.RenderPartial("ReportViewerControl", Model.ReportModel); %>
            </div>
    <%} %>
</asp:Content>
