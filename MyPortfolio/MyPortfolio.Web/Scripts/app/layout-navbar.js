(function () {
    
    jQuery.fn.rotate = function (degrees) {
        $(this).css({
            '-webkit-transform-origin': '50% 50%',
            '-webkit-transform': 'rotate(' + degrees + 'deg)',
            '-moz-transform': 'rotate(' + degrees + 'deg)',
            '-ms-transform': 'rotate(' + degrees + 'deg)',
            'transform': 'rotate(' + degrees + 'deg)'
        });
    };

})();