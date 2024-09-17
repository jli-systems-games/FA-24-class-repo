using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{   
    RaycastHit hit;
    Ray ray;

    public MobMovement mob;
    public Light flashLight;
    bool isFlashON;
    int flashed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        ray= new Ray(transform.position, transform.forward);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("pressed");
            if(flashed > 0)
            { if (!isFlashON )
                {
                    flashLight.enabled = true;
                    
                    isFlashON = true;
                    if(Physics.Raycast(ray, out hit, 7f))
                        {
                             //Debug.Log("flashlight");
                             if(hit.collider.tag == "Mob")
                            {mob.isHunting = false;
                                 mob.hidingStarted = false;
            

                            }
                             
                        }
                }
                else
                {   flashed--;
                    flashLight.enabled = false;
                    isFlashON = false;
                }

            }
            else
            {  
                isFlashON = false;
            }
            
            
        }

        if(flashed == 3)
        {
            StartCoroutine(flickering());
        }
        
    }

    private IEnumerator flickering()
    {

        float flahDuration = 1f;
        float elapsedT = 0f;

        while(elapsedT < flahDuration)
        {
           elapsedT += Time.deltaTime;
           float t = elapsedT / flahDuration;
           flashLight.intensity = Mathf.Lerp(1.14f, 0f, t);
           yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        elapsedT = 0f;
        while (elapsedT < flahDuration)
        {
            elapsedT += Time.deltaTime;
            float t = elapsedT / flahDuration;
            flashLight.intensity = Mathf.Lerp(0f, 1.14f, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
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
            flashLight.intensity = Mathf.Lerp(0f, 1.14f, t);
            yield return null;
        }
        yield break;
    }
}
