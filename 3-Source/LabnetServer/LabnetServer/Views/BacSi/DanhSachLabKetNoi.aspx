﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetServer.Models.ThietLapKetNoiModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Danh Sách Phòng Xét Nghiệm Kết Nối</h2>
    <%= Html.ValidationSummary() %>
    <%Html.BeginForm(); %>
    <div class="PageContent">
        <div class="Row">
            <%Html.RenderPartial("DataTable", Model.DanhSachKetNoiModel); %>
        </div>
        <div class="Row">
            <input type="button" class="button multi_modal_link" value="Thêm Kết Nối Mới" href="#AddConnectionForm" />
        </div>
    </div>
    <div class="hide">
        <div id="AddConnectionForm" style="width: 250px">
            <div class="MultiModal_title hide">
                Tạo kết nối với phòng XN
            </div>
            <div class="Row" id="errorMessage">
            </div>
            <div class="Row">
                   <b>Mã Kết Nối</b> 
            </div>
            <div class="Row">
                
                <div class="Column PaddingT10 ">
                    <input type="text" id="txtConnectionCode" /></div>
                <div class="Column PaddingL10">
                    <input type="button" id="btnCreateConnect" value="Kết Nối" class="button" /></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnCreateConnect").click(function () {
                var connectionCode = $("#txtConnectionCode").val();
                if (connectionCode == "" || connectionCode.length < 7) {
                    $("#errorMessage").html("<lable class='error'>Chuỗi kết nối không hợp lệ</label>");
                }
                else {
                    $("#errorMessage").html("");
                }

                $.ajax({
                    url: "/BacSi/ConnectWithLab",
                    data: {
                        ConnectionCode: connectionCode
                    },
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        if (data == "Success") {
                            window.location.reload();
                        }
                    }

                });
            });
        });
    </script>
    <%Html.EndForm(); %>
</asp:Content>
