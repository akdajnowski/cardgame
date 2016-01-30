using UnityEngine;
using DG.Tweening;
using Adic;


public class MouseNavigation : MonoBehaviour
{
    private static readonly float FrontRotationAngle = 270.0f;

    [Inject]
    public GameStateStore GameStore { get; set; }

    void Start ()
    {
        this.Inject ();
        if (GameStore.OverworldState != null) {
            transform.position = GameStore.OverworldState.ShipPosition;
            transform.rotation = GameStore.OverworldState.ShipRotation; 
        }

    }

    void Update ()
    {
        if (Mouse.IsLeftMouseButtonIsClicked) {
            transform.RotateAnimate (Input.mousePosition, FrontRotationAngle)
            .AppendIntoSequence (transform.MoveToPlace (Input.mousePosition));             
        }          
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.tag == "swirl") {
            Debug.Log ("Kolizja kurwo z : " + coll.gameObject.name);
            GameStore.OverworldState.ShipPosition = transform.position;
            GameStore.OverworldState.ShipRotation = transform.rotation;
            coll.gameObject.SendMessage ("CollisionPerform", coll.gameObject.name);
        }
    }
}