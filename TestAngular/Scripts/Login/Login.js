
loginController =
{
    LoginRequest: function(username, password) {
       
        $.ajax(
            {
                beforesend: function (xhrObj) {
                    debugger;
                    var token = getToken();
                    xhrObj.setRequestHeader("Token", token);
                },
                type: "POST",
                data: JSON.stringify({
                    UserName: username,
                    Password: password
                }),
                url: "../api/DoLogin",
                contentType: "application/json",
                error: function(xhr, error) {
                    debugger;
                    console.debug(xhr);
                    console.debug(error);

                },
                success: function(newData) {
                    debugger;
                    setCookie("Token", newData.Token, 2);
                    window.location.reload(false);

                }
            }
        );
    },

    LogOutRequest: function() {
        var token = getToken();
        $.ajax(
            {
                beforesend: function(xhrObj) {
                    xhrObj.setRequestHeader("Token", token);
                },
                type: "POST",
                data: JSON.stringify(token),
                url: "../api/DoLogout",
                contentType: "application/json",
                error: function(xhr, error) {
                    debugger;
                    console.debug(xhr);
                    console.debug(error);

                },
                success: window.location.reload(false)
            }
        );
    }
};
window.LoginController = loginController;