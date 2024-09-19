using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHands : MonoBehaviour
{
    public KeyCode defenceKey = KeyCode.None;

    private bool canDefence = false;

    private bool attackDefenced = false;

    private Animator animator;

    private CameraShake cameraShake;

    public Sprite[] skins;

    private SpriteRenderer spriteRenderer;
    private PowerButtonAnimController powerButtonAnimController;
    private MiniGameLevelController miniGameLevelController;
    private AudioManager audioManager;
    private InputManager inputManager;

    public Hand hand;
    public enum Hand
    { 
     LeftHand, RightHand
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = FindObjectOfType<CameraShake>();
        inputManager = FindObjectOfType<InputManager>();
        audioManager = FindObjectOfType<AudioManager>();
        powerButtonAnimController = FindObjectOfType<PowerButtonAnimController>();
        miniGameLevelController = FindObjectOfType<MiniGameLevelController>();
        animator = GetComponent<Animator>();

        if (skins.Length > 0) 
        {
            int randomIndex = Random.Range(0, skins.Length); 
            spriteRenderer.sprite = skins[randomIndex]; 
        }

        Invoke("StartCountdown", 3f);
        Invoke("ButtonCheck", 4f);
    }

    private void Update()
    {
        if (canDefence && Input.GetKeyDown(defenceKey) && !attackDefenced  && inputManager.gameStarted)
        {
            cameraShake.ShakeCamera();           
            attackDefenced = true;
            animator.SetTrigger("Trigger");
            audioManager.PlayDefended();
            if (hand == Hand.LeftHand)
                inputManager.ResetHandCooldown(true);
            else if (hand == Hand.RightHand)
                inputManager.ResetHandCooldown(false);

            Invoke("DestroyHand", 1f);
        }
    }
    void StartCountdown()
    {
        audioManager.PlayDefendReady();
        canDefence = true;
    }
    void DestroyHand()
    {
        Destroy(gameObject);
    }

    void ButtonCheck()
    {
        if (!attackDefenced)
        {    
            powerButtonAnimController.TriggerButtonAnim();
            miniGameLevelController.GameEnd ();
        }
    
    }
}
