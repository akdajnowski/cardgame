using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float animationTime = 0.35f;
    public Ease ease = Ease.OutBack;
    private Sequence sequence;
    private RectTransform rect;
    private Quaternion _baseRotation;

    public bool HoverEnabled { get; set; }

    void Start()
    {
        HoverEnabled = true;
        rect = GetComponent<RectTransform>();
        _baseRotation = rect.rotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HoverEnabled)
        {
            sequence = DOTween.Sequence();
            sequence
              .Join(transform.DORotate(Vector3.forward, animationTime).SetEase(ease))
              .Join(transform.DOScale(1.3f, animationTime).SetEase(ease));

            transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HoverEnabled)
        {
            sequence = DOTween.Sequence();
            sequence
              .Join(transform.DORotateQuaternion(_baseRotation, animationTime).SetEase(ease))
              .Join(transform.DOScale(1f, animationTime).SetEase(ease))
              .OnComplete(() => Reset(true));
        }
    }

    public void Reset(bool hoverEnabled)
    {
        HoverEnabled = hoverEnabled;
        ResetRotationAndScale();
    }

    private void ResetRotationAndScale()
    {
        sequence
           .Join(transform.DORotateQuaternion(_baseRotation, animationTime))
           .Join(transform.DOScale(1f, animationTime));
    }
}
