using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static Game currentGame;

    public void CreateNewGame()
    {
        currentGame = new();
    }
}
