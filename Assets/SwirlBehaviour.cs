using UnityEngine;
using System.Collections;
using Adic;

[RequireComponent(typeof(Collider2D))]
public class SwirlBehaviour : MonoBehaviour
{

    public float rotationSpeed = 1;
    [Inject]
    public GameStateStore Store { get; set; }


    // Use this for initialization
    void Start()
    {
        this.Inject();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
    }

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
