using Common;
using System;

namespace GameEngine
{
    public interface ISecureTeam : IDisposable
    {
        string TeamName { get; set; }

        int TimeOut { get; set; }

        Team getTeamActions(Team thisTeam, Team otherTeam, Ball ball, MatchInfo matchInfo);
    }
}
