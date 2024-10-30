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

    public Animator petTransformAnimator;

    public GameObject mainCamera;
    public GameObject petCamera;

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
        mainCamera.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -6.592003f);
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
        mainCamera.SetActive(true);
        petCamera.SetActive(false);

        petAI.mouseLoc.SetActive(false);
        petAI.gameObject.transform.rotation = petAI.ogRotation;

        for (int i = 0; i < dragScript.Length; i++)
        {
            dragScript[i].isDraggable = false;
        }
        Debug.Log("setting up overworld");
    }

    void setUpPetManager()
    {
        petAI.mouseLoc.SetActive(true);
        mainCamera.SetActive(false);
        petCamera.SetActive(true);

        petAI.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator Flying()
    {
        petTransformAnimator.Play("flying");

        Debug.Log("running");

        yield return new WaitForSeconds(.67f);
        petAI.targetPos.position = new Vector3(0, 41.4f, 1.5f);
        player.flying = true;
        Debug.Log("player flies");
    }
}
