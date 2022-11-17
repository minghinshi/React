using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public static GameInterface instance;

    public Countdown Countdown;
    public TargetDisplay CorrectTargetDisplay;
    public VisualTimer VisualTimer;

    public VisibilityHandler RoundIntro;
    public VisibilityHandler RoundResult;

    public TMP_Text ScoreDisplay;
    public TMP_Text RoundResultText;

    public Button NextRoundButton;

    private void Awake()
    {
        instance = this;
    }
}
