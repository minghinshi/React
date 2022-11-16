using UnityEngine;
using UnityEngine.UI;

public class TargetDisplay : MonoBehaviour
{
    private Target target;
    private Image image;
    private new CircleCollider2D collider;

    private void Awake()
    {
        image = GetComponent<Image>();
        collider = GetComponent<CircleCollider2D>();
    }

    public void SetTarget(Target target)
    {
        this.target = target;
        image.color = target.GetColor();
    }
}
