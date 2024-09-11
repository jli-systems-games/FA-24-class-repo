using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class cardInstructions : MonoBehaviour
{

    public string[] cards;
    public string randomCard;
    public TextMeshProUGUI text;

    public GameObject win;
    public GameObject lose;

    public GameObject cardSpread;

    public GameObject gameCanvas;
    public GameObject transitionCanvas;

    void Start()
    {
        randomCard = cards[Random.Range(0, cards.Length)];
        ChangeCardInstructions();
    }

    public void ChangeCardInstructions() {
        text.text = "click the " + randomCard;
    }

    private IEnumerator pretransition()
    {
        yield return new WaitForSeconds(1);
        transitionCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
    }

    public void CheckCard()
    {
        string ClickedCardName = EventSystem.current.currentSelectedGameObject.name;

        if (randomCard == "ten of clubs")
        {
            if (ClickedCardName == "ten of clubs")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "queen of diamonds")
        {
            if (ClickedCardName == "queen of diamonds")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "three of spades")
        {
            if (ClickedCardName == "three of spades")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "seven of hearts")
        {
            if (ClickedCardName == "seven of hearts")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "nine of diamonds")
        {
            if (ClickedCardName == "nine of diamonds")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "king of hearts")
        {
            if (ClickedCardName == "king of hearts")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "four of clubs")
        {
            if (ClickedCardName == "four of clubs")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "ace of spades")
        {
            if (ClickedCardName == "ace of spades")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "eight of spades")
        {
            if (ClickedCardName == "eight of spades")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
            }
        }

        if (randomCard == "five of hearts")
        {
            if (ClickedCardName == "five of hearts")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "two of diamonds")
        {
            if (ClickedCardName == "two of diamonds")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());

            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }

        if (randomCard == "jack of clubs")
        {
            if (ClickedCardName == "jack of clubs")
            {
                win.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
                StartCoroutine(pretransition());
            }

            else
            {
                Debug.Log("incorrect");
                lose.gameObject.SetActive(true);
                cardSpread.gameObject.SetActive(false);
            }
        }
    }
}
