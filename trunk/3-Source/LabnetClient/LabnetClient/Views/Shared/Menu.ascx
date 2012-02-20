<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {

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
    });
</script>
<h3 class="MenuTitle">
    <%= Resources.GlobalStrings.Menu_MenuTitle %></h3>
<div id="Main_Menu">
    <dl>
        <dt><a class="iconPatient" href="#">Quản lý Bệnh Nhân</a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="/BenhNhan/Create">
                    <%= Resources.GlobalStrings.Menu_PatientInfo %></a></li>
                <li><a class="MenuItem ActionLink" href="/BenhNhan/PatientTestResult">
                    <%= Resources.GlobalStrings.Menu_UpdateTestResult%></a></li>
                <li><a class="MenuItem ActionLink" href="/BenhNhan">
                    <%= Resources.GlobalStrings.Menu_SearchPatients %></a></li>
            </ul>
        </dd>
        <dt><a class="iconPXN" href="#">Quản lý PXN</a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="/XetNghiem/Create/">
                    <%= Resources.GlobalStrings.Menu_CreateTest %></a></li>
                <li><a class="MenuItem ActionLink" href="/XetNghiem/Search/">
                    <%= Resources.GlobalStrings.Menu_SearchTest %></a></li>
                <li><a class="MenuItem ActionLink" href="/Panel/Create/">
                    <%= Resources.GlobalStrings.Menu_CreateTestPanel%></a></li>
                <li><a class="MenuItem ActionLink" href="/Panel">
                    <%= Resources.GlobalStrings.Menu_SearchTestPanel %></a></li>
            </ul>
        </dd>
        <dt><a class="iconLab" href="#">Quản lý Lab Gửi Mẫu</a></dt>
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
        <dt><a class="iconReport" href="#">Thống Kê - Báo Cáo</a></dt>
        <dd style="display: none">
            <ul>
                <li><a class="MenuItem ActionLink" href="#">
                    <%= Resources.GlobalStrings.Menu_SearchDoctor %></a></li>
                <li><a class="MenuItem ActionLink" href="#">
                    <%= Resources.GlobalStrings.Menu_SearchDoctor %></a></li>
                <li><a class="MenuItem ActionLink" href="#">
                    <%= Resources.GlobalStrings.Menu_SearchDoctor %></a></li>
                <li><a class="MenuItem ActionLink" href="#">
                    <%= Resources.GlobalStrings.Menu_SearchDoctor %></a></li>
            </ul>
        </dd>
    </dl>
</div>
