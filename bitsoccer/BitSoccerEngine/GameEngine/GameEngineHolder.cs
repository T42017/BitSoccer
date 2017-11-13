using GameEngine;
using System;

internal class GameEngineHolder : MarshalByRefObject
{
    private GameEngine.GameEngine _gameEngine;
    private bool _disposing;

    public void Init(string fileName1, string fileName2)
    {
        this._gameEngine = new GameEngine.GameEngine(fileName1, fileName2);
    }

    public GameState GetNext()
    {
        return this._gameEngine.GetNext();
    }

    public GameState GetCurrent()
    {
        return this._gameEngine.GetCurrent();
    }

    public void SetShouldTimeout(bool A_0)
    {
        this._gameEngine.setTimeout(A_0);
    }

    public string GetTeam1Name()
    {
        return this._gameEngine.Team1Name();
    }

    public string GetTeam2Name()
    {
        return this._gameEngine.Team2Name();
    }

    public void Clear()
    {
        if (!this._disposing)
        {
            this.Dispose(true);
        }
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool A_0)
    {
        this._disposing = true;
        if (A_0)
        {
            this._gameEngine.Dispose();
        }
    }
}
