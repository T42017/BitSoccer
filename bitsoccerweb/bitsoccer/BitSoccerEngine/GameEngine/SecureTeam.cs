using Common;
using GameEngine;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

internal class SecureTeam : ISecureTeam
{
    [CompilerGenerated]
    private sealed class InstructionString
    {
        public string Data;

        public bool Contains(Instruction instruction)
        {
            return instruction.ToString().Contains(this.Data);
        }
    }

    [CompilerGenerated]
    private sealed class SecureInfo
    {
        public SecureTeam SecureTeam;
        public Team Team1;
        public Team Team2;
        public Ball Ball;
        public MatchInfo MatchInfo;
    }

    [CompilerGenerated]
    private sealed class ThreadInfo
    {
        public SecureTeam.SecureInfo _secureInfo;
        public Thread _thread;

        public void c()
        {
            this._thread = Thread.CurrentThread;
            try
            {
                this._secureInfo.SecureTeam._team = this._secureInfo.Team1;
                this._secureInfo.SecureTeam._iTeam.Action(this._secureInfo.Team1, this._secureInfo.Team2, this._secureInfo.Ball, this._secureInfo.MatchInfo);
            }
            catch (Exception ex)
            {
                this._secureInfo.SecureTeam.Exception = ex;
            }
        }
    }

    public string TeamName { get; set; }
    public int TimeOut { get; set; }

    private ITeam _iTeam;
    private Team _team;
    private Exception Exception;

    [CompilerGenerated]
    private int e;

    public SecureTeam(string fileName)
    {
        try
        {
            TimeOut = Constants.GameEngineMaxThreadTime;
            TeamName = Path.GetFileNameWithoutExtension(fileName);
            Assembly assembly = Assembly.LoadFile(fileName);
            global::SecureTeam.LoadAssembly(assembly);
            IEnumerable<Type> types = assembly.GetTypes();
            Type type = types.Where(t => typeof(ITeam).IsAssignableFrom(t) && t.IsClass).Single<Type>();
            this._iTeam = (ITeam)Activator.CreateInstance(type);
            TeamName = type.Name;
        }
        catch (InvalidOperationException)
        {
            throw new Exception(fileName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public SecureTeam(byte[] bytes)
        : this("unknown", bytes)
    {
    }

    public SecureTeam(string teamName, byte[] data)
    {
        try
        {
            TimeOut = Constants.GameEngineMaxThreadTime;
            TeamName = teamName;
            Assembly assembly = Assembly.Load(data);
            IEnumerable<Type> types = assembly.GetTypes();
            Type type = types.Where(t => typeof(ITeam).IsAssignableFrom(t) && t.IsClass).Single<Type>();
            this._iTeam = (ITeam)Activator.CreateInstance(type);
        }
        catch
        {
            throw;
        }
    }
    public SecureTeam(ITeam team)
    {
        TimeOut = Constants.GameEngineMaxThreadTime;
        TeamName = team.GetType().Name;
        this._iTeam = team;
    }

    private static void LoadAssembly(Assembly assembly)
    {
        string[] array = new string[]
        {
            "System.IO",
            "System.Net",
            "System.Reflection",
            "System.Threading",
            "System.Diagnostics",
            "System.GC"
        };
        Type[] types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++)
        {
            Type type = types[i];
            ModuleDefinition moduleDefinition = ModuleDefinition.ReadModule(type.Module.FullyQualifiedName);
            TypeDefinition typeDefinition = (TypeDefinition)moduleDefinition.LookupToken(type.MetadataToken);
            foreach (MethodDefinition current in TypeDefinitionRocks.GetMethods(typeDefinition))
            {
                Func<Instruction, bool> func = null;
                global::SecureTeam.InstructionString a = new global::SecureTeam.InstructionString();
                string[] array2 = array;
                for (int j = 0; j < array2.Length; j++)
                {
                    a.Data = array2[j];
                    if (current.Body != null)
                    {
                        IEnumerable<Instruction> arg_DA_0 = current.Body.Instructions;
                        if (func == null)
                        {
                            func = new Func<Instruction, bool>(a.Contains);
                        }
                        IEnumerable<Instruction> source = arg_DA_0.Where(func);
                        if (source.Any<Instruction>())
                        {
                            throw new Exception(a.Data);
                        }
                    }
                }
            }
        }
    }
    public Team getTeamActions(Team thisTeam, Team otherTeam, Ball ball, MatchInfo matchInfo)
    {
        this.Exception = null;
        Team result;
        try
        {
            this.a(thisTeam, otherTeam, ball, matchInfo);
            if (this.Exception != null)
            {
                throw this.Exception;
            }
            result = this._team;
        }
        catch
        {
            throw;
        }
        return result;
    }
    private void a(Team A_0, Team A_1, Ball A_2, MatchInfo A_3)
    {
        global::SecureTeam.SecureInfo b = new global::SecureTeam.SecureInfo();
        b.Team1 = A_0;
        b.Team2 = A_1;
        b.Ball = A_2;
        b.MatchInfo = A_3;
        b.SecureTeam = this;
        try
        {
            global::SecureTeam.ThreadInfo c = new global::SecureTeam.ThreadInfo();
            c._secureInfo = b;
            c._thread = null;
            Action action = new Action(c.c);
            IAsyncResult asyncResult = action.BeginInvoke(null, null);
            if (asyncResult.AsyncWaitHandle.WaitOne(TimeOut))
            {
                action.EndInvoke(asyncResult);
            }
            else
            {
                if (c._thread != null)
                {
                    c._thread.Abort();
                    throw new TimeoutException();
                }
            }
        }
        catch
        {
            throw;
        }
    }
    public void Dispose()
    {
    }
}
