<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViewPage1</title>
    <link href="../../Content/Style/LayoutTemplate.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript">
        google.load("jqueryui", "1.8.16");
    </script>
</head>
<body>
    <script src="/Content/Lib/Utility/md5.js" type="text/javascript"></script>
    <style type="text/css">
        .text
        {
            width: 170px;
        }
        #loginWrapper
        {
            background: url(/Content/Images/gradient.png) repeat-x;
            font-size: 12px;
            height: 160px;
            width: 360px;
            margin:100px auto;
        }
        #login
        {
            margin-top: 30px;
            margin-right: 40px;
            float: right;
        }
        #login div
        {
            margin: 10px;
        }
        #loginWarpper div
        {
            margin: 5px;
        }
        #key
        {
            margin-top: 30px;
            float: left;
        }
        
        .button
        {
            padding: 4px;
            font-family: Arial;
            font-size: 12px;
            color: #146EB4;
            text-align: center;
            cursor: pointer;
            border: 1px #999999 solid;
            background-color: #eeeee;
            margin: 5px;
        }
        .button:hover
        {
            color: #FF5509;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function btnLogin_onclick() {
            var TaiKhoan = $("#txtTaiKhoan").val();
            var Pass = $("#txtMatKhau").val();
            $.ajax({
                url: "/home/Login",
                type: "POST",
                data: {
                    UserName: TaiKhoan,
                    Password: Pass
                },
                success: function (data) {
                    if (data.indexOf("labnet.vn") != -1)
                        window.location = data;
                    else
                        alert(data);
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
    <div id="loginWrapper">
        <div id="key">
            <img src="/Content/Images/login.png" alt="Login">
        </div>
        <div id="login">
            <div>
                <label for="user">
                    Username:</label>
                <input type='text' id="txtTaiKhoan" class="text" />
            </div>
            <div>
                <label for="password">
                    Password:</label>
                <input type='password' id="txtMatKhau" width='100px' class="text" />
            </div>
            <div align="center">
                <input id="btnLogin" onclick="btnLogin_onclick()" type="button" class="button" value="Đăng Nhập"
                    width="67px" />
            </div>
        </div>
    </div>
</body>
</html>
