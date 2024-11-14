using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
     // UI elements for player stats
    public Slider playerHealthBar;
    public Slider playerAttackPowerBar;
    public Slider playerDefenseBar;

    // UI elements for opponent stats
    public Slider opponentHealthBar;
    public Slider opponentAttackPowerBar;
    public Slider opponentDefenseBar;

    // Face icon holders for player and opponent
    public Transform playerFaceIconHolder;
    public Transform opponentFaceIconHolder;

    // Spawn points for player and opponent characters
    public Transform playerSpawnPoint;
    public Transform opponentSpawnPoint;

    // Instances of player and opponent characters
    private GameObject playerCharacterInstance;
    private GameObject opponentCharacterInstance;

    private Character playerCharacter;
    private Character opponentCharacter;

    private void Start()
    {
        // Get the characters from the GameManager
        playerCharacter = GameManager.Instance.playerCharacter;
        opponentCharacter = GameManager.Instance.opponentCharacter;

        // Initialize health, attack, and defense bars for the player
        playerHealthBar.maxValue = playerCharacter.health;
        playerHealthBar.value = playerCharacter.health;

        playerAttackPowerBar.maxValue = playerCharacter.attackPower;
        playerAttackPowerBar.value = playerCharacter.attackPower;

        playerDefenseBar.maxValue = playerCharacter.defense;
        playerDefenseBar.value = playerCharacter.defense;

        // Initialize health, attack, and defense bars for the opponent
        opponentHealthBar.maxValue = opponentCharacter.health;
        opponentHealthBar.value = opponentCharacter.health;

        opponentAttackPowerBar.maxValue = opponentCharacter.attackPower;
        opponentAttackPowerBar.value = opponentCharacter.attackPower;

        opponentDefenseBar.maxValue = opponentCharacter.defense;
        opponentDefenseBar.value = opponentCharacter.defense;

        // Instantiate the player character facing right
        playerCharacterInstance = Instantiate(playerCharacter.characterPrefab, playerSpawnPoint.position, Quaternion.identity, playerSpawnPoint);

        // Instantiate the opponent character facing left (flipped 180 degrees on Y-axis)
        opponentCharacterInstance = Instantiate(opponentCharacter.characterPrefab, opponentSpawnPoint.position, Quaternion.Euler(0, 180, 0), opponentSpawnPoint);

        // Instantiate face icons
        InstantiateFaceIcons();

        // Start the battle routine
        StartCoroutine(BattleRoutine());
    }

    private void InstantiateFaceIcons()
    {
        // Instantiate player face icon prefab
        if (playerCharacter.faceIconPrefab != null)
        {
            Instantiate(playerCharacter.faceIconPrefab, playerFaceIconHolder.position, Quaternion.identity, playerFaceIconHolder);
        }

        // Instantiate opponent face icon prefab
        if (opponentCharacter.faceIconPrefab != null)
        {
            Instantiate(opponentCharacter.faceIconPrefab, opponentFaceIconHolder.position, Quaternion.identity, opponentFaceIconHolder);
        }
    }

    private IEnumerator BattleRoutine()
    {
        while (playerHealthBar.value > 0 && opponentHealthBar.value > 0)
        {
            // Player's turn
            yield return StartCoroutine(PlayerTurn());

            // Check if opponent is defeated
            if (opponentHealthBar.value <= 0)
            {
                Debug.Log("Player Wins!");
                yield break;
            }

            // Opponent's turn
            yield return StartCoroutine(OpponentTurn());

            // Check if player is defeated
            if (playerHealthBar.value <= 0)
            {
                Debug.Log("Opponent Wins!");
                yield break;
            }
        }
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("Player's Turn: Attacks Opponent");

        // Trigger player attack animation
        /*if (GameManager.Instance.playerCharacter.characterPrefab.TryGetComponent(out Animator playerAnimator))
        {
            playerAnimator.SetTrigger("Attack");
        }
        */
        // Calculate damage based on player's attack power and opponent's defense
        float damage = Mathf.Max(0, playerCharacter.attackPower - opponentCharacter.defense);
        opponentHealthBar.value -= damage;

        yield return new WaitForSeconds(1f); // Wait for a moment to visualize the turn
    }

    private IEnumerator OpponentTurn()
    {
        Debug.Log("Opponent's Turn: Attacks Player");

        /*
        // Trigger opponent attack animation
        if (GameManager.Instance.opponentCharacter.characterPrefab.TryGetComponent(out Animator opponentAnimator))
        {
            opponentAnimator.SetTrigger("Attack");
        }
        */

        // Calculate damage based on opponent's attack power and player's defense
        float damage = Mathf.Max(0, opponentCharacter.attackPower - playerCharacter.defense);
        playerHealthBar.value -= damage;

        yield return new WaitForSeconds(1f); // Wait for a moment to visualize the turn
    }
}
