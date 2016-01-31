using UnityEngine;
using DG.Tweening;
using Adic;
using System.Linq;


public class MouseNavigation : MonoBehaviour
{
    private static readonly float FrontRotationAngle = 270.0f;
    public bool restrictShipMovement = false;

    [Inject]
    public GameStateStore GameStore { get; set; }

    void Start()
    {
        this.Inject();
        if (GameStore.OverworldState != null && GameStore.OverworldState.CurrentIsland != null)
        {
            transform.position = GameStore.OverworldState.ShipPosition;
            transform.rotation = GameStore.OverworldState.ShipRotation;
        }

    }

    void Update()
    {
        if (Mouse.IsLeftMouseButtonIsClicked)
        {
            var target = restrictShipMovement ? RestrictMouseClick(Input.mousePosition) : Input.mousePosition;
            transform.RotateAnimate(Input.mousePosition, FrontRotationAngle)
                .AppendIntoSequence(transform.MoveToPlace(target));
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "WorldEventPlace")
        {
            Debug.Log("Kolizja kurwo z : " + coll.gameObject.name);
            GameStore.OverworldState.ShipPosition = transform.position;
            GameStore.OverworldState.ShipRotation = transform.rotation;
            coll.gameObject.SendMessage("CollisionPerform", coll.gameObject.name);
        }
    }

    private Vector3 RestrictMouseClick(Vector3 clicked)
    {
        var worldViewed = Camera.main.ScreenToWorldPoint(clicked);
        return Camera.main.WorldToScreenPoint(
            new Vector3(ForceValueWithinRange(worldViewed.x, -32.3f, 33f),
                ForceValueWithinRange(worldViewed.y, -29.5f, 30.4f)));
    }

    private float ForceValueWithinRange(float value, float minimum, float maximum)
    {
        if (value > maximum)
        {
            return maximum;
        }

        if (value < minimum)
        {
            return minimum;
        }

        return value;
    }
}