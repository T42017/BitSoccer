using Common;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[Serializable]
public class TeamInfo
{
    private List<PlayerInfo> _players = new List<PlayerInfo>();

    public TeamInfo()
    {
        this._players.Clear();
        this._players.Add(new PlayerInfo(new Vector(50f, Field.Borders.Height / 2f), this, PlayerType.Keeper));
        this._players.Add(new PlayerInfo(new Vector(Field.Borders.X + (float)(1.0 * (double)Field.Borders.Width / 5.0), (float)(2.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.LeftDefender));
        this._players.Add(new PlayerInfo(new Vector(Field.Borders.X + (float)(1.0 * (double)Field.Borders.Width / 5.0), (float)(3.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.RightDefender));
        this._players.Add(new PlayerInfo(new Vector(Field.Borders.X + (float)(2.0 * (double)Field.Borders.Width / 5.0), (float)(1.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.LeftForward));
        this._players.Add(new PlayerInfo(new Vector(Field.Borders.X + (float)(2.0 * (double)Field.Borders.Width / 5.0), Field.Borders.Height / 2f), this, PlayerType.CenterForward));
        this._players.Add(new PlayerInfo(new Vector(Field.Borders.X + (float)(2.0 * (double)Field.Borders.Width / 5.0), (float)(4.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.RightForward));
    }

    public TeamInfo(int index)
    {
        this._players.Clear();
        this._players.Add(new PlayerInfo(6 * index, new Vector(50f, Field.Borders.Height / 2f), this, PlayerType.Keeper));
        this._players.Add(new PlayerInfo(6 * index + 1, new Vector(Field.Borders.X + (float)(1.0 * (double)Field.Borders.Width / 5.0), (float)(2.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.LeftDefender));
        this._players.Add(new PlayerInfo(6 * index + 2, new Vector(Field.Borders.X + (float)(1.0 * (double)Field.Borders.Width / 5.0), (float)(3.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.RightDefender));
        this._players.Add(new PlayerInfo(6 * index + 3, new Vector(Field.Borders.X + (float)(2.0 * (double)Field.Borders.Width / 5.0), (float)(1.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.LeftForward));
        this._players.Add(new PlayerInfo(6 * index + 4, new Vector(Field.Borders.X + (float)(2.0 * (double)Field.Borders.Width / 5.0), Field.Borders.Height / 2f), this, PlayerType.CenterForward));
        this._players.Add(new PlayerInfo(6 * index + 5, new Vector(Field.Borders.X + (float)(2.0 * (double)Field.Borders.Width / 5.0), (float)(4.0 * (double)Field.Borders.Height / 5.0)), this, PlayerType.RightForward));
    }

    public TeamInfo(List<PlayerInfo> players)
    {
        this._players.Clear();
        foreach (PlayerInfo player in players)
            this._players.Add(new PlayerInfo(player.PlayerIndex(), player.GetPosition(), player.GetSpeed(), player.GetPlayerInfo(), player.GetFallenTimer(), player.GetTackleCoolDown(), this, player.GetPlayerType(), player.y()));
    }

    [SpecialName]
    public List<PlayerInfo> GetPlayers()
    {
        return this._players;
    }

    [SpecialName]
    public Team GetTeam()
    {
        return new Team(this, false);
    }

    [SpecialName]
    public Team b()
    {
        return new Team(this, true);
    }

    [SpecialName]
    public TeamInfo CopyTeamInfo()
    {
        List<PlayerInfo> A_0 = new List<PlayerInfo>();
        foreach (PlayerInfo e in this._players)
            A_0.Add(e.GetPlayerInfo1());
        return new TeamInfo(A_0);
    }
}
