using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    public bool clicked = false;
    private LilyPadSpawner spawner;
    private CameraFollow cameraFollow;

    private void Start()
    {
        spawner = FindObjectOfType<LilyPadSpawner>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        spawner.RegisterLilyPad(this);
    }

    private void OnMouseDown()
    {
        if (!clicked)
        {
            clicked = true;
            spawner.UpdateLilyPads(this.gameObject);

            cameraFollow.MoveToLilyPad(transform.position);
        }
    }
}
