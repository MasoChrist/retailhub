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

getApiEntryPoint = function () {
    //todo
    return "../api/";
}

showErrorWindow = function (xhr, error, _type, endPoint, data) {
    //todo
    debugger;
    console.debug(xhr); console.debug(error);

}

webApiCall = function (_type, endPoint, data) {
    return new Promise(
        (resolve, reject)=>
        {
            $.ajax({
                headers: { 'Token': getToken() },
                type: _type,
                data: JSON.stringify(data),
                url: getApiEntryPoint() + endPoint,
                contentType: "application/json",
                error: function (xhr, error) {
                    debugger;
                    showErrorWindow(xhr, error, _type, endPoint, data)

                }, success: function (newData) {
                    debugger;
                    resolve(newData)
                }
                
            });

        }
    )
}