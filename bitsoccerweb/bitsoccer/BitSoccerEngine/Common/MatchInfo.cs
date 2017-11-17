// Decompiled with JetBrains decompiler
// Type: Common.MatchInfo
// Assembly: Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29030A24-AB57-4350-A3CB-B2D86A295459
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\Common.dll

using System;

namespace Common
{
  /// <summary>
  /// The matchinfo contains information about the current state of the game.
  /// 
  /// </summary>
  [Serializable]
  public sealed class MatchInfo
  {
    private readonly int _currentTimeStep;
    private readonly int _enemyScore;
    private readonly int _myScore;
    private readonly bool _myTeamScored;
    private readonly bool _enemyTeamScored;

    /// <summary>
    /// Gets your teams score.
    /// 
    /// </summary>
    public int MyScore
    {
      get
      {
        return this._myScore;
      }
    }

    /// <summary>
    /// Gets the enemy teams score.
    /// 
    /// </summary>
    public int EnemyScore
    {
      get
      {
        return this._enemyScore;
      }
    }

    /// <summary>
    /// Gets the current time step.
    /// 
    /// </summary>
    public int CurrentTimeStep
    {
      get
      {
        return this._currentTimeStep;
      }
    }

    /// <summary>
    /// Is true if your team scored the last round.
    /// 
    /// </summary>
    public bool MyTeamScored
    {
      get
      {
        return this._myTeamScored;
      }
    }

    /// <summary>
    /// Is true if the enemy team scored last round.
    /// 
    /// </summary>
    public bool EnemyTeamScored
    {
      get
      {
        return this._enemyTeamScored;
      }
    }

    /// <summary>
    /// Creates a new MatchInfo. SHOULD NOT BE USED DURING NORMAL PLAY.
    /// 
    /// </summary>
    /// <param name="myScore"/><param name="enemyScore"/><param name="currentTimeStep"/><param name="myTeamScored"/><param name="enemyTeamScored"/>
    public MatchInfo(int myScore, int enemyScore, int currentTimeStep, bool myTeamScored, bool enemyTeamScored)
    {
      this._myScore = myScore;
      this._enemyScore = enemyScore;
      this._currentTimeStep = currentTimeStep;
      this._myTeamScored = myTeamScored;
      this._enemyTeamScored = enemyTeamScored;
    }
  }
}
