using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public static GameInterface instance;

    public Countdown Countdown;
    public TargetDisplay CorrectTargetDisplay;
    public VisualTimer VisualTimer;

    private void Awake()
    {
        instance = this;
    }
}
