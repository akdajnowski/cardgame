using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnStateChangeHandler();
public class GameStateStore
{
    private static GameStateStore _instance;

    public static GameStateStore Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStateStore();
            return _instance;
        }
    }

    public OnStateChangeHandler OnStateCHange;

    public Scenes ReturnScene { get; internal set; }

    public void AdvanceState(Scenes state)
    {
        this.ReturnScene = state;
        SceneManager.LoadScene((int)state);

        if (OnStateCHange!=null)
        {
            OnStateCHange();
        }


    }

    private GameStateStore()
    {
        ReturnScene = Scenes.Intro;
    }
    public void DialogResolution(int hp, int crew, bool negative)
    {
        //throw new NotImplementedException();
    }
}
