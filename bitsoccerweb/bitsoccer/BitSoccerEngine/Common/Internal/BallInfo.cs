using Common;
using System;
using System.Runtime.CompilerServices;

[Serializable]
public class BallInfo
{

    #region Constructors 

    public BallInfo()
    {
        Position = new Vector(Field.Borders.Width / 2f, Field.Borders.Height / 2f);
        Speed = Vector.Zero;
        Owner = null;
        Timer = 0;
    }

    public BallInfo(Vector position) : this()
    {
        Position = position;
    }

    public BallInfo(Vector position, Vector speed, PlayerInfo player, int timer)
    {
        Position = position;
        Speed = speed;
        Owner = player;
        Timer = timer;
    }

    #endregion 

    #region Public Members

    public Ball CopyBall()
    {
        return new Ball(this.Position, Speed, Owner, Timer);
    }

    public BallInfo CopyBallInfo()
    {
        if (PlayerInfo.ArePlayersEqual(Owner, null))
            return new BallInfo(new Vector(Field.Borders.Width - this.Position.X, Field.Borders.Height - this.Position.Y), -Speed, (PlayerInfo)null, Timer);
        else
            return new BallInfo(new Vector(Field.Borders.Width - this.Position.X, Field.Borders.Height - this.Position.Y), -Speed, Owner.GetPlayerInfo1(), Timer);
    }

    public void Reset()
    {
        this.Position = new Vector(Field.Borders.Width / 2f, Field.Borders.Height / 2f);
        Speed = new Vector();
        Owner = null;
        Speed = new Vector(0.0f, 0.0f);
        Timer = 0;
    }

    public int Update()
    {
        int num = -1;
        if (Timer > 0)
            --Timer;
        if (PlayerInfo.ArePlayersEqual(Owner, (PlayerInfo)null))
        {
            Position += Speed;
            Speed *= Constants.BallSlowDownFactor;
            if ((double)Position.X < (double)Field.Borders.Left.X)
            {
                if ((double)Field.Borders.Height / 2.8 < (double)this.Position.Y && (double)this.Position.Y < (double)Field.Borders.Height / 1.555)
                    num = 1;
                Position = new Vector(Field.Borders.Left.X, Position.Y);
                Speed = new Vector(-Speed.X, Speed.Y);
            }
            if ((double)this.Position.X > (double)Field.Borders.Right.X)
            {
                if ((double)Field.Borders.Height / 2.8 < (double)this.Position.Y && (double)this.Position.Y < (double)Field.Borders.Height / 1.555)
                    num = 0;
                Position = new Vector(Field.Borders.Right.X, Position.Y);
                Speed = new Vector(-Speed.X, Speed.Y);
            }
            if ((double)this.Position.Y < (double)Field.Borders.Top.Y)
            {
                Position = new Vector(Position.X, Field.Borders.Top.Y);
                Speed = new Vector(Speed.X, -Speed.Y);
            }
            if ((double)this.Position.Y > (double)Field.Borders.Bottom.Y)
            {
                Position = new Vector(Position.X, Field.Borders.Bottom.Y);
                Speed = new Vector(Speed.X, -Speed.Y);
            }
        }
        else
        {
            Owner.Reset();
            this.Position = Owner.GetPosition();
            Speed = Owner.GetSpeed();
        }
        return num;
    }

    public void GrabBall(PlayerInfo player)
    {
        if ((double)Vector.Distance(this.Position, player.GetPosition()) >= (double)Constants.BallMaxPickUpDistance || 
            !PlayerInfo.ArePlayersEqual(Owner, null) || Timer != 0)
            return;
        Owner = player;
        if (Owner.y())
            Owner.GetPlayerInfo().SetFallenTimer(-Constants.BallPickUpImmunityTimer);
        else
            Owner.SetFallenTimer(-Constants.BallPickUpImmunityTimer);
        this.Update();
        Speed = Vector.Zero;
    }

    public void DropBall(PlayerInfo player)
    {
        if (!PlayerInfo.ArePlayersEqual(player, Owner))
            return;

        Speed = (Owner.GetSpeed());
        Owner = null;
        Timer = Constants.BallTackleTimer;
    }

    public void ShootBall(PlayerInfo playerInfo, Vector direction)
    {
        this.ShootBall(playerInfo, direction, Constants.PlayerMaxShootStr);
    }

    public void ShootBall(PlayerInfo playerInfo, Vector A_1, float A_2)
    {
        if (PlayerInfo.ArePlayersEqual(Owner, null) || Owner.PlayerIndex() != playerInfo.PlayerIndex())
            return;
        if (playerInfo.y())
            A_1 = new Vector(Field.Borders.Width - A_1.X, Field.Borders.Height - A_1.Y);
        Timer = Constants.BallShootTimer;
        Vector vector = A_1 - Position;
        if ((double)vector.Length != 0.0)
            vector.Normalize();
        A_2 = Math.Max(0.0f, Math.Min(Constants.PlayerMaxShootStr, A_2));
        //Random random = new Random();
        //double num1 = Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) * Math.Sin(2.0 * Math.PI * random.NextDouble());
        double num1 = Math.Sqrt(-2.0 * Math.Log(Global.Random.NextDouble())) * Math.Sin(2.0 * Math.PI * Global.Random.NextDouble());
        double num2 = (double)A_2 / (double)Constants.PlayerMaxShootStr * (double)Constants.BallMaxStrStd * num1;
        vector.X = (float)((double)vector.X * Math.Cos(num2) - (double)vector.Y * Math.Sin(num2));
        vector.Y = (float)((double)vector.X * Math.Sin(num2) + (double)vector.Y * Math.Cos(num2));
        Speed = vector * A_2 * Constants.BallMaxVelocity / Constants.PlayerMaxShootStr;
        Owner = (PlayerInfo)null;
    }

    #endregion 

    public Vector Position { get; set; }
    public Vector Speed { get; set; }
    public PlayerInfo Owner { get; set; }
    public int Timer { get; private set; }

}
