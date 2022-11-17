using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    public void ShowLives(int lives)
    {
        for (int i = 0; i < 3; i++) transform.GetChild(i).gameObject.SetActive(i < lives);
    }
}
