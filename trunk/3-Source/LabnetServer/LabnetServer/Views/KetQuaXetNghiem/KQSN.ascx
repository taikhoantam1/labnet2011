<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetServer.Models.KQXNModel>" %>

<div class="PaddingT15"></div>
<%=Html.ValidationSummary() %>
<% Html.BeginForm(); %>
<div class="PageContent">
        <h3 class="PaddingT20 PaddingL20">
            <%=Resources.GlobalStrings.KQXN_MaXetNghiem%></h3>
        <div class="Row PaddingL20">
            <div class="Column Width200 PaddingT10">
                <%= Html.TextBoxFor(p=>p.ExaminationNumber) %>
            </div>
            <div class="Column Width150 ">
                <input type="submit" id="Text1" value="Xem kết quả" class="button" />
            </div>
        </div>
    <%if (!string.IsNullOrEmpty(Model.LabUrl))
      {%>
    <div class="Row">
        <iframe id="reportViewer" width="760" scrolling="no" height="1000" style="overflow: hidden"
            frameborder="0"></iframe>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var url = "<%=Model.LabUrl%>";
            //alert(url);
            $("#reportViewer").attr("src", url);
            $("#reportViewer").reload();
        });
    </script>
    <%}
      else
      {%>
        <div style="width:100%; height:400px;" ></div>
    <%} %>
</div>
<%Html.EndForm(); %>
</div> 
            <div class="clear"></div>