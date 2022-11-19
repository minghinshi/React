using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoundResultDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text body;
    [SerializeField] private TMP_Text buttonText;

    [SerializeField] private Button nextRoundButton;
    [SerializeField] private Image panelImage;

    private VisibilityHandler visibilityHandler;

    private void Awake()
    {
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    public void DisplayCorrect(int score)
    {
        header.text = "Correct!";
        body.text = string.Format("+{0} Points", score);
        buttonText.text = "Faster!";
        panelImage.color = new Color32(0xc7, 0xe5, 0xc8, 0xff);
        ShowScreen();
    }

    public void DisplayOutOfTime()
    {
        DisplayDeductLife("You ran out of time.");
    }

    public void DisplayIncorrect()
    {
        DisplayDeductLife("You clicked on the wrong target.");
    }

    public void BindButtonToGame(UnityAction action)
    {
        nextRoundButton.onClick.AddListener(action);
    }

    public void UnbindButtonFromGame(UnityAction action)
    {
        nextRoundButton.onClick.RemoveListener(action);
    }

    private void DisplayDeductLife(string mistakeMessage)
    {
        header.text = mistakeMessage;
        body.text = "-1 Life";
        buttonText.text = "Next";
        panelImage.color = new Color32(0xff, 0xcd, 0xd2, 0xff);
        ShowScreen();
    }

    private void ShowScreen()
    {
        GameInterface.instance.GameplayInfoPanels.SwitchPanel(visibilityHandler);
    }
}
