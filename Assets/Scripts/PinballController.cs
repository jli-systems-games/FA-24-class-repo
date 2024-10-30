using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PinballController : MonoBehaviour
{
    public GameObject ball, barrier;

    [Header("UI Elements")]
    public GameObject customizer;
    public GameObject gameUI;
    public Button shakeButton;

    private bool cooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        shakeButton.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        ball.transform.position = new Vector3(0, 4, 31.5f);
        LaunchBall();

        barrier.SetActive(true);
        gameUI.SetActive(true);
    }

    public void LaunchBall()
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.useGravity = true;
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(0.5f, 1f);
        //float randomZ = Random.Range(-1f, 1f);

        Vector3 randomForce = new Vector2(randomX, randomY).normalized * 10;
        ballRigidbody.AddForce(randomForce, ForceMode.Impulse);
    }

    public void ScreenShake()
    {

    }

    private void OnButtonClick()
    {
        if (!cooldown)
        {
            StartCoroutine(ButtonCooldown());
        }
    }

    public void ShakeMachine()
    {
        LaunchBall();
        ScreenShake();
    }

    public IEnumerator ButtonCooldown()
    {
        cooldown = true;
        shakeButton.interactable = false;

        yield return new WaitForSeconds(1f);

        shakeButton.interactable = true;
        cooldown = false;
    }


    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
