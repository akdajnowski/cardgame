using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public delegate void OnStateChangeHandler ();
public class GameStateStore
{
    public OverworldState OverworldState { get; set; }

    public Ship Ship { get; set; }

    private static GameStateStore _instance;

    public static GameStateStore Instance {
        get {
            if (_instance == null)
                _instance = new GameStateStore {
                    OverworldState = new OverworldState(),
                    Ship = new Ship(50, 25)
                };
            return _instance;
        }
    }

    public OnStateChangeHandler OnStateCHange;

    public Scenes ReturnScene { get; internal set; }

    public void AdvanceState (Scenes state)
    {
        this.ReturnScene = state;
        Debug.Log ("Going to " + state);

        SceneManager.LoadScene ((int)state);

        if (OnStateCHange != null) {
            OnStateCHange ();
        }


    }

    private GameStateStore ()
    {
        ReturnScene = Scenes.Intro;
    }

    public void DialogResolution (int hp, int crew, bool negative)
    {
        //throw new NotImplementedException();
    }
}
