using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPreview : MonoBehaviour
{
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI defenseText;
    public Image characterFullImage;

    private readonly Vector3 fixedScale = new Vector3(3.2f, 3.2f, 3.2f);

    // Method to show a specific character's preview
    public void ShowCharacterPreview(Character character, bool isOpponent = false, bool flipSprite = false)
    {
        characterNameText.text = character.characterName;
        healthText.text = "Health: " + character.health;
        attackPowerText.text = "Attack Power: " + character.attackPower;
        defenseText.text = "Defense: " + character.defense;
        characterFullImage.sprite = character.characterIcon;

        RectTransform imageRect = characterFullImage.GetComponent<RectTransform>();
        imageRect.sizeDelta = new Vector2(character.displayWidth, character.displayHeight);
        imageRect.localScale = new Vector3((flipSprite ? -1 : 1) * fixedScale.x, fixedScale.y, fixedScale.z);

        // Apply position offset based on player or opponent
        imageRect.anchoredPosition = isOpponent ? character.opponentPreviewPositionOffset : character.playerPreviewPositionOffset;

        gameObject.SetActive(true); // Show the preview panel with character info
    }

    // Method to hide the character preview
    public void HideCharacterPreview()
    {
        gameObject.SetActive(false);
    }
}
