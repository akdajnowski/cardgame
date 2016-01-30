using System;
using UnityEngine;

public class GameStateStore
{
    private static GameStateStore _instance;
    public System.Collections.Generic.List<CardDescriptor> CardInformation;

    public static GameStateStore Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameStateStore();
            return _instance;
        }
    }

    public int ReturnScene { get; internal set; }

    private GameStateStore()
    {
    }
    public void DialogResolution(int hp, int crew, bool negative)
    {
        //throw new NotImplementedException();
    }
}
