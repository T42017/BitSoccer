// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.LiberoStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
    public class LiberoStrategy : PlayerStrategyBase
    {
        public const float DistanceToGoal = 450f;

        public LiberoStrategy(List<IPlayerStrategy> strategies)
            : base(strategies)
        {
            this.PlayerTypes = new PlayerType[1] {(PlayerType) 2};
        }

        public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
        {
            return base.HasAction(player, myTeam, enemyTeam, ball, matchInfo);
        }

        public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
        {
            if ((ball.Owner == player))
            {
                Player player1 = player;
                Rectangle goal = (Rectangle) Field.MyGoal;
                // ISSUE: explicit reference operation
                Vector center = goal.Center;
                if ((double) player1.GetDistanceTo((IPosition) center) < 450.0)
                    this.Get<BallOwnerStrategy>().Action(player, myTeam, enemyTeam, ball, matchInfo);
                else
                    this.ActionForwardPass(player, myTeam, enemyTeam, ball, matchInfo);
            }
            else
            {
                Player player1 = player;
                Rectangle goal1 = (Rectangle) Field.MyGoal;
                // ISSUE: explicit reference operation
                Vector center1 = goal1.Center;
                if ((double) player1.GetDistanceTo((IPosition) center1) > 450.0)
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
        }
    }
}
