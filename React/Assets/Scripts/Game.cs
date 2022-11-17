using UnityEngine;

public class Game
{
    private int score = 0;
    private int lives = 3;
    private int level = 0;

    private Difficulty difficulty;
    private Round currentRound;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            GameInterface.instance.ScoreDisplay.text = value.ToString();
        }
    }

    public Game()
    {
        GenerateNewRound();
        GameInterface.instance.NextRoundButton.onClick.AddListener(GenerateNewRound);
    }

    private void GenerateNewRound()
    {
        currentRound = new(3f, 5);
        currentRound.RoundCompleted += OnRoundCompleted;
        GameInterface.instance.RoundResult.SetInvisibleImmediately();
        GameInterface.instance.RoundIntro.SetVisibleImmediately();
    }

    private void OnRoundCompleted(bool isCorrect)
    {
        if (isCorrect) AddScore();
        currentRound.RoundCompleted -= OnRoundCompleted;
        GameInterface.instance.RoundResult.SetVisibleImmediately();

    }

    private void AddScore()
    {
        int addedScore = GetAddedScore();
        Score += addedScore;
        GameInterface.instance.RoundResultText.text = string.Format("+{0} Points", addedScore);
    }

    private int GetAddedScore()
    {
        return Mathf.CeilToInt(GameInterface.instance.VisualTimer.GetPercentage() * 10f);
    }
}