// Decompiled with JetBrains decompiler
// Type: Common.Field
// Assembly: Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29030A24-AB57-4350-A3CB-B2D86A295459
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\Common.dll

namespace Common
{
  /// <summary>
  /// Field contains information on the size of the game field, and where the goals are.
  /// 
  /// </summary>
  public static class Field
  {
    /// <summary>
    /// A rectangle which's borders is the borders of the playing field
    /// 
    /// </summary>
    public static readonly Rectangle Borders = new Rectangle(0.0f, 0.0f, 1920f, 1080f);
    /// <summary>
    /// A rectangle with width 0 that is situated at the enemy goal.
    /// 
    /// </summary>
    public static readonly Rectangle EnemyGoal = new Rectangle(Field.Borders.Right.X, 383f, 0.0f, 312f);
    /// <summary>
    /// A rectangle with width 0 that is situated at the your goal.
    /// 
    /// </summary>
    public static readonly Rectangle MyGoal = new Rectangle(Field.Borders.Left.X, 383f, 0.0f, 312f);
  }
}
