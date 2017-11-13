// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.VectorExtensions
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
  public static class VectorExtensions
  {
    public static double GetAngle(this Vector v)
    {
      // ISSUE: explicit reference operation
      if ((double) ((Vector) @v).Length == 0.0)
        return double.NaN;
      return Math.Atan2((double) v.X, (double) v.Y);
    }

    public static double GetAngle(this Vector v0, Vector v)
    {
      // ISSUE: explicit reference operation
      if ((double) ((Vector) @v0).Length == 0.0)
        return double.NaN;
      return Math.Atan2((double) (v0.X - v.X), (double) (v0.Y - v.Y));
    }

    public static double GetAngle(this Vector v0, IPosition pos)
    {
      return v0.GetAngle(pos.Position);
    }
  }
}
