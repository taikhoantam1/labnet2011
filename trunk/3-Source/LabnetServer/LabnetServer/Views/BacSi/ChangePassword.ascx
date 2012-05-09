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
    function btnChangePass_onclick() {
        var oldPass= $("#txtOldPass").val();
        var newPass = $("#txtNewPass").val();
        $.ajax({
            url: "/BacSi/ChangePassword",
            type: "POST",
            data: {
                oldPass: oldPass,
                newPass: newPass
            },
            dataType: "Json",
            success: function (data) {
                if (data.Message == "Success") {
                    $(".btnCloseDialog").click();
                    alert("Đỗi Mật Khẩu Thành Công");
                }
                else {
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
            <label for="txtOldPass">
                Mật Khẩu Cũ:</label>
            <input type='password' id="txtOldPass" class="text" />
        </div>
        <div>
            <label for="password">
                Mật Khẩu Mới:</label>
            <input type='password' id="txtNewPass" width='100px' class="text" />
        </div>
        <div>
            <input id="btnChangePass" onclick="btnChangePass_onclick()" type="button" class="button" value="Đổi Mật Khẩu"/>
            <input id="btnCancel" type="button"  value="Hủy" class="button btnCloseDialog" width="67px" />
        </div>
    </div>
</div>
