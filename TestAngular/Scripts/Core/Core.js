function getToken() {
    return getCookie('Token');
}

getCookie  = function(name)
{
    debugger;
    match = document.cookie.match(new RegExp(name + '=([^;]+)'));
    if (match) return match[1];
    return '';
};

setCookie = function (name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}