﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Adic;

public class ShiftScene : MonoBehaviour
{
    [Inject]
    public GameStateStore Store { get; set; }

    public Scenes Scene = Scenes.Overworld;
    // Use this for initialization
    void Start ()
    {
        this.Inject ();
        Debug.Log ("Shift Scene behaviour injected");
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (Mouse.IsLeftMouseButtonIsClicked) {
            Store.AdvanceState (Scene);
        }
    }
}
