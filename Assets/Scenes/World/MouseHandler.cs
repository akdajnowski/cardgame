﻿using UnityEngine;
using Adic;

public class MouseHandler : MonoBehaviour
{
    void Update ()
    {
        if (Mouse.IsLeftMouseButtonIsClicked) {
            transform.MoveToPlace (Input.mousePosition);        
        }

        transform.RotateInPlace (Input.mousePosition);
    }
}
