using UnityEngine;
using Adic;

[RequireComponent(typeof(Collider2D))]
public class WorldEventPlaceBehaviour : MonoBehaviour
{
    [Inject]
    public GameStateStore Store { get; set; }

    void Start() { this.Inject(); }

    void CollisionPerform(string stuff)
    {
        var visitedDictionary = Store.OverworldState.VisitedIslands;
        if (!visitedDictionary.ContainsKey(stuff) || !visitedDictionary[stuff])
        {
            visitedDictionary[stuff] = true;
            Debug.Log("We encountered: " + stuff);
            Store.OverworldState.CurrentIsland = stuff;
            Store.AdvanceState(Scenes.Dialog);
        }
    }
}
