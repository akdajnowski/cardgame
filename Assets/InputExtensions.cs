using UnityEngine;

public class Mouse
{
    private static readonly int LeftMouseButton = 0;

    public static bool IsLeftMouseButtonIsClicked { 
        get { return Input.GetMouseButtonDown (LeftMouseButton); } 
    }
}

