using UnityEngine;
using DG.Tweening;

public class MouseNavigation : MonoBehaviour
{
    private static readonly float FrontRotationAngle = 270.0f;

    void Update ()
    {
        if (Mouse.IsLeftMouseButtonIsClicked) {
            transform.RotateAnimate (Input.mousePosition, FrontRotationAngle)
            .AppendIntoSequence (transform.MoveToPlace (Input.mousePosition));             
        }          
    }
}