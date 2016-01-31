using Adic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adic.Injection;
using UnityEngine;

public class GlobalStateFactory : IFactory
{
    public object Create (InjectionContext context)
    {
        Debug.Log ("faktoria");
        return GameStateStore.Instance;
    }
}

