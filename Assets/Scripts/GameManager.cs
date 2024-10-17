using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Item_Drag[] dragScript;

    public GameObject mainCamera;

    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        rocks = FindObjectsOfType<Rock>();
        dragScript = FindObjectsOfType<Item_Drag>();
        //changeState(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeState(int state)
    {
        if (state == 0)
        {
            gameState = GameState.Overworld;
            setUpOverworld();
        }
        else if (state == 1) 
        {
            gameState = GameState.PetManager;
            setUpPetManager();
        }
    }

    void setUpOverworld()
    {
        mainCamera.GetComponent<Camera>().orthographicSize = 5;
        for (int i = 0; i < dragScript.Length; i++)
        {
            dragScript[i].isDraggable = false;
        }
        Debug.Log("setting up overworld");
    }

    void setUpPetManager()
    {
        petAI.gameObject.transform.position = Vector3.zero;
        mainCamera.GetComponent<Camera>().orthographicSize = 2;
        for (int i = 0; i < dragScript.Length; i++)
        {
            dragScript[i].isDraggable = true;
        }
        Debug.Log("setting up pet manager");
    }

    public void addToInventory(Inventory item)
    {
        player.updateInventory(item);
    }

    public void updateStats(Inventory item)
    {
        if(item == Inventory.Food)
        {
            petAI.AddFood();
        }

        else if(item == Inventory.Equipment)
        {
            petAI.AddStrength();
        }
    }

    public void hitRock(GameObject rockObj)
    {
        petAI.calculateDamage();
        rockObj.GetComponent<Rock>().takeDamage(petAI.damage);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
