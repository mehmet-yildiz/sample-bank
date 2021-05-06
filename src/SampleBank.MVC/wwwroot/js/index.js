(function () {

    if (localStorage.getItem("sampleBankToken")) {
        document.getElementById("btnLogin").innerText = "Token found in Local Storage";
        document.getElementById("btnLogin").setAttribute('disabled', 'disabled');
    } 


    document.getElementById('login-button').addEventListener('click', function (e) {
        this.innerText = "Please wait...";
        this.setAttribute('disabled', 'disabled');
        const thisBtn = this;
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;

        if (username == "" || password == "") {
            this.innerText = "Login";
            this.removeAttribute('disabled');
            alert("Fill username and password");
            return;
        }
        const model = { username: username, password: password };

        Common.Ajax("post", "auth/login", JSON.stringify(model), function (apiResponse) {
            thisBtn.innerText = "Login";
            thisBtn.removeAttribute('disabled');
            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            if (apiResponse.data?.token != null) {
                localStorage.setItem("sampleBankToken", apiResponse.data.token);
                Common.CloseModal("loginModal");
                document.getElementById("username").value = "";
                document.getElementById("password").value = "";
            }
        });
    });

    document.getElementById('btnOpenAccount').addEventListener('click', function (e) {
        this.innerText = "Please wait...";
        this.setAttribute('disabled', 'disabled');
        const thisBtn = this;
        const customerId = document.getElementById("customerId").value;
        const initialCredit = document.getElementById("initialCredit").value;

        if (customerId == "" || initialCredit == "") {
            this.innerText = "Open Account";
            this.removeAttribute('disabled');
            alert("Fill Customer Id and Initial Credit");
            return;
        }
        const model = { customerId: customerId, initialCredit: initialCredit };

        Common.Ajax("post", "account/open", JSON.stringify(model), function (apiResponse) {
            thisBtn.innerText = "Open Account";
            thisBtn.removeAttribute('disabled');
            document.getElementById("customerId").value = "";
            document.getElementById("initialCredit").value = "";
            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            if (apiResponse.data === true) {
                document.getElementById("result-section").innerText = "";
                alert("Account opened. Check Account & Transaction List");
                Common.CloseModal("openAccountModal");
            }
        });
    });


    document.getElementById('btnShowCustomers').addEventListener('click', function (e) {
        this.innerText = "Please wait...";
        this.setAttribute('disabled', 'disabled');
        const thisBtn = this;

        Common.Ajax("get", "customer/list", null, function (apiResponse) {
            thisBtn.innerText = "Show Customers";
            thisBtn.removeAttribute('disabled');
            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            document.getElementById("result-section").innerText = "Total Customer Count: " + apiResponse.data.length + " " + JSON.stringify(apiResponse.data, null, "\t");
        });
    });


    document.getElementById('btnShowAccounts').addEventListener('click', function (e) {
        this.innerText = "Please wait...";
        this.setAttribute('disabled', 'disabled');
        const thisBtn = this;

        Common.Ajax("get", "account/list", null, function (apiResponse) {
            thisBtn.innerText = "Show Accounts";
            thisBtn.removeAttribute('disabled');
            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            document.getElementById("result-section").innerText = "Total Account Count: " + apiResponse.data.length+ " " + JSON.stringify(apiResponse.data, null, "\t");
        });
    });


    document.getElementById('btnShowTransactions').addEventListener('click', function (e) {
        this.innerText = "Please wait...";
        this.setAttribute('disabled', 'disabled');
        const thisBtn = this;

        Common.Ajax("get", "transaction/list", null, function (apiResponse) {
            thisBtn.innerText = "Show Transactions";
            thisBtn.removeAttribute('disabled');
            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            document.getElementById("result-section").innerText = "Total Transaction Count: " + apiResponse.data.length + " " + JSON.stringify(apiResponse.data, null, "\t");
        });
    });
})();



