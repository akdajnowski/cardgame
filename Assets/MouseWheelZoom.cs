using UnityEngine;
using DG.Tweening;

public class MouseWheelZoom : MonoBehaviour
{
    private float offset = 0;
    public float zoomSpeed = 1;
    public float upperLimit = 30;
    private float baseZoom;

    // Use this for initialization
    void Start()
    {
        baseZoom = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
        {
            offset += zoomSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // back
        {
            offset -= zoomSpeed;
        }
        offset = offset.Restrict(0, upperLimit);
        DOTween.To(() => Camera.main.orthographicSize, x => Camera.main.orthographicSize = x, baseZoom + offset, 1f);
    }
}
