public enum RoundResult
{
    Correct,
    TimedOut,
    Incorrect
}

public class Round
{
    private Target correctTarget;
    private float timeLimit;
    private int spawnCount;

    public delegate void RoundCompletedHandler(RoundResult result);
    public event RoundCompletedHandler RoundCompleted;

    private readonly GameInterface UI = GameInterface.instance;

    public Round(float timeLimit, int spawnCount)
    {
        correctTarget = Target.GetTarget();
        this.timeLimit = timeLimit;
        this.spawnCount = spawnCount;

        UI.CorrectTargetDisplay.SetTarget(correctTarget);
        UI.Countdown.CountdownFinished += StartRound;
    }

    private void StartRound()
    {
        GenerateTargets();
        UI.VisualTimer.StartTimer(timeLimit);
        UI.Countdown.CountdownFinished -= StartRound;
        UI.VisualTimer.TimerEnded += TimeOut;
    }

    private void EndRound()
    {
        TargetSpawner.instance.DestroyTargets();
        UI.VisualTimer.StopTimer();
        UI.VisualTimer.TimerEnded -= TimeOut;
    }

    private void GenerateTargets()
    {
        GenerateTarget(correctTarget);
        for (int i = 0; i < spawnCount - 1; i++)
        {
            Target incorrectTarget = Target.GetWrongTarget(correctTarget);
            GenerateTarget(incorrectTarget);
        }
    }

    private void GenerateTarget(Target target)
    {
        TargetDisplay targetInstance = TargetSpawner.instance.Spawn(target);
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
