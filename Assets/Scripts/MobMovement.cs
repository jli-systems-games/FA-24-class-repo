using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
  

    public Transform plyr, rightSide, leftSide,ghoul;
    List<Transform> spawnPoints = new List<Transform>();

    float steps;
    public float speed = 1.5f;
    RaycastHit hit;
    Ray _ray;
    public bool isHunting,gameOver,hidingStarted;
    public CameraMovement cam;
    public GhoulAnimationManager animationManager;
    public GameManager manage;
    int pos;
    float plyDistance,t,elapsedTime;
    bool started;
    AudioSource cue;
    Renderer monst;
    Vector3 ogPos;
    Quaternion ogRotation;
   
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints.Add(rightSide);
        spawnPoints.Add(leftSide);
        ogRotation = ghoul.rotation;
        cue = GetComponent<AudioSource>();
        monst = GetComponent<Renderer>();
        cue.Play();
        cue.volume = 0f;
        ogPos = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manage.gameStart)
        {   steps = speed * Time.deltaTime;
            plyDistance = Vector3.Distance(transform.position, plyr.position);
            if (manage.startAudio)
            {
                cue.volume = 0.25f;
                manage.ChangeState(GameState.GamePlay);
                manage.startAudio = false;
            }

            if (pos == 0)
            {
                _ray = new Ray(transform.position, -transform.right);
                ghoul.rotation = ogRotation;
                cue.panStereo = 1.0f;
            }
            else
            {
                //Debug.Log(ghoul.rotation.y);
                _ray = new Ray(transform.position, transform.right);
                ghoul.rotation = Quaternion.Euler(0, ghoul.rotation.y + 90, 0);
                cue.panStereo = -1.0f;
            }
            if(!started)
            {
                reSpawn();
                started = true;
               
           
            }
            else
            {
      
                if (!hidingStarted)
                {
                    StartCoroutine(Hide());
                    
                    hidingStarted = true;
                
                }
            
            }
            if (isHunting)
                {   //Debug.Log("getting close:" + plyDistance);
                
                    transform.position = Vector3.MoveTowards(transform.position, plyr.position, steps);
                    if(plyDistance < 15f && cue.volume <= 0.6f)
                    {
                        cue.volume += 0.03f;
                
                    }
               
                }

            if(Physics.Raycast(_ray, out hit, 2f))
            {
                //Debug.Log("Jumpscare!");
                if(hit.collider.tag == "Player")
                {
                    animationManager.ChangeStage(AnimationStages.Jumpscare);
                    isHunting = false;
                    gameOver = true;
                    manage.ChangeState(GameState.EndScreen);
                }
            
            }

        }
        

     
    }

    
    void reSpawn()
    {
        pos = Mathf.FloorToInt(Random.Range(0, spawnPoints.Count));
        transform.position = spawnPoints[pos].position;
        //Debug.Log(pos);
        animationManager.ChangeStage(AnimationStages.Hunting);
        isHunting = true;


    }

    private IEnumerator Hide()
    { float endPoint; 
        float startPoint = transform.position.x;
        float moveDuration = 1.5f;
        while(!isHunting && !gameOver)
        {
           // Debug.Log("sPoint:" + startPoint);
            if(pos == 0)
            {   
                endPoint= transform.position.x + 2.0f;
                //Debug.Log(endPoint);

            }
            else
            {
                endPoint = transform.position.x - 2.0f;
                //Debug.Log(endPoint);
            }

            
            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;
                
                t = elapsedTime / moveDuration;
                cue.volume -= t;
                transform.position = new Vector3(Mathf.Lerp(startPoint, endPoint, t), transform.position.y, transform.position.z);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            elapsedTime = 0f;

           while (monst.isVisible && !cam.isLookingForward )  
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
                yield return null;
            }
            //reset transform to behind player.
            transform.position = ogPos;
            yield return new WaitForSeconds(1f);
            isHunting = true;
            started = false;
          


        }
        
      
    }
  
}
