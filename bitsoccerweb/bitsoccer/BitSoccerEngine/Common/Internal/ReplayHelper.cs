using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

public class ReplayHelper
{
    private static readonly char[] chars = new char[52]
      {
        'a',    'b',    'c',    'd',    'e',    'f',    'g',    'h',
        'i',    'j',    'k',    'l',    'm',    'n',    'o',    'p',
        'q',    'r',    's',    't',    'u',    'v',    'w',    'x',
        'y',    'z',    'A',    'B',    'C',    'D',    'E',    'F',
        'G',    'H',    'I',    'J',    'K',    'L',    'M',    'N',
        'O',    'P',    'Q',    'R',    'S',    'T',    'U',    'V',
        'W',    'X',    'Y',    'Z'
      };

    private const int a = 2;

    public static void SaveReplay(string fileName, GameStateList gameStateList)
    {
        byte[] buffer = ReplayHelper.ToByteArray(gameStateList);
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream)File.Open(fileName, FileMode.Create)))
        {
            binaryWriter.Write(buffer);
            binaryWriter.Close();
        }
    }

    public static byte[] ToByteArray(GameStateList gameStateList)
    {
        try
        {
            byte[] buffer;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter textWriter = (TextWriter)new StreamWriter((Stream)memoryStream))
                {
                    textWriter.WriteLine(gameStateList.Team1);
                    textWriter.WriteLine(gameStateList.Team2);

                    for (int index = 0; index < gameStateList.Count; ++index)
                        textWriter.WriteLine(ReplayHelper.GameStateToString(gameStateList[index]));
                    textWriter.Flush();
                    buffer = memoryStream.ToArray();
                }
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream((Stream)memoryStream, CompressionMode.Compress, true))
                    gzipStream.Write(buffer, 0, buffer.Length);
                return memoryStream.ToArray();
            }
        }
        catch(Exception ex)
        {
            throw new SystemException("Failed to save replay");
        }
    }

    public static GameStateList LoadReplay(string fileName)
    {
        try
        {
            using (Stream stream1 = (Stream)File.OpenRead(fileName))
            {
                using (Stream stream2 = (Stream)new GZipStream(stream1, CompressionMode.Decompress))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        byte[] buffer = new byte[8192];
                        int count;
                        while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                            memoryStream.Write(buffer, 0, count);
                        return ReplayHelper.FromByteArray(memoryStream.ToArray());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new SystemException("Failed to load replay:" + ex.Message);
        }
    }

    private static GameStateList FromByteArray(byte[] data)
    {
        using (TextReader textReader = (TextReader)new StreamReader((Stream)new MemoryStream(data)))
        {
            GameStateList g = new GameStateList(textReader.ReadLine(), textReader.ReadLine());
            string A_0_1;
            while ((A_0_1 = textReader.ReadLine()) != null)
                g.Add(ReplayHelper.GameStateFromString(A_0_1));
            return g;
        }
    }

    private static string GameStateToString(GameState gameState)
    {
        string str1 = "";
        foreach (TeamInfo team in gameState.Teams())
        {
            foreach (PlayerInfo playerInfo in team.GetPlayers())
                str1 = str1 + ReplayHelper.DoubleToString((double)playerInfo.GetPosition().X) + ReplayHelper.DoubleToString((double)playerInfo.GetPosition().Y);
        }

        string str2 = str1 + ReplayHelper.DoubleToString((double)gameState.BallInfo.Position.X) + ReplayHelper.DoubleToString((double)gameState.BallInfo.Position.Y) + 
            ReplayHelper.DoubleToString((double)gameState.GetScoreInfo().Team1) + ReplayHelper.DoubleToString((double)gameState.GetScoreInfo().Team2);

        foreach (TeamInfo l in gameState.Teams())
        {
            foreach (PlayerInfo e in l.GetPlayers())
            {
                if (e.GetFallenTimer() > 0)
                    str2 = str2 + ReplayHelper.DoubleToString((double)e.PlayerIndex()) + ReplayHelper.DoubleToString((double)e.GetFallenTimer());
            }
        }
        return str2;
    }

    private static GameState GameStateFromString(string text)
    {
        List<TeamInfo> A_0_1 = new List<TeamInfo>();
        List<PlayerInfo> A_0_2 = new List<PlayerInfo>();
        BallInfo b = new BallInfo();
        ScoreInfo n = new ScoreInfo();
        int startIndex1 = 0;
        while (startIndex1 < 24)
        {
            PlayerType A_3 = PlayerType.Keeper;
            int A_0_3 = 0;
            if (startIndex1 == 0)
            {
                A_3 = PlayerType.Keeper;
                A_0_3 = 0;
            }
            if (startIndex1 == 4)
            {
                A_3 = PlayerType.LeftDefender;
                A_0_3 = 1;
            }
            if (startIndex1 == 8)
            {
                A_3 = PlayerType.RightDefender;
                A_0_3 = 2;
            }
            if (startIndex1 == 12)
            {
                A_3 = PlayerType.LeftForward;
                A_0_3 = 3;
            }
            if (startIndex1 == 16)
            {
                A_3 = PlayerType.CenterForward;
                A_0_3 = 4;
            }
            if (startIndex1 == 20)
            {
                A_3 = PlayerType.RightForward;
                A_0_3 = 5;
            }
            A_0_2.Add(new PlayerInfo(A_0_3, new Vector((float)ReplayHelper.StringToInt(text.Substring(startIndex1, 2)), (float)ReplayHelper.StringToInt(text.Substring(startIndex1 + 2, 2))), new TeamInfo(), A_3));
            startIndex1 += 4;
        }
        A_0_1.Add(new TeamInfo(A_0_2));
        A_0_2.Clear();
        int startIndex2 = 24;
        while (startIndex2 < 48)
        {
            PlayerType A_3 = PlayerType.Keeper;
            int A_0_3 = 0;
            if (startIndex2 == 24)
            {
                A_3 = PlayerType.Keeper;
                A_0_3 = 6;
            }
            if (startIndex2 == 28)
            {
                A_3 = PlayerType.LeftDefender;
                A_0_3 = 7;
            }
            if (startIndex2 == 32)
            {
                A_3 = PlayerType.RightDefender;
                A_0_3 = 8;
            }
            if (startIndex2 == 36)
            {
                A_3 = PlayerType.LeftForward;
                A_0_3 = 9;
            }
            if (startIndex2 == 40)
            {
                A_3 = PlayerType.CenterForward;
                A_0_3 = 10;
            }
            if (startIndex2 == 44)
            {
                A_3 = PlayerType.RightForward;
                A_0_3 = 11;
            }
            PlayerInfo e = new PlayerInfo(A_0_3, new Vector((float)ReplayHelper.StringToInt(text.Substring(startIndex2, 2)), (float)ReplayHelper.StringToInt(text.Substring(startIndex2 + 2, 2))), new TeamInfo(), A_3);
            A_0_2.Add(new PlayerInfo(A_0_3, e.GetPlayerInfo1().GetPosition(), new TeamInfo(), A_3));
            startIndex2 += 4;
        }
        A_0_1.Add(new TeamInfo(A_0_2));
        BallInfo A_1 = new BallInfo(new Vector((float)ReplayHelper.StringToInt(text.Substring(48, 2)), (float)ReplayHelper.StringToInt(text.Substring(50, 2))));
        ScoreInfo A_2 = new ScoreInfo(ReplayHelper.StringToInt(text.Substring(52, 2)), ReplayHelper.StringToInt(text.Substring(54, 2)));
        int startIndex3 = 56;
        while (startIndex3 < text.Length)
        {
            int num = ReplayHelper.StringToInt(text.Substring(startIndex3, 2));
            int A_0_3 = ReplayHelper.StringToInt(text.Substring(startIndex3 + 2, 2));
            foreach (TeamInfo l in A_0_1)
            {
                foreach (PlayerInfo e in l.GetPlayers())
                {
                    if (e.PlayerIndex() == num)
                        e.SetFallenTimer(A_0_3);
                }
            }
            startIndex3 += 4;
        }
        return new GameState(A_0_1, A_1, A_2, "", "", "");
    }

    private static string DoubleToString(double A_0)
    {
        int num = Math.Max(0, (int)A_0) % (ReplayHelper.chars.Length * ReplayHelper.chars.Length - 1);
        return ReplayHelper.chars[num / ReplayHelper.chars.Length].ToString() + ReplayHelper.chars[num % ReplayHelper.chars.Length].ToString();
    }

    private static int StringToInt(string A_0)
    {
        return Array.IndexOf<char>(ReplayHelper.chars, A_0[0]) * ReplayHelper.chars.Length + Array.IndexOf<char>(ReplayHelper.chars, A_0[1]);
    }
}
