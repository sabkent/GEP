var controllers = controllers || {};

var module = angular.module("customersIndex", ['ngRoute']);

module.config(function($routeProvider) {
    $routeProvider.when("/", {
        controller: "controllers.customersIndexController",
        templateUrl: "/templates/customersIndex.html"
    });

    $routeProvider.when("/search", {
        controller: "searchCustomerController",
        templateUrl :"/templates/customersSearch.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
});

controllers.customersIndexController = function($scope, $http, $location) {
    $scope.isBusy = true;
    $scope.customers = [];
    
    $http.get("/api/customers")
        .then(function(result) {
        angular.copy(result.data, $scope.customers); //angualr ensures binding directives handled correctly
    }, function() {
        console.log("error");
    }).then(function() {
        $scope.isBusy = false;
    });


    $scope.search = function() {
        $location.path("search");
    };

    $scope.test = this.test;
};


controllers.customersIndexController.prototype.test = function() {
    console.log('test');
};

controllers.customersIndexController.prototype.search = function() {
    console.log('search');
};


controllers.searchCustomerController = function($scope) {

};