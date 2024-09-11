using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class cardInstructions : MonoBehaviour
{

    public string[] cards;
    public string randomCard;
    public TextMeshProUGUI text;

    void Start()
    {
        randomCard = cards[Random.Range(0, cards.Length)];
        ChangeCardInstructions();
    }

    public void ChangeCardInstructions() {
        text.text = "click the " + randomCard;
    }

    void Update()
    {
        Debug.Log(randomCard);
    }
}
