using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public static GameInterface instance;

    public Countdown Countdown;
    public TargetDisplay CorrectTargetDisplay;

    private void Awake()
    {
        instance = this;
    }
}
