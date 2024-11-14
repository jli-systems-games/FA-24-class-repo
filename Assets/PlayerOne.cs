using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    public GameObject Player1;

    public int p1_hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        p1_hp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (p1_hp <= 0)
        {
            Player1.SetActive(false);
        }
    }
}
