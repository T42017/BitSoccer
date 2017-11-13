// Decompiled with JetBrains decompiler
// Type: Common.Team
// Assembly: Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29030A24-AB57-4350-A3CB-B2D86A295459
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\Common.dll

using System;
using System.Collections.Generic;

namespace Common
{
  /// <summary>
  /// The team class contains all the players in the team, and can also be tested for equality against another team.
  /// 
  /// </summary>
  [Serializable]
  public class Team
  {
    private readonly List<Player> _players;
    private string devMessage;

    /// <summary>
    /// Get the players in the team.
    /// 
    /// </summary>
    public List<Player> Players
    {
      get
      {
        return this._players;
      }
    }

    /// <summary>
    /// Allows the developer to display a message in the devPrompt.
    /// </summary>
    /// 
    /// <value>
    /// The message to be displayed
    /// </value>
    public string DevMessage
    {
      get
      {
        return this.devMessage;
      }
      set
      {
        this.devMessage = value;
      }
    }

    /// <summary>
    /// Creates a new team. SHOULD NOT BE USED DURING NORMAL DEVELOPMENT.
    /// 
    /// </summary>
    public Team()
    {
      this._players = new List<Player>();
      this.devMessage = "";
    }

    /// <summary>
    /// Creates a new team. SHOULD NOT BE USED DURING NORMAL DEVELOPMENT.
    /// 
    /// </summary>
    /// <param name="players"/>
    public Team(List<Player> players)
    {
      this._players = players;
      this.devMessage = "";
    }

    public Team(TeamInfo team, bool withAction)
    {
      this._players = new List<Player>();
      this.devMessage = "";
      foreach (PlayerInfo parent in team.GetPlayers())
      {
        if (withAction)
          this._players.Add(new Player(parent.PlayerIndex(), new Vector(parent.GetPosition().X, parent.GetPosition().Y), new Vector(parent.GetSpeed().X, parent.GetSpeed().Y), parent.GetFallenTimer(), parent.GetTackleCoolDown(), this, parent.GetPlayerType(), parent));
        else
          this._players.Add(new Player(parent.PlayerIndex(), new Vector(parent.GetPosition().X, parent.GetPosition().Y), new Vector(parent.GetSpeed().X, parent.GetSpeed().Y), parent.GetFallenTimer(), parent.GetTackleCoolDown(), this, parent.GetPlayerType(), (PlayerInfo) null));
      }
    }

    /// <summary>
    /// Checks two teams for equality.
    /// 
    /// </summary>
    /// <param name="a"/><param name="b"/>
    /// <returns/>
    public static bool operator ==(Team a, Team b)
    {
      if (object.ReferenceEquals((object) a, (object) b))
        return true;
      if ((object)a == null || (object)b == null)
        return false;
      else
        return a.Equals(b);
    }

    /// <summary>
    /// Checks two teams for inequality.
    /// 
    /// </summary>
    /// <param name="a"/><param name="b"/>
    /// <returns/>
    public static bool operator !=(Team a, Team b)
    {
      return !(a == b);
    }

    /// <summary>
    /// Returns the number of players in the team.
    /// </summary>
    public int Count()
    {
      return this.Players.Count;
    }

    /// <summary>
    /// Checks if this team is equal to a object.
    /// 
    /// </summary>
    /// <param name="obj"/>
    /// <returns/>
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      Team team = obj as Team;
      if (team == null)
        return false;
      else
        return this._players[0] == team._players[0];
    }

    /// <summary>
    /// Checks if this team is equal to another team.
    /// 
    /// </summary>
    /// <param name="t"/>
    /// <returns/>
    public bool Equals(Team t)
    {
      if (t == null)
        return false;
      else
        return this._players[0] == t._players[0];
    }
  }
}
