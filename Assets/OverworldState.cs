using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class OverworldState
{
    public Vector3 ShipPosition { get; set; }

    public Quaternion ShipRotation { get; set; }

    public IDictionary<string, bool> VisitedIslands { get; set; }

    public string CurrentIsland { get; set; }

    public OverworldState ()
    {
        VisitedIslands = new Dictionary<string, bool> ();
    }
}


