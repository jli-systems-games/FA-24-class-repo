using UnityEngine;
using UnityEngine.SceneManagement; 

public class RestartGame : MonoBehaviour
{
    private float holdTime = 0f; 
    private float requiredHoldTime = 1.95f;
    private Animator animator;
    private GunsmithData gunsmithData;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gunsmithData = FindObjectOfType<GunsmithData>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Trigger");
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetTrigger("UnTrigger");
            holdTime = 0f;
        }

        if (Input.GetKey(KeyCode.R))
        {
            
            holdTime += Time.deltaTime; 

            if (holdTime >= requiredHoldTime)
            {
                SceneManager.LoadScene("Gunmaker");
                Destroy(gunsmithData.gameObject);                
            }
        }
    }
}
