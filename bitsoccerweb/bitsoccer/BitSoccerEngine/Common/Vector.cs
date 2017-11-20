using System;
using System.Globalization;

namespace Common
{
    /// <summary>
    /// The vector class. Used to specify a position or direction. A vector is given by a pair of floats (X,Y).
    /// 
    /// </summary>
    [Serializable]
    public struct Vector : IPosition
    {
        /// <summary>
        /// X value of vector.
        /// 
        /// </summary>
        public float X;
        /// <summary>
        /// Y value of vector.
        /// 
        /// </summary>
        public float Y;

        /// <summary>
        /// Returns a unit vector in the X direction (1,0).
        /// 
        /// </summary>
        public static Vector UnitX
        {
            get
            {
                return new Vector(1f, 0.0f);
            }
        }

        /// <summary>
        /// Returns a unit vector in the Y direction (0,1).
        /// 
        /// </summary>
        public static Vector UnitY
        {
            get
            {
                return new Vector(0.0f, 1f);
            }
        }

        /// <summary>
        /// Returns the zero vector (0,0).
        /// 
        /// </summary>
        public static Vector Zero
        {
            get
            {
                return new Vector(0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets the length of this vector, sqrt(X^2+Y^2)
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt((double)this.LengthSquared);
            }
        }

        /// <summary>
        /// Gets the squared length of this vector, X^2+Y^2.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public float LengthSquared
        {
            get
            {
                return (float)((double)this.X * (double)this.X + (double)this.Y * (double)this.Y);
            }
        }

        /// <summary>
        /// Returns this vector.
        /// 
        /// </summary>
        public Vector Position
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Returns vector with specified coordinates.
        /// </summary>
        /// <param name="x">X coordinate of new vector.</param><param name="y">Y coordinate of new vector.</param>
        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Returns vector with specified coordinates. If double input is given the input is converted to floats.
        /// </summary>
        /// <param name="x">X coordinate of new vector.</param><param name="y">Y coordinate of new vector.</param>
        public Vector(double x, double y)
        {
            this = new Vector((float)x, (float)y);
        }

        /// <summary>
        /// Returns vector that is the pointwise sum of the two vectors.
        /// </summary>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        /// <summary>
        /// Returns the negative of the vector. (-this.X,-this.Y)
        /// </summary>
        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector.X, -vector.Y);
        }

        /// <summary/>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns/>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        /// <summary/>
        /// <param name="scaleFactor"/><param name="vector2"/>
        /// <returns/>
        public static Vector operator *(float scaleFactor, Vector vector2)
        {
            return new Vector(scaleFactor * vector2.X, scaleFactor * vector2.Y);
        }

        /// <summary/>
        /// <param name="scaleFactor"/><param name="vector2"/>
        /// <returns/>
        public static Vector operator *(double scaleFactor, Vector vector2)
        {
            return new Vector((float)scaleFactor * vector2.X, (float)scaleFactor * vector2.Y);
        }

        /// <summary/>
        /// <param name="vector1"/><param name="scaleFactor"/>
        /// <returns/>
        public static Vector operator *(Vector vector1, float scaleFactor)
        {
            return new Vector(scaleFactor * vector1.X, scaleFactor * vector1.Y);
        }

        /// <summary/>
        /// <param name="vector1"/><param name="scaleFactor"/>
        /// <returns/>
        public static Vector operator *(Vector vector1, double scaleFactor)
        {
            return new Vector((float)scaleFactor * vector1.X, (float)scaleFactor * vector1.Y);
        }

        /// <summary/>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns/>
        public static Vector operator *(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X * vector2.X, vector1.Y * vector2.Y);
        }

        /// <summary/>
        /// <param name="vector1"/><param name="divider"/>
        /// <returns/>
        public static Vector operator /(Vector vector1, float divider)
        {
            if ((double)divider != 0.0)
                return new Vector(vector1.X / divider, vector1.Y / divider);
            else
                throw new DivideByZeroException();
        }

        /// <summary>
        /// Checks two vectors for equality.
        /// 
        /// </summary>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns/>
        public static bool operator ==(Vector vector1, Vector vector2)
        {
            if ((double)vector1.X == (double)vector2.X)
                return (double)vector1.Y == (double)vector2.Y;
            else
                return false;
        }

        /// <summary>
        /// Checks two vectors for inequality.
        /// 
        /// </summary>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns/>
        public static bool operator !=(Vector vector1, Vector vector2)
        {
            if ((double)vector1.X == (double)vector2.X)
                return (double)vector1.Y != (double)vector2.Y;
            else
                return true;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// 
        /// </summary>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns>
        /// (vector1 - vector2).Length
        /// </returns>
        public static float Distance(Vector vector1, Vector vector2)
        {
            return (vector1 - vector2).Length;
        }

        /// <summary>
        /// Returns the distance between two positions.
        /// 
        /// </summary>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns>
        /// (vector1 - vector2).Length
        /// </returns>
        public static float Distance(IPosition obj1, IPosition obj2)
        {
            return Vector.Distance(obj1.Position, obj2.Position);
        }

        /// <summary>
        /// Returns the dot product of this vector and input vector.
        /// 
        /// </summary>
        /// <param name="vector"/>
        /// <returns>
        /// this.X * vector.X + this.Y * vector.Y
        /// </returns>
        public float Dot(Vector vector)
        {
            return (float)((double)this.X * (double)vector.X + (double)this.Y * (double)vector.Y);
        }

        /// <summary/>
        /// <param name="vector1"/><param name="vector2"/>
        /// <returns/>
        public static float Dot(Vector vector1, Vector vector2)
        {
            return (float)((double)vector1.X * (double)vector2.X + (double)vector1.Y * (double)vector2.Y);
        }

        /// <summary>
        /// Normalizes the length of this vector to 1. If this is the zero vector, does nothing.
        /// 
        /// </summary>
        public void Normalize()
        {
            float length = this.Length;
            if ((double)length == 0.0)
                return;
            this.X = this.X / length;
            this.Y = this.Y / length;
        }

        /// <summary>
        /// Rotates the vector.
        /// 
        /// </summary>
        /// <param name="theta">The rotation angle, in radians.</param>
        public void Rotate(float theta)
        {
            this = new Vector(Math.Cos((double)theta) * (double)this.X - Math.Sin((double)theta) * (double)this.Y, Math.Cos((double)theta) * (double)this.Y + Math.Sin((double)theta) * (double)this.X);
        }

        /// <summary>
        /// Returns a unit vector in the same direction as this vector.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A unit vector
        /// </returns>
        public Vector Unit()
        {
            Vector vector = new Vector(this.X, this.Y);
            vector.Normalize();
            return vector;
        }

        /// <summary>
        /// Returns a unit vector in the same direction as input vector.
        /// 
        /// </summary>
        /// <param name="value"/>
        /// <returns/>
        public static Vector Unit(Vector value)
        {
            if ((double)value.Length != 0.0)
                return new Vector(value.X / value.Length, value.Y / value.Length);
            else
                return Vector.Zero;
        }

        /// <summary>
        /// Gets the distance from ball.pos to a object
        /// </summary>
        public float GetDistanceTo(IPosition obj)
        {
            return Vector.Distance((IPosition)this, obj);
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
                if ((double)(player2.Position - this).Length < (double)(player1.Position - this).Length)
                    player1 = player2;
            }
            return player1;
        }

        public new string ToString()
        {
            return Convert.ToString(this.X, (IFormatProvider)CultureInfo.InvariantCulture) + " " + Convert.ToString(this.Y, (IFormatProvider)CultureInfo.InvariantCulture);
        }
    }
}
