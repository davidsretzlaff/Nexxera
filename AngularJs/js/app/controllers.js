angular.module("app")
.controller("indexController", function ($scope, $http) {
	$scope.title = "Home";
	
	$http.get('http://localhost:5000/api/Customer/')
	.then(function(response){
		$scope.customers = response.data;
	});
	
	$scope.openMoldaCustomer = function(){		
		$('#modalCustomer').modal('show');
	};

	
})

.controller('postserviceCtrl', function ($scope, $http) {
	// function to submit the form after all validation has occurred			
		 $scope.submitForm = function(cpf,name) {
		  
			 // check to make sure the form is completely valid
			 if ($scope.userForm.$valid) {
			 $scope.name = null;    
			 $scope.cpf = null;
			 
			 var data = {name: name,cpf: cpf}            
			 
			 //Call the services
						   
			   $http.post('http://localhost:5000/api/customer', JSON.stringify(data),{headers: {'Content-Type': 'application/json'}})
			   .then(function(response) {
					 console.log(response);
					 this.userForm.reset();
					 $('#modalCustomer').modal('hide');
				  }, 
				  function(response) { // optional
					 $scope.messageErro = response.data.errors;
					 $scope.showErro = true;
 
					 console.log(response.data.errors);
				  });                         
			};    
	}        
 })
 
.controller("extractDebtController",function ($scope, $http, $routeParams) {

	$scope.titulo = "Extrato conta";
	$scope.customerId = $routeParams.customerId;
	
	$http.get('http://localhost:5000/api/account/'+$scope.customerId)	
	.then(function(response){		
		$http.get('http://localhost:5000/api/DebtHistory/'+response.data.id)
		.then(function(response){		
		$scope.debtHistorys = response.data;
	});
	});
})



//  .controller('ExampleController', function($scope,$http) {
// 	$http.get('http://localhost:5000/api/period/')
// 	.then(function(response){	
		
// 		$scope.data = {
// 			model: null,
// 			availableOptions: response.data
// 		   };		
// 	});   
// })


// .controller('langCtrl', function($scope, $location,$filter/*, $translate*/) {
// 	$scope.switchLanguage = function() {	  		  
	
// 	  }	  //$translate.use(langKey);
// 	}
//   )
.controller("extractCreditController",function ($scope, $http, $routeParams) {
	$scope.titulo = "Extrato Cartão de crédito";
	
	$scope.customerId = $routeParams.customerId;
console.log("$scope.customerIds");
	$http.get('http://localhost:5000/api/account/'+$scope.customerId)	
	.then(function(response){		
		$http.get('http://localhost:5000/api/creditcard/'+response.data.id)
		.then(function(response){		
		$scope.creditCard = response.data;
		console.log($scope.creditCard);
	});
	});
})

