<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script src="/Content/Lib/Utility/md5.js" type="text/javascript"></script>
<style type="text/css">
    .text
    {
        width: 170px;
    }
    #key
    {
        margin: 15px;
        float: left;
    }
    #loginWrapper
    {
        width: 300px;
    }
</style>
<script language="javascript" type="text/javascript">
    var redirectUrl = "";
    var labId = "";
    function CallDomainLogin(UserName, Password, Domain) {
        var url = Domain + "/User/Login";
        $.ajax({
            url: url,
            data: {
                UserName: UserName,
                Password: Password
            },
            dataType: 'jsonp', // Notice! JSONP <-- P (lowercase)
            type: "GET",
            jsonpCallback: "localJsonpCallback"
        });

    }

    function localJsonpCallback(json) {
        window.location = redirectUrl + "?token=" + json.toString()+"&Id="+labId;
    }

    function btnLogin_onclick() {
        var TaiKhoan = $("#txtTaiKhoan").val();
        var Pass = $("#txtMatKhau").val();
        $.blockUI();
        $.ajax({
            url: "/home/Login",
            type: "POST",
            data: {
                UserName: TaiKhoan,
                Password: Pass
            },
            dataType:"Json",
            success: function (data) {
                if (data.Message == "Success") {
                    redirectUrl = data.Url;
                    labId =data.LabId;
                    CallDomainLogin(TaiKhoan, Pass, data.Url );
                }
                else {
                    $.unblockUI();
                    alert(data.Message);
                }
            }
        });
    }

    $(document).ready(function () {

        $('#txtMatKhau').keypress(function (event) {

            if (event.which == '13') {
                btnLogin_onclick()
            }

        });

    });

</script>
<div class="MultiModal_title hide">
    Đăng Nhập</div>
<div id="loginWrapper">
    <div id="key">
        <img src="/Content/Images/login.png" alt="Login">
    </div>
    <div id="login">
        <div>
            <label for="user">
                Tài Khoản:</label>
            <input type='text' id="txtTaiKhoan" class="text" />
        </div>
        <div>
            <label for="password">
                Mật Khẩu:</label>
            <input type='password' id="txtMatKhau" width='100px' class="text" />
        </div>
        <div>
            <input id="btnLogin" onclick="btnLogin_onclick()" type="button" class="button" value="Đăng Nhập"
                width="67px" />
        </div>
    </div>
</div>
