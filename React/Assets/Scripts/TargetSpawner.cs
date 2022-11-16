using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner instance;

    public TargetDisplay targetPrefab;
    public const int MaxSpawningAttempts = 10;

    private void Awake()
    {
        instance = this;
    }

    public TargetDisplay Spawn(Target target)
    {
        TargetDisplay targetInstance = Instantiate(targetPrefab, GetValidSpawn(), Quaternion.identity, transform);
        targetInstance.SetTarget(target);
        return targetInstance;
    }

    public void DestroyTargets()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }

    private Vector2 GetValidSpawn()
    {
        while (true)
        {
            Vector2 position = new(Random.Range(0f, 1920f), Random.Range(0f, 1080f));
            if (!Physics2D.OverlapCircle(position, 60f)) return position;
        }
    }
}
