using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PigeonStates
{
    idle,
    regularBowels,
    diarrhea,
    dead
}
public class PigeonController : MonoBehaviour
{
    public int TeamNum;

    public int stomach, speed;

    public GameManager _gameManager;

    public PigeonStates pigeonState;

    public Pigeon_Stats_Base pigeonBase;

    private Transform poopPosition;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        pigeonState = PigeonStates.idle;
    }

    private void Update()
    {
        // have some sort of event that calls a function when Dead to play the dead animation
    }

    private IEnumerator Pooping()
    {
        if(pigeonState == PigeonStates.regularBowels)
        {
            poopPosition = gameObject.transform;
            Instantiate(pigeonBase.splatter, poopPosition);
        }
        yield return new WaitForSeconds(5);

        StartCoroutine(Pooping());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Team1Poop") && TeamNum == 2)
        {
            pigeonState = PigeonStates.dead;
        }

        if(other.gameObject.CompareTag("Team2Poop") && TeamNum == 1)
        {
            pigeonState = PigeonStates.dead;
        }
    }
}
