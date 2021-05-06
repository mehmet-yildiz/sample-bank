function showLoader() {

}

function hideLoader() {

}


function closeModal(modalId) {
    var loginModalEl = document.getElementById(modalId);
    var modal = bootstrap.Modal.getInstance(loginModalEl);
    modal.hide();
}


var Common = {
    BaseApiUrl: "https://localhost:44361/",
    Token: function () {
        return localStorage.getItem("sampleBankToken");
    },
    Ajax: function (httpMethod, url, data, successCallback, canShowLoader = true) {
        if (canShowLoader) {
            showLoader();
        }

        var request = new XMLHttpRequest();
        request.open(httpMethod.toUpperCase(), Common.BaseApiUrl + url, true);

        request.setRequestHeader("Accept", "application/json");
        request.setRequestHeader("Content-type", "application/json");
        if (Common.Token()) {
            request.setRequestHeader("Authorization", "Bearer " + Common.Token());
        }

        request.onload = function () {
            hideLoader();
            if (this.status >= 200 && this.status < 400) {
                // Success!
                var data = JSON.parse(this.response);
                if (successCallback) {
                    successCallback(data);
                }
            } else {
               
            }
        };

        request.onerror = function (err) {
            // There was a connection error of some sort
            debugger;
        };

        request.send(data);

    },
    AjaxErrorCallback: function (error, type, httpStatus) {

    },
};
