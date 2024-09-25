using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public bool isStarting;
    [SerializeField] Camera startCamera;

    float zoom;
    float velocity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        zoom = startCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //start zooming in coroutine.
            Debug.Log("starting");
            StartCoroutine(BeginScence());
        }
    }

    IEnumerator BeginScence()
    {
        //move the letters out of the way.

        yield return new WaitForSeconds(1f);

        //zooming in 
        while(zoom > 30)
        {
            Debug.Log(zoom);
            zoom -= Time.deltaTime * 7f;
            startCamera.orthographicSize = Mathf.SmoothDamp(startCamera.orthographicSize, zoom, ref velocity, 1f);
            yield return null;
        }

    }
}
