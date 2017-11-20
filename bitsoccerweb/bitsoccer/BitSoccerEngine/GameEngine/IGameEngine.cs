using System;

namespace GameEngine
{
    public interface IGameEngine : IDisposable
    {
        string Team1Name();

        string Team2Name();

        void setTimeout(bool timeout);

        GameState GetCurrent();

        GameState GetNext();
    }
}
