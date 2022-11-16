using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private TMP_Text text;
    private int countsLeft = 0;
    private float timeLeft = 1f;

    public int CountsLeft
    {
        get => countsLeft;
        set
        {
            countsLeft = value;
            if (countsLeft == 0) CountdownFinished?.Invoke();
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
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (CountsLeft > 0) TimeLeft -= Time.deltaTime;
    }

    private void DecreaseCount()
    {
        TimeLeft += 1f;
        CountsLeft--;
    }

    private void SetTextColor()
    {
        float alpha = CountsLeft > 0 ? 0.87f * timeLeft : 0f;
        text.color = new Color(0f, 0f, 0f, alpha);
    }
}
