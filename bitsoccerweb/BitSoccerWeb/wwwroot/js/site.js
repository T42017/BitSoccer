// Write your JavaScript code.

var canvas;

function setup() {
    canvas = createCanvas();
    canvas.class("col-md-8 col-md-offset-2");
    canvas.style("background-color", "black");
    canvas.parent("game-div");

    var request = new XMLHttpRequest();
    request.open("GET", "/Matches/0fa6110b-28c0-4519-8297-66c9cdf4133f.xml", true);
    request.send();
    var xml = request.responseXML;
    var gameStates = xml.getElementsByTagName("SerializableGameState");
    for (var i = 0; i < gameStates.length; i++) {
        var gameState = gameStates[i];
        var serializableGameStates = xml.getElementsByTagName("GameStates");
        for (var j = 0; j < serializableGameStates.length; j++) {
            alert(serializableGameStates[j].childNodes[i].nodeValue);
        }
    }
}

function draw() {
    fill(255);
    rect(0, 0, 50, 50);
}