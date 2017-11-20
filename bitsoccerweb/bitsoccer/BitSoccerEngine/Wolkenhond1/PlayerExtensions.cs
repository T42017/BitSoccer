// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.PlayerExtensions
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public static class PlayerExtensions
  {
    public static bool IsKeeper(this Player p)
    {
      return (int)p.PlayerType == 1;
    }

    public static bool IsDefender(this Player p)
    {
      if ((int)p.PlayerType != 2)
        return (int)p.PlayerType == 3;
      return true;
    }

    public static bool IsForward(this Player p)
    {
      if ((int)p.PlayerType != 5 && (int)p.PlayerType != 4)
        return (int)p.PlayerType == 6;
      return true;
    }

    public static bool IsAttacker(this Player p)
    {
      if ((int)p.PlayerType != 4)
        return (int)p.PlayerType == 6;
      return true;
    }

    public static bool IsMidfielder(this Player p)
    {
      return (int)p.PlayerType == 5;
    }

    public static bool IsLeftWing(this Player p)
    {
      if ((int)p.PlayerType != 2)
        return (int)p.PlayerType == 4;
      return true;
    }

    public static bool IsRightWing(this Player p)
    {
      if ((int)p.PlayerType != 3)
        return (int)p.PlayerType == 6;
      return true;
    }

    public static bool IsOnOwnHalf(this Player p)
    {
      // ISSUE: variable of the null type
      var x1 = p.Position.X;
      Rectangle borders = (Rectangle) Field.Borders;
      // ISSUE: explicit reference operation
      // ISSUE: variable of the null type
      var  x2 = borders.Center.X;
      return x1 <= x2;
    }

    public static bool IsOnEnemyHalf(this Player p)
    {
      return !p.IsOnOwnHalf();
    }
  }
}
