using UnityEngine;
using System.Collections;
using Adic;

public class StartGame : MonoBehaviour {

    [Inject]
    public GameStateStore Store { get; set; }

    public void Start()
    {
        this.Inject();
    }
	
    public void Apply()
    {
        Store.AdvanceState(Scenes.Overworld);
        Debug.Log("Weszłem");
    }
}
