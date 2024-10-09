using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndertoneSwitch : MonoBehaviour
{
    public Image undertoneImage;
    public Sprite gold;
    public Sprite silver;
    public Sprite orange;
    public Sprite blue;

    private bool isGold = true;
    private bool isOrange = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchGoldSilver()
    {
        if (isGold)
        {
            // Switch to sprite2
            undertoneImage.sprite = silver;
        }
        else
        {
            undertoneImage.sprite = gold;
        }

        isGold = !isGold;
    }

    public void SwitchOrangeBlue()
    {
        if (isOrange)
        {
            undertoneImage.sprite = blue;
        }
        else
        {
            undertoneImage.sprite = orange;
        }

        isOrange = !isOrange;
    }
}
