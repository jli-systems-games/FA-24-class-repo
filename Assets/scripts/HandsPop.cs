using System.Collections;
using UnityEngine;

public class HandsPop : MonoBehaviour
{
    private Vector2 startPosition; 
    private Vector2 endPosition;   
    private float showDuration = 0.5f;
    private HandsManager gameManager; 
    private bool hittable = false;

    

    private void Awake()
    {
        
        startPosition = transform.localPosition;
        
        endPosition = startPosition + new Vector2(0f, 5f); 
    }

    public void Initialize(HandsManager manager)
    {
        gameManager = manager;
    }

    
    public IEnumerator ShowHand()
    {
        hittable = true;

        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            
            transform.localPosition = Vector2.Lerp(startPosition, endPosition, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPosition;

        
        float timer = 3f;
        while (timer > 0f && hittable)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        if (hittable)
        {
            Debug.Log("Missed");
            yield return HideHand();
        }
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            hittable = false;
            Debug.Log("Hand Hit!");
            gameManager.handNumber++;
            //StopAllCoroutines(); 
            StartCoroutine(HideHand());
            Debug.Log("Hand: " + gameManager.handNumber);
        }
    }


    private IEnumerator HideHand()
    {
        float elapsed = 0f;
        while (elapsed < showDuration)
        {

            transform.localPosition = Vector2.Lerp(endPosition, startPosition, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        
        transform.localPosition = startPosition;
        gameObject.SetActive(false);
        if (gameManager.handNumber >= 3)
        {
            gameManager.OnHandCleared();
        }
        else
        {
            gameManager.StartNextHand();
        }

    }
}
