using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TargetDisplay : MonoBehaviour
{
    private Target target;
    private Image image;
    private Button button;
    private new CircleCollider2D collider;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        collider = GetComponent<CircleCollider2D>();
    }

    public void SetTarget(Target target)
    {
        this.target = target;
        image.color = target.GetColor();
    }

    public void SetClickAction(UnityAction unityAction)
    {
        button.onClick.AddListener(unityAction);
    }
}
