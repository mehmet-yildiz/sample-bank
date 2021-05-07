(function () {

    //check local storage has token. If it has it, then login button is disabled. --If it hasn't it, all buttons have needsToken class are disabled to prevent tokenfree request.
    if (localStorage.getItem("sampleBankToken")) {
        document.getElementById("btnLogin").innerText = "Token found in Local Storage";
        document.getElementById("btnLogin").setAttribute('disabled', 'disabled');
    }

    //first checks the generated data. if yes, then makes generate button disable
    Common.Ajax("get", "auth/check", null, function (apiResponse) {

        if (apiResponse.hasError === true) {
            alert("An error occured. " + apiResponse.errorMessage);
            return;
        }

        if (apiResponse.data === true) {
            document.querySelectorAll(".btn").forEach((function (el) { el.removeAttribute('disabled'); }));
            document.getElementById("btnGenerateData").setAttribute('disabled', 'disabled');
        }
        else {
            document.getElementById("btnGenerateData").removeAttribute('disabled');
        }
    });

    //generate data for EF (in-memory)
    document.getElementById("btnGenerateData").addEventListener('click', function (e) {
        const thisBtn = this;

        Common.Ajax("post", "auth/generateData", null, function (apiResponse) {

            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            if (apiResponse.data === true) {
                thisBtn.innerText = "Generate Data";
                thisBtn.setAttribute('disabled', 'disabled');
            }
            else {
                alert("Reload page and try again!");
            }
        });
    });

    //login operations
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

            //sets token to local storage and resets elements
            if (apiResponse.data?.token != null) {
                localStorage.setItem("sampleBankToken", apiResponse.data.token);
                Common.CloseModal("loginModal");
                document.getElementById("username").value = "";
                document.getElementById("password").value = "";
            }
        });
    });

    //opens account. if initialCredit is higher than zero then a transaction is created.
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

    document.getElementById('btnGetCustomerInfo').addEventListener('click', function (e) {
        const thisBtn = this;

        const id = Math.floor(Math.random() * 2) + 1 ; //random customer Id between 1 and 2 

        Common.Ajax("get", "customer/info?id="+ id, null, function (apiResponse) {
            thisBtn.innerText = "Get Customer Info (Random)";
            thisBtn.removeAttribute('disabled');
            if (apiResponse.hasError === true) {
                alert("An error occured. " + apiResponse.errorMessage);
                return;
            }

            //sets token to local storage and resets elements
            if (apiResponse.data != null) {
                document.getElementById("result-section").innerText = "Customer Info: " + JSON.stringify(apiResponse.data, null, "\t");
            }
        });
    });

    //shows all customers in json 
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


    //shows all accounts in json 
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


    //shows all transactions in json 
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