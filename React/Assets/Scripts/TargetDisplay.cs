using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TargetDisplay : MonoBehaviour
{
    [SerializeField] private Image ballImage;
    [SerializeField] private Image modifierImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;

    private new Rigidbody2D rigidbody;
    private VisibilityHandler visibilityHandler;

    private Vector2 velocity;
    private float angularVelocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        visibilityHandler = GetComponent<VisibilityHandler>();
    }

    public void SetTarget(Target target)
    {
        ballImage.color = target.GetColor();
        modifierImage.sprite = target.GetSprite();
        text.text = target.GetText();
    }

    public void SetClickAction(UnityAction unityAction)
    {
        button.onClick.AddListener(unityAction);
    }

    public void SetRandomVelocity()
    {
        rigidbody.velocity = Random.insideUnitCircle.normalized * Random.Range(200f, 400f);
    }

    public void SetRandomRotation()
    {
        rigidbody.angularVelocity = Random.Range(-90f, 90f);
        rigidbody.rotation = Random.Range(-180f, 180f);
    }

    public void Hide()
    {
        visibilityHandler.SetInvisibleImmediately();
        angularVelocity = rigidbody.angularVelocity;
        velocity = rigidbody.velocity;
        rigidbody.Sleep();
    }

    public void Show()
    {
        rigidbody.WakeUp();
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
        visibilityHandler.SetVisibleImmediately();
    }
}
