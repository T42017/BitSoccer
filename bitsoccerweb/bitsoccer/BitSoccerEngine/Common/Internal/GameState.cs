using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[Serializable]
public class GameState
{
    private readonly List<TeamInfo> _teams = new List<TeamInfo>();
    private readonly ScoreInfo _scoreInfo;
    private readonly string _devMessage1;
    private readonly string _devMessage2;
    private readonly string _errorMessage;

    public GameState()
    {
        this._teams.Clear();
        this._teams.Add(new TeamInfo());
        this._teams.Add(new TeamInfo());
        BallInfo = new BallInfo();
        this._scoreInfo = new ScoreInfo();
    }

    public GameState(List<TeamInfo> teams, BallInfo ballInfo, ScoreInfo scoreInfo, string devMessage1, string devMessage2, string errorMessage)
    {
        this._teams.Clear();
        this._teams.Add(new TeamInfo(teams[0].GetPlayers()));
        this._teams.Add(new TeamInfo(teams[1].GetPlayers()));
        BallInfo = new BallInfo(ballInfo.Position, ballInfo.Speed, ballInfo.Owner, ballInfo.Timer);

        this._scoreInfo = new ScoreInfo(scoreInfo.Team1, scoreInfo.Team2);
        this._devMessage1 = devMessage1;
        this._devMessage2 = devMessage2;
        this._errorMessage = errorMessage;
    }

    public BallInfo BallInfo { get; private set; }

    public List<TeamInfo> Teams()
    {
        return new List<TeamInfo>()
        {
          this._teams[0],
          this._teams[1].CopyTeamInfo()
        };
    }

    public ScoreInfo GetScoreInfo()
    {
        return this._scoreInfo;
    }

    public string GetDevMessage()
    {
        string str = "";
        if (this._devMessage1 != "")
            str = str + "Red team: " + this._devMessage1;
        if (this._devMessage2 != "")
            str = !(str == "") ? str + Environment.NewLine + "Blue team: " + this._devMessage2 : str + "Blue team: " + this._devMessage2;
        if (this._errorMessage != "")
            str = !(str == "") ? str + Environment.NewLine + this._errorMessage : str + this._errorMessage;
        return str;
    }
}
