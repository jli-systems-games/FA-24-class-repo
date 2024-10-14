using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    public List<GameObject> ui = new();

    private void Update()
    {
        Off();
    }

    void Off()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            foreach (GameObject element in ui)
            {
                if (element.activeSelf)
                {
                    element.SetActive(false);
                }
                else
                {
                    element.SetActive(true);
                }

            }
        }
    }
}
