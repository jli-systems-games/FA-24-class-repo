using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [Header("Game References")]
    public EnemyScript enemy;
    public GameManager gameManager;

    [Header("Player Stats")]
    public int maxLives = 3;
    private int currentLives;

    [Header("UI")]
    public Image[] hpImages;

    private string expectedInput;
    private bool hasResponded;

    public Animator anim;

    void Start()
    {
        if (enemy != null)
        {
            enemy.onAttackPrompt.AddListener(OnEnemyPrompt);
        }
        currentLives = maxLives;

        anim = GetComponent<Animator>();
    }


    private void OnDestroy()
    {
        if (enemy != null)
            enemy.onAttackPrompt.RemoveListener(OnEnemyPrompt);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            HandlePlayerInput();
    }

    private void OnEnemyPrompt(string enemyDirection)
    {
        expectedInput = GetOppositeDirection(enemyDirection);
        ResetResponseState();
    }

    private void HandlePlayerInput()
    {
        var inputDirection = GetInputDirection();
        if (string.IsNullOrEmpty(inputDirection)) return;

        if (inputDirection == expectedInput)
        {
            hasResponded = true;
        }
        else
        {
            LoseLife();
        }
    }

    private string GetInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("UpKey");
            return "Up";
        }
        else
        {
            anim.ResetTrigger("UpKey");
            anim.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("LeftKey");
            return "Left";
        }
        else
        {
            anim.ResetTrigger("LeftKey");
            anim.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("DownKey");
            return "Down";
        }
        else
        {
            anim.ResetTrigger("DownKey");
            anim.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("RightKey");
            return "Right";
        }
        else
        {
            anim.ResetTrigger("RightKey");
            anim.SetTrigger("Idle");
        }

        if (Input.anyKey == false)
        {
            anim.SetTrigger("Idle");
        }
        return null;
    }

    private string GetOppositeDirection(string direction)
    {
        return direction switch
        {
            "Up" => "Down",
            "Down" => "Up",
            "Left" => "Right",
            "Right" => "Left",
            _ => null
        };
    }

    private void LoseLife()
    {
        currentLives = Mathf.Max(currentLives - 1, 0);
        UpdateHpUI();

        if (currentLives <= 0)
        {
            if (gameManager != null)
            {
                gameManager.PlayerDied();
            }
        }
    }


    private void UpdateHpUI()
    {
        for (int i = 0; i < hpImages.Length; i++)
        {
            hpImages[i].enabled = i < currentLives;
        }
    }

    private void ResetResponseState()
    {
        hasResponded = false;
    }

    public void MissedInput()
    {
        if (!hasResponded)
        {
            LoseLife();
        }
    }

    public bool RegisterPlayerResponse()
    {
        return hasResponded;
    }
}
