using System;

[Serializable]
public class ScoreInfo
{
    private int _team1Score;
    private int _team2Score;

    public int Team1 {  get { return _team1Score; } }
    public int Team2 {  get { return _team2Score; } }

    public ScoreInfo()
        : this(0, 0)
    {
    }

    public ScoreInfo(int score1, int score2)
    {
        _team1Score = score1;
        _team2Score = score2;
    }

    public void Team1Score()
    {
        _team1Score++;
    }

    public void Team2Score()
    {
        _team2Score++;
    }

    public override string ToString()
    {
        return string.Format("{0} - {1}", _team1Score, _team2Score);
    }
}
