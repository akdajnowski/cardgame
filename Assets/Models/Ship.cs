using UnityEngine;
using System.Collections;
using System;

public class Ship
{
    private int MaxHP;
    private int MaxCrew;


    private int hp;
    private int crew;

    public Ship(int hp, int crew)
    {
        MaxHP = this.hp = hp;
        MaxCrew = this.crew = crew;
    }


    /// <summary>
    /// Method receives hp and crew bounds, picks a random number from these and subtract it from the current HP and crew state;
    /// </summary>
    /// <param name="hpRange"></param>
    /// <param name="crewRange"></param>
    void Loose(int[] hpRange, int[] crewRange)
    {
        if (hpRange.Length != 2 || crewRange.Length != 2)
        {
            throw new ArgumentException("hp and crew MUST be 2-element arrays");
        }

        var rand = new System.Random();
        hp -= rand.Next(hpRange[0], hpRange[1]);
        crew -= rand.Next(crewRange[0], crewRange[1]);

        if(hp < 0)
        {
            hp = 0;
        }

        if(crew < 0)
        {
            crew = 0;
        }
    }

    void Win(int[] hpRange, int[] crewRange)
    {
        if (hpRange.Length != 2 || crewRange.Length != 2)
        {
            throw new ArgumentException("ho and crew MUST be 2-element arrays");
        }

        var rand = new System.Random();
        hp += rand.Next(hpRange[0], hpRange[1]);
        crew += rand.Next(crewRange[0], crewRange[1]);

        if (hp > MaxHP)
        {
            hp = MaxHP;
        }

        if (crew > MaxCrew)
        {
            crew = MaxCrew;
        }

    }

    bool StillAlive()
    {
        return hp > 0 && crew > 0;
    }


}
