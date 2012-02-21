<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.ReporViewModel>" %>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="Module">
        <div class="ModuleTitle">
            <h3 class="Title">
                 <%= Model.ReportName%>
            </h3>
        </div>
        <div class="ModuleContent">
            <iframe id="reportViewer" width="760" height="1000" frameborder="0"  >

            </iframe>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            
            <% string queryString = "";
                foreach (var param in Model.ReportParams)
               {
                   queryString += string.Format("&{0}={1}", param.Key, param.Value);
                }
                Response.Write(string.Format("var params='{0}';", queryString));
            %>
            var reportName = '<%= Model.ReportName%>';
            var url="/Report/ReportViewer.aspx?r="+Math.random()+"&ReportName="+reportName+params;
            $("#reportViewer").attr("src", url);
            $("#reportViewer").reload();
        });
    </script>
</asp:Content>
