using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GameEngine
{
    public class GameEngine : IGameEngine
    {
        private readonly List<TeamInfo> _teamInfos = new List<TeamInfo>();
        private int _goalForTeam = -1;
        //private Random _random = new Random();
        private int _threadTime = Constants.GameEngineMaxThreadTime;
        private int _timeouts = Constants.GameEngineMaxAllowedTimeouts;
        private global::BallInfo _ballInfo;
        private ScoreInfo _scoreInfo;
        private GameState _gameState;
        private GameStateList _gameStateList;
        private int j;
        private int k;
        private string _devMessage1;
        private string _devMessage2;
        private string _errorMessage;
        private bool _disposing;

        private ITeam _teamOne, _teamTwo;

        public GameEngine(string team1Path, string team2Path)
        {
            Assembly teamOneAssembly, teamTwoAssembly;
            try
            {
                teamOneAssembly = Assembly.LoadFile(team1Path);
                teamTwoAssembly = Assembly.LoadFile(team2Path);
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Could not find one of the teams.");
            }
            catch (Exception)
            {
                throw new Exception("Sum went wrong");
            }

            var teamOneInstance = (from type
                                   in teamOneAssembly.GetExportedTypes()
                                   where typeof(ITeam).IsAssignableFrom(type)
                                   select type).Single();

            var teamTwoInstance = (from type
                                   in teamTwoAssembly.GetExportedTypes()
                                   where typeof(ITeam).IsAssignableFrom(type)
                                   select type).Single();

            _teamOne = (ITeam) Activator.CreateInstance(teamOneInstance);
            _teamTwo = (ITeam) Activator.CreateInstance(teamTwoInstance);
            InitData();
        }

        public GameEngine(byte[] homeBytes, byte[] awayBytes)
        {
        }

        public GameEngine(string homeName, byte[] homeBytes, string awayName, byte[] awayBytes)
        {
        }

        private void InitData()
        {
            this._gameStateList = new GameStateList("Team One", "Team Two");
            this._teamInfos.Add(new TeamInfo(0));
            this._teamInfos.Add(new TeamInfo(1));
            this._ballInfo = new BallInfo();
            this._scoreInfo = new ScoreInfo();
            this._gameState = new GameState(this._teamInfos, this._ballInfo, this._scoreInfo, "", "", "Game Started");
        }



        public GameState GetNext()
        {
            List<Team> teams = this.a();
            this.Update(teams, Global.Random.Next(2));
            for (int index = 0; index < 2; ++index)
            {
                foreach (PlayerInfo e in this._teamInfos[index].GetPlayers())
                    e.Update();
            }
            this._goalForTeam = this._ballInfo.Update();
            if (this._goalForTeam != -1)
            {
                if (this._goalForTeam == 0)
                    this._scoreInfo.Team1Score();
                else if (this._goalForTeam == 1)
                    this._scoreInfo.Team2Score();
                this.Reset(this._teamInfos, this._ballInfo);
            }
            this._gameStateList.Add(this._gameState);
            this._devMessage1 = teams[0].DevMessage;
            this._devMessage2 = teams[1].DevMessage;
            this._gameState = new GameState(this._teamInfos, this._ballInfo, this._scoreInfo, this._devMessage1, this._devMessage2, this._errorMessage);
            return this._gameState;
        }

        private List<Team> a()
        {
            List<Team> list = new List<Team>();
            MatchInfo matchInfo1 = new MatchInfo(this._scoreInfo.Team1, this._scoreInfo.Team2, this._gameStateList.Count, this._goalForTeam == 0, this._goalForTeam == 1);
            MatchInfo matchInfo2 = new MatchInfo(this._scoreInfo.Team2, this._scoreInfo.Team1, this._gameStateList.Count, this._goalForTeam == 1, this._goalForTeam == 0);
            Team team1;
            if (this.j < Constants.GameEngineMaxAllowedTimeouts)
            {
                try
                {
                    //team1 = this._team1.getTeamActions(this._teamInfos[0].b(), this._teamInfos[1].CopyTeamInfo().GetTeam(), this._ballInfo.CopyBall(), matchInfo1);
                    _teamOne.Action(_teamInfos[0].b(), _teamInfos[1].CopyTeamInfo().GetTeam(), _ballInfo.CopyBall(), matchInfo1);
                    team1 = _teamInfos[0].b();
                }
                catch (Exception ex)
                {
                    ++this.j;
                    team1 = this._teamInfos[0].b();
                    team1.DevMessage = ex.Message;
                    //this._errorMessage = (this._errorMessage + (object)"\nRed team failed " + (string)(object)this.j + "/" + (string)(object)Constants.GameEngineMaxAllowedTimeouts).Trim();
                    _errorMessage = (_errorMessage + "\nRed team failed " + j + "/" + Constants.GameEngineMaxAllowedTimeouts).Trim();
                }
            }
            else
                team1 = this._teamInfos[0].b();
            list.Add(team1);
            Team team2;
            if (this.k < Constants.GameEngineMaxAllowedTimeouts)
            {
                try
                {
                    //team2 = this._team2.getTeamActions(this._teamInfos[1].b(), this._teamInfos[0].CopyTeamInfo().GetTeam(), this._ballInfo.CopyBallInfo().CopyBall(), matchInfo2);
                    _teamTwo.Action(_teamInfos[1].b(), _teamInfos[0].CopyTeamInfo().GetTeam(), _ballInfo.CopyBallInfo().CopyBall(), matchInfo2);
                    team2 = _teamInfos[1].b();
                }
                catch (Exception ex)
                {
                    ++this.k;
                    team2 = this._teamInfos[1].b();
                    team2.DevMessage = ex.Message;
                    //this._errorMessage = (this._errorMessage + (object)"\nBlue team failed " + (string)(object)this.k + "/" + (string)(object)Constants.GameEngineMaxAllowedTimeouts).Trim();
                    _errorMessage = (_errorMessage + "\nBlue team failed " + k + "/" + Constants.GameEngineMaxAllowedTimeouts).Trim();
                }
            }
            else
                team2 = this._teamInfos[1].b();
            list.Add(team2);
            return list;
        }

        private void Update(List<Team> teams, int teamNumber)
        {
            for (int index1 = 0; index1 < 2; ++index1)
            {
                int index2 = Math.Abs(index1 - teamNumber);
                Team team = teams[index2];

                foreach (Player player in team.Players)
                {
                    List<PlayerInfo> list = new List<PlayerInfo>();
                    list.Add(this._teamInfos[index2].GetPlayers()[team.Players.IndexOf(player)]);
                    list.Add(this._teamInfos[index2].GetPlayers()[team.Players.IndexOf(player)].GetPlayerInfo1());
                    if (!list[0].IsFallen())
                    {
                        if (list[0].GetPlayerActionType() == PlayerActionInfo.PickUpBall)
                            this._ballInfo.GrabBall(list[index2]);
                        else if (list[0].GetPlayerActionType() == PlayerActionInfo.DropBall)
                            this._ballInfo.DropBall(list[index2]);
                        else if (list[0].GetPlayerActionType() == PlayerActionInfo.Shoot)
                            this._ballInfo.ShootBall(list[index2], list[0].GetAim(), list[0].GetShootPower());
                        else if (list[0].GetPlayerActionType() == PlayerActionInfo.Tackle)
                        {
                            int index3 = teams[Math.Abs(1 - index2)].Players.IndexOf(list[0].GetPlayer());
                            if (index3 >= 0)
                            {
                                PlayerInfo tacklingPlayer = this._teamInfos[Math.Abs(1 - index2)].GetPlayers()[index3];
                                if ((double)(list[1].GetPosition() - tacklingPlayer.GetPosition()).Length < (double)Constants.PlayerMaxTackleDistance 
                                    && tacklingPlayer.GetFallenTimer() == 0 && list[0].IsReady()
                                    && (!PlayerInfo.ArePlayersEqual(this._ballInfo.Owner, (PlayerInfo)null) || PlayerInfo.ArePlayersEqual(this._ballInfo.Owner, list[0])))
                                {
                                    list[0].SetTackleCoolDown(Constants.PlayerTackleCooldown);
                                    if (Global.Random.NextDouble() < (double)Constants.PlayerTackleChance)
                                    {
                                        if (!PlayerInfo.ArePlayersEqual(this._ballInfo.Owner, null) && PlayerInfo.ArePlayersEqual(this._ballInfo.Owner, tacklingPlayer))
                                        {
                                            this._ballInfo.Owner = list[index2];
                                            this._ballInfo.Owner.SetFallenTimer(-Constants.BallPickUpImmunityTimer);
                                        }
                                        tacklingPlayer.PlayerFall();
                                    }
                                    else
                                        list[0].PlayerFall();
                                }
                            }
                        }
                        else if (list[0].GetPlayerActionType() == PlayerActionInfo.Move)
                            list[0].IncreaseSpeed();
                    }
                }
            }
        }

        private void Reset(List<TeamInfo> teamInfos, BallInfo ball)
        {
            teamInfos.Clear();
            teamInfos.Add(new TeamInfo(0));
            teamInfos.Add(new TeamInfo(1));
            ball.Reset();
        }

        public ScoreInfo GetResults()
        {
            while (this._gameStateList.Count < Constants.GameEngineMatchLength)
                this.GetNext();
            //this.CleanUpTeams();
            return this._scoreInfo;
        }

        public byte[] GetReplay()
        {
            while (this._gameStateList.Count < Constants.GameEngineMatchLength)
                this.GetNext();
            //this.CleanUpTeams();
            return ReplayHelper.ToByteArray(this._gameStateList);
        }

        //public void CleanUpTeams()
        //{
        //    this._team1.Dispose();
        //    this._team2.Dispose();
        //}

        public void Dispose()
        {
            if (!this._disposing)
                this.Clear(true);
            GC.SuppressFinalize((object)this);
        }

        private void Clear(bool disposing)
        {
            this._disposing = true;
            if (!disposing)
                return;
            //this.CleanUpTeams();
        }

        public GameState GetCurrent()
        {
            if (this._gameState != null)
                return this._gameState;
            else
                return null;
        }

        public string Team1Name()
        {
            return "Team One";
            //return this._teamOne.TeamName;
        }

        public string Team2Name()
        {
            return "Team Two";
            //return this._team2.TeamName;
        }

        public void setTimeout(bool shouldTimeout)
        {
            //    if (shouldTimeout)
            //    {
            //        this._team1.TimeOut = Constants.GameEngineMaxThreadTime;
            //        this._team2.TimeOut = Constants.GameEngineMaxThreadTime;
            //    }
            //    else
            //    {
            //        this._team1.TimeOut = 60000000;
            //        this._team2.TimeOut = 60000000;
            //    }
        }
    }
}
