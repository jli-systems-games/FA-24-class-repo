using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3, Vector3> RenderLine;
    public static event Action<Vector3, Vector3> RemoveLine;
    public static Stack<Vector3> points = new Stack<Vector3>();
    public static void DrawLine(Vector3 direction, Vector3 nP)
    {
        RenderLine?.Invoke(direction, nP);
    }
    public static void DelLine(Vector3 nP, Vector3 d)
    {
        RemoveLine?.Invoke(nP,d);
    }
}
