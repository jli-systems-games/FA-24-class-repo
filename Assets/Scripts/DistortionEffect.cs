using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class DistortionEffect : MonoBehaviour
{
    [SerializeField] Volume ppProfile;
    LensDistortion _lens;
    bool disabled = true;
    IEnumerator distort;
    void Start()
    {
        ppProfile.profile.TryGet(out _lens);
        eventManager.triggerAttack += enableDistort;
        eventManager.resetAttack += disableProfile;

        distort = Distorting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void enableDistort(GameObject p)
    {   
        if(p.name == "hunger" || p.name == "irritation")
        {
             ppProfile.enabled = true;
            

            if (disabled)
            {   
                Debug.Log("Hey"); 
                disabled = false;
                StartCoroutine(distort);
               
            }
             
        }
       
    }
    IEnumerator Distorting()
    {
        Debug.Log(disabled);
        float duration = 2f; // Time to go from min to max and back to min
        float elapsedTime = 0f;
       
        while (!disabled)
        {
            float t = Mathf.PingPong(elapsedTime / duration, 1f);

            _lens.scale.value = Mathf.Lerp(0.1f, 1f, t);


            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
    }
    void disableProfile()
    {   
        if(!disabled)
        {
            disabled = true;
            StopCoroutine(distort);
        }
        
      //_lens.scale.value = 1f;
        ppProfile.enabled = false;
    }
}
