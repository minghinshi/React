using UnityEngine;

public class Menu : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        print("Quitting game...");
        Application.Quit();
    }
}
