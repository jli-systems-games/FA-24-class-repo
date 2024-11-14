using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject defaultPreviewPanel; // Reference to the default placeholder panel
    public CharacterPreview playerPreview; // Reference to player’s preview component
    public CharacterPreview opponentPreview; // Reference to opponent's preview component
    public List<Character> allCharacters; // List of available characters

    // Button references for each character type
    public Button meleeButton;
    public Button rangedButton;
    public Button magicButton;
    public Button continueButton; // Reference to the Continue button

    private Character selectedCharacter; // Track the selected player character
    private Character opponentCharacter; // Track the randomly selected opponent character
    private CharacterType opponentCharacterType; // Track the opponent's character type
    private Button currentlyDisabledButton; // Track the currently disabled button
    private bool isHovering = false; // Track whether the player is currently hovering over any button

    void Start()
    {
        // Randomly pick a character for the opponent at the start
        opponentCharacter = allCharacters[Random.Range(0, allCharacters.Count)];
        ShowOpponentCharacter(opponentCharacter);

        // Store the opponent's character type and disable the corresponding button
        opponentCharacterType = opponentCharacter.characterType;
        DisableCharacterTypeButton(opponentCharacterType);

        // Show the default preview panel initially
        ShowDefaultPlayerPreview();

        // Initially disable the Continue button
        continueButton.interactable = false;

        // Link the Continue button to the ConfirmSelection method
        continueButton.onClick.AddListener(ConfirmSelection);
    }

    // Method to show the default preview panel
    public void ShowDefaultPlayerPreview()
    {
        if (selectedCharacter == null) // Only show default if no character has been selected
        {
            playerPreview.HideCharacterPreview(); // Hide any active character preview
            defaultPreviewPanel.SetActive(true); // Show the default preview panel
        }
    }

    // Called when hovering over a character button
    public void OnCharacterHover(Character character)
    {
        isHovering = true; // Player is hovering over a character button

        // Only show the preview if the character type is different from the opponent's
        if (character.characterType != opponentCharacterType)
        {
            defaultPreviewPanel.SetActive(false);
            playerPreview.ShowCharacterPreview(character, isOpponent: false, flipSprite: true); // Player faces right
        }
    }

    // Called when hover exits
    public void OnCharacterHoverExit()
    {
        isHovering = false; // Player is no longer hovering over any character button

        // If a character has been selected, show its preview; otherwise, show default
        if (selectedCharacter != null && !isHovering)
        {
            playerPreview.ShowCharacterPreview(selectedCharacter, isOpponent: false, flipSprite: true); // Player faces right
        }
        else if (selectedCharacter == null)
        {
            ShowDefaultPlayerPreview();
        }
    }

    // Called when a character is selected by the player
    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;

        // Show the selected character's preview and hide the default panel
        defaultPreviewPanel.SetActive(false);
        playerPreview.ShowCharacterPreview(character, isOpponent: false, flipSprite: true); // Player faces right

        // Enable the Continue button since a character has been selected
        continueButton.interactable = true;

        // Re-enable the previously disabled button if any
        if (currentlyDisabledButton != null)
        {
            currentlyDisabledButton.interactable = true;
        }

        // Disable the newly selected character's button
        currentlyDisabledButton = GetButtonForCharacterType(character.characterType);
        currentlyDisabledButton.interactable = false;
    }

    // Method to confirm character selection and store it in GameManager
    public void ConfirmSelection()
    {
        // Store the selected characters in the GameManager
        GameManager.Instance.SetSelectedCharacters(selectedCharacter, opponentCharacter);
    }

    // Show the randomly selected opponent character
    private void ShowOpponentCharacter(Character character)
    {
        opponentPreview.ShowCharacterPreview(character, isOpponent: true, flipSprite: false); // Opponent faces left
    }

    // Method to get the button for a specific character type
    private Button GetButtonForCharacterType(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Melee:
                return meleeButton;
            case CharacterType.Ranged:
                return rangedButton;
            case CharacterType.Magic:
                return magicButton;
            default:
                return null;
        }
    }

    // Method to disable the button for a specific character type
    private void DisableCharacterTypeButton(CharacterType characterType)
    {
        Button button = GetButtonForCharacterType(characterType);
        if (button != null)
        {
            button.interactable = false;
        }
    }
}
