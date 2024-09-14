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
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraShake = FindObjectOfType<CameraShake>();
        powerButtonAnimController = FindObjectOfType<PowerButtonAnimController>();
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
        if (canDefence && Input.GetKeyDown(defenceKey) && !attackDefenced)
        {
            cameraShake.ShakeCamera();
            attackDefenced = true;
            animator.SetTrigger("Trigger");
            Invoke("DestroyHand", 1f);            
        }
    }
    void StartCountdown()
    {
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

        }
    
    }
}
