function CheckValidate(element) {
    kt = true;
    if ($(element).hasClass("Required"))
        kt&=isEmpty(element);
    if ($(element).hasClass("Number"))
        kt &= isNumber(element);
    if ($(element).hasClass("DateTime"))
        kt &= isDateTime(element);
    if ($(element).hasClass("Email"))
        kt &= isEmail(element);
    return kt;
}
function ClearAllValidate() {
    $(".lbValidation").remove();
    $(".lbError").remove();
}
function isEmpty(element) {

    if (element.value != "") {

        GenlbError("", element);
        return true;
    }
    //Error = FindLbError(element);
    Message = "Required";
    GenlbError(Message, element);
    return false;
}

document.getElementsByClassName = function(cl) {
    var retnode = [];
    var myclass = new RegExp('\\b' + cl + '\\b');
    var elem = this.getElementsByTagName('*');
    for (var i = 0; i < elem.length; i++) {
        var classes = elem[i].className;
        if (myclass.test(classes)) retnode.push(elem[i]);
    }
    return retnode;
};
function isNumber(element) {
    if (/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(element.value)) {
        GenlbError("", element);
        return true;
    }
    Message = "Number Required";
    GenlbError(Message, element);
    return false;
}
function isEmail(element) {
    if (/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i.test(element.value)) {
        GenlbError("", element);
        return true;
    }
    Message = "Invalid Email";
    GenlbError(Message, element);

    return false;
}
function isDateTime(element) {

    if (/^\d{1,2}[\/-]\d{1,2}[\/-]\d{4}$/.test(element.value) && !/Invalid|NaN/.test(new Date(element.value))) {
        GenlbError("", element);
        return true;
    }
    Message = "DateTime format(MM/DD/YYYY)";
    GenlbError(Message, element);
    return false;
}

function FindLbError(element) {
    return $(".lbError").filter("[for='" + element.id + "']");
}
function FindlbVaildation(element) {
    return $(".lbValidation").filter("[for='" + element.id + "']");
}
function GenlbError(Message, element) {
    Error = FindLbError(element);
    Validate = FindlbVaildation(element);
    Error.remove();
    Validate.remove();
  
    label = $("<label for='" + element.id + "'>" + Message + "</label>");

    if (Message == "")
        label.addClass("lbValidation");
    else
        label.addClass("lbError");
        
    label.insertAfter(element);
   

}
/*
function GenlbError(Message, element) {
    Error = FindLbError(element);
    if (Error.length == 0) {
        //create lbError
        label = $("<label for='" + element.id + "'>" + Message + "</label>");
        if (Message == "")
            label.addClass("lbValidation");
        else
            label.addClass("lbError");
        label.insertAfter(element);
    }
    else {
        Error.html("<label  for='" + element.id + "'>" + Message + "</label>")
        Error.removeClass("lbError");
        Error.removeClass("lbValidation");
        if (Message == "")
            Error.addClass("lbValidation");
        else
            Error.addClass("lbError");
    }

}*/
