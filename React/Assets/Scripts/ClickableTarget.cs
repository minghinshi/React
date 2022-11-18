using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickableTarget : MonoBehaviour
{
    private Target target;

    [SerializeField] private Image ballImage;
    [SerializeField] private Image modifierImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;

    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Target target)
    {
        this.target = target;
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
        rigidbody.velocity = Random.insideUnitCircle.normalized * Random.Range(250f, 500f);
    }

    public void SetRandomRotation()
    {
        rigidbody.angularVelocity = Random.Range(-90f, 90f);
        rigidbody.rotation = Random.Range(-180f, 180f);
    }
}
