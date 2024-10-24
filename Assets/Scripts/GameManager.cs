using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameMode gameMode = GameMode.Normal;

    private bool limitedPower = true;
    private bool limitedSanity = true;

    public bool isPaused;
    public bool isDangerZone;
    public GameObject playerTransform;

    private float playerHealth = 100f;
    private float currentHealth;
    private float playerSanity = 100f;
    private float currentSanity;
    private float sanityDrainRate = 1.2f;
    private float batteryPower = 100f;
    private float currentPower;
    private float powerDrainRate = 4f;

    //public RectTransform healthBarRectTransform;
    // public RectTransform sanityBarRectTransform;
    //public RectTransform batteryBarRectTransform;


    private GameObject flashLight;

    public int bulletCount = 100;
    private int pistolBullet;
    private int smgBullet;
    private int maxPistolBullet = 13;
    private int maxSmgBullet = 35;
    private bool isReloading = false;
    private float reloadTime;
    public GameObject pistolReloadAnim;
    public GameObject smgReloadAnim;    
    public TextMeshProUGUI allBulletUI;
    public TextMeshProUGUI currentBulletUI;
    public Animator weaponAnimator;
    public PointerToMouse pointerToMouse;
    private CanvasGroup restartTip;

    private TMP_Text timerText;  
    private float elapsedTime = 0f;

    public TextMeshProUGUI killCountUI;
    private int killCount = 0;

    private float fireRate = 0.1f;
    private float lastFireTime = 0f;
    private PlayerWeapon playerWeapon = PlayerWeapon.Pistol;
    private AudioManager audioManager;    
    private EnemyGenerater enemyGenerater;
    private PlayerController playerController;
    private PointerToMouse pointer;
    private float holdTimer = 0f;
    public enum GameMode
    { 
        Normal,
        Infinite,
    }

    public enum PlayerWeapon
    { 
        Empty,
        Pistol,
        SMG,
    }
    private void Start()
    {
        isDangerZone = false;
        Cursor.visible = false;
        //restartTip.alpha = 0f;
        reloadTime = 1.4f;
        UpdateBulletUI();
        audioManager = GetComponent<AudioManager>();
        enemyGenerater = GetComponent<EnemyGenerater>();
        playerController = FindObjectOfType<PlayerController>();
        pointer = FindObjectOfType<PointerToMouse>();
        StartCoroutine(StartSanityDrop());

        //pistolBullet = maxPistolBullet;
        smgBullet = maxSmgBullet;

        //currentHealth = playerHealth;
        //currentSanity = playerSanity;
        //currentPower = batteryPower;

        //if (!limitedPower)
        //{
        //    powerDrainRate = 0;
        //    batteryBarRectTransform.gameObject.SetActive(false);
        //}
        //if (!limitedSanity)
        //{
        //    sanityDrainRate = 0;
        //    sanityBarRectTransform.gameObject.SetActive(false);
        //}

        switch (gameMode)
        { 
            case GameMode.Normal:
                bulletCount = 200;
                allBulletUI.gameObject.SetActive(true);
                break;

            case GameMode.Infinite:
                bulletCount = 0;
                allBulletUI.gameObject.SetActive(false);
                break;
        }
    }
    private void Update()
    {

        if (!isPaused)
        {
            //elapsedTime += Time.deltaTime;

            //int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            //int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            //if (limitedSanity)
            //{
            //    if (!isDangerZone)
            //    {
            //        if (currentSanity > 99)
            //            sanityDrainRate = 0;
            //        else
            //            sanityDrainRate = -0.5f;
            //    }
            //    currentSanity -= sanityDrainRate * Time.deltaTime;
            //}



            //if (Input.GetMouseButtonDown(1) && currentPower > 1)
            //{
            //    flashLight.SetActive(!flashLight.activeSelf);
            //    audioManager.PlaySwitchLight();
            //}

            //if (flashLight.activeSelf)
            //{
            //    if (limitedPower)
            //    {
            //        currentPower -= powerDrainRate * Time.deltaTime;

            //        if (limitedSanity)
            //        {
            //            if (isDangerZone)
            //                sanityDrainRate = 0.2f;
            //        }
            //        if (currentPower <= 0)
            //        {
            //            currentPower = 0;
            //            flashLight.SetActive(false);
            //            audioManager.PlaySwitchLight();
            //        }
            //    }
            //}
            //else
            //{
            //    if (isDangerZone && limitedSanity)
            //        sanityDrainRate = 2f;
            //}

            if (Input.GetMouseButton(0) && Time.time >= lastFireTime + fireRate && smgBullet > 0 && !isReloading)
            {
                smgBullet--;
                lastFireTime = Time.time;
                pointerToMouse.ShootProjectile(pointerToMouse.bullet);
                audioManager.PlaySmgShot();
                UpdateBulletUI();
            }
            else if (Input.GetMouseButtonDown(0) && smgBullet <= 0 && !isReloading)
            {
                audioManager.PlayEmptyMeg();
            }
            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                switch (gameMode)
                {
                    case GameMode.Normal:
                        if (bulletCount <= 0)
                            return;
                        if (smgBullet < maxSmgBullet && bulletCount >= 0)
                        {
                            pointerToMouse.SpawnMeg();
                            int bulletneeded = maxSmgBullet - smgBullet;
                            Instantiate(smgReloadAnim, playerTransform.transform.position + Vector3.up * 1.2f, Quaternion.identity, playerTransform.transform);
                            audioManager.PlaySmgReload();
                            if (bulletneeded >= bulletCount)
                            {
                                StartCoroutine(ReloadCoroutine(bulletCount));
                            }
                            else
                            {
                                StartCoroutine(ReloadCoroutine(bulletneeded));
                            }
                        }
                        break;
                    case GameMode.Infinite:
                        if (smgBullet < maxSmgBullet)
                        {
                            pointerToMouse.SpawnMeg();
                            Instantiate(smgReloadAnim, playerTransform.transform.position + Vector3.up * 1.2f, Quaternion.identity, playerTransform.transform);
                            audioManager.PlaySmgReload();
                            StartCoroutine(ReloadCoroutine(maxSmgBullet));
                        }
                        break;
                }
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                holdTimer += Time.deltaTime;

                if (holdTimer >= 2)
                {
                    SceneManager.LoadScene("Market");
                }
            }
            else
            {
                holdTimer = 0f;
            }
        }

        //currentHealth = Mathf.Clamp(currentHealth, 0, playerHealth);
        //currentSanity = Mathf.Clamp(currentSanity, 0, playerSanity);
        //currentPower = Mathf.Clamp(currentPower, 0, batteryPower);

        //UpdateHealthBar();
        //UpdateSanityBar();

        //if (limitedPower)
        //    UpdateBatteryBar();
    }
    private IEnumerator ReloadCoroutine(int bulletsToReload)
    {
        switch (gameMode)
        {
            case GameMode.Normal:
                isReloading = true;
                yield return new WaitForSeconds(reloadTime);

                switch (playerWeapon)
                {
                    case PlayerWeapon.Pistol:
                        bulletCount -= bulletsToReload;
                        pistolBullet += bulletsToReload;
                        break;
                    case PlayerWeapon.SMG:
                        bulletCount -= bulletsToReload;
                        smgBullet += bulletsToReload;
                        break;
                }
                UpdateBulletUI();

                isReloading = false;
                break;
            case GameMode.Infinite:
                isReloading = true;
                yield return new WaitForSeconds(reloadTime);

                switch (playerWeapon)
                {
                    case PlayerWeapon.Pistol:
                        pistolBullet = bulletsToReload;
                        break;
                    case PlayerWeapon.SMG:
                        smgBullet = bulletsToReload;
                        break;
                }
                UpdateBulletUI();

                isReloading = false;
                break;
        }

        
    }
    public void UpdateBulletUI()
    {
        switch (gameMode)
        {
            case GameMode.Normal:
                allBulletUI.text = bulletCount.ToString();

                switch (playerWeapon)
                {
                    case PlayerWeapon.Pistol:
                        currentBulletUI.text = pistolBullet.ToString();
                        break;
                    case PlayerWeapon.SMG:
                        currentBulletUI.text = smgBullet.ToString();
                        break;
                }
                break;
            case GameMode.Infinite:
                switch (playerWeapon)
                {
                    case PlayerWeapon.Pistol:
                        currentBulletUI.text = pistolBullet.ToString();
                        break;
                    case PlayerWeapon.SMG:
                        currentBulletUI.text = smgBullet.ToString();
                        break;
                }
                break;
        }        
    }
    public void AddHealth()
    {
        currentHealth += 25f;

        if (currentHealth > playerHealth) 
        {
            currentHealth = playerHealth;
        }
    }
    public void AddPower()
    {
        currentPower = batteryPower;
    }
    public void AddSanity()
    {
        currentSanity += 25f;
        currentHealth += 10f;
        if (currentSanity > playerSanity)
        {
            currentSanity = playerSanity;
        }
        if (currentHealth > playerHealth)
        {
            currentHealth = playerHealth;
        }
    }

    //void UpdateHealthBar()
    //{
    //    float healthPercent = currentHealth / playerHealth;
    //    healthBarRectTransform.localScale = new Vector3(healthPercent, 1, 1);  
    //}

    //void UpdateSanityBar()
    //{
    //    float sanityPercent = currentSanity / playerSanity;
    //    sanityBarRectTransform.localScale = new Vector3(sanityPercent, 1, 1);
    //}

    //void UpdateBatteryBar()
    //{
    //    float powerPercent = currentPower / batteryPower;
    //    batteryBarRectTransform.localScale = new Vector3(powerPercent, 1, 1);
    //}

    public void AddMobKill()
    {
        killCount++;
        killCountUI.text = killCount.ToString();
        audioManager.PlayRandomBuffPickUp();
    }

    IEnumerator StartSanityDrop()
    {
        yield return new WaitForSeconds(5f);

        isDangerZone = true;
    
    }
    public void DecreaseHealth(float damage)
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
            isPaused = true;
            flashLight.SetActive(false);
            enemyGenerater.StopSpawningAndKillEnemies();
            playerController.canMove = false;
            pointer.canMove = false;
            StartCoroutine(FadeCanvasGroup(restartTip, 0, 1, 1));
        }
    
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = endAlpha;  // 保证最后的alpha值精确到目标值
    }

}
