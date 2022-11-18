using UnityEngine;

public class Target
{
    private Color color;
    private Sprite sprite;
    private string text;

    public Target(Color color, Sprite sprite, string text)
    {
        this.color = color;
        this.sprite = sprite;
        this.text = text;
    }

    public Color GetColor() => color;
    public Sprite GetSprite() => sprite;
    public string GetText() => text;

    public bool Equals(Target target)
    {
        return color == target.color && sprite == target.sprite && text == target.text;
    }
}
