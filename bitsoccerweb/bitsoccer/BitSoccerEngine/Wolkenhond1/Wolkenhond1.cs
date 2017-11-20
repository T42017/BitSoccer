// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.Wolkenhond1
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class Wolkenhond1 : ITeam
  {
    public Wolkenhond1()
    {
      this.Strategies = new List<IPlayerStrategy>();
      this.Strategies.AddRange((IEnumerable<IPlayerStrategy>) new List<IPlayerStrategy>()
      {
        (IPlayerStrategy) new AnalyticsStrategy(this.Strategies),
        (IPlayerStrategy) new KickOffStrategy(this.Strategies),
        (IPlayerStrategy) new GetFreeBallStrategy(this.Strategies),
        (IPlayerStrategy) new KeeperStrategy(this.Strategies),
        (IPlayerStrategy) new LiberoStrategy(this.Strategies),
        (IPlayerStrategy) new DefenderStrategy(this.Strategies),
        (IPlayerStrategy) new MidfielderStrategy(this.Strategies),
        (IPlayerStrategy) new AttackerStrategy(this.Strategies),
        (IPlayerStrategy) new ShootOnGoalStrategy(this.Strategies),
        (IPlayerStrategy) new BallOwnerStrategy(this.Strategies),
        (IPlayerStrategy) new PlayerWaitsStrategy(this.Strategies)
      });
    }

    public List<IPlayerStrategy> Strategies { get; protected set; }

    public void Action(Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      using (List<Player>.Enumerator enumerator = myTeam.Players.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Player current = enumerator.Current;
          foreach (IPlayerStrategy strategy in this.Strategies)
          {
            if (strategy.HasAction(current, myTeam, enemyTeam, ball, matchInfo))
            {
              strategy.Action(current, myTeam, enemyTeam, ball, matchInfo);
              break;
            }
          }
        }
      }
    }
  }
}
