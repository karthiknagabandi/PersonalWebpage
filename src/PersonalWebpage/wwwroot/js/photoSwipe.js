var pswpElement = document.querySelectorAll('.pswp')[0];

// build items array
var items = [
    {
        src: 'c:\project\personalwebpage\src\personalwebpage\wwwroot\img\img_2445.jpg',
        w: 600,
        h: 400
    },
    {
        src: 'c:\project\personalwebpage\src\personalwebpage\wwwroot\img\img_2704.jpg',
        w: 1200,
        h: 900
    }
];

// define options (if needed)
var options = {
    // optionName: 'option value'
    // for example:
    index: 0 // start at first slide
};

// Initializes and opens PhotoSwipe
var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
gallery.init();