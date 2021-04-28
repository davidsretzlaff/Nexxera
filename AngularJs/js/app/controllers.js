angular
  .module("app")
  .controller("indexController", function ($scope, $http) {
    $scope.title = "Home";

    $http.get("http://localhost:5000/api/Customer/").then(function (response) {
      $scope.customers = response.data;
    });

    $scope.openMoldaCustomer = function () {
      $("#modalCustomer").modal("show");
    };
  })

  .controller("postserviceCtrl", function ($scope, $http, $routeParams) {

    // function to submit the form after all validation has occurred
    $scope.submitForm = function (cpf, name) {
      // check to make sure the form is completely valid
      if ($scope.userForm.$valid) {
        $scope.name = null;
        $scope.cpf = null;

        var data = { name: name, cpf: cpf };

        //Call the services
        $http
          .post("http://localhost:5000/api/customer", JSON.stringify(data), {
            headers: { "Content-Type": "application/json" },
          })
          .then(
            function (response) {
              this.userForm.reset();
              $("#modalCustomer").modal("hide");
            },
            function (response) {
              // optional
              $scope.messageErro = response.data.errors;
              $scope.showErro = true;
            }
          );
      }
    };
  })

  .controller(
    "extractDebtController",
    function ($scope, $http, $routeParams, $location, $route) {
      $scope.titulo = "Extrato conta";
      $scope.customerId = $routeParams.customerId;
      $scope.debtHistorys = null;
      $scope.$evalAsync();

      $scope.submitFormPaymentDebt = function (description, value, periodId) {
        if ($scope.modelForm.$valid) {
          $scope.description = null;
          $scope.value = null;
          $scope.periodId = null;
          $scope.accountId = null;
        }
        $scope.customerIdtmp = $routeParams.customerId;

        $http
          .get("http://localhost:5000/api/account/" + $scope.customerIdtmp)
          .then(function (response) {
            $scope.accounttmp = response.data;
            var data = {
              description: description,
              value: value,
              periodId: periodId,
              accountId: $scope.accounttmp.id,
            };
            $http
              .post(
                "http://localhost:5000/api/DebtHistory",
                JSON.stringify(data),
                { headers: { "Content-Type": "application/json" } }
              )
              .then(
                function (response) {
                  this.modelForm.reset();
                  $("#modalNewPaymentDebt").modal("hide");

                  scope.safeApply(function () {
                    $http
                      .get(
                        "http://localhost:5000/api/account/" + $scope.customerId
                      )
                      .then(function (response) {
                        $scope.account = response.data;
                        $http
                          .get(
                            "http://localhost:5000/api/DebtHistory/" +
                              response.data.id
                          )
                          .then(function (response) {
                            $scope.debtHistorys = [];
                            $scope.debtHistorys = response.data;
                          });
                      });
                  });
                },
                function (response) {
                  // optional
                  $scope.messageErro = response.data;
                  $scope.showErro = true;
                }
              );
          });
      };

      $scope.$evalAsync(function () {
        $http
          .get("http://localhost:5000/api/account/" + $scope.customerId)
          .then(function (response) {
            $scope.account = response.data;
            $http
              .get("http://localhost:5000/api/DebtHistory/" + response.data.id)
              .then(function (response) {
                $scope.debtHistorys = [];
                $scope.debtHistorys = response.data;
              });
            $http
              .get("http://localhost:5000/api/period/")
              .then(function (response) {
                $scope.data = {
                  model: null,
                  availableOptions: response.data,
                };
              });
          });
      });
      $scope.openMoldaPaymentDebt = function () {
        $("#modalNewPaymentDebt").modal("show");
      };
    }
  )
  
  .controller(
    "extractCreditController",
    function ($scope, $http, $routeParams) {
      $scope.titulo = "Extrato Cartão de crédito";
      $scope.customerId = $routeParams.customerId;

      $http
        .get("http://localhost:5000/api/customer/" + $scope.customerId)
        .then(function (response) {
          $scope.customer = response.data;
        });

      $http.get("http://localhost:5000/api/period/").then(function (response) {
        $scope.data = {
          model: null,
          availableOptions: response.data,
        };
	  });
	  
      $scope.openMoldaPaymentCredit = function () {
        $("#modalNewPaymentCredit").modal("show");
      };

      $http
        .get("http://localhost:5000/api/account/" + $scope.customerId)
        .then(function (response) {
          $http
            .get("http://localhost:5000/api/creditCard/" + response.data.id)
            .then(function (response) {
              console.log(response.data.id);
              $scope.creditCard = response.data;
            });
        });
	
	
      $scope.submitFormPaymentCredit = function (description, value, periodId) {
        if ($scope.modelForm.$valid) {
          $scope.description = null;
          $scope.value = null;
          $scope.periodId = null;
          $scope.cardCreditId = null;
        }
        $scope.customerIdtmp = $routeParams.customerId;

        $http
          .get("http://localhost:5000/api/account/" + $scope.customerIdtmp)
          .then(function (response) {
            $scope.accounttmp = response.data;
            $http
              .get("http://localhost:5000/api/account/" + $scope.customerId)
              .then(function (response) {
                $http
                  .get(
                    "http://localhost:5000/api/creditcard/" + response.data.id
                  )
                  .then(function (response) {
                    var data = {
                      description: description,
                      value: value,
                      periodId: periodId,
                      creditCardId: response.data.id,
                    };
                    console.log(data);
                    $http
                      .post(
                        "http://localhost:5000/api/CreditCardHistory/BuyWithCreditCard",
                        JSON.stringify(data),
                        { headers: { "Content-Type": "application/json" } }
                      )
                      .then(
                        function (response) {
                          this.modelForm.reset();
                          $("#modalNewPaymentCredit").modal("hide");
                        },
                        function (response) {
                          // optional
                          $scope.messageErro = response.data;
                          $scope.showErro = true;
                        }
                      );
                  });
              });
          });
      };
    }
  );
