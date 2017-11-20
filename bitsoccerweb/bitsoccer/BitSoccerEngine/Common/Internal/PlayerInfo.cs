using Common;
using System;
using System.Runtime.CompilerServices;

[Serializable]
public class PlayerInfo
{
    private readonly int _playerIndex;
    private readonly PlayerInfo _originalPlayerInfo;
    private readonly TeamInfo _teamInfo;
    private int _fallenTimer;
    private bool _logic;
    private PlayerType _playerType;
    private Vector _position;
    private int _TackleCoolDowntimer;
    private Vector _speed;
    private PlayerActionInfo _playerActionType;
    private Vector _previousPosition;
    private Vector _aim;
    private float _shootPower;
    private Player _player;

    public PlayerInfo(Vector position, TeamInfo teamInfo, PlayerType playerType)
    {
        this._position = new Vector(position.X, position.Y);
        this._speed = Vector.Zero;
        this._previousPosition = position;
        this._teamInfo = teamInfo;
        this._playerType = playerType;
    }

    public PlayerInfo(int playerIndex, Vector position, TeamInfo teamInfo, PlayerType playerType)
    {
        this._position = new Vector(position.X, position.Y);
        this._speed = Vector.Zero;
        this._previousPosition = position;
        this._playerIndex = playerIndex;
        this._teamInfo = teamInfo;
        this._playerType = playerType;
    }

    public PlayerInfo(int playerIndex, Vector position, Vector speed, PlayerInfo playerInfo, int A_4, int A_5, TeamInfo teamInfo, PlayerType type, bool A_8)
    {
        this._position = position;
        this._speed = speed;
        this._originalPlayerInfo = playerInfo;
        this._playerIndex = playerIndex;
        this._fallenTimer = A_4;
        this._TackleCoolDowntimer = A_5;
        this._teamInfo = teamInfo;
        this._playerType = type;
        this._logic = A_8;
    }

    public void Reset()
    {
        if (PlayerInfo.ArePlayersEqual(this._originalPlayerInfo, (PlayerInfo)null))
            return;
        this._position = new Vector(Field.Borders.Width - this._originalPlayerInfo.GetPosition().X, Field.Borders.Height - this._originalPlayerInfo.GetPosition().Y);
        this._speed = -this._originalPlayerInfo.GetSpeed();
        this._fallenTimer = this._originalPlayerInfo.GetFallenTimer();
        this._TackleCoolDowntimer = this._originalPlayerInfo.GetTackleCoolDown();
    }

    public void Update()
    {
        if (_TackleCoolDowntimer > 0)
            _TackleCoolDowntimer--;
        if (_fallenTimer > 0)
        {
            _fallenTimer--;
            _speed = Vector.Zero;
        }
        else
        {
            if (this._fallenTimer < 0)
                ++this._fallenTimer;
            this._speed = this._speed * Constants.PlayerSlowDownFactor;
            this._position += this._speed;
            if (!Field.Borders.Contains(this.GetPosition()))
                this._speed = Vector.Zero;
            if ((double)this._position.X < 0.0)
                this._position.X = 0.0f;
            if ((double)this._position.X > (double)Field.Borders.Width)
                this._position.X = Field.Borders.Width;
            if ((double)this._position.Y < 0.0)
                this._position.Y = 0.0f;
            if ((double)this._position.Y <= (double)Field.Borders.Height)
                return;
            this._position.Y = Field.Borders.Height;
        }
    }

    public void IncreaseSpeed()
    {
        this._speed += (this._previousPosition - this._position).Unit() * Constants.PlayerMaxVelocity * Constants.PlayerAccelerationFactor;
    }

    [SpecialName]
    public Vector GetPosition()
    {
        return this._position;
    }

    [SpecialName]
    public int PlayerIndex()
    {
        return this._playerIndex;
    }

    [SpecialName]
    public PlayerType GetPlayerType()
    {
        return this._playerType;
    }

    [SpecialName]
    public void SetPlayerType(PlayerType type)
    {
        this._playerType = type;
    }

    [SpecialName]
    public Vector GetSpeed()
    {
        return this._speed;
    }

    [SpecialName]
    public void SetSpeed(Vector speed)
    {
        this._speed = speed;
    }

