// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.DefenderStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
    public class DefenderStrategy : PlayerStrategyBase
    {
        public DefenderStrategy(List<IPlayerStrategy> strategies)
            : base(strategies)
        {
            this.PlayerTypes = new PlayerType[1] {(PlayerType) 3};
        }

        public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
        {
            if (base.HasAction(player, myTeam, enemyTeam, ball, matchInfo))
                return (ball.Owner != player);
            return false;
        }

        public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
        {
            if (!this.Analytics.OwnHasBall)
            {
                if (this.Analytics.EnemyHasBall)
                {
                    Player player1 = player;
                    Rectangle goal1 = (Rectangle) Field.MyGoal;
                    // ISSUE: explicit reference operation
                    Vector center1 = goal1.Center;
                    double distanceTo1 = (double) player1.GetDistanceTo((IPosition) center1);
                    Ball ball1 = ball;
                    Rectangle goal2 = (Rectangle) Field.MyGoal;
                    // ISSUE: explicit reference operation
                    Vector center2 = goal2.Center;
                    double distanceTo2 = (double) ball1.GetDistanceTo((IPosition) center2);
                    if (distanceTo1 > distanceTo2)
                        goto label_3;
                }
                player.ActionGo((IPosition) ball);
                return;
            }
            label_3:
            player.ActionGo(
                (IPosition) ((IEnumerable<Player>) ((IEnumerable<Player>) enemyTeam.Players).OrderBy<Player, float>(
                    (Func<Player, float>) (enemy =>
                    {
                        Player player1 = enemy;
                        Rectangle goal = (Rectangle) Field.MyGoal;
                        // ISSUE: explicit reference operation
                        Vector center = goal.Center;
                        return player1.GetDistanceTo((IPosition) center);
                    }))).First<Player>());
        }
    }
}
