var BoardSlice, BullsEye, DartBoard;
var __bind = function (fn, me) { return function () { return fn.apply(me, arguments); }; };
DartBoard = (function () {
    DartBoard.prototype.ScoreOrder = [20, 1, 18, 4, 13, 6, 10, 15];
    function DartBoard(game, postAction, moves) {
        this.moves = moves;
        this.game = game;
        this.paper = Raphael('board');
        this.originX = Math.min(this.paper.width, this.paper.height) / 2;
        this.originY = Math.min(this.paper.width, this.paper.height) / 2;
        this.radius = Math.min(this.paper.width, this.paper.height) / 2 - 15;
        this.postAction = postAction;
        this.slices = [];
        this.draw();
    }
    DartBoard.prototype.setMoves = function (moves) {
        this.moves = moves;
    }
    DartBoard.prototype.draw = function () {
        var idx, score, slice, _i, _len, _ref;
        idx = 0;
        _ref = this.ScoreOrder;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            score = _ref[_i];
            if (this.slices[_i] == null) {
                this.slices[_i] = new BoardSlice(score, _i, this);
            }
            this.slices[_i].color(idx);
            this.slices[_i].rotate(idx * 45);
            idx += 1;
        }
        return new BullsEye(this);
    };
    DartBoard.prototype.emitCoordinates = function (x, y, section) {
        this.postAction(x, y, section);
        console.log(section);
    };
    DartBoard.prototype.findSectionIndex = function (x, y) {
        for (var i = 0; i < this.moves.length; i++) {
            if (this.moves[i].position.X == x && this.moves[i].position.Y == y) {
                return i;
            }
        }
        return null;
    };

    return DartBoard;
})();
BullsEye = (function () {
    function BullsEye(board) {
        this.board = board;
        this.draw();
    }
    BullsEye.prototype.draw = function () {
        var double, single;
        single = this.board.paper.circle(this.board.originX, this.board.originY, this.board.radius * 0.1);
        single.attr({
            fill: "#60af75",
            "stroke-width": 2,
            "stroke": "#999999"
        });
        single.click(__bind(function (event) {
            return this.emitHit(25);
        }, this));
        double = this.board.paper.circle(this.board.originX, this.board.originY, this.board.radius * 0.048);
        double.attr({
            fill: "#bb2e36",
            "stroke-width": 2,
            "stroke": "#999999"
        });
        double.click(__bind(function (event) {
            return this.emitHit(50);
        }, this));
        single.mouseover(__bind(function (event) {
            single.toFront();
            double.toFront();
            return single.animate({
                scale: "1.07"
            }, 250, "bounce");
        }, this));
        single.mouseout(__bind(function (event) {
            single.stop();
            return single.animate({
                scale: "1"
            }, 250, "bounce");
        }, this));
        double.mouseover(__bind(function (event) {
            double.toFront();
            return double.animate({
                scale: "1.07"
            }, 250, "bounce");
        }, this));
        return double.mouseout(__bind(function (event) {
            double.stop();
            return double.animate({
                scale: "1"
            }, 250, "bounce");
        }, this));
    };
    BullsEye.prototype.emitHit = function (x, y) {
        alert("x: " + this.x + " y: " + range[2]);
        return this.board.emitCoordinates(x, y);
    };
    return BullsEye;
})();
BoardSlice = (function () {
    BoardSlice.prototype.Sections = [[0.96, 0.75, 3], [0.75, 0.54, 2], [0.54, 0.33, 1], [0.33, 0.10, 0]];
    BoardSlice.prototype.SingleColors = ['#f5e1c3', '#040204'];
    //BoardSlice.prototype.DoubleTripleColors = ['#60af75', '#bb2e36'];
    BoardSlice.prototype.DoubleTripleColors = ['#040204', '#f5e1c3'];
    function BoardSlice(value, x, board) {
        var section, _i, _len, _ref;
        this.value = value;
        this.board = board;
        this.x = x;
        this.slices = this.board.paper.set();

        _ref = this.Sections;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            section = _ref[_i];
            this.slices.push(this.drawSection(section));
            this.slices.push(this.drawNumber());
        }
        this.color(0);
    }
    BoardSlice.prototype.rotate = function (angle) {
        return this.slices.rotate(angle, this.board.originX, this.board.originY);
    };
    BoardSlice.prototype.color = function (idx) {
        var x = this.x;
        var j = 0;
        for (var i = 0; i < 4; i++) {
            var y = this.Sections[i][2];
            var index = this.board.findSectionIndex(x, y);
            if (index == null) {
                if (j % 4 == 0) {
                    this.slices[j].attr({
                        fill: this.DoubleTripleColors[idx % 2]
                    });
                } else {
                    this.slices[j].attr({
                        fill: this.SingleColors[idx % 2]
                    });
                }
            } else if (index % 2 == 0) {
                this.slices[j].attr({
                    fill: "#60af75"
                });
            } else if (index % 2 == 1) {
                this.slices[j].attr({
                    fill: "#bb2e36"
                });
            }
            j = j + 2;
        }
      
    };
    BoardSlice.prototype.drawSection = function (range) {
        var section;
        section = this.shapePath(range[0], range[1]);
        section.attr({
            "stroke-width": 2,
            "stroke": "#999999"
        });
        section.click(__bind(function (event) {
            return this.emitHit(this.x, range[2], section);
        }, this));
        section.mouseover(__bind(function (event) {
            return this.sectionHoverIn(section);
        }, this));
        section.mouseout(__bind(function (event) {
            return this.sectionHoverOut(section);
        }, this));
        return section;
    };
    BoardSlice.prototype.drawNumber = function () {
        var circle, number, set;
        set = this.board.paper.set();
        //        circle = this.board.paper.circle(this.board.originX, 12, 10);
        //        circle.attr({
        //            fill: "#f5e1c3",
        //            stroke: "#f5e1c3"
        //        });
        set.push(circle);
        //        number = this.board.paper.text(this.board.originX, 12, this.value);
        //        number.attr({
        //            "font-weight": "bold"
        //        });
        set.push(number);
        return set;
    };
    BoardSlice.prototype.sectionHoverIn = function (section) {
        section.toFront();
        return section.animate({
            scale: "1.07"
        }, 250, "bounce");
    };
    BoardSlice.prototype.sectionHoverOut = function (section) {
        section.stop();
        return section.animate({
            scale: "1"
        }, 250, "bounce");
    };
    BoardSlice.prototype.emitHit = function (x, y, section) {
        return this.board.emitCoordinates(x, y, section);
    };
    BoardSlice.prototype.pointsFor = function (factor) {
        var lx, ly, radius, rx, ry;
        //radius = this.board.radius * factor;
        radius = this.board.radius * factor;
        //    lx = radius * Math.cos(Raphael.rad(261)) + this.board.originX;
        //    ly = radius * Math.sin(Raphael.rad(261)) + this.board.originX;
        //    rx = radius * Math.cos(Raphael.rad(279)) + this.board.originX;
        //    ry = radius * Math.sin(Raphael.rad(279)) + this.board.originX;
        lx = radius * Math.cos(Raphael.rad(261)) + this.board.originX;
        ly = radius * Math.sin(Raphael.rad(261)) + this.board.originX;
        rx = radius * Math.cos(Raphael.rad(306)) + this.board.originX;
        ry = radius * Math.sin(Raphael.rad(306)) + this.board.originX;
        return {
            l: {
                x: lx,
                y: ly
            },
            r: {
                x: rx,
                y: ry
            },
            radius: radius
        };
    };
    BoardSlice.prototype.shapePath = function (top_p, bottom_p) {
        var bottom, top;
        top = this.pointsFor(top_p);
        bottom = this.pointsFor(bottom_p);
        return this.board.paper.path("M " + bottom.l.x + " " + bottom.l.y + "L" + top.l.x + " " + top.l.y + "A " + top.radius + " " + top.radius + " 0 0 1 " + top.r.x + " " + top.r.y + "L " + bottom.r.x + " " + bottom.r.y + "A " + bottom.radius + " " + bottom.radius + " 0 0 0 " + bottom.l.x + " " + bottom.l.y);
    };
    return BoardSlice;
})();
