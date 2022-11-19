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
    private Target correctTarget = TargetFactory.instance.GetTarget();

    private float timeLimit;
    private int spawnCount;

    public delegate void RoundCompletedHandler(RoundResult result);
    public event RoundCompletedHandler RoundCompleted;

    private readonly GameInterface UI = GameInterface.instance;

    public Round(float timeLimit, int spawnCount)
    {
        this.timeLimit = timeLimit;
        this.spawnCount = spawnCount;

        UI.GameplayInfoPanels.SwitchPanel(UI.RoundIntro);
        UI.CorrectTargetDisplay.SetTarget(correctTarget);
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

    private void EndRound()
    {
        status = RoundStatus.Inactive;
        UI.Timer.PauseTimer();
        UI.Timer.TimerEnded -= TimeOut;
        TargetManager.instance.DestroyTargets();
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

    private void GenerateTargets()
    {
        GenerateTarget(correctTarget);
        for (int i = 0; i < spawnCount - 1; i++)
        {
            Target incorrectTarget = TargetFactory.instance.GetWrongTarget(correctTarget);
            GenerateTarget(incorrectTarget);
        }
    }

    private void GenerateTarget(Target target)
    {
        ClickableTarget targetInstance = TargetManager.instance.Spawn(target);
        targetInstance.SetClickAction(() => SubmitAnswer(target));
    }

    private void SubmitAnswer(Target target)
    {
        EndRound();
        if (target.Equals(correctTarget)) RoundCompleted?.Invoke(RoundResult.Correct);
        else RoundCompleted?.Invoke(RoundResult.Incorrect);
    }

    private void TimeOut()
    {
        EndRound();
        RoundCompleted?.Invoke(RoundResult.TimedOut);
    }
}
