// Decompiled with JetBrains decompiler
// Type: Common.Rectangle
// Assembly: Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29030A24-AB57-4350-A3CB-B2D86A295459
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\Common.dll

using System;

namespace Common
{
  /// <summary>
  /// A rectangle can be used to specify a specific area, such as the whole field, a goal, or your half of the plane.
  /// 
  /// </summary>
  public struct Rectangle : IPosition
  {
    /// <summary>
    /// The height of the rectangle.
    /// 
    /// </summary>
    public float Height;
    /// <summary>
    /// The width of the rectangle.
    /// 
    /// </summary>
    public float Width;
    /// <summary>
    /// X coordinate of the upper left corner of the rectangle.
    /// 
    /// </summary>
    public float X;
    /// <summary>
    /// Y coordinate of the upper left corner of the rectangle.
    /// 
    /// </summary>
    public float Y;

    /// <summary>
    /// Gets the position of the top center of the rectangle.
    /// 
    /// </summary>
    public Vector Top
    {
      get
      {
        return new Vector(this.X + this.Width / 2f, this.Y);
      }
    }

    /// <summary>
    /// Gets the postion of the bottom center of the rectangle.
    /// 
    /// </summary>
    public Vector Bottom
    {
      get
      {
        return new Vector(this.X + this.Width / 2f, this.Y + this.Height);
      }
    }

    /// <summary>
    /// Gets the position of the left center of the rectangle.
    /// 
    /// </summary>
    public Vector Left
    {
      get
      {
        return new Vector(this.X, this.Y + this.Height / 2f);
      }
    }

    /// <summary>
    /// Gets the position of the right center of the rectangle.
    /// 
    /// </summary>
    public Vector Right
    {
      get
      {
        return new Vector(this.X + this.Width, this.Y + this.Height / 2f);
      }
    }

    /// <summary>
    /// Gets the position of the center of the rectangle.
    /// 
    /// </summary>
    public Vector Center
    {
      get
      {
        return new Vector(this.X + this.Width / 2f, this.Y + this.Height / 2f);
      }
    }

    /// <summary>
    /// Returns the center of this rectangle.
    /// 
    /// </summary>
    public Vector Position
    {
      get
      {
        return this.Center;
      }
    }

    /// <summary>
    /// Creates a new rectangle.
    /// 
    /// </summary>
    /// <param name="x">X coordinate of the upper left corner of the rectangle.</param><param name="y">Y coordinate of the upper left corner of the rectangle.</param><param name="width">Width of the rectangle.</param><param name="height">Height of the rectangle.</param>
    public Rectangle(float x, float y, float width, float height)
    {
      this.Height = height;
      this.Width = width;
      this.X = x;
      this.Y = y;
    }

    /// <summary>
    /// Tests two rectangles for inequality.
    /// 
    /// </summary>
    /// <param name="a"/><param name="b"/>
    /// <returns/>
    public static bool operator !=(Rectangle a, Rectangle b)
    {
      return !(a == b);
    }

    /// <summary>
    /// Tests two rectangles for equality.
    /// 
    /// </summary>
    /// <param name="a"/><param name="b"/>
    /// <returns/>
    public static bool operator ==(Rectangle a, Rectangle b)
    {
      return a.Equals(b);
    }

    /// <summary>
    /// Checks of this rectangle contains a vector.
    /// 
    /// </summary>
    /// <param name="vector"/>
    /// <returns/>
    public bool Contains(Vector vector)
    {
      return this.Contains(vector.X, vector.Y);
    }

    /// <summary>
    /// Checks if this rectangle contains another rectangle.
    /// 
    /// </summary>
    /// <param name="rectangle"/>
    /// <returns/>
    public bool Contains(Rectangle rectangle)
    {
      if (this.Contains(rectangle.Left) && this.Contains(rectangle.Right) && this.Contains(rectangle.Top))
        return this.Contains(rectangle.Bottom);
      else
        return false;
    }

    /// <summary>
    /// Checks if this rectangle cointains a point.
    /// 
    /// </summary>
    /// <param name="x">X coordinate of point.</param><param name="y">Y coordinate of point.</param>
    /// <returns/>
    public bool Contains(float x, float y)
    {
      if ((double) this.Left.X <= (double) x && (double) x <= (double) this.Right.X && (double) this.Top.Y <= (double) y)
        return (double) y <= (double) this.Bottom.Y;
      else
        return false;
    }

    /// <summary>
    /// Checks if this rectangle is equal to another rectangle.
    /// 
    /// </summary>
    /// <param name="other"/>
    /// <returns/>
    public bool Equals(Rectangle other)
    {
      if ((double) other.X == (double) this.X && (double) other.Y == (double) this.Y && (double) other.Height == (double) this.Height)
        return (double) other.Width == (double) this.Width;
      else
        return false;
    }

    /// <summary>
    /// Inflates the rectangle by a specified ammount.
    /// 
    /// </summary>
    /// <param name="horizontalAmount"/><param name="verticalAmount"/>
    public void Inflate(float horizontalAmount, float verticalAmount)
    {
      this.Width += horizontalAmount;
      this.Height += verticalAmount;
    }

    /// <summary>
    /// Gets the shortest distance from the rectangle to object.
    /// </summary>
    public float GetDistanceTo(IPosition obj)
    {
      float num1 = this.X + this.Width;
      float num2 = this.Y + this.Height;
      float num3 = (double) obj.Position.X < (double) this.X ? this.X - obj.Position.X : obj.Position.X - num1;
      float num4 = (double) obj.Position.Y < (double) this.Y ? this.Y - obj.Position.Y : obj.Position.Y - num2;
      float num5 = (double) num3 < 0.0 ? 0.0f : num3;
      float num6 = (double) num4 < 0.0 ? 0.0f : num4;
      return (float) Math.Sqrt((double) num5 * (double) num5 + (double) num6 * (double) num6);
    }

    /// <summary>
    /// Gets the closest player in a team.
    /// 
    /// </summary>
    /// <param name="team">Team to check, either your or the enemy team.</param>
    /// <returns/>
    public Player GetClosest(Team team)
    {
      Player player1 = team.Players[0];
      foreach (Player player2 in team.Players)
      {
        if ((double) this.GetDistanceTo((IPosition) player2) < (double) this.GetDistanceTo((IPosition) player1))
          player1 = player2;
      }
      return player1;
    }
  }
}
