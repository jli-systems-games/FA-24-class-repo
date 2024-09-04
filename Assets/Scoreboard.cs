using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TMP_Text counterText;
    private int counter = 0;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //if (collision.gameObject.CompareTag("ball"))
        //{
        //    counter++; 
        //    UpdateCounterText();
        //    Debug.Log("Counter updated to: " + counter);
        //}
    }

    private void UpdateCounterText()
    {
        counterText.text = "Count: " + counter.ToString();
        
    }
}
