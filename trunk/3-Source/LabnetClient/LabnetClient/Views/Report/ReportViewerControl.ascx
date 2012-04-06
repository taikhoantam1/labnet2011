<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LabnetClient.Models.ReporViewModel>" %>


    <div class="Module">
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
            //$("#reportViewer").reload();
        });
    </script>