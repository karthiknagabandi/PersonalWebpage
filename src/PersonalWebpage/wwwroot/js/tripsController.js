//tripsController.js
(function () {

    "use strict";
    angular.module("app-trips")
        .controller("tripsController", tripsController);


    //code for the controller
    function tripsController($http) {
        
        var vm = this;
        vm.trips = [];

        vm.newTrip = {};
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                //success
                angular.copy(response.data, vm.trips);
            }, function () {
                //failure
                vm.errorMessage = "Failed to Load Data";
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            vm.isBusy = true;

            $http.post("/api/trips", vm.newTrip)
            .then(function (response) {
                //success
                vm.trips.push(response.data);
                vm.newTrip = {};

            }, function () {
                //fail
                vm.errorMessage = "Failed to dave new trip";
            })
            .finally(function () {
                vm.isBusy = false;
            });
            
            //vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            //vm.newTrip = {};
        };
    }

})();