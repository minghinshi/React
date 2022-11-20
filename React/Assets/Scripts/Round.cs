using System.Collections.Generic;
using UnityEngine;

public enum RoundResult
{
    Correct,
    TimedOut,
    Incorrect
}

public enum RoundStatus
{
    Inactive,
    Countdown,
    Running,
}

public class Round
{
    private RoundStatus status = RoundStatus.Inactive;
    private readonly List<Target> correctTargets = new();

    private readonly float timeLimit;
    private readonly int spawnCount;
    private readonly int correctCount;

    public delegate void RoundCompletedHandler(RoundResult result);
    public event RoundCompletedHandler RoundCompleted;

    private readonly GameInterface UI = GameInterface.instance;

    public Round(float timeLimit, int spawnCount, int correctCount)
    {
        this.timeLimit = timeLimit;
        this.spawnCount = spawnCount;
        this.correctCount = correctCount;

        SetCorrectTargets();
        UI.GameplayInfoPanels.SwitchPanel(UI.RoundIntro);
        UI.CorrectTargetsDisplay.DisplayTargets(correctTargets);
    }

    public void StartCountdown()
    {
        status = RoundStatus.Countdown;
        Countdown.instance.CountDown(3);
        UI.GameplayInfoPanels.TogglePanel(UI.RoundIntro);
        UI.Countdown.CountdownFinished += StartRound;
    }

    private void StartRound()
    {
        status = RoundStatus.Running;
        GenerateTargets();
        UI.Timer.StartTimer(timeLimit);
        UI.Countdown.CountdownFinished -= StartRound;
        UI.Timer.TimerEnded += TimeOut;
    }

    private void EndRound(RoundResult result)
    {
        status = RoundStatus.Inactive;
        UI.Timer.PauseTimer();
        UI.Timer.TimerEnded -= TimeOut;
        TargetManager.instance.DestroyTargets();
        RoundCompleted?.Invoke(result);
    }

    public void ForceEndRound()
    {
        UI.Countdown.CountdownFinished -= StartRound;
        UI.Countdown.CountdownFinished -= Restart;
        UI.Timer.TimerEnded -= TimeOut;
        TargetManager.instance.DestroyTargets();
    }

    public void Pause()
    {

        if (status == RoundStatus.Countdown)
            UI.Countdown.Terminate();
        else if (status == RoundStatus.Running)
        {
            TargetManager.instance.HideAllTargets();
            UI.Timer.PauseTimer();
        }
    }

    public void Continue()
    {
        if (status == RoundStatus.Countdown || status == RoundStatus.Running)
            UI.Countdown.CountDown(3);
        if (status == RoundStatus.Running)
            UI.Countdown.CountdownFinished += Restart;
    }

    private void Restart()
    {
        TargetManager.instance.ShowAllTargets();
        UI.Timer.ContinueTimer();
        UI.Countdown.CountdownFinished -= Restart;
    }

    private void SetCorrectTargets()
    {
        for (int i = 0; i < correctCount; i++)
        {
            Target target = TargetFactory.instance.GetTarget(correctTargets);
            correctTargets.Add(target);
        }
    }

    private void GenerateTargets()
    {
        correctTargets.ForEach(GenerateTarget);
        for (int i = 0; i < spawnCount - correctCount; i++)
        {
            Target target = TargetFactory.instance.GetTarget(correctTargets);
            GenerateTarget(target);
        }
    }

    private void GenerateTarget(Target target)
    {
        TargetDisplay targetInstance = TargetManager.instance.Spawn(target);
        targetInstance.SetClickAction(() => SubmitAnswer(target));
        targetInstance.SetClickAction(() => Object.Destroy(targetInstance.gameObject));
    }

    private void SubmitAnswer(Target target)
    {
        if (IsTargetCorrect(target))
        {
            AudioHandler.instance.Play("Correct");
            if (correctTargets.Count == 0)
                EndRound(RoundResult.Correct);
        }
        else EndRound(RoundResult.Incorrect);
    }

    private void TimeOut()
    {
        EndRound(RoundResult.TimedOut);
    }

    private bool IsTargetCorrect(Target target)
    {
        foreach (Target correctTarget in correctTargets)
        {
            if (target.Equals(correctTarget))
            {
                correctTargets.Remove(correctTarget);
                return true;
            }
        }
        return false;
    }
}
