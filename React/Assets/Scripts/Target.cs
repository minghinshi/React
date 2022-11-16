using System.Collections.Generic;
using UnityEngine;

public class Target
{
    public static List<Color> possibleColors = new()
    {
        new Color32(0xFA, 0xFA, 0xFA, 0xFF),    //white
        new Color32(0x42, 0x42, 0x42, 0xFF),    //black
        new Color32(0xF4, 0x43, 0x36, 0xFF),    //red
        new Color32(0xFF, 0xEB, 0x3B, 0xFF),    //yellow
        new Color32(0x4C, 0xAF, 0x50, 0xFF),    //green
        new Color32(0x21, 0x96, 0xF3, 0xFF)     //blue
    };

    private Color color;

    public static Target GetTarget()
    {
        Target target = new();
        target.Randomize();
        return target;
    }

    public static Target GetWrongTarget(Target correctTarget)
    {
        Target target = new();
        while (true)
        {
            target.Randomize();
            if (!target.Equals(correctTarget)) return target;
        }
    }

    public Color GetColor() => color;

    public void Randomize()
    {
        color = ListRandomizer.GetRandomItem(possibleColors);
    }

    public bool Equals(Target target)
    {
        return color == target.color;
    }
}
