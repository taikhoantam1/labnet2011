<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.ResultNoteBookViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ResultNoteBook
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<%= Html.ValidationSummary() %>

<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.ReportStrings.ResultNoteBook_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <%Html.BeginForm(); %>
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.ReportStrings.ResultNoteBook_StartDate%></label>
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
                            <%=Resources.ReportStrings.ResultNoteBook_EndDate%></label>
                    </div>
                    <div class="Column">
                    <%=Html.TextBoxFor(m => m.EndDate, new { Class = "textInput100 date" , Style="width:125px"})%>
                        
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
