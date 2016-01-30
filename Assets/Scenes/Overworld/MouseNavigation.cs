using UnityEngine;
using DG.Tweening;
using Adic;


public class MouseNavigation : MonoBehaviour
{
    private static readonly float FrontRotationAngle = 270.0f;

    [Inject]
    public GameStateStore GameStore { get; set; }

    void Update ()
    {
        if (Mouse.IsLeftMouseButtonIsClicked) {
            transform.RotateAnimate (Input.mousePosition, FrontRotationAngle)
            .AppendIntoSequence (transform.MoveToPlace (Input.mousePosition));             
        }          


        transform.RotateInPlace (Input.mousePosition, FrontRotationAngle);
        if(CollisionDetected(Input.mousePosition))
        {
            GameStore.AdvanceState(Scenes.Dialog);
        }
    }

    private bool CollisionDetected(Vector3 mousePosition)
    {
        return false;
    }
}