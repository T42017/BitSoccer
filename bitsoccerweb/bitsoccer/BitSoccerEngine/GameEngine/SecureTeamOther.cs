using Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GameEngine
{
    public class SecureTeamOther : ISecureTeam
    {
        [CompilerGenerated]
        private sealed class SecureTeamHolder
        {
            public SecureTeamOther OtherTeam;
            public Team Team1;
            public Team Team2;
            public Ball Ball;
            public MatchInfo MatchInfo;
        }
        [CompilerGenerated]
        private sealed class SecureTeamThread
        {
            public SecureTeamOther.SecureTeamHolder OtherSecureTeam;
            public Thread Thread;

            public void Call()
            {
                this.Thread = Thread.CurrentThread;
                this.OtherSecureTeam.OtherTeam.callTeam(this.OtherSecureTeam.Team1, this.OtherSecureTeam.Team2, this.OtherSecureTeam.Ball, this.OtherSecureTeam.MatchInfo);
            }
        }

        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private Process _process;
        private Team _team;
        private Exception _exception;
        private bool _disposing;

        public string TeamName { get; set; }

        public int TimeOut { get; set; }

        public SecureTeamOther(string teamPath)
        {
            this.TimeOut = Constants.GameEngineMaxThreadTime;
            this.TeamName = Path.GetFileNameWithoutExtension(teamPath);
            this.Compile(teamPath);
        }

        public SecureTeamOther(string teamName, Process processIn)
        {
            this.TimeOut = Constants.GameEngineMaxThreadTime;
            this.TeamName = teamName;
            this._process = processIn;
            this.SetStreams();
        }

        public Team getTeamActions(Team thisTeam, Team otherTeam, Ball ball, MatchInfo matchInfo)
        {
            this._exception = (Exception)null;
            this._team = (Team)null;
            this.Load(thisTeam, otherTeam, ball, matchInfo);
            if (this._exception == null)
                return this._team;
            else
                throw this._exception;
        }

        private void Load(Team team1, Team team2, Ball ball, MatchInfo matchInfo)
        {
            SecureTeamOther.SecureTeamHolder teamHolder = new SecureTeamOther.SecureTeamHolder();
            teamHolder.Team1 = team1;
            teamHolder.Team2 = team2;
            teamHolder.Ball = ball;
            teamHolder.MatchInfo = matchInfo;
            teamHolder.OtherTeam = this;

            try
            {
                SecureTeamOther.SecureTeamThread secureTeamThread = new SecureTeamOther.SecureTeamThread();
                secureTeamThread.OtherSecureTeam = teamHolder;
                secureTeamThread.Thread = null;
                Action action = new Action(secureTeamThread.Call);
                IAsyncResult asyncResult = action.BeginInvoke(null, null);
                if (!asyncResult.AsyncWaitHandle.WaitOne(this.TimeOut))
                {
                    if (secureTeamThread.Thread != null)
                    {
                        secureTeamThread.Thread.Abort();
                    }
                    throw new TimeoutException();
                }
                action.EndInvoke(asyncResult);
            }
            catch
            {
                throw;
            }
        }

        public void callTeam(Team thisTeam, Team otherTeam, Ball ball, MatchInfo matchinfo)
        {
            try
            {
                try
                {
                    foreach (Player player in thisTeam.Players)
                        this._streamWriter.Write(player.Position.ToString() + (object)" " + player.Velocity.ToString() + " " + (string)(object)player.TackleTimer + " " + (string)(object)player.FallenTimer + this._streamWriter.NewLine);
                    foreach (Player player in otherTeam.Players)
                        this._streamWriter.Write(player.Position.ToString() + (object)" " + player.Velocity.ToString() + " " + (string)(object)player.TackleTimer + " " + (string)(object)player.FallenTimer + this._streamWriter.NewLine);
                    if (ball.Owner != (Player)null)
                    {
                        int num = ball.Owner.Team == thisTeam ? 1 : 0;
                        this._streamWriter.Write(ball.Position.ToString() + (object)" " + ball.Velocity.ToString() + " " + (string)(object)num + " " + (string)(object)ball.Owner.PlayerType + this._streamWriter.NewLine);
                    }
                    else
                        this._streamWriter.Write(ball.Position.ToString() + (object)" " + ball.Velocity.ToString() + " " + (string)(object)-1 + " " + (string)(object)-1 + this._streamWriter.NewLine);
                    ((TextWriter)this._streamWriter).Flush();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while sending output: " + ex.Message);
                }
                for (int index1 = 0; index1 < thisTeam.Count(); ++index1)
                {
                    string str;
                    try
                    {
                        str = this._streamReader.ReadLine();
                        if (str.Equals(""))
                            throw new Exception("Empty line recieved.");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error while reading input: " + ex.Message);
                    }
                    string[] strArray = str.Split();
                    int num;
                    try
                    {
                        num = int.Parse(strArray[0]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Bad input (no action): " + ex.Message);
                    }
                    try
                    {
                        if (num == 1)
                        {
                            float x = float.Parse(strArray[1], (IFormatProvider)CultureInfo.InvariantCulture);
                            float y = float.Parse(strArray[2], (IFormatProvider)CultureInfo.InvariantCulture);
                            thisTeam.Players[index1].ActionGo((IPosition)new Vector(x, y));
                        }
                        else if (num == 2)
                        {
                            float shootPower = float.Parse(strArray[1], (IFormatProvider)CultureInfo.InvariantCulture);
                            float x = float.Parse(strArray[2], (IFormatProvider)CultureInfo.InvariantCulture);
                            float y = float.Parse(strArray[3], (IFormatProvider)CultureInfo.InvariantCulture);
                            thisTeam.Players[index1].ActionShoot((IPosition)new Vector(x, y), shootPower);
                        }
                        else if (num == 3)
                        {
                            int index2 = int.Parse(strArray[1]);
                            thisTeam.Players[index1].ActionTackle(otherTeam.Players[index2]);
                        }
                        else if (num == 0)
                        {
                            thisTeam.Players[index1].ActionWait();
                        }
                        else
                        {
                            if (num != 4)
                                throw new Exception("Bad action code: " + (object)num);
                            thisTeam.Players[index1].ActionPickUpBall();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Bad input: " + ex.Message);
                    }
                }
                this._team = thisTeam;
            }
            catch (Exception ex)
            {
                this._exception = ex;
            }
        }

        private void Compile(string fileName)
        {
            try
            {
                ProcessStartInfo processStartInfo;
                if (fileName.EndsWith(".jar"))
                    processStartInfo = new ProcessStartInfo("java", " -jar " + fileName);
                else if (fileName.EndsWith(".py"))
                {
                    processStartInfo = new ProcessStartInfo(Path.GetDirectoryName(fileName) + "\\python.exe");
                    processStartInfo.Arguments = fileName;
                }
                else
                    processStartInfo = new ProcessStartInfo(fileName);
                processStartInfo.CreateNoWindow = true;
                processStartInfo.ErrorDialog = false;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                this._process = new Process();
                this._process.StartInfo = processStartInfo;
                this._process.Start();
                this.SetStreams();
            }
            catch (Exception ex)
            {
                if (fileName.EndsWith(".jar"))
                    throw new Exception("Could not load team, are you sure you have a JVM? :" + ex.Message);
                if (fileName.EndsWith(".py"))
                    throw new Exception("Could not load team, are you sure you have a python.exe file in the same folder as the team? :" + ex.Message);
                else
                    throw new Exception("Could not load team:" + ex.Message);
            }
        }

        private void SetStreams()
        {
            this._streamWriter = this._process.StandardInput;
            this._streamReader = this._process.StandardOutput;
        }

        public void Dispose()
        {
            if (!this._disposing)
                this.Close(true);
            GC.SuppressFinalize((object)this);
        }

        private void Close(bool killProcess)
        {
            this._disposing = true;
            if (!killProcess)
                return;
            try
            {
                if (!this._process.HasExited)
                    this._process.Kill();
                this._streamReader.Close();
                this._streamWriter.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
