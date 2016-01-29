using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MouseHandler : MonoBehaviour
{
    private static readonly int LeftMouseButton = 0;

    void Update ()
    {
        if (IsLeftMouseButtonIsClicked ()) {
            MoveShip ();         
        }

        RotateShip ();
    }

    private void RotateShip ()
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        float deltaX = Input.mousePosition.x - objectPos.x;
        float deltaY = Input.mousePosition.y - objectPos.y;
        float angle = Mathf.Atan2 (deltaY, deltaX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
    }

    private bool IsLeftMouseButtonIsClicked ()
    {
        return Input.GetMouseButtonDown (LeftMouseButton);
    }

    private void MoveShip ()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        pz.z = 0;
        gameObject.transform.DOMove (pz, 2);
    }
}
