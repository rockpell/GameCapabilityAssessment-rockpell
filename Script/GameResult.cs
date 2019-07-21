using System;

[Serializable]
public class GameResult
{
    public string name; // gameName
    public Result resultValue;

    public GameResult(string gameName, Result resultValue)
    {
        this.name = gameName;
        this.resultValue = resultValue;
    }
}