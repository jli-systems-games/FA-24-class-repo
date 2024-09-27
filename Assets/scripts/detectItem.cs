using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectItem : MonoBehaviour
{
    public Camera camera;
    public GameObject item;
    Plane[] cameraFrustum;
    Collider collider;

    public bool withinTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            withinTarget = true;
        }
        else
        {
            withinTarget = false;
        }
    }
}
