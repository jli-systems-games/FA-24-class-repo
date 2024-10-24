using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float reloadSpeed = 1;
    public float fireRate = 0.115f;
    public int maxBullet;
    private float spreadAngle = 6f;

    public bool haveGrenade = false;
    public bool silencer = false;

    public SpriteRenderer gunSkin;
    public GunsmithData gunsmithData;
    public TextMeshProUGUI playerInfo;
    public PointerToMouse pointerToMouse;
    public GameObject grenadeUI, grenadeUI2;
    private void Awake()
    {
      gunsmithData = FindObjectOfType<GunsmithData>();
        switch (gunsmithData.scopeType)
        {           
            case GunsmithData.ScopeType.MidRange:
                moveSpeed -= 0.2f;
                break;
            case GunsmithData.ScopeType.LongRange:
                moveSpeed -= 0.7f;
                break;
        }
        switch (gunsmithData.barrelType)
        {
            case GunsmithData.BarrelType.ShortS:
                spreadAngle += 0.5f;
                break;
            case GunsmithData.BarrelType.ShortM:
                spreadAngle += 0f;
                moveSpeed -= 0.1f;
                break;
            case GunsmithData.BarrelType.ShortL:
                spreadAngle -= 0.5f;
                moveSpeed -= 0.2f;
                break;
            case GunsmithData.BarrelType.LongS:
                spreadAngle -= 0.9f;
                moveSpeed -= 0.1f;
                break;
            case GunsmithData.BarrelType.LongM:
                spreadAngle -= 1.3f;
                moveSpeed -= 0.2f;
                break;
            case GunsmithData.BarrelType.LongL:
                spreadAngle -= 2.6f;
                moveSpeed -= 0.3f;
                break;
        }
        switch (gunsmithData.muzzleType)
        {
            case GunsmithData.MuzzleType.Silencer:
                silencer = true;
                spreadAngle -= 1.2f;
                break;
            case GunsmithData.MuzzleType.Compensator:
                silencer = false;
                spreadAngle -= 1.3f;
                break;
            case GunsmithData.MuzzleType.None:
                silencer = false;
                spreadAngle -= 0f;
                break;
        }       
        switch (gunsmithData.gripType)
        {
            case GunsmithData.GripType.TGrip:
                spreadAngle -= 2f;
                reloadSpeed += 0.1f;
                break;
            case GunsmithData.GripType.ThumbGrip:
                spreadAngle -= 2f;
                moveSpeed += 0.2f;
                break;
            case GunsmithData.GripType.AngledGrip:
                spreadAngle -= 1.5f;
                moveSpeed += 0.3f;
                break;
            case GunsmithData.GripType.LightGrip:
                spreadAngle -= 1f;
                reloadSpeed -= 0.1f;
                moveSpeed += 0.5f;
                break;
            case GunsmithData.GripType.Grenade:
                spreadAngle += 1f;
                reloadSpeed += 0.2f;
                moveSpeed -= 0.3f;
                haveGrenade = true;
                break;
            case GunsmithData.GripType.None:
                spreadAngle -= 0f;
                reloadSpeed -= 0.05f;
                moveSpeed += 0.1f;
                break;
        }       
        switch (gunsmithData.stockType)
        {
            case GunsmithData.StockType.Standard:
                spreadAngle -= 2f;
                break;
            case GunsmithData.StockType.Heavy:
                reloadSpeed += 0.1f;
                spreadAngle -= 3.5f;
                moveSpeed -= 0.2f;
                break;
            case GunsmithData.StockType.Light:
                spreadAngle -= 0.3f;
                reloadSpeed -= 0.2f;
                moveSpeed += 0.5f;
                break;
            case GunsmithData.StockType.Sniper:
                spreadAngle -= 4f;
                reloadSpeed += 0.2f;
                moveSpeed -= 1f;
                break;
            case GunsmithData.StockType.None:
                spreadAngle += 5f;
                reloadSpeed -= 0.4f;
                break;
        }
        switch (gunsmithData.megType)
        {
            case GunsmithData.MegType.VeryLight:
                spreadAngle -= 0.5f;
                reloadSpeed -= 0.3f;
                maxBullet = 15;
                moveSpeed += 0.7f;
                break;
            case GunsmithData.MegType.Light:
                spreadAngle -= 0.4f;
                reloadSpeed -= 0.15f;
                maxBullet = 20;
                moveSpeed += 0.3f;
                break;
            case GunsmithData.MegType.Standard:
                reloadSpeed += 0;
                maxBullet = 30;
                break;
            case GunsmithData.MegType.StandardEx:
                reloadSpeed += 0.03f;
                maxBullet = 35;
                break;
            case GunsmithData.MegType.Drum:
                reloadSpeed += 1f;
                maxBullet = 70;
                moveSpeed -= 0.3f;
                break;
            case GunsmithData.MegType.DoubleDrum:
                reloadSpeed += 2.2f;
                maxBullet = 120;
                moveSpeed -= 0.7f;
                break;
        }
        switch (gunsmithData.triggerType)
        {
            case GunsmithData.TriggerType.Sports:
                fireRate = 0.12f;
                spreadAngle -= 0.5f;
                break;
            case GunsmithData.TriggerType.Upgrade:
                fireRate = 0.1f;
                spreadAngle += 0.8f;
                break;
            case GunsmithData.TriggerType.Illegal:
                fireRate = 0.087f;
                spreadAngle += 2f;
                break;
        }
    }
    private void Start()
    {
        if (reloadSpeed < 0.1f)
            reloadSpeed = 0.1f;
        if (spreadAngle < 0.1f)
            spreadAngle = 0.1f;
        if (moveSpeed < 0.1f)
            moveSpeed = 0.1f;

        playerInfo.text = $"Move Speed: <color=red>{moveSpeed}</color>\n" +
                   $"Reload Speed: <color=red>{reloadSpeed:F2}</color>\n" +
                   $"Fire Rate: <color=red>{fireRate}</color>\n" +
                   $"Max Bullet: <color=red>{maxBullet}</color>\n" +
                   $"Spread Angle: <color=red>{spreadAngle}°</color>\n" +
                   $"Have Grenade: <color=red>{haveGrenade}</color>\n" +
                   $"Silencer: <color=red>{silencer}</color>";


        playerInfo.gameObject.SetActive(false);
        gunSkin.color = gunsmithData.color;
        pointerToMouse.UpdateSpreadAngle(spreadAngle);

        if (gunsmithData.gripType == GunsmithData.GripType.Grenade)
        {
            grenadeUI.SetActive(true);
            grenadeUI2.SetActive(true);

        }
        else 
        {
            grenadeUI.SetActive(false);
            grenadeUI2.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerInfo.gameObject.SetActive(!playerInfo.gameObject.activeSelf);
        }

    }

}
