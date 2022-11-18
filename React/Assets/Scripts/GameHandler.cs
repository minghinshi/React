using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static Game currentGame;

    private GameInterface UI;

    private void Start()
    {
        UI = GameInterface.instance;
    }

    public void CreateNewGame(Difficulty difficulty)
    {
        TargetManager.instance.SetDifficulty(difficulty);
        TargetFactory.instance.SetRandomizedItems(difficulty.randomizedItems);
        UI.MainPanels.SwitchPanel(UI.GameplayScreen);
        currentGame = new(difficulty);
    }

    public void CreateNewGame()
    {
        CreateNewGame(currentGame.Difficulty);
    }

    public void QuitGame()
    {
        currentGame.ForceEndGame();
        UI.MainPanels.SwitchPanel(UI.MainMenu);
    }

    public void Retry()
    {
        QuitGame();
        CreateNewGame();
    }

    public void StartCountdown()
    {
        currentGame.Round.StartCountdown();
    }

    public void PauseGame()
    {
        currentGame.Round.Pause();
        UI.MainPanels.SwitchPanel(UI.PauseScreen);
    }

    public void ResumeGame()
    {
        currentGame.Round.Continue();
        UI.MainPanels.SwitchPanel(UI.GameplayScreen);
    }
}
