using UnityEngine;

public class Game
{
    private int score;
    private int lives;
    private int level;

    private Difficulty difficulty;
    private Round currentRound;
    private readonly GameInterface UI = GameInterface.instance;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            UI.ScoreDisplay.text = value.ToString();
            UI.UpdateInGameRecordDisplay(value);
        }
    }

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            UI.livesDisplay.ShowLives(value);
        }
    }

    public Game()
    {
        GenerateNewRound();
        Score = 0;
        Lives = 3;
        UI.RoundResultDisplay.BindButtonToGame(ContinueGame);
    }

    private void ContinueGame()
    {
        if (Lives == 0) EndGame();
        else GenerateNewRound();
    }

    private void EndGame()
    {
        UI.GameOverDisplay.ShowGameResults(Score);
        RecordsHandler.records.UpdateRecord(Score);
    }

    private void GenerateNewRound()
    {
        currentRound = new(GetRoundTime(), level + 3);
        currentRound.RoundCompleted += OnRoundCompleted;
        UI.GameplayInfoPanels.SwitchPanel(UI.RoundIntro);
    }

    private void OnRoundCompleted(RoundResult roundResult)
    {
        EvaluateResult(roundResult);
        currentRound.RoundCompleted -= OnRoundCompleted;
    }

    private int GetAddedScore()
    {
        return Mathf.CeilToInt(GameInterface.instance.VisualTimer.GetPercentage() * 10f);
    }

    private void EvaluateResult(RoundResult roundResult)
    {
        switch (roundResult)
        {
            case RoundResult.Correct:
                OnCorrectAnswer();
                break;
            case RoundResult.Incorrect:
                OnIncorrectAnswer();
                break;
            case RoundResult.TimedOut:
                OnTimeOut();
                break;
        }
    }

    private void OnCorrectAnswer()
    {
        int addedScore = GetAddedScore();
        Score += addedScore;
        level++;
        UI.RoundResultDisplay.DisplayCorrect(addedScore);
    }

    private void OnIncorrectAnswer()
    {
        Lives--;
        UI.RoundResultDisplay.DisplayIncorrect();
    }

    private void OnTimeOut()
    {
        Lives--;
        UI.RoundResultDisplay.DisplayOutOfTime();
    }

    private float GetRoundTime()
    {
        return 60f / Mathf.Pow(1 + level * 0.414f, 2);
    }
}