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
	.when("/extractCredit",{
		templateUrl:"view/extractcredit.html",
		controller:"extractcCreditController"
	});

	$routeProvider.otherwise({redirectTo:"/home"});

})