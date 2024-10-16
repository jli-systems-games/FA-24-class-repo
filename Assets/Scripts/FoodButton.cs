using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodButton : MonoBehaviour
{
   public SpawnFood spawnFood;
    public Animator animator;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        animator.SetTrigger("Trigger");
        spawnFood.SpawnSlimeFood();
    }
}
