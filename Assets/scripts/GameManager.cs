using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<BuckController> controllers = new List<BuckController>();

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    public void RegisterController(BuckController controller)
    {
        if (!controllers.Contains(controller))
        {
            controllers.Add(controller);
        }
    }


    public void UnregisterController(BuckController controller)
    {
        if (controllers.Contains(controller))
        {
            controllers.Remove(controller);
        }
    }

    public void CheckGameOver()
    {
        foreach (var controller in controllers)
        {
            if (controller.isActive)
            {
                return; 
            }
        }

        GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over! No active controllers left.");
        
    }
}
