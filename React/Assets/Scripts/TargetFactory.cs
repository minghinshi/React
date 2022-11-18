using System.Collections.Generic;
using UnityEngine;

public class TargetFactory : MonoBehaviour
{
    public static TargetFactory instance;

    [SerializeField] private List<Color> colors;
    [SerializeField] private List<Sprite> modifiers;
    [SerializeField] private List<string> texts;

    private bool[] randomizeItem;

    private void Awake()
    {
        instance = this;
    }

    public void SetRandomizedItems(bool[] randomizeItem)
    {
        this.randomizeItem = randomizeItem;
    }

    public Target GetTarget()
    {
        Color color = randomizeItem[0] ? GetRandomItem(colors) : colors[0];
        Sprite modifier = randomizeItem[1] ? GetRandomItem(modifiers) : modifiers[0];
        string text = randomizeItem[2] ? GetRandomItem(texts) : texts[0];
        return new(color, modifier, text);
    }

    public Target GetWrongTarget(Target correctTarget)
    {
        while (true)
        {
            Target target = GetTarget();
            if (!target.Equals(correctTarget)) return target;
        }
    }

    private T GetRandomItem<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
