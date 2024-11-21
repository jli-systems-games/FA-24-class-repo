using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMatch : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") > 5 || Input.GetAxis("Mouse X") < -5) { 
            fire.SetActive(true); 
           _gameManager.ChangeState(GameState.burning); }
        Debug.Log(Input.GetAxis("Mouse X"));
    }
}
