//tripEditorController.js

(function(){

    "use strict";
    
    angular.module("app-trips")
    .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http)
    {
        var vm = this;
        vm.name = "KarthikNagabandi";
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};
        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
        .then(function (response)
            {
            //success
            angular.copy(response.data, vm.stops);
            _showMap(vm.stops);
            }, function (err)
            {
            //failure
            vm.errorMessage = "Failed to load Stops";
            })
        .finally(function ()
            {
            vm.isBusy = false;
        });


        vm.addStop = function () {
            vm.isBusy = true;
            $http.post(url, vm.newStop)
                .then(function (response) {
                    //success
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                }, function (err) {
                    //failure
                    vm.errorMessage = "Failed to add new Stop";
                })
            .finally(function () {
                vm.isBusy = false;
            });
        };


    }

    function _showMap(stops) {
        if (stops && stops.length > 0) {


            //using the underscore js library for the maps to one type to another
            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });
            //show Map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });

        }


    }

})();