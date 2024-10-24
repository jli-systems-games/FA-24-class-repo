using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGunManager : MonoBehaviour
{  
    public GameObject playerTransform;  
   
    private int smgBullet;
    private int maxSmgBullet;
    private bool canUseGrenade;
    private bool haveGrenade = true;
    private bool isReloading = false;
    private float reloadTime;
    public GameObject smgReloadAnim;
    public TextMeshProUGUI currentBulletUI;
    public PointerToMouse pointerToMouse;

    public TextMeshProUGUI killCountUI;
    private int killCount = 0;
    public TextMeshProUGUI bittenCountUI;
    private int bittenCount = 0;

    private float fireRate;
    private float lastFireTime = 0f;
    private AudioManager audioManager;
    private PlayerData playerData;
    private float holdTimer = 0f;

    private void Start()
    {
        Cursor.visible = false;     
        audioManager = GetComponent<AudioManager>();
        playerData = GetComponent<PlayerData>();
        pointerToMouse = FindObjectOfType<PointerToMouse>();

        canUseGrenade = playerData.haveGrenade;
        maxSmgBullet = playerData.maxBullet;
        smgBullet = maxSmgBullet;
        fireRate = playerData.fireRate;
        reloadTime = playerData.reloadSpeed;

        UpdateBulletUI();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)&& haveGrenade && canUseGrenade)
        {
            haveGrenade = false;
            pointerToMouse.ShootGrenade();
            audioManager.PlayPistolShot();
        }
        if (Input.GetMouseButton(0) && Time.time >= lastFireTime + fireRate && smgBullet > 0 && !isReloading)
        {
            smgBullet--;
            lastFireTime = Time.time;
            pointerToMouse.ShootProjectile(pointerToMouse.bullet);
            if (playerData.silencer)
            audioManager.PlaySmgShotSilencer();
            else
            audioManager.PlaySmgShot();
            UpdateBulletUI();
        }
        else if (Input.GetMouseButtonDown(0) && smgBullet <= 0 && !isReloading)
        {
            audioManager.PlayEmptyMeg();
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (smgBullet < maxSmgBullet)
            {
                pointerToMouse.SpawnMeg();

                // 实例化重载动画
                GameObject reloadAnimInstance = Instantiate(smgReloadAnim, playerTransform.transform.position + Vector3.up * 1.2f, Quaternion.identity, playerTransform.transform);

                // 获取 Animator 组件并设置播放速度
                Animator reloadAnimator = reloadAnimInstance.GetComponent<Animator>();
                if (reloadAnimator != null)
                {
                    reloadAnimator.speed = 1f / reloadTime; // 设置动画播放速度
                }

                audioManager.PlaySmgReloadUnplug();
                StartCoroutine(ReloadCoroutine(maxSmgBullet));
            }
        }

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
    private IEnumerator ReloadCoroutine(int bulletsToReload)
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime - 0.1f);        
        audioManager.PlaySmgReloadPlug();
        yield return new WaitForSeconds(0.1f);
        smgBullet = maxSmgBullet;
        UpdateBulletUI();
        isReloading = false;     

    }
    public void UpdateBulletUI()
    {
        currentBulletUI.text = smgBullet.ToString();        
    }
    
    public void AddMobKill()
    {
        killCount++;
        killCountUI.text = killCount.ToString();
        audioManager.PlayRandomBuffPickUp();
    }
    public void DecreaseHealth(float damage)
    {
        bittenCount++;
        bittenCountUI.text = bittenCount.ToString();
        audioManager.PlayBatteryPickup();

    }
    public void AddGrenade()
    { 
        haveGrenade = true;
        audioManager.PlaySmgReloadPlug();
    }
}
