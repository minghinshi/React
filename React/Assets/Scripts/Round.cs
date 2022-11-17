public class Round
{
    private Target correctTarget;
    private float timeLimit;
    private int spawnCount;

    public delegate void RoundCompletedHandler(bool isCorrect);
    public event RoundCompletedHandler RoundCompleted;

    public Round(float timeLimit, int spawnCount)
    {
        correctTarget = Target.GetTarget();
        this.timeLimit = timeLimit;
        this.spawnCount = spawnCount;

        GameInterface.instance.CorrectTargetDisplay.SetTarget(correctTarget);
        GameInterface.instance.Countdown.CountdownFinished += StartRound;
    }

    private void StartRound()
    {
        GenerateTargets();
        GameInterface.instance.VisualTimer.StartTimer(timeLimit);
        GameInterface.instance.Countdown.CountdownFinished -= StartRound;
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
        TargetSpawner.instance.DestroyTargets();
        GameInterface.instance.VisualTimer.StopTimer();
        RoundCompleted?.Invoke(target.Equals(correctTarget));
    }
}
