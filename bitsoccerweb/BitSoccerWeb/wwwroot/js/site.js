﻿// The p5 sketch.
var sketch = function(p) {
    // -------- members ------------------
    var canvas, xml;
    var currentGameState = 0;
    var fps = 1000 / 60;
    var playbackBufferHeight = 5;

    var teamOne = {
        players: [],
        score: 0
    };
    var teamTwo = {
        players: [],
        score: 0
    };
    var ball;
    // -----------------------------------


    // ----- p5 functions -------
    p.setup = function() {
        canvas = p.createCanvas(480, 270);
        //canvas.class("col-md-8 col-md-offset-2");
        //canvas.style("background-color", "#00FF00");
        canvas.style("border", "2px solid black");
        
        p.initializeGame();
    };

    p.update = function() {
        p.frameRate(fps);

        // move each team's players
        teamOne.players.forEach(player => player.move());
        teamTwo.players.forEach(player => player.move());

        // move the ball
        ball.move();

        // update the scores
        if (currentGameState < xml.gameStates.length) {
            teamOne.score = xml.gameStates[currentGameState].getAttribute("Team1Scores");
            teamTwo.score = xml.gameStates[currentGameState].getAttribute("Team2Scores");
        }
    };

    p.draw = function() {
        p.background(0, 255, 0);
        p.update();

        teamOne.players.forEach(player => player.draw());
        teamTwo.players.forEach(player => player.draw());
        ball.draw();
        document.getElementById("teamOneScore").innerHTML = teamOne.score;
        document.getElementById("teamTwoScore").innerHTML = teamTwo.score;
        p.drawPlaybackBuffer();
    };

    p.mousePressed = function () {
        if (p.mouseY < p.height - playbackBufferHeight) {
            return;
        }

        var x = p.map(p.mouseX, 0, p.width, 0, xml.gameStates.length);
        currentGameState = p.floor(x) - 1;
    }

    p.mouseDragged = function() {
        p.mousePressed();
    }

    // ---------------------------



    // ----------- initialization functions -------------------
    p.initializeGame = function() {
        xml = p.getXML("/js/2eba505c-aeb9-4e76-9be3-1933109a6a38.xml");
        p.initializePlayers();
        p.initializeBall();
        document.getElementById("teamOneName").innerHTML = xml.teamNames.getAttribute("Team1Name");
        document.getElementById("teamTwoName").innerHTML = xml.teamNames.getAttribute("Team2Name");
    };

    // Fetches the match's XML and returns the gamestates as well as the name of the teams
    p.getXML = function(filePath) {
        var request = new XMLHttpRequest();
        request.open("GET", filePath, false);
        request.send();
        var response = request.responseXML;
        var gameStates = response.getElementsByTagName("SerializableGameState");
        var teamNames = response.getElementsByTagName("Match")[0];

        return {
            gameStates: gameStates,
            teamNames: teamNames,
        };
    };

    // Initializes both of the teams' with players
    p.initializePlayers = function() {
        for (var i = 1; i <= 6; i++) {
            teamOne.players.push(new Player("Team1Player" + i, "one"));
            teamTwo.players.push(new Player("Team2Player" + i, "two"));
        }
    };

    p.initializeBall = function() {
        ball = new Ball();
    };
    // ------------------------------



    // ----------- constructor functions ---------------
    // Represents a player with a position, tag (what position to search for in the XML) and a team for drawing the correct colour.
    function Player(tag, team) {
        this.pos = p.createVector(0, 0);
        this.tag = tag;
        this.team = team;
        this.width = 10;
        this.height = 10;
    }

    // Represents a ball with a position.
    function Ball() {
        this.pos = p.createVector(0, 0);
    }
    // ----------------------------------------------------



    // ------------- prototype attachments ----------------------
    Player.prototype.move = function() {
        p.move(this, this.tag);
    }

    Player.prototype.draw = function() {
        p.stroke(0);
        var colour = {
            r: (this.team === "one") * 255,
            g: 0,
            b: (this.team === "two") * 255
        };

        p.fill(colour.r, colour.g, colour.b);
        p.rect(this.pos.x - this.width  / 2,
               this.pos.y - this.height / 2,
               this.width,
               this.height
        );
    }

    Ball.prototype.move = function() {
        p.move(this, "BallPosition");
    }

    Ball.prototype.draw = function() {
        p.stroke(0);
        p.fill(255);
        p.ellipse(this.pos.x, this.pos.y, 10, 10);
    }
    // --------------------------------------------------



    // ------------- helper functions ---------------
    p.move = function(obj, tag) {
        if (currentGameState >= xml.gameStates.length || !obj || !obj.pos) {
            return;
        }

        var gameState = xml.gameStates[currentGameState++];
        var playerPosXML = gameState.getElementsByTagName(tag);
        var pos = {
            x: parseInt(playerPosXML[0].getAttribute("X")),
            y: parseInt(playerPosXML[0].getAttribute("Y"))
        };
        obj.pos = p.createVector(
            p.map(pos.x, 0, 1920, 0, p.width),
            p.map(pos.y, 0, 1080, 0, p.height)
        );
    }

    p.drawPlaybackBuffer = function() {
        p.stroke(0, 150, 0);
        p.fill(0, 150, 0);
        p.rect(0, p.height - playbackBufferHeight, p.map(currentGameState, 0, xml.gameStates.length, 0, p.width), p.height);
    }
    // ----------------------------------------------
};

// Append the sketch to the DOM-element with id "game-div"
new p5(sketch, "game-div");