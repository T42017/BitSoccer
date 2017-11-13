// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.PlayerStrategyBase
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public abstract class PlayerStrategyBase : IPlayerStrategy
  {
    protected PlayerStrategyBase(List<IPlayerStrategy> strategies)
    {
      this.Strategies = strategies;
      this.Rnd = new Random(17);
      this.PlayerTypes = new PlayerType[6]
      {
        (PlayerType) 1,
        (PlayerType) 2,
        (PlayerType) 3,
        (PlayerType) 4,
        (PlayerType) 6,
        (PlayerType) 5
      };
    }

    public AnalyticsStrategy Analytics
    {
      get
      {
        return this.Get<AnalyticsStrategy>();
      }
    }

    public Random Rnd { get; protected set; }

    public PlayerType[] PlayerTypes { get; protected set; }

    public List<IPlayerStrategy> Strategies { get; protected set; }

    public T Get<T>() where T : IPlayerStrategy
    {
      return (T) this.Strategies.First<IPlayerStrategy>((Func<IPlayerStrategy, bool>) (s => s.GetType() == typeof (T)));
    }

    public virtual bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      return ((IEnumerable<PlayerType>) this.PlayerTypes).Contains<PlayerType>(player.PlayerType);
    }

    public virtual int GetScore(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      return 0;
    }

    public abstract void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo);

    public Player GetSavestForwardPlayer(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      Dictionary<Player, float> dictionary = new Dictionary<Player, float>();
      using (List<Player>.Enumerator enumerator1 = myTeam.Players.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          Player current1 = enumerator1.Current;
          if (!(player == current1) && !current1.IsKeeper() && (current1.Position.X >= player.Position.X && (double) player.GetDistanceTo((IPosition) current1) >= 50.0))
          {
            dictionary[current1] = float.MaxValue;
            using (List<Player>.Enumerator enumerator2 = enemyTeam.Players.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                Player current2 = enumerator2.Current;
                float num = (player.GetDistanceTo((IPosition) current2) + current1.GetDistanceTo((IPosition) current2)) / player.GetDistanceTo((IPosition) current1);
                if ((double) num < (double) dictionary[current1])
                  dictionary[current1] = num;
              }
            }
          }
        }
      }
      if (dictionary.Count != 0)
        return ((IEnumerable<KeyValuePair<Player, float>>) ((IEnumerable<KeyValuePair<Player, float>>) dictionary).OrderByDescending<KeyValuePair<Player, float>, float>((Func<KeyValuePair<Player, float>, float>) (c => c.Value))).First<KeyValuePair<Player, float>>().Key;
      return (Player) null;
    }

    public void ActionForwardPass(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      Player savestForwardPlayer = this.GetSavestForwardPlayer(player, myTeam, enemyTeam, ball, matchInfo);
      if ((savestForwardPlayer == null))
      {
        player.ActionShootGoal();
      }
      else
      {
        float num = 7.5f;
        player.ActionShoot((IPosition) savestForwardPlayer, num);
      }
    }

    public void ActionAwayFromOpponent(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      List<Vector> list = ((IEnumerable<Player>) enemyTeam.Players).Select<Player, Vector>((Func<Player, Vector>) (p => p.Position)).ToList<Vector>();
      float x = (float) player.Position.X;
      float y1 = (float) player.Position.Y;
      List<Vector> vectorList1 = list;
      List<Vector> vectorList2 = new List<Vector>();
      List<Vector> vectorList3 = vectorList2;
      double num1 = (double) x;
      Rectangle borders1 = (Rectangle) Field.Borders;
      // ISSUE: explicit reference operation
      // ISSUE: variable of the null type
      var y2 = ((Rectangle) @borders1).Top.Y;
      Vector vector1 = new Vector((float) num1, (float) y2);
      vectorList3.Add(vector1);
      List<Vector> vectorList4 = vectorList2;
      double num2 = (double) x;
      Rectangle borders2 = (Rectangle) Field.Borders;
      // ISSUE: explicit reference operation
      // ISSUE: variable of the null type
      var y3 = ((Rectangle) @borders2).Bottom.Y;
      Vector vector2 = new Vector((float) num2, (float) y3);
      vectorList4.Add(vector2);
      List<Vector> vectorList5 = vectorList2;
      Rectangle goal = (Rectangle) Field.MyGoal;
      // ISSUE: explicit reference operation
      Vector vector3 = new Vector((float) ((Rectangle) @goal).Center.X, y1);
      vectorList5.Add(vector3);
      List<Vector> vectorList6 = vectorList2;
      Rectangle enemyGoal = (Rectangle) Field.EnemyGoal;
      // ISSUE: explicit reference operation
      Vector vector4 = new Vector((float) ((Rectangle) @enemyGoal).Center.X, y1);
      vectorList6.Add(vector4);
      List<Vector> vectorList7 = vectorList2;
      vectorList1.AddRange((IEnumerable<Vector>) vectorList7);
      // ISSUE: explicit reference operation
      Vector vector5 = ((IEnumerable<Vector>) ((IEnumerable<Vector>) list).OrderBy<Vector, float>((Func<Vector, float>) (l => ((Vector) @l).GetDistanceTo((IPosition) player)))).First<Vector>();
      // ISSUE: explicit reference operation
      Vector vector6 = (player.Position - ((Vector) @vector5).Position);
      Vector vector7 = (player.Position + vector6);
      player.ActionGo((IPosition) (object) vector7);
    }
  }
}
