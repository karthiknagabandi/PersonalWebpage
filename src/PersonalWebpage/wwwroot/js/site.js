//site.js
// self executing anonymous function
// nameless function that is execute immediately

(function () {
    var ele = $("#username");
    ele.text("Karthik Kumar Nagabandi");

    var main = $("#Main");
    main.on("mouseenter", function () {
        main.style = "background-color: #888;";
    });

    main.on("mouseleave", function () {
        main.style = "";
    });

    var $sidebarAndWrapper = $("#Sidebar,#wrapper");
    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        }
        else {
            $(this).text("Hide Sidebar");
        }
    });
})();

