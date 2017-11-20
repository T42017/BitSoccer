// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.BallOwnerStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class BallOwnerStrategy : PlayerStrategyBase
  {
    public BallOwnerStrategy(List<IPlayerStrategy> strategies)
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

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (base.HasAction(player, myTeam, enemyTeam, ball, matchInfo))
        return (ball.Owner != player);
      return false;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if ((double) player.GetDistanceTo((IPosition) player.GetClosest(enemyTeam)) > 150.0)
        player.ActionGo((player.Position + new Vector(100f, 0.0f)));
      else
        this.ActionForwardPass(player, myTeam, enemyTeam, ball, matchInfo);
    }
  }
}