    [SpecialName]
    public int GetFallenTimer()
    {
        return this._fallenTimer;
    }

    [SpecialName]
    public void SetFallenTimer(int time)
    {
        this._fallenTimer = time;
    }

    [SpecialName]
    public int GetTackleCoolDown()
    {
        return this._TackleCoolDowntimer;
    }

    [SpecialName]
    public void SetTackleCoolDown(int time)
    {
        this._TackleCoolDowntimer = time;
    }

    [SpecialName]
    public PlayerInfo GetPlayerInfo1()
    {
        return new PlayerInfo(this._playerIndex, new Vector(Field.Borders.Width - this._position.X, Field.Borders.Height - this._position.Y), -this._speed, this, this.GetFallenTimer(), this.GetTackleCoolDown(), this._teamInfo, this._playerType, !this._logic);
    }

    [SpecialName]
    public Player GetPlayer1()
    {
        return new Player(this._playerIndex, new Vector(this._position.X, this._position.Y), new Vector(this._speed.X, this._speed.Y), this._fallenTimer, this._TackleCoolDowntimer, this._teamInfo.GetTeam(), this._playerType, (PlayerInfo)null);
    }

    [SpecialName]
    public Player GetPlayer2()
    {
        return new Player(this._playerIndex, new Vector(this._position.X, this._position.Y), new Vector(this._speed.X, this._speed.Y), this._fallenTimer, this._TackleCoolDowntimer, this._teamInfo.GetTeam(), this._playerType, this);
    }

    [SpecialName]
    public bool y()
    {
        return this._logic;
    }

    [SpecialName]
    public void a(bool A_0)
    {
        this._logic = A_0;
    }

    [SpecialName]
    public PlayerInfo GetPlayerInfo()
    {
        return this._originalPlayerInfo;
    }

    [SpecialName]
    public Vector GetAim()
    {
        return this._aim;
    }

    [SpecialName]
    public Vector GetPreviousPosition()
    {
        return this._previousPosition;
    }

    [SpecialName]
    public float GetShootPower()
    {
        return this._shootPower;
    }

    [SpecialName]
    public Player GetPlayer()
    {
        return this._player;
    }

    [SpecialName]
    public PlayerActionInfo GetPlayerActionType()
    {
        return this._playerActionType;
    }

    public void SetKeeper(Vector previousPosition)
    {
        this._playerActionType = PlayerActionInfo.Move;
        this._previousPosition = previousPosition;
    }

    public void SetLeftDefender(Vector A_0, float A_1)
    {
        this._playerActionType = PlayerActionInfo.Shoot;
        this._aim = A_0;
        this._shootPower = A_1;
    }

    public void SetActionTackle(Player player)
    {
        this._playerActionType = PlayerActionInfo.Tackle;
        this._player = player;
    }

    public void SetActionPickUpBall()
    {
        this._playerActionType = PlayerActionInfo.PickUpBall;
    }

    public void SetActionDropBall()
    {
        this._playerActionType = PlayerActionInfo.DropBall;
    }

    public void SetRightForward()
    {
        this._playerActionType = PlayerActionInfo.RightForward;
    }

    public void PlayerFall()
    {
        this._fallenTimer = Constants.PlayerFallenTime;
    }

    public bool IsFallen()
    {
        return this._fallenTimer > 0;
    }

    public bool IsReady()
    {
        return _fallenTimer == 0 && _TackleCoolDowntimer == 0;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        PlayerInfo player = obj as PlayerInfo;
        if (player == null)
            return false;
        else
            return PlayerInfo.ArePlayersEqual(this, player);
    }

    public bool IsEqualPlayerInfo(PlayerInfo playerInfo)
    {
        if (playerInfo == null)
            return false;
        else
            return this._playerIndex == playerInfo._playerIndex;
    }

    [SpecialName]
    public static bool ArePlayersEqual(PlayerInfo player1, PlayerInfo player2)
    {
        if (object.ReferenceEquals((object)player1, (object)player2))
            return true;
        if (player1 == null || player2 == null)
            return false;
        else
            return player1.PlayerIndex() == player2.PlayerIndex();
    }

    public override int GetHashCode()
    {
        return this._playerIndex;
    }
}
