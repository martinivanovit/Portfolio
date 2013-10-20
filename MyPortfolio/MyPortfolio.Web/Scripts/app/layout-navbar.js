(function () {
    var rotation = 0;
    $('.nav-activate-btn').on('click', function () {
        
        var self = this;

        var interval = setInterval(function () {
            rotation += 1;
            $(self).rotate(rotation);

            var innerTextElement = $(self).find('span');
            innerTextElement.rotate(rotation * -1);

            if (rotation % 90 === 0) {
                clearInterval(interval);

                

                if (innerTextElement.text() === 'Explore') {
                    $('.main-nav').animate({ "opacity": "1" }, 400);
                    $('.main-navbar').animate({ "opacity": "1" }, 400);
                    innerTextElement.text('Hide');
                } else {
                    $('.main-nav').animate({ "opacity": "0" }, 400);
                    $('.main-navbar').animate({ "opacity": "0" }, 400);
                    innerTextElement.text('Explore');
                }
            }
        }, 0);

    });

    jQuery.fn.rotate = function (degrees) {
        $(this).css({
            '-webkit-transform': 'rotate(' + degrees + 'deg)',
            '-moz-transform': 'rotate(' + degrees + 'deg)',
            '-ms-transform': 'rotate(' + degrees + 'deg)',
            'transform': 'rotate(' + degrees + 'deg)'
        });
    };

})();