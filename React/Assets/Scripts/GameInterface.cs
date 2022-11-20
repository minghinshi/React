using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public static GameInterface instance;

    public GameTimer Timer;
    public Countdown Countdown;
    public LivesDisplay LivesDisplay;
    public GameOverDisplay GameOverDisplay;
    public RoundResultDisplay RoundResultDisplay;
    public CorrectTargetsDisplay CorrectTargetsDisplay;

    public VisibilityHandler RoundIntro;
    public VisibilityHandler MainMenu;
    public VisibilityHandler GameplayScreen;
    public VisibilityHandler PauseScreen;

    public PanelSwitcher MainPanels;
    public PanelSwitcher GameplayInfoPanels;

    public TMP_Text ScoreDisplay;
    public TMP_Text RecordDisplay;

    //Ugly hack
    public List<Difficulty> Difficulties;
    public List<TMP_Text> MenuScoreDisplays;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateInGameRecordDisplay(Difficulty difficulty, int score)
    {
        int highScore = RecordsHandler.records.GetHighestScore(difficulty);
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

    public void UpdateMenuScoreDisplay()
    {
        for (int i = 0; i < Difficulties.Count; i++)
        {
            int score = RecordsHandler.records.GetHighestScore(Difficulties[i]);
            MenuScoreDisplays[i].text = string.Format("Best: {0}", score);
        }
    }
}
