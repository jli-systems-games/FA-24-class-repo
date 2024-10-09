using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;
    public Button RestartButton;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    private int battleCount = 0;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        /*GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();*/

        SpawnEnemy();

        playerGO.transform.position = new Vector3(0, 0, 0);
        /*enemyGO.transform.position = new Vector3(4, 3, 0);*/

        playerUnit = playerGO.GetComponent<Unit>();
        /*enemyUnit = enemyGO.GetComponent<Unit>();*/

        dialogueText.text = "Challenger " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void SpawnEnemy()
    {
        if (battleCount < enemyPrefabs.Length)
        {
            GameObject enemyGo = Instantiate(enemyPrefabs[battleCount]);
            enemyUnit = enemyGo.GetComponent<Unit>();
            enemyGo.transform.position = new Vector3(4, 3, 0);
        }
        else
        {
            dialogueText.text = "No more enemies!";
        }
    }

    IEnumerator PlayerAttack()
    {
        // dmg enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        // check if enemy is dead
        if (isDead)
        {
            // end battle
            state = BattleState.WON;
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            EndBattle();
        }
        else
        {
            // enemy turn
            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You dealt " + playerUnit.damage + " damage...";

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "An enemy has been slain!";
            battleCount++;

            if (battleCount < 3)
            {
                StartCoroutine(NextBattle());
            }
            else
            {
                dialogueText.text = "Victory! +18LP";
                ShowRestartButton();
            }
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "Defeat. -25LP";
            ShowRestartButton();
        }
    }

    void ShowRestartButton()
    {
        RestartButton.gameObject.SetActive(true);
    }

    IEnumerator NextBattle()
    {
        yield return new WaitForSeconds(2f);

        dialogueText.text = "Another challenger approaches...";

        Destroy(enemyUnit.gameObject);

        SpawnEnemy();

        playerUnit.currentHP = playerUnit.maxHP;
        playerHUD.SetHP(playerUnit.currentHP);

        enemyHUD.SetHUD(enemyUnit);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(10);

        state = BattleState.ENEMYTURN;

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You used a potion!";

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
