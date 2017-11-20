// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.PlayerOneStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class PlayerOneStrategy : PlayerStrategyBase
  {
    public PlayerOneStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
      this.PlayerTypes = new PlayerType[5]
      {
        (PlayerType) 2,
        (PlayerType) 3,
        (PlayerType) 4,
        (PlayerType) 6,
        (PlayerType) 5
      };
    }

    public override int GetScore(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      return 1;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if ((ball.Owner == player))
        player.ActionShootGoal();
      else if (player.CanPickUpBall(ball))
        player.ActionPickUpBall();
      else
        player.ActionGo((IPosition) (object) ball.Position);
    }
  }
}
