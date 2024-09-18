using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts_GameManager : MonoBehaviour
{
    //public Scripts_MusicManager musicManager;
    //private Scripts_MusicManager _musicManager;
    public GameObject musicManager;
    
    private Scripts_MusicManager _musicManagerScript;
    private AudioSource _musicSource;

    public float playerHealth;
    public Transform playerTransform;

    public int enemyAmount;


    // Start is called before the first frame update
    void Start()
    {
        _musicManagerScript = GetComponent<Scripts_MusicManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
