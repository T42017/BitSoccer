using System;
using System.Linq;

namespace Common
{
    /// <summary>
    /// The player object cointains all information about the players visible on the field.
    /// 
    /// </summary>
    [Serializable]
    public class Player : IPosition
    {
        private readonly int _id;
        private int _tackleTimer;
        private readonly Team _team;
        private readonly Vector _velocity;
        private int _fallenTimer;
        private PlayerType _playerType;
        private Vector _pos;
        private PlayerInfo _parent;

        /// <summary>
        /// Gets the position of the player.
        /// </summary>
        public Vector Position
        {
            get
            {
                return this._pos;
            }
        }

        /// <summary>
        /// Gets the velocity of the player.
        /// </summary>
        public Vector Velocity
        {
            get
            {
                return this._velocity;
            }
        }

        /// <summary>
        /// Gets the players team.
        /// </summary>
        public Team Team
        {
            get
            {
                return this._team;
            }
        }

        /// <summary>
        /// Gets what type the player is.
        /// </summary>
        public PlayerType PlayerType
        {
            get
            {
                return this._playerType;
            }
        }

        /// <summary>
        /// Gets the players fallen timer. The number of rounds remaining untill the player can preform any action.
        /// </summary>
        public int FallenTimer
        {
            get
            {
                return this._fallenTimer;
            }
        }

        /// <summary>
        /// Gets the players tackle timer. How long before the player can tackle again.
        /// </summary>
        public int TackleTimer
        {
            get
            {
                return this._tackleTimer;
            }
        }

        public Player(int id, Vector pos, Vector velocity, int fallenTimer, int tackleTimer, Team team, PlayerType playerType, PlayerInfo parent)
        {
            this._pos = pos;
            this._velocity = velocity;
            this._id = id;
            this._fallenTimer = fallenTimer;
            this._tackleTimer = tackleTimer;
            this._team = team;
            this._playerType = playerType;
            this._parent = parent;
        }

        /// <summary>
        /// To which position you want you player to move to, with the default power (max)
        /// 
        /// </summary>
        /// <param name="destination"/>
        public void ActionGo(IPosition destination)
        {
            if (!this.OkInput(destination.Position) || PlayerInfo.ArePlayersEqual(this._parent, null))
                throw new ArgumentException("Illegal input");
            this._parent.SetKeeper(destination.Position);
        }

        /// <summary>
        /// Shoots towards the specific location with default power (max)
        /// 
        /// </summary>
        /// <param name="shootDestination"/>
        public void ActionShoot(IPosition shootDestination)
        {
            this.ActionShoot(shootDestination, Constants.PlayerMaxShootStr);
        }

        /// <summary>
        /// Shoots towards the specific location with specified power
        /// 
        /// </summary>
        /// <param name="shootDestination"/><param name="shootPower"/>
        public void ActionShoot(IPosition shootDestination, float shootPower)
        {
            if (!this.OkInput(shootDestination.Position) || !this.OkInput(shootPower) || PlayerInfo.ArePlayersEqual(_parent, null))
                throw new ArgumentException("Illegal input");
            this._parent.SetLeftDefender(shootDestination.Position, shootPower);
        }

        /// <summary>
        /// Shoots towards the enemy goal with default power (max)
        /// 
        /// </summary>
        public void ActionShootGoal()
        {
            try
            {
                this.ActionShoot((IPosition)Field.EnemyGoal.Center, Constants.PlayerMaxShootStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Shoots towards the enemy goal with specified power
        /// 
        /// </summary>
        /// <param name="shootPower"/>
        public void ActionShootGoal(float shootPower)
        {
            try
            {
                this.ActionShoot((IPosition)Field.EnemyGoal.Center, shootPower);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tackles the player.
        ///             You don't always win!
        /// 
        /// </summary>
        /// <param name="target">The specific player you want to tackle</param>
        public void ActionTackle(Player target)
        {
            if (!this.OkInput(target) || !(target.Team != this.Team) || PlayerInfo.ArePlayersEqual(this._parent, (PlayerInfo)null))
                throw new ArgumentException("Illegal input");
            this._parent.SetActionTackle(target);
        }

        /// <summary>
        /// Picks upp the ball.
        /// 
        /// </summary>
        public void ActionPickUpBall()
        {
            this._parent.SetActionPickUpBall();
        }

        /// <summary>
        /// Drops the ball if you have it
        /// 
        /// </summary>
        public void ActionDropBall()
        {
            this._parent.SetActionDropBall();
        }

        /// <summary>
        /// The player waits for one round (does nothing).
        /// 
        /// </summary>
        public void ActionWait()
        {
            this._parent.SetRightForward();
        }

        private bool OkInput(Vector vector)
        {
            return !float.IsNaN(vector.X) && !float.IsNaN(vector.Y) && (!float.IsInfinity(vector.X) && !float.IsInfinity(vector.Y));
        }

        private bool OkInput(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }

        private bool OkInput(Player player)
        {
            return !(player == (Player)null) && this.OkInput(player.Position) && this.OkInput(player.Velocity);
        }

        /// <summary>
        /// Checks if the player can tackle another player.
        /// </summary>
        public bool CanTackle(Player target)
        {
            if (target != (Player)null && target.Team != this.Team && ((double)Vector.Distance(this.Position, target.Position) < (double)Constants.PlayerMaxTackleDistance && target._fallenTimer == 0))
                return this._tackleTimer == 0;
            else
                return false;
        }

        /// <summary>
        /// Checks if the player can pick up the ball.
        /// </summary>
        public bool CanPickUpBall(Ball target)
        {
            if ((double)Vector.Distance(this.Position, target.Position) < (double)Constants.BallMaxPickUpDistance && this.FallenTimer == 0 && target.Owner == (Player)null)
                return target.PickUpTimer == 0;
            else
                return false;
        }

        /// <summary>
        /// Gets the closest player in the specified team. Excluding calling player.
        /// </summary>
        public Player GetClosest(Team team)
        {
            Player closest = team.Players.First(p => p != this);
            foreach (Player player in team.Players)
            {
                if ((double)(player._pos - this._pos).Length < (double)(closest._pos - this._pos).Length 
                    && player != this)
                    closest = player;
            }
            return closest;
        }

        public float GetDistanceTo(IPosition pos)
        {
            return Vector.Distance((IPosition)this, pos);
        }

        /// <summary>
        /// Tests if this player is equal to a object.
        /// 
        /// </summary>
        /// <param name="obj"/>
        /// <returns/>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Player player = obj as Player;
            return player != null && this._id == player._id;
        }

        /// <summary>
        /// Tests if this player is equal to another player.
        /// 
        /// </summary>
        /// <param name="p"/>
        /// <returns/>
        public bool Equals(Player p)
        {
            return p != null && this._id == p._id;
        }

        /// <summary>
        /// Tests two players for equality.
        /// 
        /// </summary>
        /// <param name="a"/><param name="b"/>
        /// <returns/>
        public static bool operator ==(Player a, Player b)
        {
            return object.ReferenceEquals(a, b) || ((object)a != null && (object)b != null && a._id == b._id);
        }

        /// <summary>
        /// Tests two players for inequality.
        /// 
        /// </summary>
        /// <param name="a"/><param name="b"/>
        /// <returns/>
        public static bool operator !=(Player a, Player b)
        {
            return !(a == b);
        }

    }
}
