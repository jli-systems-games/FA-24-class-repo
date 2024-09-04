using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public KeyCode player1Key = KeyCode.F;
    public KeyCode player2Key = KeyCode.L;

    private GameStatusManager statusManager;
    private AudioManager audioManager;
    private bool canUsePower = true;

    public Powers player1Power = Powers.None;
    public Powers player2Power = Powers.None;

    public GameObject player1;
    public GameObject player2;

    public Animator player1Anim;
    public Animator player2Anim;

    public GameObject shieldPrefab;
    public GameObject randomPowerAnimPrefab;
    public GameObject timeIcon;
    public GameObject obstaclePrefab;
    public Transform iconAnchor;

    public GameObject powerIconPlayer1;
    public GameObject powerIconPlayer2;

    private Animator powerIconP1Anim;
    private Animator powerIconP2Anim;
    private SpriteRenderer powerIconP1SpriteRenderer;
    private SpriteRenderer powerIconP2SpriteRenderer;

    public Sprite heal;
    public Sprite shield;
    public Sprite split;
    public Sprite superPower;
    public Sprite timeSpeedUp;
    public Sprite obstacle;

    public enum Powers
    {
        None,
        Heal,
        Shield,
        Split,
        SuperPower,
        TimeSpeedUp,
        Obstacle,
    }

    private void Start()
    {
        statusManager = GetComponent<GameStatusManager>();
        audioManager = GetComponent<AudioManager>();

        powerIconP1Anim = powerIconPlayer1.GetComponent<Animator>();
        powerIconP2Anim = powerIconPlayer2.GetComponent<Animator>();
        powerIconP1SpriteRenderer = powerIconPlayer1.GetComponent<SpriteRenderer>();
        powerIconP2SpriteRenderer = powerIconPlayer2.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(player1Key) && statusManager.player1Power >= 100 && canUsePower)
        {
            audioManager.PlayUsePower();
            UsePowerPlayer1();
            powerIconP1Anim.SetTrigger("Set");
        }
        else if (Input.GetKeyDown(player1Key) && statusManager.player1Power < 100 && canUsePower)
        {
            audioManager.PlayCannotUsePower();
            powerIconP1Anim.SetTrigger("Trigger");
        }

        if (Input.GetKeyDown(player2Key) && statusManager.player2Power >= 100 && canUsePower)
        {
            audioManager.PlayUsePower();
            UsePowerPlayer2();
            powerIconP2Anim.SetTrigger("Set");
        }
        else if (Input.GetKeyDown(player2Key) && statusManager.player2Power < 100 && canUsePower)
        {
            audioManager.PlayCannotUsePower();
            powerIconP2Anim.SetTrigger("Trigger");
        }

    }

    public void GameStartAnim()
    {
        // 生成随机能力动画并销毁
        GameObject randomPowerAnim = Instantiate(randomPowerAnimPrefab, iconAnchor.position, Quaternion.identity);
        Destroy(randomPowerAnim, 3f);

        // 获取玩家的随机能力并更新图标
        GetRandomPowerP1();
        GetRandomPowerP2();        

        // 启动协程，在3秒后触发动画
        StartCoroutine(TriggerPowerIconAnimAfterDelay(3f));
    }

    IEnumerator TriggerPowerIconAnimAfterDelay(float delay)
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);

        // 触发两个动画
        powerIconP1Anim.SetTrigger("Set");
        powerIconP2Anim.SetTrigger("Set");
    }


    void UpdatePowerSprites()
    {
        // 更新Player 1的能力图标
        switch (player1Power)
        {
            case Powers.Heal:
                powerIconP1SpriteRenderer.sprite = heal;
                break;
            case Powers.Shield:
                powerIconP1SpriteRenderer.sprite = shield;
                break;
            case Powers.Split:
                powerIconP1SpriteRenderer.sprite = split;
                break;
            case Powers.SuperPower:
                powerIconP1SpriteRenderer.sprite = superPower;
                break;
            case Powers.TimeSpeedUp:
                powerIconP1SpriteRenderer.sprite = timeSpeedUp;
                break;
            case Powers.Obstacle:
                powerIconP1SpriteRenderer.sprite = obstacle;
                break;
            case Powers.None:
            default:
                powerIconP1SpriteRenderer.sprite = null;
                break;
        }

        // 更新Player 2的能力图标
        switch (player2Power)
        {
            case Powers.Heal:
                powerIconP2SpriteRenderer.sprite = heal;
                break;
            case Powers.Shield:
                powerIconP2SpriteRenderer.sprite = shield;
                break;
            case Powers.Split:
                powerIconP2SpriteRenderer.sprite = split;
                break;
            case Powers.SuperPower:
                powerIconP2SpriteRenderer.sprite = superPower;
                break;
            case Powers.TimeSpeedUp:
                powerIconP2SpriteRenderer.sprite = timeSpeedUp;
                break;
            case Powers.Obstacle:
                powerIconP2SpriteRenderer.sprite = obstacle;
                break;
            case Powers.None:
            default:
                powerIconP2SpriteRenderer.sprite = null;
                break;
        }
    }

    public void GetRandomPowerP1()
    {
        //为P1获取一个随机的能力，该函数在每局开始时被调用一次，然后在P1每次使用完当前技能后调用一次。
        player1Power = (Powers)Random.Range(1, System.Enum.GetValues(typeof(Powers)).Length);
        UpdatePowerSprites();
    }

    public void GetRandomPowerP2()
    {
        //为P2获取一个随机的能力，该函数在每局开始时被调用一次，然后在P2每次使用完当前技能后调用一次。
        player2Power = (Powers)Random.Range(1, System.Enum.GetValues(typeof(Powers)).Length);
        UpdatePowerSprites();
    }

    void UsePowerPlayer1()
    {
        player1Anim.SetTrigger("Trigger");
        statusManager.player1Power = 0;
        statusManager.UpdatePowerBar();       
        ExecutePower(player1Power, UsePowerP1);
        GetRandomPowerP1();
    }

    public void UsePowerPlayer2()
    {
        player2Anim.SetTrigger("Trigger");
        statusManager.player2Power = 0;
        statusManager.UpdatePowerBar();
        GetRandomPowerP2();
       ExecutePower(player2Power, UsePowerP2);
    }
    void ExecutePower(Powers power, System.Action powerUsageAction)
    {
        powerUsageAction();       
    }
    void UsePowerP1()
    {
        switch (player1Power)
        {
            case Powers.Heal:
                UseHealP1();
                break;
            case Powers.Shield:
                UseShieldP1();
                break;
            case Powers.Split:
                UseSplit();
                break;
            case Powers.SuperPower:
                UseSuperPower();
                break;
            case Powers.TimeSpeedUp:
                UseTimeSpeedUp();
                break;
            case Powers.Obstacle:
                UseObstacle();
                break;
        }
    }

    void UsePowerP2()
    {
        switch (player2Power)
        {
            case Powers.Heal:
                UseHealP2();
                break;
            case Powers.Shield:
                UseShieldP2();
                break;
            case Powers.Split:
                UseSplit();
                break;
            case Powers.SuperPower:
                UseSuperPower();
                break;
            case Powers.TimeSpeedUp:
                UseTimeSpeedUp();
                break;
            case Powers.Obstacle:
                UseObstacle();
                break;
        }
    }

    void UseHealP1()
    {
        statusManager.UseHealP1();
    }

    void UseHealP2()
    {
        statusManager.UseHealP2();
    }

    void UseShieldP1()
    {
        Instantiate(shieldPrefab, player1.transform.position, Quaternion.identity, player1.transform);
    }

    void UseShieldP2()
    {
        Instantiate(shieldPrefab, player2.transform.position, Quaternion.identity, player2.transform);
    }

    void UseSplit()
    {
        // 分裂能力实现保留
    }

    void UseSuperPower()
    {
        // 超能力实现保留
    }

    void UseTimeSpeedUp()
    {
        GameObject icon = Instantiate(timeIcon, iconAnchor.position, Quaternion.identity);
        StartCoroutine(FadeOutAndDestroy(icon, 4f));
        Time.timeScale = 2f;
        StartCoroutine(ResetTimeScaleAfterDelay(4f));
    }

    IEnumerator ResetTimeScaleAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
    }

    void UseObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, iconAnchor.position, Quaternion.identity);
        StartCoroutine(FadeOutAndDestroy(obstacle, 5f));
    }


    IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float startAlpha = canvasGroup.alpha;
            float rate = startAlpha / duration;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * rate;
                yield return null;
            }
        }
        Destroy(obj);
    }
}
