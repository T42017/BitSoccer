// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.ShootOnGoalStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class ShootOnGoalStrategy : PlayerStrategyBase
  {
    public ShootOnGoalStrategy(List<IPlayerStrategy> strategies)
      : base(strategies)
    {
    }

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (!base.HasAction(player, myTeam, enemyTeam, ball, matchInfo) || !(ball.Owner == player))
        return false;
      Player player1 = player;
      Rectangle enemyGoal = (Rectangle) Field.EnemyGoal;
      Vector center = enemyGoal.Center;
      return (double) player1.GetDistanceTo((IPosition) center) < 400.0;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      Rectangle enemyGoal1 = (Rectangle) Field.EnemyGoal;
      // ISSUE: explicit reference operation
      float num1 = (float) (((Rectangle) @enemyGoal1).Top.Y + 15.0);
      Rectangle enemyGoal2 = (Rectangle) Field.EnemyGoal;
      // ISSUE: explicit reference operation
      float num2 = (float) (((Rectangle) @enemyGoal2).Bottom.Y - 15.0);
      Rectangle enemyGoal3 = (Rectangle) Field.EnemyGoal;
      // ISSUE: explicit reference operation
      float y = (float) ((Rectangle) @enemyGoal3).Center.Y;
      float[] numArray = new float[3]{ num1, y, num2 };
      int index = this.Rnd.Next(0, 3);
      Vector vector = new Vector((float) ((Rectangle) Field.EnemyGoal).X, numArray[index]);
      player.ActionShoot((IPosition) (object) vector);
    }
  }
}
