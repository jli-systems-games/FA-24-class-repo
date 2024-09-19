using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameLevelController : MonoBehaviour
{
    public TextMeshPro scoreText;
    public Animator scoreAnimator;
    public Animator screenAnim;
    public GameObject gameFailText;
    private int score = 0;

    public GameObject platformUnit;
    public Transform[] generateAnchor;

    private int currentAnchorIndex;
    private int nextAnchorIndex;
    private int anchorCount = 5;

    public GameObject introCanvas;
    public GameObject gameCanvas;
    public GameObject scoreObject;
    public GameObject begainPlatform;
    private SpriteRenderer spriteRenderer;

    private Coroutine platformCoroutine; 

    private GameManager gameManager;
    private AudioManager audioManager;
    private void Awake()
    {
        ShowIntro();
        scoreText.text = "0";
        currentAnchorIndex = 0;
    }

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
        spriteRenderer = begainPlatform.GetComponent<SpriteRenderer>();
        scoreObject.SetActive(false);
        gameFailText.SetActive(false);
    }

    IEnumerator SpawnPlatform()
    {
        while (true)
        {
            GameObject newPlatform = Instantiate(platformUnit, generateAnchor[currentAnchorIndex].position, Quaternion.identity);

            // 随机改变新生成物体的scale.x
            float randomScaleX = Random.Range(0.9f, 3.5f);
            newPlatform.transform.localScale = new Vector3(randomScaleX, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

            nextAnchorIndex = Mathf.Clamp(currentAnchorIndex + Random.Range(-1, 2), 0, anchorCount - 1);
            currentAnchorIndex = nextAnchorIndex;

            // 调整下一次生成的等待时间
            yield return new WaitForSeconds((2 * randomScaleX) / 5);
        }
    }

    public void AddScore()
    {
        score++;
        scoreAnimator.SetTrigger("Trigger");
        audioManager.PlayBallScore();
        scoreText.text = score.ToString();
    }

    public void StartGame()
    {
        introCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        platformCoroutine = StartCoroutine(SpawnPlatform()); 
        scoreObject.SetActive(true);
        StartCoroutine(FadeOut());
        Invoke("DestroyPlatform", 10f);
    }

    public void ShowIntro()
    {
        introCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    public void GameFail()
    {
        scoreAnimator.SetTrigger("Fail");
        gameFailText.SetActive(true);
        GameObject ball = GameObject.FindWithTag("Player");
        Destroy(ball);

        gameManager.EndGame();

        if (platformCoroutine != null)
        {
            StopCoroutine(platformCoroutine); 
        }
    }

    public void GameEnd()
    {
        gameManager.EndGame();
        audioManager.PlayButtonPress();
        screenAnim.SetTrigger("End");

        scoreAnimator.SetTrigger("End");

        if (platformCoroutine != null)
        {
            StopCoroutine(platformCoroutine); 
        }

        GameObject ball = GameObject.FindWithTag("Player");
        Destroy(ball);
    }

    void DestroyPlatform()
    {
        Destroy(begainPlatform);
    }

    private IEnumerator FadeOut()
    {
        if (spriteRenderer != null)
        {
            Color spriteColor = spriteRenderer.color;
            float startAlpha = spriteColor.a;
            float startRed = spriteColor.r;
            float startGreen = spriteColor.g;
            float startBlue = spriteColor.b;
            float timeElapsed = 0f;
            float duration = 10f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / duration;

                float alpha = Mathf.Lerp(startAlpha, 0, t);
                spriteColor.a = alpha;

                spriteColor.r = Mathf.Lerp(startRed, 1, t);
                spriteColor.g = Mathf.Lerp(startGreen, 0, t);
                spriteColor.b = Mathf.Lerp(startBlue, 0, t);

                spriteRenderer.color = spriteColor;

                yield return null;
            }
        }
        else
            yield return null;
    }
}
