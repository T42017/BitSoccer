// The p5 sketch.
var sketch = function (p) {
    // -------- members ------------------
    var canvas, xml;
    var currentGameState = 0;
    var fps = 1000 / 60;

    var teamOne = [];
    var teamTwo = [];
    var ball;
    // -----------------------------------


    // ----- p5 functions -------
    p.setup = function() {
        canvas = p.createCanvas(480, 270);
        canvas.class("col-md-8 col-md-offset-2");
        canvas.style("background-color", "#00FF00");
        canvas.style("border", "2px solid black");
        
        p.initializeGame();
    };

    p.update = function() {
        p.frameRate(fps);
        teamOne.forEach(player => player.move());
        teamTwo.forEach(player => player.move());
        ball.move();
    };

    p.draw = function() {
        p.background(0, 255, 0);
        p.update();

        teamOne.forEach(player => player.draw());
        teamTwo.forEach(player => player.draw());
        ball.draw();
    };
    // ---------------------------



    // ----------- initialization functions -------------------
    p.initializeGame = function() {
        xml = p.getXML("/js/2eba505c-aeb9-4e76-9be3-1933109a6a38.xml");
        p.initializePlayers();
        p.initializeBall();
    };

    // Fetches the matches' XML and returns the gamestates as well as the name of the teams
    p.getXML = function(filePath) {
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
    };

    // Initializes both of the teams' with players
    p.initializePlayers = function() {
        for (var i = 1; i <= 6; i++) {
            teamOne.push(new Player("Team1Player" + i, "one"));
        }

        for (var i = 1; i <= 6; i++) {
            teamTwo.push(new Player("Team2Player" + i, "two"));
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
    }

    // Represents a ball with a position.
    function Ball() {
        this.pos = p.createVector(0, 0);
    }
    // ----------------------------------------------------



    // ------------- prototype attachments ----------------------
    Player.prototype.move = function () {
        p.move(this, this.tag);
    }

    Player.prototype.draw = function () {
        p.noStroke();
        var color = {
            r: this.team === "one" ? 255 : 0,
            g: 0,
            b: this.team === "two" ? 255 : 0
        };

        p.fill(color.r, color.g, color.b);
        p.rect(this.pos.x, this.pos.y, 10, 10);
    }

    Ball.prototype.move = function () {
        p.move(this, "BallPosition");
    }

    Ball.prototype.draw = function () {
        p.stroke(0);
        p.fill(255);
        p.ellipse(this.pos.x, this.pos.y, 10, 10);
    }
    // --------------------------------------------------



    // ------------- helper functions ---------------
    p.move = function(obj, tag) {
        if (currentGameState >= xml.gameStates.length) {
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
    // ----------------------------------------------
};

// Append the sketch to the DOM-element with id "game-div"
new p5(sketch, "game-div");