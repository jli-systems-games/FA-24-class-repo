using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsPop : MonoBehaviour
{

    private Vector2 startPosition = new Vector2(0f,-2.56f);
    private Vector2 endPosition = Vector2.zero;

    private float showDuration = 0.5f;
    private float duration = 1f;

    private IEnumerator ShowHide(Vector2 start,Vector2 end)
    {
        transform.localPosition = start;

        float elapsed = 0f;
        while(elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = end;

        yield return new WaitForSeconds(duration);

        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(ShowHide(startPosition, endPosition)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
