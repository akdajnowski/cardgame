using UnityEngine;
using System.Collections;
using Adic;

public class SwirlBehaviour : MonoBehaviour
{
    [Inject]
    public GameStateStore Store { get; set; }


    // Use this for initialization
    void Start ()
    {
        this.Inject ();
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }

    void CollisionPerform (string stuff)
    {
        var visitedDictionary = Store.OverworldState.VisitedIslands;
        if (!visitedDictionary.ContainsKey (stuff) || !visitedDictionary [stuff]) {
            visitedDictionary [stuff] = true;
            Debug.Log ("We encountered: " +stuff);
            Store.OverworldState.CurrentIsland = stuff;
            Store.AdvanceState (Scenes.Dialog);
        }
    }
}
