using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{   
    RaycastHit hit;
    Ray ray;

    public AudioClip[] clips;
    public MobMovement mob;
    public GhoulAnimationManager animationManager;
    public Light flashLight;
    bool isFlashON,isflickering;
    AudioSource on;

    int flashed = 10;
    // Start is called before the first frame update
    void Start()
    {
        on = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        ray= new Ray(transform.position, transform.forward);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("pressed");
            if(flashed > 0)
            { if (!isFlashON )
                {
                    flashLight.enabled = true;
                    on.Play();
                    isFlashON = true;
                    
                    if(Physics.Raycast(ray, out hit, 7f))
                        {
                             //Debug.Log(hit.collider.name);
                             if(hit.collider.tag == "Mob")
                            {
                                animationManager.ChangeStage(AnimationStages.Caught);
                                mob.isHunting = false;
                                mob.hidingStarted = false;
                            }
                             
                        }
                }
                else
                {   flashed--;
                    flashLight.enabled = false;
                    on.clip = clips[0];
                    on.Play();
                    isFlashON = false;
                }

            }
            else
            {  
                isFlashON = false;
            }
            
            
        }

        if(flashed == 3 && !isflickering)
        {
            StartCoroutine(flickering());
            isflickering = true;
        }
        
    }

    private IEnumerator flickering()
    {

        float flahDuration =1f;
        float elapsedT = 0f;

        on.clip = clips[1];
        on.Play();
        while(elapsedT < flahDuration)
        {
           elapsedT += Time.deltaTime;
           float t = elapsedT / flahDuration;
           flashLight.intensity = Mathf.Lerp(1.7f, 0f, t);
           yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        elapsedT = 0f;
        while (elapsedT < flahDuration)
        {
            elapsedT += Time.deltaTime;
            float t = elapsedT / flahDuration;
            flashLight.intensity = Mathf.Lerp(0f, 1.17f, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        elapsedT = 0f;
        while (elapsedT < flahDuration)
        {
            elapsedT += Time.deltaTime;
            float t = elapsedT / flahDuration;
            flashLight.intensity = Mathf.Lerp(1.14f, 0f, t);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        elapsedT = 0f;
        while (elapsedT < flahDuration)
        {
            elapsedT += Time.deltaTime;
            float t = elapsedT / flahDuration;
            flashLight.intensity = Mathf.Lerp(0f, 1.7f, t);
            yield return null;
        }
        yield break;
    }
}
