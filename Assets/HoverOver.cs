using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;
    private RectTransform rect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("Hovering", true);
        transform.SetAsLastSibling(); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("Hovering", false);
    }

    void Start()
    {
        rect = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();

        anim.SetFloat("IdleRotationZ", rect.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
