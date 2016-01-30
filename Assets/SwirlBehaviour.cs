using UnityEngine;
using System.Collections;
using Adic;

public class SwirlBehaviour : MonoBehaviour
{
    private bool visited;

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
        Debug.Log ("Already visited: " + visited);
        if (!visited) {
            visited = true;
            Debug.Log ("Napierdalamy w swirlu z paramsem: " + stuff);
            Store.AdvanceState (Scenes.CardBattle);
        }
    }
}
