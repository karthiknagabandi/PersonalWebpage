//simpleControl.js

(function () {
    'use strict';

    //Generate module
    angular.module("simpleControls", [])
    .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            templateUrl: "/view/waitCursor.html"
        };

    }




})();