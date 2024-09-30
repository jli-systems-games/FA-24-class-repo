using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEXT : MonoBehaviour
{
    [SerializeField]
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToChat("you pressed space");
            Debug.Log("space");
        }
    }

    public void SendMessageToChat(string text)
    {
        Message newMessage = new Message();

        //newMessage.text = text;

        //GameObject newText = Instantiate(textObject, chatPanel.transform);

        //newMessage.textObject = newText.GetComponent<Text>();

        //newMessage.textObject.text = newMessage.text;

        newMessage.text = text;

        messageList.Add(newMessage);
    }
}

//[System.Serializable]

//public class Message
//{
//    public string text;
//    public Text textObject;
//}