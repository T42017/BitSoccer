// The p5 sketch.
var sketch = function(p) {
    // -------- members ------------------
    let canvas, xml;
    let currentGameState = 0;
    const fps = 1000 / 60;
    const playbackBufferHeight = 5;
    let backgroundImage;
    
    const teamOne = {
        players: [],
        score: 0
    };
    const teamTwo = {
        players: [],
        score: 0
    };
    let ball;

    // -----------------------------------


    // ----- p5 functions -------
    p.setup = function() {
        canvas = p.createCanvas(960, 540);
        backgroundImage = p.loadImage("js/SoccerFieldPs.png");
        //canvas.class("col-md-8 col-md-offset-2");
        //canvas.style("background-color", "#00FF00");
        //canvas.style("border", "2px solid black");

        p.initializeGame();
    };

    p.update = () => {
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

    p.draw = () => {
        backgroundImage.resize(960, 0);
        p.image(backgroundImage, 0, 0);
        p.update();

        teamOne.players.forEach(player => player.draw());
        teamTwo.players.forEach(player => player.draw());
        ball.draw();
        document.getElementById("teamOneScore").innerHTML = teamOne.score;
        document.getElementById("teamTwoScore").innerHTML = teamTwo.score;
        p.drawPlaybackBuffer();
    };

    p.mousePressed = () => {
        if (p.mouseY < p.height - playbackBufferHeight ||
            p.mouseY > p.height ||
            p.mouseX < 0 ||
            p.mouseX > p.width) {
            return;
        }

        const x = p.map(p.mouseX, 0, p.width, 0, xml.gameStates.length);
        currentGameState = p.floor(x) - 1;
    };

    p.mouseDragged = function() {
        p.mousePressed();
    };

    // ---------------------------



    // ----------- initialization functions -------------------
    p.initializeGame = () => {
        xml = p.getXML("js/2eba505c-aeb9-4e76-9be3-1933109a6a38.xml");
        p.initializePlayers();
        p.initializeBall();
        document.getElementById("teamOneName").innerHTML = xml.teamNames.getAttribute("Team1Name");
        document.getElementById("teamTwoName").innerHTML = xml.teamNames.getAttribute("Team2Name");
    };

    // Fetches the match's XML and returns the gamestates as well as the name of the teams
    p.getXML = (filePath) => {
        const request = new XMLHttpRequest();
        request.open("GET", filePath, false);
        request.send();
        const response = request.responseXML;
        const gameStates = response.getElementsByTagName("SerializableGameState");
        const teamNames = response.getElementsByTagName("Match")[0];

        return {
            gameStates: gameStates,
            teamNames: teamNames
        };
    };

    // Initializes both of the teams' with players
    p.initializePlayers = () => {
        for (let i = 1; i <= 6; i++) {
            teamOne.players.push(new Player(`Team1Player${i}`, "one"));
            teamTwo.players.push(new Player(`Team2Player${i}`, "two"));
        }
    };

    p.initializeBall = () => {
        ball = new Ball();
    };
    // ------------------------------



    // ----------- constructor functions ---------------
    // Represents a player with a position, tag (what position to search for in the XML) and a team for drawing the correct colour.
    //function Player(tag, team) {
    //    this.pos = p.createVector(0, 0);
    //    this.tag = tag;
    //    this.team = team;
    //    this.width = 10;
    //    this.height = 10;
    //}

    // Represents a ball with a position.
    //function Ball() {
    //    this.pos = p.createVector(0, 0);
    //}
    // ----------------------------------------------------
    class Player {
        constructor(tag, team) {
            this.pos = p.createVector(0, 0);
            this.tag = tag;
            this.team = team;
            this.width = 10;
            this.height = 10;
        }

        move() {
            p.move(this, this.tag);
        }

        draw() {
            p.stroke(0);
            const colour = {
                r: (this.team === "one") * 255,
                g: 0,
                b: (this.team === "two") * 255
            };

            p.fill(colour.r, colour.g, colour.b);
            p.rect(this.pos.x - this.width / 2,
                this.pos.y - this.height / 2,
                this.width,
                this.height
            );
        }
    }

    class Ball {
        constructor() {
            this.pos = p.createVector(0, 0);
        }

        move() {
            p.move(this, "BallPosition");
        }

        draw() {
            p.stroke(0);
            p.fill(255);
            p.ellipse(this.pos.x, this.pos.y, 10, 10);
        }
    }



    // ------------- prototype attachments ----------------------
    //Player.prototype.move = function() {
    //    p.move(this, this.tag);
    //};

    //Player.prototype.draw = function() {
    //    p.stroke(0);
    //    var colour = {
    //        r: (this.team === "one") * 255,
    //        g: 0,
    //        b: (this.team === "two") * 255
    //    };

    //    p.fill(colour.r, colour.g, colour.b);
    //    p.rect(this.pos.x - this.width / 2,
    //        this.pos.y - this.height / 2,
    //        this.width,
    //        this.height
    //    );
    //};


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

    //Ball.prototype.move = function() {
    //    p.move(this, "BallPosition");
    //};

    //Ball.prototype.draw = function() {
    //    p.stroke(0);
    //    p.fill(255);
    //    p.ellipse(this.pos.x, this.pos.y, 10, 10);
    //};
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
    };

    p.drawPlaybackBuffer = function() {
        p.stroke(0, 0, 0);
        p.fill(0, 0, 0);
        p.rect(0,
            p.height - playbackBufferHeight,
            p.map(currentGameState, 0, xml.gameStates.length, 0, p.width),
            p.height);
    };
    // ----------------------------------------------
};

// Append the sketch to the DOM-element with id "game-div"
new p5(sketch, "game-div");