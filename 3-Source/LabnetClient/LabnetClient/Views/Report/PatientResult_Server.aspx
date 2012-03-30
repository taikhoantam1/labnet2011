<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LabnetClient.Models.ReporViewModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PatientResult_Server</title>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/redmond/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="../../Content/Style/LayoutTemplate.css" rel="stylesheet" type="text/css" />
    <%--<link rel="stylesheet" type="text/css" href="../../Content/Lib/FlexiGrid/flexigrid.css" />--%>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript">
        google.load("jqueryui", "1.8.16");
    </script>
</head>
<body>
    <h3 class="Title">
        <%= Model.ReportTitle%>
    </h3>
    <%if (Model.ReportParams.Count != 0)
      { %>
    <iframe id="reportViewer" width="760" height="1000" frameborder="0" scrolling="no"></iframe>
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
    <%}
      else
      { %>
        <h3 style="color:red">Không tìm thấy kết quả cho mã xét nghiệm .</h3>
    <%} %>
</body>
</html>
