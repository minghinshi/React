public class Round
{
    private Target correctTarget;

    public Round()
    {
        correctTarget = Target.GetTarget();
        GameInterface.instance.CorrectTargetDisplay.SetTarget(correctTarget);
        GameInterface.instance.Countdown.CountdownFinished += GenerateTargets;
    }

    public void EndRound()
    {
        GameInterface.instance.Countdown.CountdownFinished -= GenerateTargets;
    }

    private void GenerateTargets()
    {
        TargetSpawner.instance.Spawn(correctTarget);
        for (int i = 0; i < 30; i++)
        {
            Target incorrectTarget = Target.GetWrongTarget(correctTarget);
            TargetSpawner.instance.Spawn(incorrectTarget);
        }
    }
}
