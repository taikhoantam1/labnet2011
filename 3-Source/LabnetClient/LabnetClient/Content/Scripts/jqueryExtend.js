jQuery.fn.startWith = function (str) {
        return $(this).val().indexOf(str) === 0;
    }

jQuery.fn.containt= function (str) {
        return $(this).val().indexOf(str) != -1;
    }

jQuery.fn.readonly = function () {
    $(this).keypress(function (event) {
        event.preventDefault();
    });

}


jQuery.fn.nomalizeString = function (str) {
    var patterns = [
                    { Regex: "[á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ]", Value: "a" },
                    { Regex: "[đ]", Value: "d" },
                    { Regex: "[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]", Value: "e" },
                    { Regex: "[í|ì|ỉ|ĩ|ị]", Value: "i" },
                    { Regex: "[ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ]", Value: "o" },
                    { Regex: "[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]", Value: "u" },
                    { Regex: "[ý|ỳ|ỷ|ỹ|ỵ]", Value: "y"}];

    for (i = 0; i < patterns.length; i++) {
        var pattern = patterns[i];
        var re = new RegExp(pattern.Regex, "g");
        str = str.replace(re, pattern.Value);
    }
    return str.toUpperCase();
}

jQuery.fn.number = function () {
    $(this).keypress(function (event) {
        // Allow only backspace and delete
        if (event.keyCode == 46 || event.keyCode == 8) {
            // let it happen, don't do anything
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.keyCode < 48 || event.keyCode > 57) {
                event.preventDefault();
            }
        }
    });
}

if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}