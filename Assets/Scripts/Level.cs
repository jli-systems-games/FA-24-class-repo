using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public int Wall { get;  }
    public int Spike { get;  }

    public Level(int w, int s = 0)
    {
        this.Wall = w;
        this.Spike = s;

    }

    public int assignValue(string n)
    {
        int i;
        if(n == nameof(Wall))
        {
            i = this.Wall;

        }
        else
        {
            i = this.Spike;
        }

        return i;
    }
}
