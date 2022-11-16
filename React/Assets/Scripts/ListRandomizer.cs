using System.Collections.Generic;
using UnityEngine;

public static class ListRandomizer
{
    public static T GetRandomItem<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
