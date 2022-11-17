using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VisualTimer : MonoBehaviour
{
    private float length;
    private float timeLeft;
    private bool isEnabled;

    private Slider timerSlider;

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
    }

    private void Update()
    {
        if (isEnabled) TimeLeft -= Time.deltaTime;
    }

    public void StartTimer(float length)
    {
        this.length = length;
        TimeLeft = length;
        isEnabled = true;
    }

    public void StopTimer()
    {
        isEnabled = false;
    }

    public float GetPercentage()
    {
        return timeLeft / length;
    }

    private void EndTimer()
    {
        TimerEnded?.Invoke();
        isEnabled = false;
    }

    private void UpdateSlider()
    {
        timerSlider.value = GetPercentage();
    }
}
