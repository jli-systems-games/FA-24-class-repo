using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public EnemyScript enemy;
    public GameManager gameManager;

    private string expectedInput;

    public int maxLives = 3;
    private int currentLives;
    public Image[] hpImages;

    void Start()
    {
        currentLives = maxLives;
        enemy.onAttackPrompt.AddListener(OnEnemyPrompt);
    }

    void OnDestroy()
    {
        enemy.onAttackPrompt.RemoveListener(OnEnemyPrompt);
    }

    private void OnEnemyPrompt(string enemyDirection)
    {
        expectedInput = GetOppositeDirection(enemyDirection);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            string inputDirection = GetInputDirection();
            if (string.IsNullOrEmpty(inputDirection)) return;

            if (inputDirection == expectedInput)
            {
                // Correct input, no action for now
            }
            else
            {
                LoseLife();
            }
        }
    }

    private string GetInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.W)) return "Up";
        if (Input.GetKeyDown(KeyCode.A)) return "Left";
        if (Input.GetKeyDown(KeyCode.S)) return "Down";
        if (Input.GetKeyDown(KeyCode.D)) return "Right";
        return null;
    }

    private void LoseLife()
    {
        currentLives--;
        UpdateHp();

        if (currentLives <= 0)
        {
            gameManager.PlayerDied();
        }
    }

    private void UpdateHp()
    {
        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].enabled = i < currentLives;
        }
    }

    private string GetOppositeDirection(string direction)
    {
        return direction switch
        {
            "Up" => "Down",
            "Down" => "Up",
            "Left" => "Right",
            "Right" => "Left",
            _ => ""
        };
    }
}
