using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class IntroPage : MonoBehaviour
{
    
    public GameStage gameStage = GameStage.Stage1;

    public CanvasGroup timeCanvas, blackCanvas;

    public GameObject playerCam,ghostCam1,ghostCam2;

    public GameObject palyerWeapon,aim;

    public GameObject bkg,playerFace,playerFace2,playerFace3,qInstruction,skipTip;

    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject playerLight;

    public Transform finalAnchor,playerTransform;

    private AudioManager audioManager;
    public PointerToMouse pointerToMouse;
    private int hitTimes = 0;

    public GameObject tipText,objectiveText;

    private float holdTimer = 0f;

    public enum GameStage
    { 
        Stage1,
        Stage2,
        Stage3,
        Stage4,   
        Stage5,
        Stage6,
        Stage7,
    }

    private void Start()
    {
        blackCanvas.alpha = 0f;
        audioManager = GetComponent<AudioManager>();
        playerFace3.SetActive(false);
        skipTip.SetActive(false);
    }

    private void Update()
    {
        if (gameStage == GameStage.Stage1 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeCanvasGroup(timeCanvas, 1, 0, 1));
            StartCoroutine(ChangeUIText(tipText, "press D to move Right"));
            StartCoroutine(ChangeUIText(objectiveText, "Objective: go home"));
            skipTip.SetActive(true);
            gameStage = GameStage.Stage2;
        }
        if (gameStage == GameStage.Stage3 && Input.GetKeyDown(KeyCode.A)) 
        {           
            gameStage = GameStage.Stage4;
            playerCam.SetActive(true);
            ghostCam1.SetActive(false);
        }
        if (gameStage == GameStage.Stage6 && Input.GetKeyDown(KeyCode.Q))
        {
           aim.SetActive(true);
           palyerWeapon.SetActive(true);
           gameStage = GameStage.Stage7;
           playerCam.SetActive(true);
           ghostCam2.SetActive(false);
           qInstruction.SetActive(false);
            StartCoroutine(ChangeUIText(tipText, "LMB to fire"));
            StartCoroutine(ChangeUIText(objectiveText, "WTFFF"));

            playerTransform.position = finalAnchor.position;

        }

        if (Input.GetMouseButtonDown(0) && gameStage == GameStage.Stage7)
        {
            if (hitTimes >= 20)
            {
                StartCoroutine(LoadScene());
            }
            else
            {
               // pointerToMouse.ShootProjectile(pointerToMouse.bullet2);
                audioManager.PlayPistolShot();
                IncreaseAlpha();
                hitTimes++;
            }            
        }

        if (Input.GetKey(KeyCode.R))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= 2)
            {
                SceneManager.LoadScene("Home");
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    IEnumerator ChangeUIText(GameObject target, string text)
    {
        Animator animator = target.GetComponent<Animator>();
        TextMeshProUGUI tmp = target.GetComponent<TextMeshProUGUI>();
        animator.SetTrigger("Switch");

        yield return new WaitForSeconds(0.5f);        

        tmp.text = text;
    }

    public void SwitchToStage3()
    {
        if (gameStage == GameStage.Stage2)
        {
            StartCoroutine(ChangeUIText(tipText, "press A to move left"));
            StartCoroutine(ChangeUIText(objectiveText, "Objective: stay away from the ghost"));
            playerLight.SetActive(false);
            ghost2.SetActive(true);
            playerCam.SetActive(false);
            ghostCam1.SetActive(true);
            gameStage = GameStage.Stage3;
        }
        
    }
    public void SwitchToStage5()
    {
        if (gameStage == GameStage.Stage4)
        {
            Animator anim1 = tipText.GetComponent<Animator>();
            anim1.SetTrigger("MoveAway");
            Animator anim2 = objectiveText.GetComponent<Animator>();
            anim2.SetTrigger("MoveAway");
            ghost1.SetActive(false);
            playerCam.SetActive(false);
            ghostCam2.SetActive(true);
            gameStage = GameStage.Stage5;
            StartCoroutine(Anim());
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

    private IEnumerator Anim()
    {
        playerFace3.SetActive(true);

        yield return new WaitForSeconds(2f);
        bkg.SetActive(true);
        playerFace.SetActive(true);

        yield return new WaitForSeconds(1f);

        playerFace2.SetActive(true);

        yield return new WaitForSeconds(1f);

        qInstruction.SetActive(true);

        gameStage = GameStage.Stage6;
    }

    private void IncreaseAlpha()
    {
        blackCanvas.alpha = Mathf.Clamp(blackCanvas.alpha + 0.05f, 0f, 1f);
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Home");
    }
}
