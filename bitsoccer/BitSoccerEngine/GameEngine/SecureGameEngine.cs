// Decompiled with JetBrains decompiler
// Type: GameEngine.SecureGameEngine
// Assembly: GameEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F411E0E-088F-4982-A52C-8DCF1FA4847B
// Assembly location: C:\GIT\prg1-nijo14\CloudBall\Libs\GameEngine.dll

using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace GameEngine
{
    public class SecureGameEngine : IDisposable
    {
        private byte[] _homeBytesIn;
        private byte[] _awayBytesIn;
        private string _homeName;
        private string _awayName;
        private static AppDomain _appDomain;
        private static SecureGameRunner _secureGameRunner;
        private bool _disposing;

        public SecureGameEngine(byte[] homeBytesIn, byte[] awayBytesIn)
            : this("unknown", homeBytesIn, "unknowntest", awayBytesIn)
        {
        }

        public SecureGameEngine(string homeNameIn, byte[] homeBytesIn, string awayNameIn, byte[] awayBytesIn)
        {
            this._homeBytesIn = homeBytesIn;
            this._awayBytesIn = awayBytesIn;
            this._homeName = homeNameIn;
            this._awayName = awayNameIn;
            this.SetPermissions();
        }

        public ScoreInfo getResults()
        {
            return SecureGameEngine._secureGameRunner.GetScore(this._homeBytesIn, this._awayBytesIn);
        }

        public byte[] GetReplay()
        {
            return SecureGameEngine._secureGameRunner.GetReplay(this._homeName, this._homeBytesIn, this._awayName, this._awayBytesIn);
        }

        public static bool teamOk(byte[] teamIn)
        {
            try
            {
                new SecureGameEngine(teamIn, teamIn).Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (this._disposing)
                return;
            this.SetDisposing(true);
        }

        private void SetDisposing(bool dispose)
        {
            this._disposing = true;
        }

        private void SetPermissions()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            AppDomainSetup info = new AppDomainSetup()
            {
                ApplicationName = "Name",
                ApplicationBase = currentDirectory
            };
            PermissionSet grantSet = new PermissionSet(PermissionState.None);
            grantSet.AddPermission((IPermission)new SecurityPermission(SecurityPermissionFlag.Execution));
            grantSet.AddPermission((IPermission)new SecurityPermission(SecurityPermissionFlag.ControlThread));
            grantSet.AddPermission((IPermission)new FileIOPermission(FileIOPermissionAccess.PathDiscovery, currentDirectory));
            grantSet.AddPermission((IPermission)new FileIOPermission(FileIOPermissionAccess.Read, currentDirectory));
            SecureGameEngine._appDomain = AppDomain.CreateDomain("Game Domain", (Evidence)null, info, grantSet);
            SecureGameEngine._secureGameRunner = (SecureGameRunner)SecureGameEngine._appDomain.CreateInstanceAndUnwrap(typeof(SecureGameRunner).Assembly.FullName, typeof(SecureGameRunner).FullName);
        }
    }
}
