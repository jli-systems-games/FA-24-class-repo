using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    private float defDistanceRay = 100;
    public Transform beamPoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;

    public void Awake()
    {
        m_transform = GetComponent<Transform>();

    }

    public void Update()
    {
        ShootBeam();
    }

    void ShootBeam()
    {
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            Draw2DRay(beamPoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(beamPoint.position, beamPoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
