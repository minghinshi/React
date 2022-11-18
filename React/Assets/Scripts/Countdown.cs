using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;

    private TMP_Text text;
    private int countsLeft = 0;
    private float timeLeft = 1f;
    private bool isStopped = false;

    private int CountsLeft
    {
        get => countsLeft;
        set
        {
            countsLeft = value;
            text.text = countsLeft.ToString();
        }
    }

    public float TimeLeft
    {
        get => timeLeft;
        set
        {
            timeLeft = value;
            if (timeLeft < 0) DecreaseCount();
            SetTextColor();
        }
    }

    public delegate void OnCountdownFinished();
    public event OnCountdownFinished CountdownFinished;

    private void Awake()
    {
        instance = this;
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (CountsLeft > 0 && !isStopped) TimeLeft -= Time.deltaTime;
    }

    public void CountDown(int count)
    {
        CountsLeft = count;
        timeLeft = 1f;
        isStopped = false;
    }

    public void Terminate()
    {
        isStopped = true;
    }

    private void DecreaseCount()
    {
        TimeLeft += 1f;
        CountsLeft--;
        if (CountsLeft == 0) CountdownFinished?.Invoke();
    }

    private void SetTextColor()
    {
        float alpha = CountsLeft > 0 ? 0.87f * timeLeft : 0f;
        text.color = new Color(0f, 0f, 0f, alpha);
    }
}
