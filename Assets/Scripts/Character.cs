using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public int health;
    public int attackPower;
    public int defense;
    public Sprite characterIcon; // Icon for UI display
    // Public fields to allow editing display size in the Inspector
    public float displayWidth = 100f;  // Default width for display
    public float displayHeight = 100f; // Default height for display
    // Position offsets for preview customization
    public Vector2 playerPreviewPositionOffset = Vector2.zero; // Offset for player side
    public Vector2 opponentPreviewPositionOffset = Vector2.zero; // Offset for opponent side

    //For Battle Scene!
    public GameObject faceIconPrefab; // Prefab Icon for UI display next to the health bar
    public GameObject characterPrefab; // Reference to the character prefab with animations
    public Vector3 playerPositionOffset; // Offset for player's position
    public Vector3 opponentPositionOffset; // Offset for opponent's position

    public CharacterType characterType; // Enum for melee, ranged, magic
}

public enum CharacterType { Melee, Ranged, Magic }
