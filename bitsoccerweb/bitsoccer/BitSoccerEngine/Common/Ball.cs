using System;

namespace Common
{
    /// <summary>
    /// The ball object contains all information about the ball. Where it is, and where it is going.
    /// 
    /// </summary>
    [Serializable]
    public class Ball : IPosition
    {
        private Player _owner;
        private int _pickUpTimer;
        private Vector _pos;
        private Vector _velocity;

        /// <summary>
        /// Gets the current position of the ball.
        /// </summary>
        public Vector Position
        {
            get
            {
                return this._pos;
            }
        }

        /// <summary>
        /// Gets the current velocity of the ball.
        /// </summary>
        public Vector Velocity
        {
            get
            {
                return this._velocity;
            }
        }

        /// <summary>
        /// Gets the balls pick up timer. How many rounds before it can be picked up again.
        /// </summary>
        public int PickUpTimer
        {
            get
            {
                return this._pickUpTimer;
            }
        }

        /// <summary>
        /// Gets the balls current owner. Returns null if it has no owner.
        /// </summary>
        public Player Owner
        {
            get
            {
                return this._owner;
            }
        }

        public Ball(Vector pos, Vector velocity, PlayerInfo owner, int pickUpTimer)
        {
            this._pos = pos;
            this._velocity = velocity;
            this._owner = PlayerInfo.ArePlayersEqual(owner, null) ? null : owner.GetPlayer1();
            this._pickUpTimer = pickUpTimer;
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
                if ((double)(player2.Position - this._pos).Length < (double)(player1.Position - this._pos).Length)
                    player1 = player2;
            }
            return player1;
        }

        /// <summary>
        /// Check if the ball can be picked up by a player.
        /// </summary>
        public bool CanBePickedUpBy(Player p)
        {
            if (this._owner == (Player)null && (double)Vector.Distance(p.Position, this.Position) < (double)Constants.BallMaxPickUpDistance && this.PickUpTimer == 0)
                return p.FallenTimer == 0;
            else
                return false;
        }
    }
}
