using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    private MiniGameLevelController levelController;
    private GameObject playerObject;
    private bool scoreAdded = false;
    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        levelController = FindObjectOfType<MiniGameLevelController>();
    }
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }

        if (!scoreAdded && gameObject.transform.position.x < playerObject.transform.position.x)
        {
            levelController.AddScore();
            scoreAdded = true;
        }
    }
}
