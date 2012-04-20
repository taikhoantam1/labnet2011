<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.DoctorViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<% string display = ViewData.ModelState.IsValid ? "none" : "block"; %>
<div class="errorbox" id="validationSummary" style="display: <%=display%>">
    <span class='errorimage'><span class='errorhead'>Looks like we have a small problem...</span></span>
    <%= Html.ValidationSummary() %>
</div>
<%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
  {%>
<% Html.BeginForm("Create", "BacSi");%>
<%}
  else
  { %>
<% Html.BeginForm("Edit", "BacSi"); %>
<%} %>
<%= Html.HiddenFor(m=>m.Doctor.Id) %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%if (Model.ViewMode == DomainModel.Constant.ViewMode.Create)
              {%>
            <%=Resources.DoctorStrings.DoctorInsert_Title %>
            <%}
              else
              { %>
            <%=Resources.DoctorStrings.DoctorEdit_Title%>
            <%} %>
        </h3>
    </div>
    <div class="ModuleContent">
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Name%></label>
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
                            <%=Resources.DoctorStrings.DoctorInsert_Address%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Address, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Phone%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Phone, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_IsActive%></label>
                    </div>
                    <div class="Column">
                        <%=Html.CheckBoxFor(m=>m.Doctor.IsActive) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Email%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Email, new { Class = "textInput" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_Commission%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.Commission, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_BankAccount%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.Doctor.BankAccountNumber, new { Class = "textInput number" })%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_ConnectionCode%></label>
                    </div>
                    <div class="Column">
                        <%if (string.IsNullOrEmpty(Model.Doctor.ConnectionCode))
                          { %>
                            <input type="button" class="btnCreateConnectionCode" value="Tạo Mã Liên Kết"/>
                        <%}
                          else
                          { %>
                            <label class="Red Bold Large" id="lbConnectionCode"><%= Model.Doctor.ConnectionCode %></label>
                            <input type="button" value="Tạo Mã Liên Kết" class="hide btnCreateConnectionCode" />
                        <%} %>
                        <%= Html.HiddenFor(p=>p.Doctor.ConnectionCode) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.DoctorStrings.DoctorInsert_IsConnected%></label>
                    </div>
                    <div class="Column">
                        <%= Html.CheckBoxFor(m => m.Doctor.IsConnected, new { @class="disabled"})%>
                    </div>
                    <div class="Column">
                        <input type="button" id="btnRemoveConnection" value="Hũy Kết Nối" <%= Model.Doctor.IsConnected && !string.IsNullOrEmpty(Model.Doctor.ConnectionCode)? "": "disabled='disabled'" %> />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="Row clear">
                <div class="Column">
                    <label class="lbTitle">
                        <%=Resources.DoctorStrings.DoctorInsert_Note%></label>
                </div>
                <div class="Column">
                    <%=Html.TextAreaFor(m => m.Doctor.Other, new { cols=75, rows=3})%>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div align="center">
            <input type="submit" value="<%=Resources.DoctorStrings.DoctorInsert_ButtonSave%>"
                id="save" />
            <input type="button" value="<%=Resources.DoctorStrings.DoctorInsert_Back%>"
                id="btnBack" />
        </div>
    </div>
</div>
<% Html.EndForm(); %>
<script type="text/javascript">
    $(document).ready(function () {
        $(".btnCreateConnectionCode").click(function () {
            var doctorId = $("#Doctor_Id").val();
            $.ajax({
                url: "/BacSi/CreateConnectionCode",
                data: {
                    doctorId: doctorId
                },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess == true) {
                        $(".btnCreateConnectionCode").hide();
                        $(".btnCreateConnectionCode").after($("<label class='Red Bold Large'> " + data.ResponseData + "</label>"));
                        $("#Doctor_ConnectionCode").val(data.ResponseData);
                    }
                    else {
                        alert(data.ErrorMessage);
                    }
                }
            });
        });

        $("#btnRemoveConnection").click(function () {
            var doctorId = $("#Doctor_Id").val();
            if (doctorId != 0) {
                $.ajax({
                    url: "/BacSi/RemoveConnection",
                    data: {
                        doctorId: doctorId
                    },
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        if (data.IsSuccess == true) {
                            $("#btnRemoveConnection").attr("disabled", "disabled");
                            $("#Doctor_IsConnected").attr("checked", false);
                            $("#lbConnectionCode").remove();
                            $("#Doctor_ConnectionCode").val("");
                            $(".btnCreateConnectionCode").removeClass("hide");
                        }
                    }
                });
            }
        });

        $("#btnBack").click(function () {
            window.location = "/BacSi/BackToSearch";
        });

        //$("#btnBack").click(function () {
        //    window.location = "/BacSi/Back"
        //});
    });
</script>
