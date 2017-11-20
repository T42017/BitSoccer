using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace GameEngine
{
    public class LocalSecureGameEngine : IGameEngine
    {
        private AppDomain _appDomain;
        private GameEngineHolder _gameEngineHolder;
        private bool _disposing;

        public LocalSecureGameEngine(string path1, string path2)
        {
            this.Init(path1, path2);
        }

        public GameState GetNext()
        {
            return this._gameEngineHolder.GetNext();
        }

        public GameState GetCurrent()
        {
            return this._gameEngineHolder.GetCurrent();
        }

        public void setTimeout(bool shouldTimeout)
        {
            this._gameEngineHolder.SetShouldTimeout(shouldTimeout);
        }

        public string Team1Name()
        {
            return this._gameEngineHolder.GetTeam1Name();
        }

        public string Team2Name()
        {
            return this._gameEngineHolder.GetTeam2Name();
        }

        private void Init(string fileName1, string fileName2)
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
            grantSet.AddPermission((IPermission)new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Path.GetDirectoryName(fileName1)));
            grantSet.AddPermission((IPermission)new FileIOPermission(FileIOPermissionAccess.Read, Path.GetDirectoryName(fileName1)));
            grantSet.AddPermission((IPermission)new FileIOPermission(FileIOPermissionAccess.PathDiscovery, Path.GetDirectoryName(fileName2)));
            grantSet.AddPermission((IPermission)new FileIOPermission(FileIOPermissionAccess.Read, Path.GetDirectoryName(fileName2)));
            this._appDomain = AppDomain.CreateDomain("Game Domain", (Evidence)null, info, grantSet);
            this._gameEngineHolder = (GameEngineHolder)this._appDomain.CreateInstanceAndUnwrap(typeof(GameEngineHolder).Assembly.FullName, typeof(GameEngineHolder).FullName);
            this._gameEngineHolder.Init(fileName1, fileName2);
        }

        public void Dispose()
        {
            if (!this._disposing)
                this.Clear(true);
            GC.SuppressFinalize((object)this);
        }

        private void Clear(bool clearEngine)
        {
            this._disposing = true;
            if (!clearEngine)
                return;
            this._gameEngineHolder.Clear();
            AppDomain.Unload(this._appDomain);
        }
    }
}
