using UnityEngine;

public class MouseNavigation : MonoBehaviour
{
    private static readonly float FrontRotationAngle = 270.0f;

    void Update ()
    {
        if (Mouse.IsLeftMouseButtonIsClicked) {
            transform.MoveToPlace (Input.mousePosition);        
        }

        transform.RotateInPlace (Input.mousePosition, FrontRotationAngle);
    }
}