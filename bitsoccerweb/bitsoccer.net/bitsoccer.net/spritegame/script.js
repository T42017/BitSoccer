var posxnew;
var posynew;
var team1goal;
var team2goal;
var team1name;
var team2name;
$(document).ready(function () {

    var CANVAS_WIDTH = 978;
    var CANVAS_HEIGHT = 694;

    var canvasElement = $("<canvas id='canvas' width='" + CANVAS_WIDTH +
                          "' height='" + CANVAS_HEIGHT + "'></canvas>");
    var canvas = canvasElement.get(0).getContext("2d");
    canvasElement.appendTo('.game');

    var FPS = 60;
    setInterval(function () {
        update();
        draw();

    }, 1000 / FPS);
    var posx = 128;
    var posy = 127;

    var names;

    var request = new XMLHttpRequest();
    request.open("GET", "/spritegame/test.xml", false);
    request.send();
    var xml = request.responseXML;
    var users = xml.getElementsByTagName("SerializableGameState");
    var teamnames = xml.getElementsByTagName("GamePlayed")[0];
    var index = 0;

    team1name = teamnames.getAttribute("Team1Name");
    team2name = teamnames.getAttribute("Team2Name");

    var player = {
        color: "#000099",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player2 = {
        color: "#000099",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player3 = {
        color: "#000099",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player4 = {
        color: "#000099",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player5 = {
        color: "#000099",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player6 = {
        color: "#000099",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player7 = {
        color: "#990000",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player8 = {
        color: "#990000",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player9 = {
        color: "#990000",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player10 = {
        color: "#990000",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player11 = {
        color: "#990000",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var player12 = {
        color: "#990000",
        width: 16,
        height: 16,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.fillRect(this.x, this.y, this.width, this.height);
        }
    };

    var ball = {
        color: "#FFF",
        width: 4,
        height: 4,
        draw: function () {
            canvas.fillStyle = this.color;
            canvas.beginPath();
            canvas.arc(this.x +8, this.y+8, 8, 0, Math.PI * 2, true);
            canvas.closePath();
            canvas.fill();
        }
    };

    function update() {

        var user = users[index];
        movePlayer(player, user.getElementsByTagName("T1P1"));
        movePlayer(player2, user.getElementsByTagName("T1P2"));
        movePlayer(player3, user.getElementsByTagName("T1P3"));
        movePlayer(player4, user.getElementsByTagName("T1P4"));
        movePlayer(player5, user.getElementsByTagName("T1P5"));
        movePlayer(player6, user.getElementsByTagName("T1P6"));
        movePlayer(player7, user.getElementsByTagName("T2P1"));
        movePlayer(player8, user.getElementsByTagName("T2P2"));
        movePlayer(player9, user.getElementsByTagName("T2P3"));
        movePlayer(player10, user.getElementsByTagName("T2P4"));
        movePlayer(player11, user.getElementsByTagName("T2P5"));
        movePlayer(player12, user.getElementsByTagName("T2P6"));
        movePlayer(ball, user.getElementsByTagName("BallPosition"));
        team1goal = user.getAttribute("Team1Goal");
        team2goal = user.getAttribute("Team2Goal");

        //UPDATE END

        if (index < users.length - 1) {
            index++;
        }
    }

    function movePlayer(player, names) {

            posxnew = parseInt(names[0].getAttribute("X"));
            posynew = parseInt(names[0].getAttribute("Y"));

        player.x = posxnew * 724/1920 + 128 -8;
        player.y = posynew * 401/1080 +127 -8;
    }

    function draw() {

        canvas.clearRect(0, 0, CANVAS_WIDTH, CANVAS_HEIGHT);
        player.draw();
        player2.draw();
        player3.draw();
        player4.draw();
        player5.draw();
        player6.draw();
        player7.draw();
        player8.draw();
        player9.draw();
        player10.draw();
        player11.draw();
        player12.draw();
        ball.draw();
        
        // Update style for team names
        canvas.fillStyle = "white";
        canvas.font = "bold 20px Arial";
        canvas.textAlign = "center";

        // Draw team names
        canvas.fillText(team1name, 236, 619);
        canvas.fillText(team2name, 741, 619);

        // Update style for score & draw score
        canvas.font = "bold 60px Arial";
        canvas.fillText(team1goal + " - " + team2goal, 489, 633);

        // Post-game text
        if (index >= users.length - 1) {
            // Update style for rectangle & draw rectangle
            canvas.fillStyle = "#2D2D2D";
            canvas.fillRect(180, 267, 618, 120);

            // Update style for post-game text & draw post-game text
            canvas.fillStyle = "white";
            canvas.font = "bold 80px Arial";
            canvas.fillText("Game finished!", 489, 356);
        }
    }
});