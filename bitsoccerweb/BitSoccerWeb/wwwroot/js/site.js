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
    var gameStates = xml.getElementsByTagName("Match");
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

// add 



function add() {
    $("#test").append(
        "<div  id='newSelect' class='form-group'>" +
            "<label class='col-md-4 control-label' for='SelectTeam'>Select Team</label>" +
                "<div class='col-md-4'>" +
                    "<select id='SelectTeamTwo' name='TeamTwo' class='form-control'>" + 
                        "<option value='1'>Team1</option>" +
                        "<option value='2'>Team2</option>" +
                        "<option value='3'>Team3</option>" +
                    "</select>" + 
        "</div>" + "<a href='#' class='btn btn-xs' onclick='remove()'style='padding-top:1vh;'><span class='glyphicon glyphicon-remove'></span></a>" +
        "</div>");

}

function remove() {
    $("#newSelect").remove();
}