$(document).ready(function () {
    $('.autoNumeric').autoNumeric({ aSep: ',', aDec: '.', vMin: '0.00', aPad: false, wEmpty: 'empty' });

    $('.number').number();

    $(".date").datepicker({dateFormat:'dd/mm/yy'});

    $(".readonly").readonly();
});
