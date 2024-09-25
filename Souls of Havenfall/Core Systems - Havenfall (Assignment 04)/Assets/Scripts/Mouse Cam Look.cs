using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;

    public GameObject player;

    private Vector2 mouseLook;
    private Vector2 smoothV;

    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        // Incrementally add to the camera look
        mouseLook += smoothV;

        // Clamp vertical rotation to avoid flipping
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        // Apply the vertical rotation (X-axis) for the camera
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        // Apply the horizontal rotation (Y-axis) for the player
        player.transform.localRotation = Quaternion.Euler(0, mouseLook.x, 0);
    }
}
