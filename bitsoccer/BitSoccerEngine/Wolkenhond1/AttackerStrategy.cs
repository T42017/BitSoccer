// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.AttackerStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
    public class AttackerStrategy : PlayerStrategyBase
    {
        public const float DistanceToMidfielder = 400f;

        public AttackerStrategy(List<IPlayerStrategy> strategies)
            : base(strategies)
        {
            this.PlayerTypes = new PlayerType[2]
            {
                (PlayerType) 4,
                (PlayerType) 6
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
            if (player.IsLeftWing())
            {
                // ISSUE: variable of the null type
                var y1 = player.Position.Y;
                Rectangle borders = (Rectangle) Field.Borders;
                // ISSUE: explicit reference operation
                // ISSUE: variable of the null type
                var y2 = ((Rectangle) @borders).Center.Y;
                if (y1 >= y2)
                    goto label_4;
            }
            if (player.IsRightWing())
            {
                // ISSUE: variable of the null type
                var y1 = player.Position.Y;
                Rectangle borders = (Rectangle) Field.Borders;
                // ISSUE: explicit reference operation
                // ISSUE: variable of the null type
                var y2 = ((Rectangle) @borders).Center.Y;
                if (y1 <= y2)
                    goto label_4;
            }
            if (this.Analytics.EnemyHasBall && (double) ((IEnumerable<Player>) myTeam.Players)
                .Select<Player, float>((Func<Player, float>) (p => p.GetDistanceTo((IPosition) ball)))
                .OrderBy<float, float>((Func<float, float>) (d => d)).Skip<float>(2).First<float>() <=
                (double) player.GetDistanceTo((IPosition) ball))
            {
                player.ActionGo((IPosition) ball);
                return;
            }
            if (this.Analytics.EnemyHasBall)
            {
                Player player1 = player;
                Rectangle goal = (Rectangle) Field.MyGoal;
                // ISSUE: explicit reference operation
                Vector center = ((Rectangle) @goal).Center;
                player1.ActionGo((IPosition) center);
                return;
            }
            if ((double) player.GetDistanceTo((IPosition) myTeam.GetCF()) < 400.0)
            {
                Player player1 = player;
                Vector vector1;
                if (!player.IsLeftWing())
                {
                    Rectangle enemyGoal = (Rectangle) Field.EnemyGoal;
                    // ISSUE: explicit reference operation
                    vector1 = ((Rectangle) @enemyGoal).Bottom;
                }
                else
                {
                    Rectangle enemyGoal = (Rectangle) Field.EnemyGoal;
                    // ISSUE: explicit reference operation
                    vector1 = ((Rectangle) @enemyGoal).Top;
                }
                Vector vector2 = vector1;
                player1.ActionGo((IPosition) vector2);
                return;
            }
            this.ActionAwayFromOpponent(player, myTeam, enemyTeam, ball, matchInfo);
            return;
            label_4:
            int num1 = this.Analytics.EnemyHasBall ? -1 : 1;
            int num2 = player.IsLeftWing() ? -1 : 1;
            Vector vector = new Vector((float) num1, (float) num2);
            player.ActionGo(player.Position + vector);
        }
    }
}
