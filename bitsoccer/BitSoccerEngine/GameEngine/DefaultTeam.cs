using Common;
using GameEngine;

internal class DefaultTeam : ISecureTeam
{
    public string TeamName { get; set; }
    public int TimeOut { get; set; }
    
    public DefaultTeam()
    {
        this.TeamName = "DefaultTeam";
    }

    public Team getTeamActions(Team thisTeam, Team otherTeam, Ball ball, MatchInfo matchInfo)
    {
        return thisTeam;
    }

    public void Dispose()
    {
    }
}
