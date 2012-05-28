<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetServer.Models.LabModel>" %>

<script src="/Content/Lib/Utility/md5.js" type="text/javascript"></script>
<style type="text/css">
    
    #informationWrapper
    {
        width: 670px;
    }
</style>
<script language="javascript" type="text/javascript">
    function CheckUserName(userName) {
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

    function AccountInformation() {
        var name = $("#Lab_Name").val();
        var address = $("#Lab_Address").val();
        var phoneNumber = $("#Lab_Phone").val();
        var checkPhone = CheckPhoneNumber(phoneNumber);
        //$(".MultiModal_box").dialog("close");
        if (name.length > 0 && checkPhone) {
            //$.blockUI();
            $.ajax({
                url: "/Lab/AccountInformation",
                type: "POST",
                data: {
                    Name: name,
                    Address: address,
                    PhoneNumber: phoneNumber
                },
                dataType: "Json",
                success: function (data) {
                    if (data.Message == "Success") {
                        alert("Bạn đã cập nhật thành công");
                        //$(this).find(".btnCloseDialog").click(CloseDialog_Click);
                        $(".MultiModal_box").dialog("close");
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

            if (checkPhone == false) {
                alert("Xin vui lòng kiểm tra lại số điện thoại. Lưu ý rằng chỉ chấp nhận số");
                return;
            }
        }
    }
</script>
<div class="MultiModal_title hide">
    Thông Tin Tài Khoản</div>
<div id="informationWrapper">
    <div class="ContentTop">
        <div class="LeftCol" style="width:48%">
            <div class="Row">
                <div class="Column">
                     <label class="lbTitle">
                        <%=Resources.BacSiStrings.BacSi_Create_Name%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Lab.Name, new  {Class="textInput" })%>
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
                    <%=Html.TextBoxFor(m => m.Lab.Address, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>   
        </div>

        <div class="RightCol" style="width:48%">
            <div class="Row">
                <div class="Column">
                    <label class="lbTitle">
                    <%=Resources.BacSiStrings.BacSi_Create_Phone%></label>
                </div>
            
                <div class="Column">
                    <%=Html.TextBoxFor(m => m.Lab.Phone, new  {Class="textInput" })%>
                </div>
                <div class="clear">
                </div>
            </div>
      </div>
</div>
<div class="ContentBottom">
    <div class="Row" align="center">
        <input type="button" onclick="AccountInformation()" id="btnView" value="Cập Nhật" class="button" />
        <input id="btnCancel" type="button"  value="Hủy" class="button btnCloseDialog" width="67px" />
    </div>
</div>