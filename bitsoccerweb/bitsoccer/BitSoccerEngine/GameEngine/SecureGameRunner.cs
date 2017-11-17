using System;
using System.Reflection;

namespace GameEngine
{
    public class SecureGameRunner : MarshalByRefObject
    {
        public Assembly GetAssembly(string AssemblyPath)
        {
            return Assembly.LoadFrom(AssemblyPath);
        }

        public ScoreInfo GetScore(byte[] homeBytes, byte[] awayBytes)
        {
            using (GameEngine gameEngine = new GameEngine(homeBytes, awayBytes))
                return gameEngine.GetResults();
        }

        public ScoreInfo GetScore(string homePath, string awayPath)
        {
            using (GameEngine gameEngine = new GameEngine(homePath, awayPath))
                return gameEngine.GetResults();
        }

        public byte[] GetReplay(string homeName, byte[] homeBytes, string awayName, byte[] awayBytes)
        {
            using (GameEngine gameEngine = new GameEngine(homeName, homeBytes, awayName, awayBytes))
                return gameEngine.GetReplay();
        }

        public byte[] GetReplay(string homePath, string awayPath)
        {
            using (GameEngine gameEngine = new GameEngine(homePath, awayPath))
                return gameEngine.GetReplay();
        }
    }
}
