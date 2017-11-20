// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.KeeperStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class KeeperStrategy : PlayerStrategyBase
  {
    public const float DistanceToGoal = 200f;

    public KeeperStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
      this.PlayerTypes = new PlayerType[1]{ (PlayerType) 1 };
    }

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      return base.HasAction(player, myTeam, enemyTeam, ball, matchInfo);
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if ((ball.Owner == player))
      {
        if ((double) player.GetDistanceTo((IPosition) ball) < 400.0)
          this.Get<BallOwnerStrategy>().Action(player, myTeam, enemyTeam, ball, matchInfo);
        else
          this.ActionForwardPass(player, myTeam, enemyTeam, ball, matchInfo);
      }
      else if (player.CanPickUpBall(ball))
        player.ActionPickUpBall();
      else if ((double) player.GetDistanceTo((IPosition) ball) < 400.0 && this.Get<GetFreeBallStrategy>().HasAction(player, myTeam, enemyTeam, ball, matchInfo))
        this.Get<GetFreeBallStrategy>().Action(player, myTeam, enemyTeam, ball, matchInfo);
      else if (this.Analytics.EnemyHasBall && (double) player.GetDistanceTo((IPosition) ball) < 200.0)
      {
        player.ActionGo((IPosition) ball);
      }
      else
      {
        Vector position = ball.Position;
        Rectangle goal1 = (Rectangle) Field.MyGoal;
        // ISSUE: explicit reference operation
        Vector center = ((Rectangle) @goal1).Center;
        Vector vector1 = (position - center);
        // ISSUE: explicit reference operation
        vector1 = (vector1 * 200f / ((Vector) @vector1).Length);
        Rectangle goal2 = (Rectangle) Field.MyGoal;
        // ISSUE: explicit reference operation
        Vector vector2 = (((Rectangle) @goal2).Center + vector1);
        player.ActionGo((IPosition) (object) vector2);
      }
    }
  }
}
