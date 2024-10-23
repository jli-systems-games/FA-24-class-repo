using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballController : MonoBehaviour
{
    public GameObject ball, canvas, barrier;

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
        ball.transform.position = new Vector3(0, 4, 26);
        LaunchBall();

        canvas.SetActive(false);
        barrier.SetActive(true);
    }

    void LaunchBall()
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.useGravity = true;
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(0.5f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        Vector3 randomForce = new Vector3(randomX, randomY, randomZ).normalized * 10;
        ballRigidbody.AddForce(randomForce, ForceMode.Impulse);
    }
}
