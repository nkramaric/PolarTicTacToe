var options = {
    dimens: {
        x: 500,
        y: 500
    }
}
var paper = Raphael(10, 50, options.dimens.x, options.dimens.y);

function init() {
    var circles = [];

    // draw the click able circles
    for (var i = 5; i > 1; i--) {
        // store the circle to wire up the click event later
        circles.push(
			paper.circle(options.dimens.x / 2, options.dimens.y / 2, i * 49)
				.attr({ stroke: "#000", fill: '#fff' })
				.node);
    }

    // draw the lines
    for (var i = 1; i <= 8; i++) {
        paper.path("M250,4L250,496")
		    .rotate((360 / 8) * i);
    }

    // draw a circle ontop of the lines
    paper.circle(options.dimens.x / 2, options.dimens.y / 2, 50)
	    .attr({ stroke: "#000", fill: '#fff' });

    // TODO draw game pieces

    $(circles).on('click', function (e) {
        // TODO: figure out the co-ords just clicked and send to server
        alert('click');
    });
}

$(document).ready(function () {
    init();
});