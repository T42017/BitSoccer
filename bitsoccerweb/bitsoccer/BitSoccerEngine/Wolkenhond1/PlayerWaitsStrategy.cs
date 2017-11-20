// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.PlayerWaitsStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class PlayerWaitsStrategy : PlayerStrategyBase
  {
    public PlayerWaitsStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
    }

    public override int GetScore(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      return 0;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      player.ActionWait();
    }
  }
}
