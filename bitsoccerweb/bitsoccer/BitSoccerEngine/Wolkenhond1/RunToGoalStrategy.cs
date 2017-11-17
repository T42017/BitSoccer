// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.RunToGoalStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class RunToGoalStrategy : PlayerStrategyBase
  {
    public RunToGoalStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
      this.PlayerTypes = new PlayerType[3]
      {
        (PlayerType) 4,
        (PlayerType) 6,
        (PlayerType) 5
      };
    }

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (!base.HasAction(player, myTeam, enemyTeam, ball, matchInfo) || !(ball.Owner == player) || (double) player.GetClosest(enemyTeam).GetDistanceTo((IPosition) player) <= 10.0)
        return false;
      Player player1 = player;
      Rectangle enemyGoal = (Rectangle) Field.EnemyGoal;
      // ISSUE: explicit reference operation
      Vector center = enemyGoal.Center;
      return (double) player1.GetDistanceTo((IPosition) center) > 400.0;
    }

    public override int GetScore(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      return 1000;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      Player player1 = player;
      Rectangle enemyGoal = (Rectangle) Field.EnemyGoal;
      // ISSUE: explicit reference operation
      Vector center = enemyGoal.Center;
      player1.ActionGo((IPosition) center);
    }
  }
}
