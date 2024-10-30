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
        mainCamera.GetComponent<Transform>().position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -8.476f);
    }

    #region states
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

        petAI.animator.Play("Bounce");

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
        petAI.animator.Play("Idle_A");

        for (int i = 0; i < dragScript.Length; i++)
        {
            dragScript[i].isDraggable = true;
        }
        Debug.Log("setting up pet manager");
    }

    #endregion

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

    #region flying
    public IEnumerator Flying()
    {
        petAI.animator.Play("Fly");
        petTransformAnimator.Play("flying");
        player.flying = true;

        Debug.Log("running");

        yield return new WaitForSeconds(.67f);

       
        //while(player.flying == true)
        //{
        petAI.flying = true;
        petAI.targetPos.position = new Vector3(0, 41.4f, 1.5f);
        //}
        Debug.Log("player flies");
    }

    public void StopFlying()
    {
        petAI.animator.Play("Bounce");
        //Debug.Log("hello");
        petAI.targetPos.position = new Vector3(0, .3f, -24.2f);
        petAI.flying = false;
    }
    #endregion

    #region petting
    public void BeginPetting()
    {
        StartCoroutine(petAI.increaseConfidence());
        petAI.animator.Play("Eyes_Happy");
        petAI.animator.Play("Roll");
    }

    public void IncrementPets()
    {
        petAI.pets = petAI.pets + .1f;
    }

    public void StopPetting()
    {
        StopCoroutine(petAI.increaseConfidence());
        petAI.animator.Play("Eyes_Blink");
        petAI.animator.Play("Idle_A");
    }
    #endregion
}
