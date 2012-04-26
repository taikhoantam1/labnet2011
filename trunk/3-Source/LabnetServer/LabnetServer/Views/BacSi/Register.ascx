<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetServer.Models.BacSiModel>" %>

<script src="/Content/Lib/Utility/md5.js" type="text/javascript"></script>
<style type="text/css">
    
    #registerWrapper
    {
        width: 670px;
    }
</style>
<script language="javascript" type="text/javascript">
    function CheckUserName(userName){
        var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";
        for (var i = 0; i < userName.length; i++) {
            if (iChars.indexOf(userName.charAt(i)) != -1 || userName.charAt(i).toString() == ' ') {
  	            return false;
  	        }
  	    }

  	    return true;
  	}

  	function CheckPassword(password, passwordVerify) {
  	    if (password == passwordVerify)
  	        return true;

  	    return false;
  	}

  	function CheckPhoneNumber(phone) {
  	    var strValidChars = "0123456789";
  	    var strChar;
  	    var blnResult = true;

  	    if (phone.length == 0) return true;
  	    for (var i = 0; i < phone.length && blnResult == true; i++) {
  	        strChar = phone.charAt(i);
  	        if (strValidChars.indexOf(strChar) == -1) {
  	            blnResult = false;
  	        }
  	    }
  	    return blnResult;
  	}

    function Register() {
        var name = $("#Doctor_Name").val();
        var userName = $("#Doctor_UserName").val();
        var password = $("#Doctor_Password").val();
        var passwordVerify = $("#Doctor_PasswordVerify").val();
        var connectionCode = $("#Doctor_ConnectionCode").val();
        var address = $("#Doctor_Address").val();
        var phoneNumber = $("#Doctor_Phone").val();
        var email = $("#Doctor_Email").val();
        var checkUserName = CheckUserName(userName);
        var checkPass = CheckPassword(password, passwordVerify);
        var checkPhone = CheckPhoneNumber(phoneNumber);
        //$(".MultiModal_box").dialog("close");
        if (name.length > 0 && userName.length > 0 && checkUserName == true && checkPass == true && password.length > 0 && connectionCode.length == 7 && checkPhone) {
            //$.blockUI();
            $.ajax({
                url: "/BacSi/Register",
                type: "POST",
                data: {
                    Name: name,
                    UserName: userName,
                    Password: password,
                    PasswordVerify: passwordVerify,
                    ConnectionCode: connectionCode,
                    Address: address,
                    PhoneNumber: phoneNumber,
                    Email: email
                },
                dataType: "Json",
                success: function (data) {
                    if (data.Message == "Success") {
                        alert("Bạn đã đăng ký thành công");
                        //$(this).find(".btnCloseDialog").click(CloseDialog_Click);
                        $(".MultiModal_box").dialog("close");
                        window.location = "/KetQuaXetNghiem/BacSi";
                    }
                    else {
                        //$.unblockUI();
                        alert(data.Message);
                    }
                }
            });
        }
        else {
            if (name.length == 0) {
                alert("Xin vui lòng nhập họ tên");
                return;
            }
            if (userName.length == 0) {
                alert("Xin vui lòng chọn tên đăng nhập");
                return;
            }
            if (checkUserName == false) {
                alert("Xin vui lòng kiểm tra tên đăng nhập. Tên đăng nhập không chứa khoảng trắng và các ký tự đặc biệt như: !@#$%^&*()+=-[]\\\';,./{}|\":<>?~_");
                return;
            }
            if (checkPass == false || password.length == 0) {
                alert("Xin vui lòng kiểm tra lại mật khẩu. Lưu ý rằng dữ liệu của 2 trường mật khẩu phải giống nhau");
                return;
            }

            if (connectionCode.length != 7) {
                alert("Xin vui lòng kiểm tra lại mã liên kết. Lưu ý rằng mã liên kết có 7 ký tự");
                return;
            }

            if (checkPhone == false) {
                alert("Xin vui lòng kiểm tra lại số điện thoại. Lưu ý rằng chỉ chấp nhận số");
                return;
            }
        }
    }
</script>
<div class="MultiModal_title hide">
    Đăng Ký</div>
<div id="registerWrapper">
    <div class="ContentTop">
        <div class="LeftCol">
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.BacSiStrings.BacSi_Create_Name%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Doctor.Name, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.BacSiStrings.BacSi_Create_UserName%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Doctor.UserName, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.BacSiStrings.BacSi_Create_Password%></label>
                </div>
            
                <div class="Column">
                    <%=Html.PasswordFor(m => m.Doctor.Password, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.BacSiStrings.BacSi_Create_PasswordVerify%></label>
                </div>
            
                <div class="Column">
                    <%=Html.PasswordFor(m => m.Doctor.PasswordVerify, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>

        <div class="RightCol">
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.BacSiStrings.BacSi_Create_ConnectionCode%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Doctor.ConnectionCode, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.BacSiStrings.BacSi_Create_Address%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Doctor.Address, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.BacSiStrings.BacSi_Create_Phone%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Doctor.Phone, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.BacSiStrings.BacSi_Create_Email%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Doctor.Email, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
      </div>
</div>
<div class="ContentBottom">
    <div class="Column">
        <input type="button" onclick="Register()" id="btnRegister" value="Đăng Ký" class="button" />
    </div>
</div>