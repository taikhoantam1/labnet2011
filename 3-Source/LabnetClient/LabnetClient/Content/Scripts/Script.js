$(document).ready(function () {

    $("a.ActionLink").unbind("click").click(function (event) {
        event.preventDefault();
        var href = $(this).attr("href");
        var r = Math.random();
        if (href.indexOf("?") == -1)
            href += "?r=" + r;
        else
            href += "&r=" + r;
        $.ajax({
            url: href,
            type: "GET",
            success: function (data) {
                $("#MainContent").html(data);
            }
        });
    });
    $("input[type='submit']").unbind("click").click(function (event) {
        event.preventDefault();
        var Url = $("#MainContent form").attr("action");
        var r = Math.random();

        if (Url.indexOf("?") == -1)
            Url += "?r=" + r;
        else
            Url += "&r=" + r;
        $.ajax({
            url: Url,
            type: "POST",
            data: $("#MainContent form").serialize(),
            success: function (data) {
                $("#MainContent").html(data);
            }
        });
    });

    $("input.number").live("keydown", function (e) {
        var key = e.charCode || e.keyCode || 0;
        // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
        return (
             key == 8 ||
             key == 9 ||
             key == 46 ||
             (key >= 37 && key <= 40) ||
             (key >= 48 && key <= 57) ||
             (key >= 96 && key <= 105));
    });

});

