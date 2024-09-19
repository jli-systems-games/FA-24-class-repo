using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHit : MonoBehaviour
{   
    RaycastHit hit;
    Ray ray;

    public AudioClip[] clips;
    public MobMovement mob;
    public GhoulAnimationManager animationManager;
    public Light flashLight;
    public Image battery;
    bool isFlashON,isflickering,isCoolingdown;
    AudioSource on;

    int flashed = 10;
    float coolDownTime = 1.75f;
    // Start is called before the first frame update
    void Start()
    {
        on = GetComponent<AudioSource>();
        Color img = battery.color;
        img.a = 0f;
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
                    isCoolingdown = false;
                    if(Physics.Raycast(ray, out hit, 8f))
                        {
                             
                             if(hit.collider.tag == "Mob")
                            {
                                animationManager.ChangeStage(AnimationStages.Caught);
                                mob.isHunting = false;
                                mob.hidingStarted = false;
                            }
                             
                        }
                }
                else
                {  
                    flashLight.enabled = false;
                    if (!isCoolingdown)
                    {   flashed--;
                        on.clip = clips[0];
                        on.Play();
                        StartCoroutine(CoolDown());
                        isCoolingdown = true;
                    }
                    
                    
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

    IEnumerator CoolDown()
    {
        //cool down;
        
        Color img = battery.color;
        img.a = 0f;

        StartCoroutine(fade(0f,1f));
        yield return new WaitForSeconds(coolDownTime);
        isFlashON = false;
        img.a = 0f; 
        yield break;
    }

    IEnumerator fade(float startAlpha, float endAlpha)
    {   
        float passedtime = 0f;
        Color img = battery.color;

        while (passedtime < coolDownTime)
        {
            passedtime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, passedtime/coolDownTime);

            img.a = alpha;
            battery.color = img;
            yield return null;
        }
        
       
    }
}
