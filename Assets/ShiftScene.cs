using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Adic;

public class ShiftScene : MonoBehaviour
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
        if (Mouse.IsLeftMouseButtonIsClicked) {
            Store.AdvanceState (Scenes.Overworld);
        }
    }
}
