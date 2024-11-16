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

    // Retry button for restarting the round
    public Button retryButton;

    // Bounce effect parameters
    public float bounceDistance = 0.5f;
    public float bounceDuration = 0.3f;

    // Camera shake parameters
    public Transform cameraTransform;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    // Scaling factor for attack power
    public float attackPowerScalingFactor = 0.8f; // Determines how attack power scales relative to health


    private void Start()
    {
        // Initialize Retry button and hide it initially
        retryButton.gameObject.SetActive(false); // Hide the button initially

        // Set up listener for retry button
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        // Get characters from the GameManager
        playerCharacter = GameManager.Instance.playerCharacter;
        opponentCharacter = GameManager.Instance.opponentCharacter;

        // Initialize health, attack, and defense bars for the player
        playerHealthBar.maxValue = playerCharacter.health;
        playerHealthBar.value = playerCharacter.health;
        playerAttackPowerBar.maxValue = playerCharacter.attackPower;
        playerAttackPowerBar.value = playerCharacter.attackPower * ((playerHealthBar.value / playerHealthBar.maxValue) * attackPowerScalingFactor);
        playerDefenseBar.maxValue = playerCharacter.defense;
        playerDefenseBar.value = playerCharacter.defense;

        // Initialize health, attack, and defense bars for the opponent
        opponentHealthBar.maxValue = opponentCharacter.health;
        opponentHealthBar.value = opponentCharacter.health;
        opponentAttackPowerBar.maxValue = opponentCharacter.attackPower;
        opponentAttackPowerBar.value = opponentCharacter.attackPower * ((opponentHealthBar.value / opponentHealthBar.maxValue) * attackPowerScalingFactor);
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
            yield return new WaitForSeconds(1f);

            // Player's turn
            yield return StartCoroutine(TakeTurn(playerCharacter, playerHealthBar, playerAttackPowerBar, playerDefenseBar, opponentHealthBar, true));

            if (opponentHealthBar.value <= 0)
            {
                uiManager.ShowActionText("Player Wins!", true); // Show win text indefinitely
                yield return new WaitForSeconds(1f);
                retryButton.gameObject.SetActive(true); // Show retry button after delay
                retryButton.interactable = true; // Make the button clickable
                yield break;
            }

            yield return new WaitForSeconds(1f);

            // Opponent's turn
            yield return StartCoroutine(TakeTurn(opponentCharacter, opponentHealthBar, opponentAttackPowerBar, opponentDefenseBar, playerHealthBar, false));

            if (playerHealthBar.value <= 0)
            {
                uiManager.ShowActionText("Opponent Wins!", true); // Show win text indefinitely
                yield return new WaitForSeconds(1f);
                retryButton.gameObject.SetActive(true); // Show retry button after delay
                retryButton.interactable = true; // Make the button clickable
                yield break;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    private void ResetCharacterBarsIfHealthIsZero(Slider healthBar, Slider attackPowerBar, Slider defenseBar)
    {
        if (healthBar.value <= 0.0f)
        {
            healthBar.value = 0.0f;
            attackPowerBar.value = 0.0f;
            defenseBar.value = 0.0f;

            // Ensure UI reflects changes
            healthBar.SetValueWithoutNotify(0.0f);
            attackPowerBar.SetValueWithoutNotify(0.0f);
            defenseBar.SetValueWithoutNotify(0.0f);
        }
    }

    private IEnumerator TakeTurn(Character character, Slider healthBar, Slider attackPowerBar, Slider defenseBar, Slider opponentHealthBar, bool isPlayer)
    {
        string actor = isPlayer ? "Player" : "Opponent";
        Debug.Log($"{actor}'s Turn");

        GameObject characterInstance = isPlayer ? playerCharacterInstance : opponentCharacterInstance;

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
            yield return StartCoroutine(ApplyBounceEffect(characterInstance, isPlayer)); // Apply attack bounce effect
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
                yield return StartCoroutine(ApplyCameraShake()); // Apply camera shake effect
                damage += baseDamage * 0.25f;
                if (isPlayer) playerSpecialTurnsUsed++;
                else opponentSpecialTurnsUsed++;
            }
            else
            {
                uiManager.ShowActionText($"{actor} attacks!");
            }

            opponentHealthBar.value = Mathf.Clamp(opponentHealthBar.value - damage, 0.0f, opponentHealthBar.maxValue);
        }

        // Update attack power bar based on health after each turn
        attackPowerBar.value = Mathf.Max(0.0f, character.attackPower * (healthBar.value / healthBar.maxValue));

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator ApplyBounceEffect(GameObject characterInstance, bool isPlayer)
    {
        Vector3 originalPosition = characterInstance.transform.localPosition;
        Vector3 bounceTarget = originalPosition + new Vector3(isPlayer ? bounceDistance : -bounceDistance, 0, 0);

        float elapsedTime = 0;

        // Move forward
        while (elapsedTime < bounceDuration / 2)
        {
            characterInstance.transform.localPosition = Vector3.Lerp(originalPosition, bounceTarget, (elapsedTime / (bounceDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        // Move back
        while (elapsedTime < bounceDuration / 2)
        {
            characterInstance.transform.localPosition = Vector3.Lerp(bounceTarget, originalPosition, (elapsedTime / (bounceDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterInstance.transform.localPosition = originalPosition; // Ensure it resets perfectly
    }

    private IEnumerator ApplyCameraShake()
    {
        Vector3 originalPosition = cameraTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition; // Reset the camera position
    }

    public void OnRetryButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Character Selection Scene");
    }
}
