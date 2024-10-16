using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Overworld,
    PetManager
}
public class GameManager : MonoBehaviour
{
    public Pet_AI petAI;
    public Player player;
    public Rock[] rocks;

    public GameObject mainCamera;

    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        rocks = FindObjectsOfType<Rock>();
        changeState(GameState.PetManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeState(GameState state)
    {
        gameState = state;
        if (state == GameState.Overworld)
        {
            setUpOverworld();
        }

        if (state == GameState.PetManager) 
        {
            setUpPetManager();
        }
    }

    void setUpOverworld()
    {
        Debug.Log("setting up overworld");
    }

    void setUpPetManager()
    {
        petAI.gameObject.transform.position = Vector3.zero;
        mainCamera.GetComponent<Camera>().orthographicSize = 2;
        Debug.Log("setting up pet manager");
    }

    public void addToInventory(Inventory item)
    {
        player.updateInventory(item);
    }

    public void hitRock(GameObject rockObj)
    {
        petAI.calculateDamage();
        rockObj.GetComponent<Rock>().takeDamage(petAI.damage);
    }
}
