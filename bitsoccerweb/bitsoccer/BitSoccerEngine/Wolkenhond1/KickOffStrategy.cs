// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.KickOffStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System.Collections.Generic;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public class KickOffStrategy : PlayerStrategyBase
  {
    public static readonly Dictionary<PlayerType, IPosition> InitialSetup;

    public KickOffStrategy(List<IPlayerStrategy> strategies)
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

    public bool IsActive { get; protected set; }

    public override bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if ((this.Analytics.LastBallOwner == null))
        this.IsActive = true;
      if (base.HasAction(player, myTeam, enemyTeam, ball, matchInfo))
        return this.IsActive;
      return false;
    }

    public override void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo)
    {
      if (player.CanPickUpBall(ball))
        player.ActionPickUpBall();
      else if ((ball.Owner == player))
      {
        IPosition iposition = KickOffStrategy.InitialSetup[(PlayerType) 3];
        int num = this.Rnd.Next(1000);
        if (num < 400)
          iposition = KickOffStrategy.InitialSetup[(PlayerType) 4];
        else if (num < 800)
          iposition = KickOffStrategy.InitialSetup[(PlayerType) 6];
        player.ActionShoot(iposition, 5.5f);
        this.IsActive = false;
      }
      else
        player.ActionGo(KickOffStrategy.InitialSetup[player.PlayerType]);
    }

    static KickOffStrategy()
    {
      Dictionary<PlayerType, IPosition> dictionary1 = new Dictionary<PlayerType, IPosition>();
      dictionary1.Add((PlayerType) 2, (IPosition) (object) new Vector(300f, 500f));
      dictionary1.Add((PlayerType) 3, (IPosition) (object) new Vector(800f, 580f));
      dictionary1.Add((PlayerType) 4, (IPosition) (object) new Vector(920f, 80f));
      dictionary1.Add((PlayerType) 6, (IPosition) (object) new Vector(920f, 1000f));
      Dictionary<PlayerType, IPosition> dictionary2 = dictionary1;
      int num = 5;
      Rectangle borders = (Rectangle) Field.Borders;
      // ISSUE: explicit reference operation
      Vector center = borders.Center;
      dictionary2.Add((PlayerType) num, (IPosition) center);
      KickOffStrategy.InitialSetup = dictionary1;
    }
  }
}
