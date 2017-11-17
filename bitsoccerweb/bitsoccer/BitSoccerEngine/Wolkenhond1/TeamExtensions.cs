// Decompiled with JetBrains decompiler
// Type: GentleWare.CloudBall.Wolkenhondjes1.TeamExtensions
// Assembly: Wolkenhondjes1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF8BB3E4-B2FA-45C8-BD2E-77F9E88183CB
// Assembly location: C:\Users\jonas\Desktop\Wolkenhond1.dll

using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GentleWare.CloudBall.Wolkenhondjes1
{
    public static class TeamExtensions
    {
        public static Player GetPlayerClosestTo(this Team team, IPosition pos)
        {
            return ((IEnumerable<Player>) ((IEnumerable<Player>) team.Players).OrderBy<Player, float>(
                (Func<Player, float>) (player => player.GetDistanceTo(pos)))).First<Player>();
        }

        public static float GetSmallestDistanceTo(this Team team, IPosition pos)
        {
            return ((IEnumerable<Player>) team.Players)
                .Select<Player, float>((Func<Player, float>) (player => player.GetDistanceTo(pos))).Min();
        }

        public static Player GetLF(this Team team)
        {
            return team.GetPlayer((PlayerType) 4);
        }

        public static Player GetRF(this Team team)
        {
            return team.GetPlayer((PlayerType) 6);
        }

        public static Player GetCF(this Team team)
        {
            return team.GetPlayer((PlayerType) 5);
        }

        public static Player GetLD(this Team team)
        {
            return team.GetPlayer((PlayerType) 2);
        }

        public static Player GetRD(this Team team)
        {
            return team.GetPlayer((PlayerType) 3);
        }

        public static Player GetGK(this Team team)
        {
            return team.GetPlayer((PlayerType) 1);
        }

        private static Player GetPlayer(this Team team, PlayerType type)
        {
            try
            {
                var res1 = team.Players.First(p => p.PlayerType == type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var res = team.Players.First(p => p.PlayerType == type);
            return res;
        }
    }
}
