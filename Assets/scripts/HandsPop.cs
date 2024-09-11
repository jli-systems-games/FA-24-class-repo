using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsPop : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite Hand;
    [SerializeField] private Sprite HandHit;

    private Vector2 startPosition = new Vector2(0f,-2.56f);
    private Vector2 endPosition = Vector2.zero;

    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;

    private bool hittable = true;

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
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = HandHit;

        StopAllCoroutines();
        StartCoroutine(QuickHide());

        hittable = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);

        if (!hittable)
        {
            Hide();
        }
    }

    private void Hide()
    {
        transform.localPosition = startPosition;
    }
}
