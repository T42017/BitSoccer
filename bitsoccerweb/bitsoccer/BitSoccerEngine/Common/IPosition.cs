// Decompiled with JetBrains decompiler
// Type: Common.IPosition
// Assembly: Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29030A24-AB57-4350-A3CB-B2D86A295459
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\Common.dll

namespace Common
{
  /// <summary>
  /// A interface used by ball, player, vector and rectangle. Guarantess that the object has a position.
  /// 
  /// </summary>
  public interface IPosition
  {
    /// <summary>
    /// Get the position of the object.
    /// 
    /// </summary>
    Vector Position { get; }

    /// <summary>
    /// Check for the closest player in a team.
    /// 
    /// </summary>
    /// <param name="team">Your team, or enemy team.</param>
    /// <returns/>
    Player GetClosest(Team team);

    /// <summary>
    /// Get distance from this object to another object.
    /// 
    /// </summary>
    /// <param name="obj"/>
    /// <returns/>
    float GetDistanceTo(IPosition obj);
  }
}
