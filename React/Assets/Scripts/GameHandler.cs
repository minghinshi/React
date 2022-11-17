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
}
