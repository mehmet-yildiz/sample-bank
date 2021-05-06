var Common = {
    BaseApiUrl: "https://localhost:44361/",
    CloseModal:function(modalId) {
        var loginModalEl = document.getElementById(modalId);
        var modal = bootstrap.Modal.getInstance(loginModalEl);
        modal.hide();
    },
    ShowLoader:function() {},
    HideLoader: function () {},
    Token: function () {
        return localStorage.getItem("sampleBankToken");
    },
    Ajax: function (httpMethod, url, data, successCallback, canShowLoader = true) {
        if (canShowLoader) {
            Common.ShowLoader();
        }

        var request = new XMLHttpRequest();
        request.open(httpMethod.toUpperCase(), Common.BaseApiUrl + url, true);

        request.setRequestHeader("Accept", "application/json");
        request.setRequestHeader("Content-type", "application/json");
        if (Common.Token()) {
            request.setRequestHeader("Authorization", "Bearer " + Common.Token());
        }
        request.onload = function () {
            Common.HideLoader();
            if (this.status >= 200 && this.status < 400) {
                var data = JSON.parse(this.response);
                if (successCallback) {
                    successCallback(data);
                }
            }
            else if (this.status == 401) {
                alert("try to login first! Please refresh page before login");
            }
        };
        request.onerror = function (err) {
            alert("An error occured!");
        };
        request.send(data);
    },
    AjaxErrorCallback: function (error, type, httpStatus) {

    },
};
