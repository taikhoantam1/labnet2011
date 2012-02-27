$(document).ready(function () {

    $('.number').number();

    $(".date").datepicker({dateFormat:'dd/mm/yy'});

    $(".readonly").readonly();
});

$(document).ajaxStart(function () {
    $.blockUI();
});

$(document).ajaxStop(function () {
    $.unblockUI();
}); 