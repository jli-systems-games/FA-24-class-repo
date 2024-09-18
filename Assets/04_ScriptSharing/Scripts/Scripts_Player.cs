using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_Player : MonoBehaviour
{
    private Scripts_MusicManager _musicManager;
    private Scripts_GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<Scripts_GameManager>();

        GameObject tempGM = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
