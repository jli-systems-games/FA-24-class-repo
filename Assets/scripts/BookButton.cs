using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BookButton : MonoBehaviour
{
    public Transform yourBook;
    public MatchingManager match;
    Image img;

    //string ClickedButtonName;
    Vector3 OGposition;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        OGposition = yourBook.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("original" + OGposition);
        Debug.Log(yourBook.position);
        if (match.ClickedButtonName != gameObject.name )
        {
            yourBook.position = OGposition;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        }
        Debug.Log(match.userInput);
    }

    public void pullingUpBook()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);

        yourBook.position = new Vector3(yourBook.position.x, yourBook.position.y + 100f, 0f);
        match.userInput = yourBook.gameObject;
        match.ClickedButtonName = EventSystem.current.currentSelectedGameObject.name;
    }
    private void OnDisable()
    {
        yourBook.position = OGposition;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }
}
