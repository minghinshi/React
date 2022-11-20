using System.Collections.Generic;
using UnityEngine;

public class CorrectTargetsDisplay : MonoBehaviour
{
    public TargetDisplay prefab;

    public void DisplayTargets(List<Target> targets)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        foreach (Target target in targets)
            Instantiate(prefab, transform).SetTarget(target);
    }
}
