using System.Collections;
using UnityEngine;

public class HungerTimer : MonoBehaviour
{
    [SerializeField] private AnimationController animationController; // Reference to the AnimationController

    public float decreaseInterval = 1f; // Time interval to decrease hunger (in seconds)
    public int hungerDecreaseAmount = 1; // Amount of hunger to decrease

    private void Start()
    {
        // Start the coroutine to decrease hunger over time
        StartCoroutine(DecreaseHungerOverTime());
    }

    private IEnumerator DecreaseHungerOverTime()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(decreaseInterval);

            // Decrease hunger in the AnimationController
            animationController.DecreaseHunger();
        }
    }
}
