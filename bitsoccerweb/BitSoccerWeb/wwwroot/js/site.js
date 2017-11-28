// -------- members ------------------
var canvas, xml;
var currentGameState = 0;
var fps = 1000 / 60;

var teamOne = [];
var teamTwo = [];
var ball;
// -----------------------------------



// ----- p5 functions -------
function setup() {
    canvas = createCanvas(480, 270);
    canvas.class("col-md-8 col-md-offset-2");
    canvas.style("background-color", "rgb(0, 255, 0)")
    canvas.style("border", "2px solid black");
    canvas.parent("game-div");

    initializeGame();
}

function update() {
    frameRate(fps);
    teamOne.forEach(player => player.move());
    teamTwo.forEach(player => player.move());
    ball.move();
}

function draw() {
    background(0, 255, 0);
    update();

    teamOne.forEach(player => player.draw());
    teamTwo.forEach(player => player.draw());
    ball.draw();
}
// ---------------------------



// ----------- initialization functions -------------------
function initializeGame() {
    xml = getXML("/js/2eba505c-aeb9-4e76-9be3-1933109a6a38.xml");
    initializePlayers();
    initializeBall();
}

// Fetches the matches' XML and returns the gamestates as well as the name of the teams
function getXML(filePath) {
    var request = new XMLHttpRequest();
    request.open("GET", filePath, false);
    request.send();
    var xml = request.responseXML;
    var gameStates = xml.getElementsByTagName("SerializableGameState");
    var teamNames = xml.getElementsByTagName("Match")[0];

    return {
        gameStates: gameStates,
        teamNames: teamNames
    };
}

// Initializes both of the teams' with players
function initializePlayers() {
    for (var i = 1; i <= 6; i++) {
        teamOne.push(new Player("Team1Player" + i, "one"));
    }

    for (var i = 1; i <= 6; i++) {
        teamTwo.push(new Player("Team2Player" + i, "two"));
    }
}

function initializeBall() {
    ball = new Ball();
}
// ------------------------------



// ----------- constructor functions ---------------
// Represents a player with a position, tag (what position to search for in the XML) and a team for drawing the correct colour.
function Player(tag, team) {
    this.pos = createVector(0, 0);
    this.tag = tag;
    this.team = team;
}

// Represents a ball with a position.
function Ball() {
    this.pos = createVector(0, 0);
}
// ----------------------------------------------------



// ------------- prototype attachments ----------------------
Player.prototype.move = function() {
    move(this, this.tag);
}

Player.prototype.draw = function() {
    noStroke();
    var color = {
        r: this.team === "one" ? 255 : 0,
        g: 0,
        b: this.team === "two" ? 255 : 0
    };

    fill(color.r, color.g, color.b);
    rect(this.pos.x, this.pos.y, 10, 10);
}

Ball.prototype.move = function() {
    move(this, "BallPosition");
}

Ball.prototype.draw = function() {
    stroke(0);
    fill(255);
    ellipse(this.pos.x, this.pos.y, 10, 10);
}
// --------------------------------------------------



// ------------- helper functions ---------------
function move(obj, tag) {
    if (currentGameState >= xml.gameStates.length) {
        return;
    }

    var gameState = xml.gameStates[currentGameState++];
    var playerPosXML = gameState.getElementsByTagName(tag);
    var pos = {
        x: parseInt(playerPosXML[0].getAttribute("X")),
        y: parseInt(playerPosXML[0].getAttribute("Y"))
    };
    obj.pos = createVector(
        map(pos.x, 0, 1920, 0, width),
        map(pos.y, 0, 1080, 0, height)
    );
}
// ----------------------------------------------