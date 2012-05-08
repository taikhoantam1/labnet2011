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
    function btnLogin_onclick() {
        var TaiKhoan = $("#txtTaiKhoan").val();
        var Pass = $("#txtMatKhau").val();
        $.blockUI();
        $.ajax({
            url: "/BacSi/Login",
            type: "POST",
            data: {
                UserName: TaiKhoan,
                Password: Pass
            },
            dataType: "Json",
            success: function (data) {
                if (data.Message == "Success") {
                    window.location = "/BacSi";
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
            <input id="btnLogin" onclick="btnLogin_onclick()" type="button" 
                style="-webkit-appearance: none;
                        -webkit-box-align: center;
                        -webkit-box-shadow: rgba(0, 0, 0, 0.0976563) 0px 1px 1px 0px;
                        -webkit-rtl-ordering: logical;
                        -webkit-transition-delay: 0s;
                        -webkit-transition-duration: 0s;
                        -webkit-transition-property: all;
                        -webkit-transition-timing-function: cubic-bezier(0.25, 0.1, 0.25, 1);
                        -webkit-user-select: none;
                        background-attachment: scroll; 
                        background-clip: border-box; background-color: #357AE8; 
                        background-image: -webkit-linear-gradient(top, #4D90FE, #357AE8); 
                        background-origin: padding-box; border-bottom-color: #2F5BB7; 
                        border-bottom-left-radius: 2px;
                        border-bottom-right-radius: 2px;
                        border-bottom-style: solid;
                        border-bottom-width: 1px;
                        border-left-color: #2F5BB7;
                        border-left-style: solid;
                        border-left-width: 1px;
                        border-right-color: #2F5BB7;
                        border-right-style: solid;
                        border-right-width: 1px;
                        border-top-color: #2F5BB7;
                        border-top-left-radius: 2px;
                        border-top-right-radius: 2px;
                        border-top-style: solid;
                        border-top-width: 1px;
                        box-shadow: rgba(0, 0, 0, 0.0976563) 0px 1px 1px 0px;
                        box-sizing: border-box;
                        color: white;
                        display: inline-block;
                        font-weight: bold;
                        height: 32px;
                        line-height: 29px;
                        margin-top: 10px;
                        padding: 0px 8px 0px 8px;"
            value="Đăng Nhập"
            width="67px" />
            <input id="btnRegister" type="button" class="multi_modal_link" href="/BacSi/Register" value="Đăng Ký"
                style=" -webkit-appearance: none;
                        -webkit-box-align: center;
                        -webkit-box-shadow: rgba(0, 0, 0, 0.0976563) 0px 1px 1px 0px;
                        -webkit-rtl-ordering: logical;
                        -webkit-transition-delay: 0s;
                        -webkit-transition-duration: 0s;
                        -webkit-transition-property: all;
                        -webkit-transition-timing-function: cubic-bezier(0.25, 0.1, 0.25, 1);
                        -webkit-user-select: none;
                        background-attachment: scroll; 
                        background-clip: border-box; 
                        background-color: #357AE8; 
                        background-image: -webkit-linear-gradient(top, #4D90FE, #357AE8); 
                        background-origin: padding-box; border-bottom-color: #2F5BB7; 
                        border-bottom-left-radius: 2px;
                        border-bottom-right-radius: 2px;
                        border-bottom-style: solid;
                        border-bottom-width: 1px;
                        border-left-color: #2F5BB7;
                        border-left-style: solid;
                        border-left-width: 1px;
                        border-right-color: #2F5BB7;
                        border-right-style: solid;
                        border-right-width: 1px;
                        border-top-color: #2F5BB7;
                        border-top-left-radius: 2px;
                        border-top-right-radius: 2px;
                        border-top-style: solid;
                        border-top-width: 1px;
                        box-shadow: rgba(0, 0, 0, 0.0976563) 0px 1px 1px 0px;
                        box-sizing: border-box;
                        color: white;
                        display: inline-block;
                        font-weight: bold;
                        height: 32px;
                        line-height: 29px;
                        margin-top: 10px;
                        padding: 0px 8px 0px 8px;"
                width="67px" />
        </div>
    </div>
</div>
