using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    
    public LifeTime lifeTime = LifeTime.Baby;
    public Sprite babySprite, teenSprite, adultSprite, pregnantSprite;
    private CircleCollider2D circleCollider;
    public SpriteRenderer spriteRenderer,thoughtsRenderer;
    public Sprite waterSprite, foodSprite, waterAndFoodSprite, laberSprite;
    private GameManager gameManager;
    private Animator animator;

    private bool needWater = false;
    private bool needFood = false;
    private bool needWaterAndFood = false;

    private bool isWatered = false;
    private bool isFeed = false;

    private float speedThreshold = 2f; 
    private float duration = 1.5f; 
    private int sampleCount = 30; // 采样帧数
    private float timer = 0f; 
    private Queue<float> speedSamples = new Queue<float>(); // 存储最近的速度样本
    private float speedSum = 0f; 

    private Vector3 lastPosition; // 上一帧的位置
    public enum LifeTime
    { 
        Baby,
        Teen,
        Adult,
        Pregnant,
    }
    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        circleCollider.radius = 0.23f;
        //thoughtsRenderer.sprite = null;
        spriteRenderer.sprite = babySprite;
        lastPosition = transform.position;

        StartCoroutine(BabyStageGroth());

    }

    private void Update()
    {
        if (lifeTime == LifeTime.Pregnant)
        {
            ShakeCheck();
        }
    }
   
    public void GetWatered()
    {
        //Debug.Log("检测到水");
        if (lifeTime == LifeTime.Baby && needWater && !isWatered)
        {
            isWatered = true;
            needWater = false;
            //Debug.Log("开始生长");
            StartCoroutine(GrowUpRandomDelay(2.5f, 5f));
        }
        if (lifeTime == LifeTime.Adult && needWaterAndFood && !isWatered)
        {
            isWatered = true;

            if (isFeed)
                StartCoroutine(GrowUpRandomDelay(5f, 10f));
        }
    
    }
    public void GetFeed()
    {
        //Debug.Log("检测到食物");
        if (lifeTime == LifeTime.Teen && needFood && !isFeed)
        {
            isFeed = true;
            needFood = false;
            StartCoroutine(GrowUpRandomDelay(4f, 10f));
        }
        if (lifeTime == LifeTime.Adult && needWaterAndFood && !isFeed)
        {
            isFeed = true;

            if (isWatered)
                StartCoroutine(GrowUpRandomDelay(5f, 10f));
        }
    }

    public void GrowUp()
    {
        animator.SetTrigger("Trigger");
        switch (lifeTime)
        {
            case LifeTime.Baby:
                isWatered = false;
                spriteRenderer.sprite = teenSprite;
                circleCollider.radius = 0.35f;
                StartCoroutine(TeenStageGroth());
                lifeTime = LifeTime.Teen;
                break;
            case LifeTime.Teen:
                isFeed = false;
                circleCollider.radius = 0.44f;
                spriteRenderer.sprite = adultSprite;
                StartCoroutine(AdultStageGroth());
                lifeTime = LifeTime.Adult;
                break;
            case LifeTime.Adult:
                isWatered = false;
                isFeed = false;
                circleCollider.radius = 0.51f;
                spriteRenderer.sprite = pregnantSprite;
                lifeTime = LifeTime.Pregnant;
                needWaterAndFood = false;
                StartCoroutine(DisplaySprites(laberSprite));
                break;
            case LifeTime.Pregnant:

                Vector3 spawnLocation = transform.position + new Vector3 (0f,-1f,0f);
                Instantiate(gameManager.slimeUnit, spawnLocation, Quaternion.identity);
                needWater = false;
                needFood = true;
                needWaterAndFood = false;
                isWatered = false;
                isFeed = false;
                circleCollider.radius = 0.35f;
                spriteRenderer.sprite = teenSprite;
                lifeTime = LifeTime.Teen;
                StartCoroutine(AdultStageGroth());
                break;


        }
    }
    private void ShakeCheck()
    {
        Vector3 currentPosition = transform.position;
        float speed = (currentPosition - lastPosition).magnitude / Time.deltaTime;

        lastPosition = currentPosition;

        speedSamples.Enqueue(speed);
        speedSum += speed;

        if (speedSamples.Count > sampleCount)
        {
            speedSum -= speedSamples.Dequeue();
        }

        float averageSpeed = speedSum / speedSamples.Count;

        if (averageSpeed > speedThreshold)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                GrowUp();
                timer = 0f; // 重置计时器
            }
        }
        else
        {
            // 如果平均速度小于阈值，重置计时器
            timer = 0f;
        }

    }

    IEnumerator GrowUpRandomDelay(float miniTime, float maxTime)
    {
        float waitTime = Random.Range(miniTime, maxTime);
        yield return new WaitForSeconds (waitTime);
        GrowUp();
    }

    IEnumerator BabyStageGroth()
    {
        Debug.Log("开始baby阶段");
        float waitTime = Random.Range(4f,10f);
        yield return new WaitForSeconds (waitTime);
    
        needWater = true;
        StartCoroutine(DisplaySprites(waterSprite));
        Debug.Log("需求出现");

    }
    IEnumerator TeenStageGroth()
    {
        float waitTime = Random.Range(5f, 10f);
        yield return new WaitForSeconds(waitTime);

        needFood = true;
        StartCoroutine(DisplaySprites(foodSprite));
    }
    IEnumerator AdultStageGroth()
    {
        float waitTime = Random.Range(6f, 10f);
        yield return new WaitForSeconds(waitTime);

        needWaterAndFood = true;
        StartCoroutine(DisplaySprites(waterAndFoodSprite));
    }    
    IEnumerator PregnantStageGroth()
    {
        float waitTime = Random.Range(3f, 7f);
        yield return new WaitForSeconds(waitTime);

        DisplaySprites(laberSprite);
    }
    IEnumerator DisplaySprites(Sprite targetSprite)
    {
        thoughtsRenderer.color = new Color(1, 1, 1, 1);
        thoughtsRenderer.sprite = targetSprite;

        yield return new WaitForSeconds(5f);

        float fadeDuration = 1f;
        float fadeSpeed = 1f / fadeDuration;
        Color currentColor = thoughtsRenderer.color;

        while (currentColor.a > 0)
        {
            currentColor.a -= fadeSpeed * Time.deltaTime;
            thoughtsRenderer.color = currentColor;


            yield return null;
        }

        // 确保完全透明
        currentColor.a = 0;
        thoughtsRenderer.color = currentColor;

    }


}
