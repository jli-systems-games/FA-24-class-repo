using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Texting : MonoBehaviour
{
    public GameObject chatPanel, textObject;

    public bool Taken = true;

    private AudioSource audioSource;
    public AudioClip Boop;

    [SerializeField]
    List<string> MessageSet1 = new List<string>
    {
        "yoyoyo",
        "picnic tiiiime?",
        "the weather is so nice",
        "we picked a good day for this",
        "lololl",
        "ummm",
        "can u unpack our food first",
        "im running a little late rn",
        "ill be there in like",
        "a jiffy",
        "or smth idk",
        "o and also",
        "separate the fruits!!!",
        "pleeaaasseeeee",
        "im not eating no",
        "'natures sugars'",
        "not again",
        ">:)",
        "ill msg u again when i get closer",
    };

    [SerializeField]

    List<string> MessageSet2 = new List<string>
    {
        "hows it goingg??",
        "did u finish yett",
        "hehehe im so exciteddd",
        "omg btw so like",
        "i was walking my fish yesterday",
        "and like",
        "apparently",
        "theres a flying fish update??",
        "'v500.000.000.20.24:'",
        "'NEW Flying Fish:'",
        "'LEGENDARY evolution.'",
        "'Is it a bird? Is it a plane?'",
        "'Is it a... FISH??'",
        //insert patch notes type comments about flying fish
        "like what does that even mean",
        "anywayss",
        "can u take a pic of the view",
        "when uve got a sec",
    };

    [SerializeField]

    List<string> MessageSet3 = new List<string>
    {
        "...",
        "daamn bruh",
        "the sky is so pretty",
        "o waiiit",
        "i think i see uuuu",
        "making my way overr",
        "see u in just a sec",
        ":)",
    };

    public float messageInterval = 2.0f;

    private bool isSendingMessages = false;

    private bool isSendingMessages1 = false;
    private bool isSendingMessages2 = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(SendMessageSet1());
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SendMessageToChat("you pressed space");
        //    Debug.Log("space");
        //}
    }

    public void SendMessageToChat(string text)
    {
        Message newMessage = new Message();

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponentInChildren<TextMeshProUGUI>();

        newMessage.text = text;

        newMessage.textObject.text = newMessage.text;


        //messageList.Add(newMessage);

        //if (messageList.Count >= maxMessages)
        //{
        //    Destroy(messageList[0].textObject.gameObject);
        //    messageList.Remove(messageList[0]);
        //}
    }

    private IEnumerator SendMessageSet1()
    {
        isSendingMessages = true;

        while (isSendingMessages)
        {
            foreach (string message in MessageSet1)
            {
                SendMessageToChat(message);
                audioSource.PlayOneShot(Boop);

                yield return new WaitForSeconds(messageInterval);
            }

            isSendingMessages = false;
            StartCoroutine(AfterMessageSet1());

        }
    }

    private IEnumerator AfterMessageSet1()
    {
        yield return new WaitForSeconds(20.0f);
        MPart2();
    }

    public void MPart2()
    {
        StartCoroutine(SendMessageSet2());
    }

    private IEnumerator SendMessageSet2()
    {
        isSendingMessages = true;

        while (isSendingMessages)
        {
            foreach (string message in MessageSet2)
            {
                SendMessageToChat(message);
                audioSource.PlayOneShot(Boop);

                yield return new WaitForSeconds(messageInterval);
            }

            isSendingMessages = false;
        }
    }

    public void MPart3()
    {
        StartCoroutine(SendMessageSet3());
    }

    private IEnumerator SendMessageSet3()
    {
        isSendingMessages = true;

        while (isSendingMessages)
        {
            foreach (string message in MessageSet3)
            {
                SendMessageToChat(message);
                audioSource.PlayOneShot(Boop);

                yield return new WaitForSeconds(messageInterval);
            }

            isSendingMessages = false;
        }
    }

    //public void StopSendingMessages()
    //{
    //    isSendingMessages = false;
    //}
}

[System.Serializable]

public class Message
{
    public string text;
    public TextMeshProUGUI textObject;
}