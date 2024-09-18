using UnityEngine;

public class keyPressed : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found");
        }
    }

    void Update()
    {
        KeyPressed();
    }

    void KeyPressed()
    {
      
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Debug.Log("IsClicked_M");
            animator.SetTrigger("M_Pressed");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //Debug.Log("IsClicked_K");
            animator.SetTrigger("K_Pressed");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //Debug.Log("IsClicked_L");
            animator.SetTrigger("L_Pressed");
        }
    }
}