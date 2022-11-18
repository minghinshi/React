using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GameTimer : MonoBehaviour
{
    private float length;
    private float timeLeft;
    private bool isEnabled;

    private Slider timerSlider;
    private Image image;
    private VisibilityHandler visibilityHandler;

    public delegate void TimerEndedHandler();
    public event TimerEndedHandler TimerEnded;

    public float TimeLeft
    {
        get => timeLeft;
        set
        {
            timeLeft = value;
            UpdateSlider();
            if (timeLeft <= 0) EndTimer();
        }
    }

    private void Awake()
    {
        timerSlider = GetComponent<Slider>();
        image = GetComponentInChildren<Image>();
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    private void Update()
    {
        if (isEnabled) TimeLeft -= Time.deltaTime;
    }

    public void StartTimer(float length)
    {
        this.length = length;
        TimeLeft = length;
        ContinueTimer();
    }

    public void PauseTimer()
    {
        isEnabled = false;
        visibilityHandler.SetInvisibleImmediately();
    }

    public void ContinueTimer()
    {
        isEnabled = true;
        visibilityHandler.SetVisibleImmediately();
    }

    public float GetPercentage()
    {
        return timeLeft / length;
    }

    private void EndTimer()
    {
        TimerEnded?.Invoke();
        PauseTimer();
    }

    private void UpdateSlider()
    {
        timerSlider.value = GetPercentage();
        image.color = GetColor();
    }

    private Color GetColor()
    {
        return Color.HSVToRGB(GetPercentage() / 3, 0.6f, 0.7f);
    }
}
