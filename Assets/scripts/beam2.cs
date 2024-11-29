using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam2 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform lightPoint;
    public int pointIndex;

    Vector2 linePointv2;
    Vector2 direction;

    bool mirror1collision;
    bool mirror2collision;
    bool mirror3collision;
    bool mirror4collision;
    bool mirror5collision;

    public GameObject startButton;
    public GameObject restartButton;
    public GameObject fire;

    public Vector2 raycast1;
    public Vector2 raycast2;

    public Vector2 oldHitPoint;
    public bool firstHit;
    public Vector2 firstHitPoint;

    public bool stopLine;
    public bool won;

    void Start()
    {
        raycast1 = transform.position;
        raycast2 = transform.right;
    }

    void Update()
    {

        UpdateLight();

    }

    void UpdateLight()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast1, raycast2);

        if(firstHit == false)
        {
            lineRenderer.SetPosition(0, lightPoint.position);
            firstHitPoint = hit.point;
            lineRenderer.SetPosition(1, firstHitPoint);
            linePointv2 = new Vector2(lightPoint.position.x, lightPoint.position.y);
            direction = (hit.point - linePointv2);
            firstHit = true;
        }
        else
        {
            if (stopLine == false)
            {
                if (hit.collider.gameObject.tag == "left")
                {
                    hit.collider.gameObject.SetActive(false);

                    RaycastHit2D hit2 = Physics2D.Raycast(raycast1, raycast2);

                    if (hit2.collider.gameObject.transform.localEulerAngles.z == -180f)
                    {
                        Debug.Log("left180");
                        lineRenderer.positionCount++;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit2.point);

                        raycast1 = hit2.point;
                        raycast2 = -(hit2.collider.gameObject.transform.right);
                        //direction = (hit.point - oldHitPoint);
                    }
                }

                if (hit.collider.gameObject.tag == "down")
                {
                
                    oldHitPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                    direction = (hit.point - oldHitPoint);
                }

                if (hit.collider.gameObject.tag == "right")
                {
                    Debug.Log("disablegameobject and turn bool on");
                }

                if (hit.collider.gameObject.tag == "up")
                {
                    Debug.Log("disablegameobject and turn bool off");
                }

                if (hit.collider.gameObject.tag == "UorR")
                {
                    Debug.Log("if bool is on, right. if bool is off, up");
                }

                if(oldHitPoint == hit.point)
                {
                     startButton.SetActive(false);
                     restartButton.SetActive(true);
                     stopLine = true;
                }

                if (hit.collider.gameObject.tag != "mirror" && hit.collider.gameObject.tag != "target")
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                    startButton.SetActive(false);
                    restartButton.SetActive(true);
                    stopLine = true;
                }

                if (hit.collider.gameObject.tag == "target")
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                    won = true;
                }
            }

            if (won == true)
            {
                fire.SetActive(true);
                startButton.SetActive(false);
                restartButton.SetActive(true);
            }
        }

    }

}