using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RollAnimation : MonoBehaviour
{
    public TextMeshProUGUI numberDisplay;
    public float animationDuration = 1.0f;
    public int finalValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartRollAnimation(int value)
    {
        finalValue = value;
        StartCoroutine(AnimateRoll());
    }

    private IEnumerator AnimateRoll()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            int randomValue = Random.Range(1, finalValue + 10);
            numberDisplay.text = randomValue.ToString();

            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }

        numberDisplay.text = finalValue.ToString();
    }
}
