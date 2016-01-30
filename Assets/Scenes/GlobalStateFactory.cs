using Adic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adic.Injection;

public class GlobalStateFactory : IFactory
{
    public object Create(InjectionContext context)
    {
        return GameStateStore.Instance;
    }
}

