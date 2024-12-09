using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_ViewCheck : MonoBehaviour
{
    Camera main;
    [SerializeField]
    Transform left, right;
    private void OnEnable()
    {   
        main = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();   
        MeshRenderer mr = GetComponent<MeshRenderer>();
        CheckPrescence(mr);
    }
    void CheckPrescence(MeshRenderer _mr)
    {
        Plane[] CameraPlanes = GeometryUtility.CalculateFrustumPlanes(main);//put other camera reference here;
        bool isInCamera = GeometryUtility.TestPlanesAABB(CameraPlanes, _mr.bounds);

        if (!isInCamera)
        {
            Vector3 pLoc = transform.parent.position;
            if(pLoc.x <= 0)
            {
                transform.localPosition = left.localPosition;

            }else if (pLoc.x >= 4)
            {
                transform.localPosition = right.localPosition;
            }
        }
    }

}
