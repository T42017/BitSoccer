// Decompiled with JetBrains decompiler
// Type: Common.ITeam
// Assembly: Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29030A24-AB57-4350-A3CB-B2D86A295459
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\Common.dll

using System.Security;

namespace Common
{
  /// <summary>
  /// All teams have to inherit from ITeam.
  /// 
  /// </summary>
  public interface ITeam
  {
    /// <summary>
    /// The game engine will call the Action method on your team to get the actions that your team wants to preform every round.
    /// 
    /// </summary>
    /// <param name="myTeam">Your team</param><param name="enemyTeam">The enemy team</param><param name="ball">The ball</param><param name="matchInfo">Information on the current gamestate</param>
    /// <returns/>
    [SecurityCritical]
    void Action(Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo);
  }
}
