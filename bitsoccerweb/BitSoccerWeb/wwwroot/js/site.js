// Write your JavaScript code.

var canvas, xml;
var index = 0;
var ratio = 1920 / 1080;
var ballTag = "BallPosition";

var player;
var ball;
var teamOne = [];
var teamTwo = [];

function setup() {
    canvas = createCanvas(480, 270);
    canvas.class("col-md-8 col-md-offset-2");
    canvas.style("background-color", "black");
    canvas.parent("game-div");

    xml = getXML();

    player = new Player();

    initializePlayers();
    initializeBall();
}

function initializePlayers() {
    teamOne.push(new Player("Team1Player1", "one"));
    teamOne.push(new Player("Team1Player2", "one"));
    teamOne.push(new Player("Team1Player3", "one"));
    teamOne.push(new Player("Team1Player4", "one"));
    teamOne.push(new Player("Team1Player5", "one"));
    teamOne.push(new Player("Team1Player6", "one"));

    teamTwo.push(new Player("Team2Player1", "two"));
    teamTwo.push(new Player("Team2Player2", "two"));
    teamTwo.push(new Player("Team2Player3", "two"));
    teamTwo.push(new Player("Team2Player4", "two"));
    teamTwo.push(new Player("Team2Player5", "two"));
    teamTwo.push(new Player("Team2Player6", "two"));
}

function initializeBall() {
    ball = new Ball();
}

function getXML() {
    var request = new XMLHttpRequest();
    request.open("GET", "/js/2eba505c-aeb9-4e76-9be3-1933109a6a38.xml", false);
    request.send();
    var xml = request.responseXML;
    var gameStates = xml.getElementsByTagName("SerializableGameState");
    var teamNames = xml.getElementsByTagName("Match")[0];

    return {
        gameStates: gameStates,
        teamNames: teamNames
    };
}

function update() {
    frameRate(1000 / 60);
    teamOne.forEach(player => player.move());
    teamTwo.forEach(player => player.move());
    ball.move();
}

function draw() {
    background(0);
    update();

    teamOne.forEach(player => player.draw());
    teamTwo.forEach(player => player.draw());
    ball.draw();
}

function Player(tag, team) {
    this.pos = createVector(0, 0);
    this.tag = tag;
    this.team = team;
}

Player.prototype.move = function () {
    if (index >= xml.gameStates.length) {
        return;
    }

    var gameState = xml.gameStates[index++];
    var playerPosXML = gameState.getElementsByTagName(this.tag);
    var pos = {
        x: parseInt(playerPosXML[0].getAttribute("X")),
        y: parseInt(playerPosXML[0].getAttribute("Y"))
    };
    this.pos = createVector(
        map(pos.x, 0, 1920, 0, width),
        map(pos.y, 0, 1080, 0, height)
    );
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

function Ball() {
    this.pos = createVector(0, 0);
}

Ball.prototype.move = function () {
    if (index >= xml.gameStates.length) {
        return;
    }

    var gameState = xml.gameStates[index++];
    var playerPosXML = gameState.getElementsByTagName("BallPosition");
    var pos = {
        x: parseInt(playerPosXML[0].getAttribute("X")),
        y: parseInt(playerPosXML[0].getAttribute("Y"))
    };
    this.pos = createVector(
        map(pos.x, 0, 1920, 0, width),
        map(pos.y, 0, 1080, 0, height)
    );
}

Ball.prototype.draw = function() {
    stroke(0);
    fill(255);
    ellipse(this.pos.x, this.pos.y, 10, 10);
}