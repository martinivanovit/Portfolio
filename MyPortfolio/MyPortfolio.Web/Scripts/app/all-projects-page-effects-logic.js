(function() {

    $(function () {
        var ANIMATION_SPEED = 200;

        //Fade in the projects container at page load
        $('.projects-wrap').fadeIn(700);

        $(".project-image-wrap").hover(function (e) {
            var el_pos = $(this).offset();
            var edge = closestEdge(e.pageX - el_pos.left, e.pageY - el_pos.top, $(this).width(), $(this).height());

            var element = $(this).find('.project-img-cover');
            element.css("position", "absolute");

            if (edge == 'left') {
                element.css("height", "100%");
                element.show().animate({ width: '100%' }, ANIMATION_SPEED);
            } else if (edge == 'top') {
                element.css("width", "100%");
                element.show().animate({ height: '100%' }, ANIMATION_SPEED);
            } else if (edge == 'bottom') {
                element.css({
                    "bottom": "0",
                    "width": "100%"
                });
                element.show().animate({ height: '100%' }, ANIMATION_SPEED);
            } else if (edge == 'right') {
                element.css({
                    "position": "static",
                    "float": "right",
                    "height": "100%"
                });
                element.show().animate({ width: '100%' }, ANIMATION_SPEED);
            }

        }, function (e) {
            var el_pos = $(this).offset();
            var edge = closestEdge(e.pageX - el_pos.left, e.pageY - el_pos.top, $(this).width(), $(this).height());

            var element = $(this).find('.project-img-cover');
            var self = this;

            element.css("float", "none");

            if (edge == 'left') {
                element.animate({ width: '1px' }, ANIMATION_SPEED,
                    function () {
                        element.css("height", "1px");
                        element.hide();
                    });
            } else if (edge == 'top') {
                element.css("bottom", "");
                element.animate({ height: '1px' }, ANIMATION_SPEED,
                    function () {
                        element.css("width", "1px");
                        element.hide();
                    });
            } else if (edge == 'bottom') {
                element.css({
                    "position": "absolute",
                    "bottom": "0"
                });
                element.animate({ height: '1px' }, ANIMATION_SPEED,
                    function () {
                        element.css({
                            "bottom": "",
                            "width": "1px"
                        });
                        element.hide();
                    });
            } else if (edge == 'right') {
                element.css({
                    "position": "static",
                    "float": "right"
                });

                element.animate({ width: '1px' }, ANIMATION_SPEED,
                    function () {
                        element.css("height", "1px");
                        element.hide();
                    });
            }
        });
    });

    function closestEdge(x, y, w, h) {
        var topEdgeDist = distMetric(x, y, w / 2, 0);
        var bottomEdgeDist = distMetric(x, y, w / 2, h);
        var leftEdgeDist = distMetric(x, y, 0, h / 2);
        var rightEdgeDist = distMetric(x, y, w, h / 2);

        var min = Math.min(topEdgeDist, bottomEdgeDist, leftEdgeDist, rightEdgeDist);
        switch (min) {
            case leftEdgeDist:
                return "left";
            case rightEdgeDist:
                return "right";
            case topEdgeDist:
                return "top";
            case bottomEdgeDist:
                return "bottom";
        }
    }

    function distMetric(x, y, x2, y2) {
        var xDiff = x - x2;
        var yDiff = y - y2;
        return (xDiff * xDiff) + (yDiff * yDiff);
    }

})();