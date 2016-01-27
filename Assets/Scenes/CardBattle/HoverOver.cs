using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rect;
    public float animationTime = 0.35f;
    public Ease ease = Ease.OutBack;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DORotate(Vector3.forward * 45, animationTime).SetEase(ease);
        transform.DOScale(1.3f, animationTime).SetEase(ease);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DORotate(Vector3.forward, animationTime).SetEase(ease);
        transform.DOScale(1f, animationTime).SetEase(ease);
    }

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
