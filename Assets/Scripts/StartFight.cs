using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartFight : MonoBehaviour
{
    public PlayerOne playerScript;
    public PlayerTwo CPUscript;
    public Button battleButton;
    public GameObject rerollButton;

    public GameObject CPUTextDamage;
    public GameObject playerTextDamage;

    public GameObject playerTextPostion;
    public GameObject CPUtextPosition;

    public TMP_Text playerAttackText;
    public TMP_Text CPUattackText;

    public TextMeshProUGUI displayText;
    public GameObject winScreen;

    public GameObject playerSlider;
    public GameObject CPUSlider;

    public void Battle()
    {
        StartCoroutine(FightButton());

    }


    public void Unactivate()
    {
        rerollButton.SetActive(false);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator FightButton()
    {
        float player1NextAttackTime = 0f;
        float player2NextAttackTime = 0f;
        float player1AttackInterval = Mathf.Max(0.5f, 2.0f / playerScript.speed);
        float player2AttackInterval = Mathf.Max(0.5f, 2.0f / CPUscript.speed);

        while (playerScript.health > 0 && CPUscript.health > 0)
        {
            yield return null;
            float currentTime = Time.time;

            if (currentTime >= player1NextAttackTime)
            {
                int damageToPlayer2 = Mathf.Max(0, playerScript.attack - CPUscript.defense);
                CPUscript.health -= damageToPlayer2;

                CPUattackText.text = playerScript.attack.ToString();
                Instantiate(CPUTextDamage, CPUtextPosition.transform.position, Quaternion.identity);

                CPUscript.UpdateCPUBar(CPUscript.health, CPUscript.maxHealth);

                Debug.Log($"Player attacks CPU for {damageToPlayer2} damage. CPU Health: {CPUscript.health}");

                player1NextAttackTime = currentTime + player1AttackInterval;

                //PlayerTwo playerTwoComponent = CPUSlider.GetComponent<PlayerTwo>();
                //playerTwoComponent.UpdateCPUBar(playerTwoComponent.health, playerTwoComponent.maxHealth);
            }

            if (currentTime >= player2NextAttackTime)
            {
                int damageToPlayer1 = Mathf.Max(0, CPUscript.attack - playerScript.defense);
                playerScript.health -= damageToPlayer1;

                playerAttackText.text = CPUscript.attack.ToString();
                Instantiate(playerTextDamage, playerTextPostion.transform.position, Quaternion.identity);

                playerScript.UpdatePlayerBar(playerScript.health, playerScript.maxHealth);

                Debug.Log($"CPU attacks Player for {damageToPlayer1} damage. Player Health: {playerScript.health}");

                player2NextAttackTime = currentTime + player2AttackInterval;



                //PlayerOne playerOneComponent = playerSlider.GetComponent<PlayerOne>();

                //playerOneComponent.UpdatePlayerBar(playerOneComponent.health, playerOneComponent.maxHealth);
            }

            if (playerScript.health <= 0 || CPUscript.health <= 0)
            {
                Debug.Log(playerScript.health <= 0 && CPUscript.health <= 0 ? "It's a draw!" :

                    playerScript.health <= 0 ? "Player 2 Wins!" : "Player 1 Wins!");


                playerSlider.SetActive(false);
                CPUSlider.SetActive(false);
                winScreen.SetActive(true);


                if (playerScript.health <= 0)
                {
                    displayText.text = "CPU Wins!";
                }
                if (CPUscript.health <= 0)
                {
                    displayText.text = "Player Wins!";
                }


                yield break;
            }
        }

    }

}