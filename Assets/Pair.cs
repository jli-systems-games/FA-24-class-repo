using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair 
{
     Vector3 _start;
     Vector3 _end;

    public Vector3 Start { get { return _start; } }
    public Vector3 End { get { return _end; } }

    public Pair(Vector3 start, Vector3 end)
    {
        this._start = start;
        this._end = end;
    }

}
