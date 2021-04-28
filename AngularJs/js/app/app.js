angular.module("app",['ngRoute'])

.config(function($routeProvider){
	$routeProvider
	.when("/home",{
		templateUrl:"view/home.html",
		controller:"indexController"
	})
	.when("/extractDebt/:customerId",{
		templateUrl:"view/extractdebt.html",
		controller:"extractDebtController"
	})
	.when("/extractCredit/:customerId",{
		templateUrl:"view/extractcredit.html",
		controller:"extractCreditController"
	});

	$routeProvider.otherwise({redirectTo:"/home"});

})