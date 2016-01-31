using UnityEngine;
using Adic;

[RequireComponent (typeof(Collider2D))]
public class WorldEventPlaceBehaviour : MonoBehaviour
{
    [Inject]
    public GameStateStore Store { get; set; }

    [Inject ("DialogRenderer")]
    public Transform dialogRenderer;
    private DialogRenderer script;

    void Start ()
    {
        this.Inject ();
        script = dialogRenderer.GetComponent<DialogRenderer> ();
    }

    void CollisionPerform (string islandKey)
    {
        var visitedDictionary = Store.OverworldState.VisitedIslands;
        if (!visitedDictionary.ContainsKey (islandKey) || !visitedDictionary [islandKey]) {
            visitedDictionary [islandKey] = true;
            Debug.Log ("We encountered: " + islandKey);
            Store.OverworldState.CurrentIsland = islandKey;
            script.RunDialog (islandKey, gameObject);
        }
    }
}
