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

    // Track special power usage for player and opponent
    private int playerSpecialTurnsUsed = 0;
    private int opponentSpecialTurnsUsed = 0;

    // Reference to the BattleUIManager for displaying action text
    public BattleUIManager uiManager;

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

        // Instantiate the player and opponent characters
        Vector3 playerPosition = playerSpawnPoint.position + playerCharacter.playerPositionOffset;
        playerCharacterInstance = Instantiate(playerCharacter.characterPrefab, playerPosition, Quaternion.identity, playerSpawnPoint);
        Vector3 opponentPosition = opponentSpawnPoint.position + opponentCharacter.opponentPositionOffset;
        opponentCharacterInstance = Instantiate(opponentCharacter.characterPrefab, opponentPosition, Quaternion.Euler(0, 180, 0), opponentSpawnPoint);

        InstantiateFaceIcons();
        StartCoroutine(BattleRoutine());
    }

    private void InstantiateFaceIcons()
    {
        if (playerCharacter.faceIconPrefab != null)
        {
            Instantiate(playerCharacter.faceIconPrefab, playerFaceIconHolder.position, Quaternion.identity, playerFaceIconHolder);
        }
        if (opponentCharacter.faceIconPrefab != null)
        {
            Instantiate(opponentCharacter.faceIconPrefab, opponentFaceIconHolder.position, Quaternion.identity, opponentFaceIconHolder);
        }
    }

    private IEnumerator BattleRoutine()
    {
        playerSpecialTurnsUsed = 0;
        opponentSpecialTurnsUsed = 0;

        while (playerHealthBar.value > 0 && opponentHealthBar.value > 0)
        {
            yield return new WaitForSeconds(2f);

            // Player's turn
            yield return StartCoroutine(TakeTurn(playerCharacter, playerHealthBar, playerAttackPowerBar, playerDefenseBar, opponentHealthBar, true));

            if (opponentHealthBar.value <= 0)
            {
                uiManager.ShowActionText("Player Wins!", true); // Show win text indefinitely
                yield break;
            }

            yield return new WaitForSeconds(2f);

            // Opponent's turn
            yield return StartCoroutine(TakeTurn(opponentCharacter, opponentHealthBar, opponentAttackPowerBar, opponentDefenseBar, playerHealthBar, false));

            if (playerHealthBar.value <= 0)
            {
                uiManager.ShowActionText("Opponent Wins!", true); // Show win text indefinitely
                yield break;
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator TakeTurn(Character character, Slider healthBar, Slider attackPowerBar, Slider defenseBar, Slider opponentHealthBar, bool isPlayer)
    {
        string actor = isPlayer ? "Player" : "Opponent";
        Debug.Log($"{actor}'s Turn");

        bool canUseDefense = healthBar.value <= healthBar.maxValue * 0.8f && defenseBar.value > 0.0f;
        bool useDefense = false;

        if (canUseDefense)
        {
            if (healthBar.value <= healthBar.maxValue * 0.4f)
            {
                useDefense = Random.value < 0.7f;
            }
            else
            {
                useDefense = Random.value < 0.3f;
            }
        }

        if (useDefense && defenseBar.value > 0.0f)
        {
            Debug.Log($"{actor} uses Defense!");
            uiManager.ShowActionText($"{actor} uses Defense!");

            defenseBar.value = Mathf.Max(0.0f, defenseBar.value - defenseBar.maxValue * 0.25f);
            float healthRestoration = character.characterType == CharacterType.Magic ? 0.25f : 0.15f;
            healthBar.value = Mathf.Min(healthBar.maxValue, healthBar.value + healthBar.maxValue * healthRestoration);
            attackPowerBar.value = Mathf.Min(attackPowerBar.maxValue, attackPowerBar.value + attackPowerBar.maxValue * 0.2f);
        }
        else
        {
            int maxSpecialUses = character.characterType == CharacterType.Melee ? 3 : 2;
            int specialTurnsUsed = isPlayer ? playerSpecialTurnsUsed : opponentSpecialTurnsUsed;
            bool useSpecial = specialTurnsUsed < maxSpecialUses && Random.value < 0.35f;

            float baseDamage = character.characterType switch
            {
                CharacterType.Ranged => 3.0f,
                CharacterType.Melee => 1.8f,
                CharacterType.Magic => 2.5f,
                _ => 0.0f
            };

            float damage = baseDamage;
            if (useSpecial)
            {
                Debug.Log($"{actor} uses Special Power!");
                uiManager.ShowActionText($"{actor} uses Special Power!");
                damage += baseDamage * 0.25f;
                if (isPlayer) playerSpecialTurnsUsed++;
                else opponentSpecialTurnsUsed++;
            }
            else
            {
                uiManager.ShowActionText($"{actor} attacks!");
            }

            //If health is below 0, zero out every bar
            if (healthBar.value <= 0.0f)
            {
                healthBar.value = 0.0f;
                attackPowerBar.value = 0.0f;
                defenseBar.value = 0.0f;
            }

            if (healthBar.value <= healthBar.maxValue * 0.0f)
            {
                Debug.Log($"{actor}'s health is critically low! Attack power reduced by an additional 25%");
                attackPowerBar.value = Mathf.Max(0.0f, attackPowerBar.value * 0.5f * 0.75f);
            }
            else if (healthBar.value <= healthBar.maxValue * 0.15f)
            {
                Debug.Log($"{actor}'s health is critically low! Attack power reduced by an additional 25%");
                attackPowerBar.value = Mathf.Max(0.0f, attackPowerBar.value * 0.5f * 0.75f);
            }
            else if (healthBar.value <= healthBar.maxValue * 0.3f)
            {
                Debug.Log($"{actor}'s health is very low! Attack power reduced by 50%");
                damage *= 0.5f;
                attackPowerBar.value = Mathf.Max(0.0f, attackPowerBar.value * 0.5f);
            }
            else if (healthBar.value <= healthBar.maxValue * 0.6f)
            {
                Debug.Log($"{actor}'s health is below 60%! Attack power reduced by 25%");
                damage *= 0.75f;
                attackPowerBar.value = Mathf.Max(0.0f, attackPowerBar.value * 0.75f);
            }

            opponentHealthBar.value = Mathf.Clamp(opponentHealthBar.value - damage, 0.0f, opponentHealthBar.maxValue);
        }

        yield return new WaitForSeconds(1f);
    }
}
