using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text recordDisplay;

    private VisibilityHandler visibilityHandler;

    private void Awake()
    {
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    public void ShowGameResults(int score)
    {
        scoreDisplay.text = string.Format("{0} Points", score);
        DisplayRecord(score);
        GameInterface.instance.MainPanels.SwitchPanel(visibilityHandler);
    }

    public void DisplayRecord(int score)
    {
        int highScore = RecordsHandler.records.GetHighestScore();
        int difference = score - highScore;
        if (score > highScore)
        {
            recordDisplay.color = new Color32(0xfb, 0xc0, 0x2d, 0xff);
            recordDisplay.text = string.Format("New Best! +{0} compared to record ({1})", difference, highScore);
        }
        else if (score == highScore)
        {
            recordDisplay.color = new Color32(0x4c, 0xaf, 0x50, 0xff);
            recordDisplay.text = "Tied with record!";
        }
        else
        {
            recordDisplay.color = new Color32(0xf4, 0x43, 0x36, 0xff);
            recordDisplay.text = string.Format("{0} compared to record ({1})", difference, highScore);
        }
    }
}
