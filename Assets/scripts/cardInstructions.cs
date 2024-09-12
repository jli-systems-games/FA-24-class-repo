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

    public GameObject cardSpread;

    public GameObject gameCanvas;
    public GameObject transitionCanvas;

    public Sprite cardBack;
    /*
    public Sprite tenClub;
    public Sprite queenDia;
    public Sprite threeSpade;
    public Sprite sevenHeart;
    public Sprite nineDia;
    public Sprite fourClub;
    public Sprite kingHeart;
    public Sprite aceSpade;
    public Sprite eightSpade;
    public Sprite fiveHeart;
    public Sprite jackClub;
    public Sprite twoDia;
    */

    public Button tenClubB;
    public Button queenDiaB;
    public Button threeSpadeB;
    public Button sevenHeartB;
    public Button nineDiaB;
    public Button fourClubB;
    public Button kingHeartB;
    public Button aceSpadeB;
    public Button eightSpadeB;
    public Button fiveHeartB;
    public Button jackClubB;
    public Button twoDiaB;

    void Start()
    {
        StartCoroutine(delayText());
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

    private IEnumerator delayText()
    {
        yield return new WaitForSeconds(1);
        randomCard = cards[Random.Range(0, cards.Length)];
        ChangeCardInstructions();
        yield return new WaitForSeconds(1);
        text.text = " ";
        StartCoroutine(cardDisplay());
    }

    private IEnumerator cardDisplay()
    {
        yield return new WaitForSeconds(1);
        tenClubB.image.sprite = cardBack;
        queenDiaB.image.sprite = cardBack;
        threeSpadeB.image.sprite = cardBack;
        sevenHeartB.image.sprite = cardBack;
        nineDiaB.image.sprite = cardBack;
        fourClubB.image.sprite = cardBack;
        kingHeartB.image.sprite = cardBack;
        aceSpadeB.image.sprite = cardBack;
        eightSpadeB.image.sprite = cardBack;
        fiveHeartB.image.sprite = cardBack;
        jackClubB.image.sprite = cardBack;
        twoDiaB.image.sprite = cardBack;
       
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
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
                SceneManager.LoadScene("gameOver");
            }
        }
    }
}
