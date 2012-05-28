<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetServer.Models.ObjectModels>" %>

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
  	    var type = $("#Object_ConnectionType").val();
        var name = $("#Object_Name").val();
        var userName = $("#Object_UserName").val();
        var password = $("#Object_Password").val();
        var passwordVerify = $("#Object_PasswordVerify").val();
        var connectionCode = $("#Object_ConnectionCode").val();
        var address = $("#Object_Address").val();
        var phoneNumber = $("#Object_Phone").val();
        var email = $("#Object_Email").val();
        var checkUserName = CheckUserName(userName);
        var checkPass = CheckPassword(password, passwordVerify);
        var checkPhone = CheckPhoneNumber(phoneNumber);
        //$(".MultiModal_box").dialog("close");
        if (name.length > 0 && userName.length > 0 && checkUserName == true && checkPass == true && password.length > 0 && connectionCode.length == 7 && checkPhone) {
            //$.blockUI();
            $.ajax({
                url: "/Home/Register",
                type: "POST",
                data: {
                    Type: type,
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
        <div class="LeftCol" style="width:48%">
            <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.HomeStrings.Home_Create_ConnectionType%></label>
                    </div>
                    <div class="Column">
                        <select name="Object.ConnectionType" id="Object_ConnectionType">
                            <option value="0">Bác Sĩ </option>
                            <option value="1">Phòng Lab Liên Kết </option>
                        </select>
                    </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.HomeStrings.Home_Create_Name%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Object.Name, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.HomeStrings.Home_Create_UserName%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Object.UserName, new { Class = "textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.HomeStrings.Home_Create_Password%></label>
                </div>
            
                <div class="Column">
                    <%=Html.PasswordFor(m => m.Object.Password, new { Class = "textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.HomeStrings.Home_Create_PasswordVerify%></label>
                </div>
            
                <div class="Column">
                    <%=Html.PasswordFor(m => m.Object.PasswordVerify, new { Class = "textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>

        <div class="RightCol" style="width:48%">
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.HomeStrings.Home_Create_ConnectionCode%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Object.ConnectionCode, new { Class = "textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.HomeStrings.Home_Create_Address%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Object.Address, new { Class = "textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.HomeStrings.Home_Create_Phone%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Object.Phone, new { Class = "textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.HomeStrings.Home_Create_Email%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Object.Email, new { Class = "textInput" })%>
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