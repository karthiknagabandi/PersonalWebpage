//simpleControl.js

(function () {
    'use strict';

    //Generate module
    angular.module("simpleControls", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            //scope name is what is visible inside the template
            // name is the consumer of the directive in trips Controller 
            scope: {
                show: "=displayWhen"
            },
            restrict : "E",
            templateUrl: "/views/waitCursor.html"
        };

    }
})();