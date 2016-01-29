using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float animationTime = 0.35f;
    public Ease ease = Ease.OutBack;
    private RectTransform rect;
    public Vector3 baseRotation;
    private Sequence sequence;

    public bool HoverEnabled { get; set; }

    void Start()
    {
        HoverEnabled = true;
        rect = GetComponent<RectTransform>();
        baseRotation = rect.rotation.eulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HoverEnabled)
        {
            sequence = DOTween.Sequence()
              .Join(transform.DORotate(Vector3.forward, animationTime).SetEase(ease))
              .Join(transform.DOScale(1.3f, animationTime).SetEase(ease));

            transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HoverEnabled)
        {
            sequence.SmoothRewind();

            //.OnComplete(() => Reset(true));
        }
    }

    public void Reset(bool hoverEnabled)
    {
        HoverEnabled = hoverEnabled;
        ResetRotationAndScale();
    }

    public void ResetRotationAndScale()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
