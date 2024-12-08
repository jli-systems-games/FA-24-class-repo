using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneDisplay : MonoBehaviour
{
    public TMP_Text displayText;

    public List<GameObject> scenes = new();
    public GameObject slider;

    //public PointerControler pointercontroler;

    // Start is called before the first frame update
    void Start()
    {
        displayText = displayText.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
