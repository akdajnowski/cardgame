using UnityEngine;
using System.Collections;
using Adic;

public class StartGame : MonoBehaviour
{

    [Inject]
    public GameStateStore Store { get; set; }

    public void Start ()
    {
        this.Inject ();
        Debug.Log ("Start Game behaviour injected");
    }

    public void Apply ()
    {
        Store.AdvanceState (Scenes.Overworld);
        Debug.Log ("Weszłem");
    }
}
