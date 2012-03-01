function import_js(jsFile) {
    var scriptElt = document.createElement('script');
    scriptElt.type = 'text/javascript';
    scriptElt.src = jsFile;
    document.getElementsByTagName('head')[0].appendChild(scriptElt);
    
}
function setCookie(c_name, value, expiredays) 
{
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toUTCString());
}
function getCookie(c_name) 
{
    if (document.cookie.length > 0) 
    {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}
function cleanCookie(c_name) {
    var value = getCookie(c_name);
    var exdate = new Date();
    document.cookie = c_name + "=" + value + ";expires=" + exdate.toGMTString() + ";" + ";";

}


//Default Receive Method
function Default_Callback(src) {
    if (src.ReadyState == 4) {
        if (src.Status == 200) {
            alert(src.ResponseText);
        }
    }
}
//Call WebMethod function
function SentRequest(PageAddress,CallMethod, Params, ParmsName, NumParams, ReceiveMethod) 
{
    //Use ajax class
    //var htmlCode=GetPageDesign();
    if (ReceiveMethod == "")
        ReceiveMethod = Default_Callback;
    var a = new System.Net.Ajax.Request("POST", PageAddress, ReceiveMethod, true);
    a.AddParam("Method", CallMethod);
    for (i = 0; i < NumParams; i++) {
        a.AddParam(ParmsName[i], Params[i]);
    }

    var b = new System.Net.Ajax.PageRequests();
    b.AddRequest(a);
    var c = new System.Net.Ajax.Connection();
    c.PageRequests = b;
    c.Open();
}
function ClearText() {
    $(':input').each(function() {
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    });
}

function DisableScreen() {
    var screen = $("<div class='DisableScreen'></div>");
    screen.css({ opacity: 0.7, 'width': $(document).width(), 'height': $(document).height() });
    $('body').append(screen);
    $('body').css({ 'overflow': 'hidden' });
  
}