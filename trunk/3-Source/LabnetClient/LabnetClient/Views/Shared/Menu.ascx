<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        var currentUrl = window.location;
        $('dd').hide();
        $('dt a').click(function () {
            var dd_visble = $('dd:visible');
            $('dd:visible').slideUp('slow');

            $('.active').removeClass('active');
            var next_visble = $(this).parent().next();
            if (dd_visble[0] == next_visble[0])
                return;
            $(this).parent().addClass('active').next().slideDown('slow');
            return false;
        });
        $("a.ActionLink").each(function () {
            if (currentUrl.toString().indexOf(this.href) != -1) {
                $(this).parents("dd").show();
                $(this).addClass("HightLight");
            }

        });
    });
</script>

<h3 class="MenuTitle">
    <%= Resources.GlobalStrings.Menu_MenuTitle %></h3>
<div id="Main_Menu">
    <dl>
        <dt><a class="iconPatient" href="#"><%= Resources.GlobalStrings.Menu_GroupTitle_Patient%></a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="/BenhNhan/Create">
                    <%= Resources.GlobalStrings.Menu_PatientInfo %></a></li>
                <li><a class="MenuItem ActionLink" href="/BenhNhan/PatientTestResult">
                    <%= Resources.GlobalStrings.Menu_UpdateTestResult%></a></li>
                <li><a class="MenuItem ActionLink" href="/BenhNhan/Index">
                    <%= Resources.GlobalStrings.Menu_SearchPatients %></a></li>
                <li><a class="MenuItem ActionLink" href="/Report/PatientResultReport">
                    <%= Resources.GlobalStrings.Menu_PatientResult%></a></li>
                <li><a class="MenuItem ActionLink" href="/InstrumentResult/Details">
                    <%= Resources.GlobalStrings.Menu_InstrumentResult%></a></li>
            </ul>
        </dd>
        <dt><a class="iconPXN" href="#"><%= Resources.GlobalStrings.Menu_GroupTitle_PXN%></a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="/XetNghiem/Create/">
                    <%= Resources.GlobalStrings.Menu_CreateTest %></a></li>
                <li><a class="MenuItem ActionLink" href="/XetNghiem/Search/">
                    <%= Resources.GlobalStrings.Menu_SearchTest %></a></li>
                <li><a class="MenuItem ActionLink" href="/Panel/Create/">
                    <%= Resources.GlobalStrings.Menu_CreateTestPanel%></a></li>
                <li><a class="MenuItem ActionLink" href="/Panel/Index">
                    <%= Resources.GlobalStrings.Menu_SearchTestPanel %></a></li>
                <li><a class="MenuItem ActionLink" href="/NhomXetNghiem/Create">
                    <%= Resources.GlobalStrings.Menu_CreateTestSection %></a></li>
                <li><a class="MenuItem ActionLink" href="/NhomXetNghiem/Index">
                    <%= Resources.GlobalStrings.Menu_TestSectionList %></a></li>
            </ul>
        </dd>
        <dt><a class="iconLab" href="#"> <%= Resources.GlobalStrings.Menu_GroupTitle_Lab%></a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="/BacSi/Create/">
                    <%= Resources.GlobalStrings.Menu_CreateDoctor %></a></li>
                <li><a class="MenuItem ActionLink" href="/DoiTac/Create/">
                    <%= Resources.GlobalStrings.Menu_CreateLabPartner %></a></li>
                <li><a class="MenuItem ActionLink" href="/DoiTac/Search/">
                    <%= Resources.GlobalStrings.Menu_SearchPartnerCost %></a></li>
                <li><a class="MenuItem ActionLink" href="/BacSi/Search/">
                    <%= Resources.GlobalStrings.Menu_SearchDoctor %></a></li>
            </ul>
        </dd>
        <dt><a class="iconReport" href="#">  <%= Resources.GlobalStrings.Menu_GroupTitle_Report %></a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="/Report/ResultNoteBook">
                    <%= Resources.GlobalStrings.Menu_ResultBook%></a></li>
                <li><a class="MenuItem ActionLink" href="/Report/QuanLyTaiChinhReport">
                    <%= Resources.GlobalStrings.Menu_FinancialBook %></a></li>
                <li><a class="MenuItem ActionLink" href="#">
                    <%= Resources.GlobalStrings.Menu_MaterialBook%></a></li>
                <li><a class="MenuItem ActionLink" href="#">
                    <%= Resources.GlobalStrings.Menu_Management%></a></li>
            </ul>
        </dd>
    </dl>
</div>
