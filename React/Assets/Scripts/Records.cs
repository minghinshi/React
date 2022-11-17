using Newtonsoft.Json;

public class Records
{
    [JsonProperty] private int highestScore = 0;

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void UpdateRecord(int score)
    {
        if (score > highestScore) highestScore = score;
    }
}
