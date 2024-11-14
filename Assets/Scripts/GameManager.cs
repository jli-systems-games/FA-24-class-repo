using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Character playerCharacter; // Stores the selected player character
    public Character opponentCharacter; // Stores the selected opponent character

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set selected characters from the selection scene
    public void SetSelectedCharacters(Character player, Character opponent)
    {
        playerCharacter = player;
        opponentCharacter = opponent;
    }
}
