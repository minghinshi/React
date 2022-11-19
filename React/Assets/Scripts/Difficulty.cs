using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    public bool targetMoves;
    public bool targetRotates;
    public int startingCount;
    public int correctAnswers;
    public bool[] randomizedItems;
}
