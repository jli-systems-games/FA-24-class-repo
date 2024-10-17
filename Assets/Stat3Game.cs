using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stat3Game : MonoBehaviour
{
    private int veggiesPlaced = 0;
    public int totalVeggies = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddVeggie()
    {
        veggiesPlaced++;
        if (veggiesPlaced >= totalVeggies)
        {
            CompleteMiniGame();
        }
    }

    private void CompleteMiniGame()
    {
        if (PetManager.Instance == null)
        {
            Debug.LogError("PetManager.Instance null");
            return;
        }
        PetManager.Instance.CompleteMiniGame3();
    }
}
