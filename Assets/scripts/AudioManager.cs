using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    public List<AudioClip> typeSoundList = new List<AudioClip>(); 
    private AudioSource audioSource;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; 
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            PlayRandomTypeSound();
        }
    }

    public void PlayRandomTypeSound()
    {
        if (typeSoundList.Count > 0)
        {
            
            int randomIndex = Random.Range(0, typeSoundList.Count);
            audioSource.clip = typeSoundList[randomIndex];
            audioSource.Play(); 
        }
        else
        {
            Debug.LogError("typeSoundList 为空！");
        }
    }
}
