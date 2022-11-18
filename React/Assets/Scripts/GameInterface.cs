using TMPro;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public static GameInterface instance;

    public Countdown Countdown;
    public GameTimer Timer;
    public TargetDisplay CorrectTargetDisplay;
    public LivesDisplay livesDisplay;
    public RoundResultDisplay RoundResultDisplay;
    public GameOverDisplay GameOverDisplay;

    public VisibilityHandler RoundIntro;
    public VisibilityHandler GameplayScreen;
    public VisibilityHandler PauseScreen;

    public PanelSwitcher MainPanels;
    public PanelSwitcher GameplayInfoPanels;

    public TMP_Text ScoreDisplay;
    public TMP_Text RecordDisplay;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateInGameRecordDisplay(int score)
    {
        int highScore = RecordsHandler.records.GetHighestScore();
        if (score > highScore)
        {
            RecordDisplay.color = new Color32(0xfb, 0xc0, 0x2d, 0xff);
            RecordDisplay.text = "New Best!";
        }
        else
        {
            RecordDisplay.color = new Color(0f, 0f, 0f, 0.87f);
            RecordDisplay.text = string.Format("Best: {0}", highScore);
        }
    }
}
