<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetServer.Models.DanhSachBenhNhanModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <h2 class="Title">
        Danh Sách Bệnh Nhân Gửi Mẫu </h2>
    <%= Html.ValidationSummary() %>
    <%Html.BeginForm(); %>
    <div class="PageContent">
        <div class="LeftCol">
            <%Html.RenderPartial("DoctorMenu"); %>
        </div>
        <div class="RightCol">
            <div class="Row">
              <div class="Column"><label class="Width120 ColumnLabel" >Ngày Gửi Mậu:</label> </div>
              <div class="Column">
                <%=Html.TextBox("SentDate", Model.SentDate.ToString("d"), new { ID = "SentDate", Class = "textInput100 date" })%>
              </div>
            </div>
            <div class="Row">
              <div class="Column"><label class="Width120 ColumnLabel">Lab Nhận Mẫu: </label> </div>
              <div class="Column"><%Html.RenderPartial("ComboBox", Model.LabComboBox);%></div>
            </div>
            <div class="Row">
                <input type="submit" class="button" value="Tìm"/>
            </div>
            <div class="Row">
                <div id="searchResult">
                    <%if (Model.DanhSachBenhNhanDataTableModel != null)
                      {%>
                        <% Html.RenderPartial("DataTable", Model.DanhSachBenhNhanDataTableModel); %>
                    <%} %>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
