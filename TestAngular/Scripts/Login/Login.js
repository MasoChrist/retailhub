
loginController =
{
    LoginRequest: function(username, password) {
        webApiCall("POST", "DoLogin", {
            UserName: username,
            Password: password
        })
            .then(function (newData) {
                debugger;
                setCookie("Token", newData.Token, 2);
                window.location.reload(false);

         })
           
    },

    LogOutRequest: function() {
        var token = getToken();
        webApiCall("POST", "DoLogout", token).then
            (function () { window.location.reload(false) }
            );
    }
};
window.LoginController = loginController;