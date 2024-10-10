using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnowballRolling : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialScale;

    public GameObject rHand;
    public GameObject lHand;
    public float speed = 2;
    private float elapsedTime = 0f;

    public float mouseY;

    //https://discussions.unity.com/t/drag-gameobject-with-mouse/1798
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        initialScale = transform.localScale;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        float dragDistanceY = curPosition.y - transform.position.y;
        Debug.Log(dragDistanceY);

        if (dragDistanceY > 0)
        {
            float scaleFactor = 1 + dragDistanceY * 0.05f;
            transform.localScale = new Vector3(initialScale.x * scaleFactor, initialScale.y * scaleFactor, initialScale.z * scaleFactor);

            HandRolling();
        }
    }

    //https://discussions.unity.com/t/how-to-move-a-game-object-up-and-down-in-a-loop/238607
    public void HandRolling()
    {
        elapsedTime += Time.deltaTime;

        float yR = Mathf.PingPong(elapsedTime * speed, 1) * 1.5f - 3.5f;
        rHand.transform.position = new Vector3(rHand.transform.position.x, yR, rHand.transform.position.z);

        float yL = Mathf.PingPong(elapsedTime * speed, 1) * -1.5f - 2.5f;
        lHand.transform.position = new Vector3(lHand.transform.position.x, yL, lHand.transform.position.z);
    }
}
