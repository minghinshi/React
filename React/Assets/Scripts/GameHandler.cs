using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static Game currentGame;
    private GameInterface UI;

    private void Start()
    {
        UI = GameInterface.instance;
    }

    public void CreateNewGame()
    {
        currentGame = new();
        UI.MainPanels.SwitchPanel(UI.GameplayScreen);
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
