using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This is to keep track of the scenes which are build, calling scenes by names is unreliable, so we need to revert to call by number.
/// To set ids of each scene, go to Unity -> File -> Build settings
/// </summary>
public enum Scenes : int
{
    MainMenu = 0,
    CardBattle = 1
}

