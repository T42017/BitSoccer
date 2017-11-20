// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.IPlayerStrategy
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public interface IPlayerStrategy
  {
    bool HasAction(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo);

    int GetScore(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo);

    void Action(Player player, Team myTeam, Team enemyTeam, Ball ball, MatchInfo matchInfo);
  }
}
