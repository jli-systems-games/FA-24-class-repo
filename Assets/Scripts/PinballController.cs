using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinballController : MonoBehaviour
{
    public GameObject ball, barrier;

    [Header("UI Elements")]
    public GameObject customizer;
    public GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
