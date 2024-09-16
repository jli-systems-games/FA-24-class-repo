using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    IEnumerator movement;

    public Transform plyr, rightSide, leftSide;
    List<Transform> spawnPoints = new List<Transform>();
    public Rigidbody rb;

    float steps;
    public float speed = 1f;
    RaycastHit hit;
    Ray _ray;
    public bool isHunting;
    int pos;
    Animator animate;
    float plyDistance;
    AudioSource cue;
    float maxDistance = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints.Add(rightSide);
        spawnPoints.Add(leftSide);
        movement = Hunt();
        animate = GetComponent<Animator>();
        cue = GetComponent<AudioSource>();
        cue.Play();
        StartCoroutine(movement);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        steps = speed * Time.deltaTime;
        plyDistance = Vector3.Distance(transform.position, plyr.position);
        Debug.Log(plyDistance);
       // _ray = new Ray(transform.position, transform.right);

        if(pos == 0)
        {
            _ray = new Ray(transform.position, -transform.right);
        }
        else
        {
            _ray = new Ray(transform.position, transform.right);
        }
        if(isHunting)
        {
            float volume = Mathf.Lerp(0.5f, 0f, plyDistance / maxDistance);
            transform.position = Vector3.MoveTowards(transform.position, plyr.position, steps);
            cue.volume = Mathf.Clamp(volume, 0f, 0.3f);
            
        }
        else
        {
            /*animate.enabled = true;
            animate.SetTrigger("Caught");*/
            Hide();
        }
        

        if(Physics.Raycast(_ray, out hit, 1f))
        {
            Debug.Log("Jumpscare!");
            isHunting = false;
        }
    }

    private IEnumerator Hunt()
    {
        while (true)
        {
            reSpawn();
            yield return new WaitForSeconds(7f);
        }
        

    }
    
    void reSpawn()
    {
        pos = Mathf.FloorToInt(Random.Range(0, spawnPoints.Count));
        transform.position = spawnPoints[pos].position;
        //Debug.Log(pos);
        isHunting = true;


    }

    void Hide()
    {
        if(pos == 0)
        {
            transform.Translate(Vector3.right);
        }
        else
        {
            transform.Translate(-Vector3.right);
        }
    }
  
}
