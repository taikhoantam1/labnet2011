<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.InstrumentResultViewModel>" %>
<script src="/Content/Scripts/Script.js" type="text/javascript"></script>
<%= Html.ValidationSummary() %>
<div class="Module">
    <div class="ModuleTitle">
        <h3 class="Title">
            <%=Resources.InstrumentResultStrings.InstrumentResult_Title%>
        </h3>
    </div>
    <div class="ModuleContent">
        <% Html.BeginForm();%>
        <div class="ContentTop">
            <div class="LeftCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.InstrumentResultStrings.InstrumentResult_ReceivedDate%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBox("InstrumentResult.ReceivedDate", Model.ReceivedDate.Value.ToString("d"), new { ID = "InstrumentResult_ReceivedDate", Class = "textInput100 date", Style = "width:220px"})%>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="Row clear">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.InstrumentResultStrings.InstrumentResult_OrderNumber%></label>
                    </div>
                    <div class="Column">
                        <%=Html.TextBoxFor(m => m.OrderNumber, new { Class = "textInput220" })%>
                    </div>
                    <div class="Column">
                        <input type="button" id="btnSearchFilter" value="<%=Resources.InstrumentResultStrings.InstrumentResult_SearchButton%>" />
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="RightCol">
                <div class="Row">
                    <div class="Column">
                        <label class="lbTitle">
                            <%=Resources.InstrumentResultStrings.InstrumentResult_InstrumentName%></label>
                    </div>
                    <div class="Column">
                        <%= Html.DropDownListFor(m => m.InstrumentId, Model.SelectListInstrument, new { Class = "textInput220" }) %>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <% Html.EndForm(); %>
        <div class="ContentBottom">
            <div class="clear">
            </div>
        </div>
        <div class="Row ResultTable" style="height: auto">
            <% Html.RenderPartial("DataTable", Model.JQGrid); %>
        </div>
    </div>
    <div class="Row" align="center">
        <input type="button" value="<%=Resources.InstrumentResultStrings.InstrumentResult_View%>"
            id="btnView" />
        <input type="button" value="<%=Resources.InstrumentResultStrings.InstrumentResult_Update%>"
            id="btnUpdate" />
        <input type="button" value="<%=Resources.InstrumentResultStrings.InstrumentResult_ChangeSID%>"
            id="btnChangeID" class="multi_modal_link" href="#ChangeSIDForm" />
    </div>
    <div class="hide">
        <div id="ChangeSIDForm">
            <div class="MultiModal_title">
                Đổi SID
            </div>
            <div class="content" style="width:400px">
                <label>
                    Bạn muốn đổi SID </label><b id="oldSID" style="color: Red"></b> thành :
                &nbsp;<input type="text" id="txtNewSID" />
                <input type="button" value="Cập Nhật SID" id="btnUpdateSID"/>
                <input type="button" class="btnCloseDialog" value="Hũy Bỏ" />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function OnModalCreate() {
        $("#oldSID").html($("#OrderNumber").val())
    }
    $(document).ready(function () {
        $("#btnUpdateSID").click(function () {
            var newSID = $("#txtNewSID").val();
            var oldSID =$("#OrderNumber").val();
            var receivedDate = $("#InstrumentResult_ReceivedDate").val();
            var instrumentId = $("#InstrumentId").val();
            if (newSID == "") {
                alert("Vui lòng nhập SID mới");
            }
            else {
                $.ajax({
                    url: "/InstrumentResult/ChangeSID",
                    data:{
                        ReceivedDate:receivedDate,
                        OldOrderNumber:oldSID,
                        NewOrderNumber:newSID,
                        InstrumentId: instrumentId,
                    },
                    type:"POST",
                    success:function(data){
                        if(data.indexOf("Success")!=-1)
                        {
                            $("#OrderNumber").val(newSID);
                            $("#btnSearchFilter").click();
                            $(".btnCloseDialog").click();
                             $("#txtNewSID").val("");
                            //window.location.reload();
                        }
                        else
                        {
                            alert(data);
                        }
                    }
                });
            }
        });

        $("#btnChangeID").click(function () {
            var date = $("#InstrumentResult_ReceivedDate").val();
            var instrumentId = $("#InstrumentId").val();
            var orderNumber = $("#OrderNumber").val();

            if (orderNumber == null || orderNumber.trim() == "") {
                alert("Xin nhập SID trước khi đồi");
                $("#btnChangeID").addClass("inactive");
            }
            else {
                $("#btnChangeID").removeClass("inactive");
            }
        });

        $("#btnSearchFilter").click(function () {
            var date = $("#InstrumentResult_ReceivedDate").val();
            var instrumentId = $("#InstrumentId").val();
            var orderNumber = $("#OrderNumber").val();
            
            if(date.length != 0){
                
                $.ajax({
                url: "/InstrumentResult/SearchBySID",
                data: {
                        ReceivedDate: date,
                        OrderNumber: orderNumber,
                        InstrumentId: instrumentId
                },
                type: "POST",
                success: function (data) {
                    if (data.trim().toLowerCase().indexOf("false") != -1) {
                        alert("Không tìm thấy dữ liệu với thông tin nhập vào");
                    }
                    else{
                        $.ajax({
								url: "/InstrumentResult/SearchInstrumentResult",
								type: "POST",
								data: {
									ReceivedDate: date,
									OrderNumber: orderNumber,
									InstrumentId: instrumentId
								},
								success: function (data) {
									$(".ResultTable").html(data);
								}
							});
						}
					}
				})
                
            }
            else{
                alert("Vui lòng chọn Ngày Xét Nghiệm");
            }
        });

        $("#btnUpdate").click(function () {
            var date = $("#InstrumentResult_ReceivedDate").val();
            var instrumentId = $("#InstrumentId").val();
            var orderNumber = $("#OrderNumber").val();
            $.ajax({
                url: "/InstrumentResult/UpdateToResult",
                type: "POST",
                data: {
                    ReceivedDate: date,
                    OrderNumber: orderNumber,
                    InstrumentId: instrumentId
                },
                success: function (data) {
                    $(".ResultTable").html(data);
                }
            });
        });
        
        $("#btnView").click(function () {
            var date = $("#InstrumentResult_ReceivedDate").val();
            var instrumentId = $("#InstrumentId").val();
            var orderNumber = $("#OrderNumber").val();
            
            if(date.length != 0){
                
                $.ajax({
                    url: "/InstrumentResult/SearchInstrumentResult",
                    type: "POST",
                    data: {
                        ReceivedDate: date,
                        OrderNumber: null,
                        InstrumentId: null
                    },
                    success: function (data) {
                        $(".ResultTable").html(data);
                    }
                });
            }
            else{
                alert("Vui lòng chọn Ngày Xét Nghiệm");
            }
        });
    });
</script>
