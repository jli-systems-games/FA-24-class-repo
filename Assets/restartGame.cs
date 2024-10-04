using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
    
public class restartGame : MonoBehaviour
{
    public void replay()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

}