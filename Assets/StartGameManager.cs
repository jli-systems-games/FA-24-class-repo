using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public bool isStarting;
    [SerializeField] Camera startCamera;
    [SerializeField] GameObject player, Camera, npc,titleParent,skytext;

    float zoom;
    float velocity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //zoom = startCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //start zooming in coroutine.
            //Debug.Log("starting");
            StartCoroutine(BeginScence());
        }
    }

    IEnumerator BeginScence()
    {
        //move the letters out of the way.

        yield return new WaitForSeconds(1f);

        //zooming in 
        float elapsedTime = 0;
        while(elapsedTime < 3.75f)
        {
           
            elapsedTime += Time.deltaTime;
            //startCamera.orthographicSize = Mathf.SmoothDamp(startCamera.orthographicSize, zoom, ref velocity, 1f);
            startCamera.transform.position += Vector3.down * 1.5f;
            yield return null;
        }
        Camera.SetActive(true);
        player.SetActive(true);
        npc.SetActive(true);
        startCamera.enabled = false;
        titleParent.SetActive(false);
        skytext.SetActive(false);

    }
}
