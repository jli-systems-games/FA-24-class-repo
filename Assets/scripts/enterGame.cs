using UnityEngine;
using UnityEngine.SceneManagement; 

public class enterGame : MonoBehaviour
{
    //private Animator animator;

    public string Game;

    void Start()
    {

       /* animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found");
        }*/
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.M))
        {
           
            LoadGameScene();
        }

    }


        void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(Game))
        {
           // animator.SetTrigger("M_Pressed");
            SceneManager.LoadScene(Game);
            
        }
        else
        {
            Debug.LogWarning("场景名称未设置！");
        }
    }
}
