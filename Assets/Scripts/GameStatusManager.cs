using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatusManager : MonoBehaviour
{
    private float maxHealth = 100;
    private float maxPower = 100;

    public bool gameStarted = false;

    public int currentRound = 0;

    public int player1score = 0;
    public int player2score = 0;

    public float player1Health;
    public float player2Health;

    public float player1Power = 0;
    public float player2Power = 0;

    public GameObject ball;
    public GameObject player1;
    public GameObject player2;

    public GameObject player1PowerBar;
    public GameObject player2PowerBar;
    public Animator player1PowerBarAnim;
    public Animator player2PowerBarAnim;

    public GameObject player1HealthBar;
    public GameObject player2HealthBar;

    public float ballSpeed = 5f;

    public TextMeshProUGUI scoreUI;
    private AnimationController animationController;
    private PlayerPowers playerPowers;
    private ToPlayer player = ToPlayer.Player2;
    private enum ToPlayer
    {
        Player1, Player2
    }

    private PowerJemSpawner powerJemSpawner;

    private void Start()
    {
        playerPowers = GetComponent<PlayerPowers>();
        animationController = GetComponent<AnimationController>();
        powerJemSpawner = FindObjectOfType<PowerJemSpawner>();
        player1Health = maxHealth;
        player2Health = maxHealth;
    }

    private void Update()
    {
        if (player1Power >= maxPower)
        {
            player1PowerBarAnim.SetBool("FullPower",true);
        }
        else
            player1PowerBarAnim.SetBool("FullPower", false);
        if (player2Power >= maxPower)
        {
            player2PowerBarAnim.SetBool("FullPower", true);
        }
        else
            player2PowerBarAnim.SetBool("FullPower", false);

        //Debug.Log("P2血量为" + player2Health);
    }

    public void ResetGame()
    {
        playerPowers.GameStartAnim();
        currentRound = 0;
        player1score = 0;
        player2score = 0;
        player1Health = maxHealth;
        player2Health = maxHealth;
        player1Power = 0;
        player2Power = 0;
        ResetScore();
        powerJemSpawner.ClearSpawnedItems();
        ResetBars();
        StartCoroutine(ShootBallAfterDelay(4f));
    }

    public void ResetRound()
    {
        player1Health = maxHealth;
        player2Health = maxHealth;
        player1Power = 0;
        player2Power = 0;
        UpdateHealthBar();
        UpdatePowerBar();
        powerJemSpawner.ClearSpawnedItems();
        StartCoroutine(ShootBallAfterDelay(3.5f));
    }

    public void ResetBall()
    {
        powerJemSpawner.ClearSpawnedItems();
        StartCoroutine(ShootBallAfterDelay(1f));
    }

    IEnumerator ShootBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

        StartBall();
    }

    public void AddP1Power()
    {
        player1Power += 15;
        player1Power = Mathf.Clamp(player1Power, 0, maxPower); 
        UpdatePowerBar();
    }

    public void AddP2Power()
    {
        player2Power += 15;
        player2Power = Mathf.Clamp(player2Power, 0, maxPower); 
        UpdatePowerBar();
    }

    public void DecreaseP1Health()
    {
        if (player1Health > 15)
        {
            player1Health -= 15;
            UpdateHealthBar();
            ResetBall();
        }
        else
        {
            PlusP2Score();
            animationController.PlayP2ScoreAnimation();
            ResetRound();
        }
    }
    public void DecreaseP2Health()
    {
        if (player2Health > 15)
        {
            player2Health -= 15;
            UpdateHealthBar();
            ResetBall();
        }
        else
        {
            PlusP1Score();
            animationController.PlayP1ScoreAnimation();
            ResetRound();
        }
    }

    public void UpdatePowerBar()
    {
        // 将当前能量映射在能量条上：能量条的scale.x == 0时代表能量为空(0%)；能量条的scale.x == 1时代表能量为满(100%)
        Vector3 p1PowerBarScale = player1PowerBar.transform.localScale;
        p1PowerBarScale.x = player1Power / maxPower; 
        player1PowerBar.transform.localScale = p1PowerBarScale;

        Vector3 p2PowerBarScale = player2PowerBar.transform.localScale;
        p2PowerBarScale.x = player2Power / maxPower; 
        player2PowerBar.transform.localScale = p2PowerBarScale;
    }

    private void UpdateHealthBar()
    {       
        Vector3 p1HealthBarScale = player1HealthBar.transform.localScale;
        p1HealthBarScale.x = player1Health / maxHealth; 
        player1HealthBar.transform.localScale = p1HealthBarScale;

        Vector3 p2HealthBarScale = player2HealthBar.transform.localScale;
        p2HealthBarScale.x = player2Health / maxHealth;
        player2HealthBar.transform.localScale = p2HealthBarScale;
    }


    private void ResetBars()
    {
        // 清空能量
        player1PowerBar.transform.localScale = new Vector3(0, player1PowerBar.transform.localScale.y, player1PowerBar.transform.localScale.z);
        player2PowerBar.transform.localScale = new Vector3(0, player2PowerBar.transform.localScale.y, player2PowerBar.transform.localScale.z);
                
        player1HealthBar.transform.localScale = new Vector3(1,player1HealthBar.transform.localScale.y, player1HealthBar.transform.localScale.z);
        player2HealthBar.transform.localScale = new Vector3(1,player2HealthBar.transform.localScale.y, player2HealthBar.transform.localScale.z);
    }

    void StartBall()
    {
        // 球从 (0, 0) 位置发射
        GameObject newBall = Instantiate(ball, Vector3.zero, Quaternion.identity);

        // 设置偏移角度范围
        float angleOffsetRange = 15f;  // 例如±15度
        float angleOffset = Random.Range(-angleOffsetRange, angleOffsetRange);
        Quaternion rotationOffset = Quaternion.Euler(0, 0, angleOffset);  // 旋转偏移

        if (player == ToPlayer.Player1)
        {
            Vector3 player1Position = player1.transform.position;
            Vector3 direction = (player1Position - newBall.transform.position).normalized;

            // 应用偏移量
            direction = rotationOffset * direction;

            // 设置球的速度并发射
            newBall.GetComponent<Rigidbody2D>().velocity = direction * ballSpeed;

            player = ToPlayer.Player2;
        }
        else
        {
            Vector3 player2Position = player2.transform.position;
            Vector3 direction = (player2Position - newBall.transform.position).normalized;

            // 应用偏移量
            direction = rotationOffset * direction;

            newBall.GetComponent<Rigidbody2D>().velocity = direction * ballSpeed;

            player = ToPlayer.Player1;
        }
    }


    private void PlusP1Score()
    {
        player1score += 1;
        UpdateScoreUI();
    }

    private void PlusP2Score()
    {
        player2score += 1;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreUI.text = player1score.ToString() + ":" + player2score.ToString();
    }

    private void ResetScore()
    {
        scoreUI.text = "0:0";
    }

    public void UseHealP1()
    { 
    
    }
    public void UseHealP2()
    {

    }
}
