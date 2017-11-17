using System.Collections.Generic;

public class GameStateList : List<GameState>
{
    public string Team1 { get; set; }
    public string Team2 { get; set; }

    public GameStateList(string team1, string team2)
    {
        Team1 = team1;
        Team2 = team2;
    }
}
