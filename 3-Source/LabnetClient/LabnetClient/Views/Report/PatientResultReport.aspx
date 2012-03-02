<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.PatientReportViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <%= Html.ValidationSummary() %>

<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.PatientStrings.PatientResultReport_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <%Html.BeginForm(); %>
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientResultReport_Number%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.OrderNumber, new { Class = "textInput" , Style="width:125px"})%>
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.PatientStrings.PatientResultReport_ReceivedDate%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.ReceivedDate, new { Class = "textInput100 date", Style = "width:125px" })%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div align="center">
        <input type="submit" id="btnPrintPreview" value="In Kết Quả" />  
    </div>
</div>

</asp:Content>