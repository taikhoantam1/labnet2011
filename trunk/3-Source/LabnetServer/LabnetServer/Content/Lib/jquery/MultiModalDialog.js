var numberModalPopup = 0;
function OnDialogClosed() {
    $(this).dialog("destroy");
    $(this).remove();
}

function CloseDialog_Click() {
    $(this).parents(".MultiModal_box").dialog("close");
}

function OnBeforeClose() {
    var modalType = $(this).data("modal_type");
    switch (modalType) {
        case "inline": //Inline content
            var divTemp = $(this).data("modal_temp");
            var contentId = $(this).data("content_id");
            $(divTemp).after($(contentId));
            break;
        case "ajax":
            break;
    }
    numberModalPopup--;
}
$(document).ready(function () {

    function LoadAjaxContent(new_modal, href) {

        var r = Math.random();
        if (href.indexOf("?") >= 0) {
            href += "&";
        }
        else {
            href += "?";
        }
        href += "r=" + r;

        new_modal.find(".MultiModal_content").load(href, $("form.selections").serialize(), function (data) {
            var modalTitle = new_modal.find(".MultiModal_title").text();
            var yOffset = 150 + numberModalPopup * 60;
            var xOffset = $(window).width() / 2 + (numberModalPopup - 1) * 40 - (new_modal.find(".MultiModal_content").width() + 30) / 2;
            new_modal.dialog("option", "position", [xOffset, yOffset]);
            new_modal.dialog('option', 'title', modalTitle);
            new_modal.find(".MultiModal_loading").hide();
            new_modal.find(".MultiModal_content").show();
            new_modal.dialog("option", "width", new_modal.find(".MultiModal_content").width() + 25);
            //Call event click button close
            new_modal.find(".btnCloseDialog").click(CloseDialog_Click);

            // Handel paging event click
            $(this).find("#pages").find("a").unbind("click").click(function (event) {
                event.preventDefault();
                var url = this.href;
                LoadAjaxContent(new_modal, url)
            });
        });

    }
    $('.multi_modal_link').live('click', function (event) {
        if ($(this).hasClass("inactive")) {
            event.preventDefault();
            return;
        }
        var new_modal = $("<div class='MultiModal_box'><div class='MultiModal_loading'>Loading...</div><div class='MultiModal_content'></div></div>");
        $("body").prepend(new_modal);
        new_modal.find(".MultiModal_loading").show();
        new_modal.find(".MultiModal_content").hide();
        //Config modal dialog
        new_modal.dialog({
            autoOpen: false,
            closeOnEscape: false,
            modal: true,
            width: 'auto',
            resizable: false,
            closeText: 'Hide',
            beforeclose: OnBeforeClose,
            close: OnDialogClosed
        });
        new_modal.dialog("open");
        var href = $(this).attr("href");
        var modalType = "ajax";
        if (href.indexOf("#") >= 0) {
            modalType = "inline";
            href = href.substr(href.indexOf("#"));
        }
        new_modal.data("modal_type", modalType);
        switch (modalType) {
            case "inline": //Inline content
                var divTemp = $("<div></div>").attr("id", "MultiModal_Temp_" + Math.floor(Math.random() * 1000));
                $(href).after(divTemp);
                new_modal.data("modal_temp", "#" + divTemp.attr("id"));
                new_modal.data("content_id", href);

                $(href).appendTo(new_modal.find(".MultiModal_content"));
                var modalTitle = new_modal.find(".MultiModal_title").text();
                var yOffset = 150 + numberModalPopup * 60
                var xOffset = $(window).width() / 2 + (numberModalPopup - 1) * 40 - (new_modal.find(".MultiModal_content").width() + 30) / 2; ;
                new_modal.dialog("option", "position", [xOffset, yOffset]);
                new_modal.dialog('option', 'title', modalTitle);
                new_modal.dialog("option", "width", new_modal.find(".MultiModal_content").width() + 30);
                new_modal.find(".MultiModal_loading").hide();
                new_modal.find(".MultiModal_content").show();
                //Call event click button close
                new_modal.find(".btnCloseDialog").click(CloseDialog_Click);
                break;
            case "ajax": //Ajax content
                LoadAjaxContent(new_modal, href);

                break;
        }
        numberModalPopup++;

        event.preventDefault();
        return false;
    });

});