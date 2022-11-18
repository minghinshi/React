using Newtonsoft.Json;
using System.Collections.Generic;

public class Records
{
    [JsonProperty]
    private readonly Dictionary<string, int> highestScores = new();

    public int GetHighestScore(Difficulty difficulty)
    {
        return highestScores.GetValueOrDefault(difficulty.name, 0);
    }

    public void UpdateRecord(Difficulty difficulty, int score)
    {
        if (score > GetHighestScore(difficulty))
        {
            highestScores[difficulty.name] = score;
            GameInterface.instance.UpdateMenuScoreDisplay();
        }
    }
}
