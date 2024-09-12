using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private GameObject _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager");
        Destroy(_gameManager);
            //StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
