// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.MidfielderStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class MidfielderStrategy : PlayerStrategyBase
  {
    public const float DistanceToGoal = 350f;
    public const float DistanceToBall = 200f;

    public MidfielderStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
      this.PlayerTypes = new PlayerType[1]{ (PlayerType) 5 };
    }

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (base.HasAction(player, myTeam, enemyTeam, ball, matchInfo))
        return (ball.Owner != player);
      return false;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (this.Analytics.EnemyHasBall)
      {
        Player player1 = player;
        Rectangle goal1 = (Rectangle) Field.MyGoal;
        // ISSUE: explicit reference operation
        Vector center1 = goal1.Center;
        if ((double) player1.GetDistanceTo((IPosition) center1) > 350.0 && (double) player.GetDistanceTo((IPosition) ball) > 200.0)
        {
          Player player2 = player;
          Rectangle goal2 = (Rectangle) Field.MyGoal;
          // ISSUE: explicit reference operation
          Vector center2 = goal2.Center;
          player2.ActionGo((IPosition) center2);
        }
        else
          player.ActionGo((IPosition) ball);
      }
      else
      {
        Dictionary<Player, float> dictionary1 = new Dictionary<Player, float>();
        dictionary1.Add(myTeam.GetRD(), player.GetDistanceTo((IPosition) myTeam.GetRD()));
        dictionary1.Add(myTeam.GetLF(), player.GetDistanceTo((IPosition) myTeam.GetLF()));
        dictionary1.Add(myTeam.GetRF(), player.GetDistanceTo((IPosition) myTeam.GetRF()));
        Dictionary<Player, float> dictionary2 = dictionary1;
        player.ActionGo((IPosition) ((IEnumerable<KeyValuePair<Player, float>>) ((IEnumerable<KeyValuePair<Player, float>>) dictionary2).OrderBy<KeyValuePair<Player, float>, float>((Func<KeyValuePair<Player, float>, float>) (r => r.Value))).Last<KeyValuePair<Player, float>>().Key);
      }
    }
  }
}
