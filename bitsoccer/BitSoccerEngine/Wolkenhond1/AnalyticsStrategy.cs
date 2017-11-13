// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.AnalyticsStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class AnalyticsStrategy : PlayerStrategyBase
  {
    public AnalyticsStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
    }

    public Player LastBallOwner { get; protected set; }

    public bool OwnHasBall { get; protected set; }

    public bool EnemyHasBall { get; protected set; }

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (ball.Owner != null)
      {
        this.LastBallOwner = ball.Owner;
        this.OwnHasBall = myTeam.Players.Contains(ball.Owner);
        this.EnemyHasBall = enemyTeam.Players.Contains(ball.Owner);
      }
      if (matchInfo.MyTeamScored || matchInfo.EnemyTeamScored)
      {
        this.LastBallOwner = (Player) null;
        this.OwnHasBall = false;
        this.EnemyHasBall = false;
      }
      return false;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      throw new NotImplementedException();
    }
  }
}
