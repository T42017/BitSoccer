// Write your JavaScript code.

var canvas;

function setup() {
    canvas = createCanvas();
    canvas.class("col-md-8 col-md-offset-2");
    canvas.style("background-color", "black");
    canvas.parent("game-div");
}

function draw() {
    fill(255);
    rect(0, 0, 50, 50);
}