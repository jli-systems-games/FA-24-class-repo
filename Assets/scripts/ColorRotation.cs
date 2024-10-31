using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRotation : MonoBehaviour
{
    public Light spotLight;
    public Color[] colors;
    public float changeInterval = 2f;

    private int colorIndex = 0;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            colorIndex = (colorIndex + 1) % colors.Length;
            spotLight.color = colors[colorIndex];
        }
    }
}
