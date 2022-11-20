using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    [SerializeField]
    private TargetDisplay targetPrefab;
    private Difficulty difficulty;

    private void Awake()
    {
        instance = this;
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }

    public TargetDisplay Spawn(Target target)
    {
        TargetDisplay targetInstance = Instantiate(targetPrefab, GetValidSpawn(), Quaternion.identity, transform);
        targetInstance.SetTarget(target);
        if (difficulty.targetMoves) targetInstance.SetRandomVelocity();
        if (difficulty.targetRotates) targetInstance.SetRandomRotation();
        return targetInstance;
    }

    public void ShowAllTargets()
    {

        foreach (TargetDisplay display in GetComponentsInChildren<TargetDisplay>()) display.Show();
    }

    public void HideAllTargets()
    {
        Physics.autoSimulation = false;
        foreach (TargetDisplay display in GetComponentsInChildren<TargetDisplay>()) display.Hide();
    }

    public void DestroyTargets()
    {
        foreach (Transform child in transform) 
            Destroy(child.gameObject);
    }

    private Vector2 GetValidSpawn()
    {
        while (true)
        {
            Vector2 position = new(Random.Range(-960f, 960f), Random.Range(-540f, 540f));
            if (!Physics2D.OverlapCircle(position, 70f)) return position;
        }
    }
}
