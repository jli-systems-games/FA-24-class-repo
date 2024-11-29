using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform lightPoint;
    public int pointIndex;

    Vector2 linePointv2;
    Vector2 direction;

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
                if (hit.collider.gameObject.tag == "mirror")
                {
                    oldHitPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                    direction = (hit.point - oldHitPoint);
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
                    
                    stopLine = true;
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

        raycast1 = hit.point + (direction * -0.0001f);
        raycast2 = Vector2.Reflect(direction, hit.normal);
    }

    // {
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
    //     lineRenderer.SetPosition(0, lightPoint.position);
    //     lineRenderer.SetPosition(1, hit.point);

    //     linePointv2 = new Vector2(lightPoint.position.x, lightPoint.position.y);
    //     direction = (hit.point - linePointv2);

    //     if(hit.collider.gameObject.tag == "mirror" && mirror1collision == false)
    //     {
    //         RaycastHit2D hitNext = Physics2D.Raycast(hit.point, Vector2.Reflect(direction, hit.normal));
    //         lineRenderer.positionCount++;
    //         lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitNext.point);

    //         mirror1collision = true;
    //         direction = (hitNext.point - hit.point);

    //         if(hitNext.collider.gameObject.tag == "mirror" && mirror2collision == false)
    //         {
                
    //             RaycastHit2D hitNext2 = Physics2D.Raycast(hitNext.point, Vector2.Reflect(direction, hitNext.normal));
    //             lineRenderer.positionCount++;
    //             lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitNext2.point);

    //             mirror2collision = true;
    //             direction = (hitNext2.point - hitNext.point);

    //             if(hitNext2.point == new Vector2(-10.91672f, -5.699986f))
    //             {
    //                 fire.SetActive(true);
    //                 startButton.SetActive(false);
    //                 restartButton.SetActive(true);
    //             }
    //             else
    //             {//if(hitNext2.point != new Vector2(-10.91672f, -5.699986f))
    //                 startButton.SetActive(false);
    //                 restartButton.SetActive(true);
    //             }
                

    //         }
    //         else
    //             {//if(hitNext2.point != new Vector2(-10.91672f, -5.699986f))
    //                 startButton.SetActive(false);
    //                 restartButton.SetActive(true);
    //             }
    //     }
    //     else
    //             {//if(hitNext2.point != new Vector2(-10.91672f, -5.699986f))
    //                 startButton.SetActive(false);
    //                 restartButton.SetActive(true);
    //             }

    // }

    

    // void CheckHit(RaycastHit2D hitInfo, Vector2 direction, LineRenderer linerenderer)
    // {
    //     if(hitInfo.collider.gameObject.tag == "mirror")
    //     {
    //         Vector2 pos = hitInfo.point;
    //         Vector2 dir = Vector2.Reflect(direction, hitInfo.normal);

    //         line
    //     }
    // }

    // private float defDistanceRay = 100;
    // public Transform beamPoint;
    // public LineRenderer m_lineRenderer;
    // Transform m_transform;

    // public void Awake()
    // {
    //     m_transform = GetComponent<Transform>();

    // }

    // public void Update()
    // {
    //     ShootBeam();
    // }

    // void ShootBeam()
    // {
    //     if (Physics2D.Raycast(m_transform.position, transform.right))
    //     {
    //         RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
    //         Draw2DRay(beamPoint.position, _hit.point);
    //     }
    //     else
    //     {
    //         Draw2DRay(beamPoint.position, beamPoint.transform.right * defDistanceRay);
    //     }
    // }

    // void Draw2DRay(Vector2 startPos, Vector2 endPos)
    // {
    //     m_lineRenderer.SetPosition(0, startPos);
    //     m_lineRenderer.SetPosition(1, endPos);
    // }
}
