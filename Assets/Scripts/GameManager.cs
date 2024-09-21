using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Idle, Squeeze, Shake, Smash
}
public class GameManager : MonoBehaviour
{
    public GameStates gameState;

    private Player _player;

    public int fidgets;
    public int beatenStage;

    [SerializeField] private GameObject _doll;
    [SerializeField] private GameObject _dollTrigger;
    private SpriteRenderer _dollSprite;

    //sprites
    public Sprite[] squeezedSprite;
    public Sprite[] smashedSprite;
    public Sprite[] shakenSprite;
    public Sprite[] idleSprite;

    //hand sprites
    public Sprite fist;
    public Sprite hand;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _dollSprite = _doll.GetComponent<SpriteRenderer>();
        fidgets = 0;
        beatenStage = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState(GameStates state)
    {
        gameState = state;
        Debug.Log("State: " + gameState);

        if (gameState == GameStates.Squeeze) 
        {
            StartCoroutine(Squeezed());
        }

        if (gameState == GameStates.Shake)
        {
            StartCoroutine(Shaken());
        }

        if(gameState == GameStates.Smash)
        {
            StartCoroutine(Smashed());
        }

        if(gameState == GameStates.Idle)
        {
            _dollSprite.sprite = idleSprite[beatenStage];
        }

        else
        {
            _player.spokeToManager = true;
            fidgets++;
        }

        if (fidgets < 5)
        {
            beatenStage = 0;
        }

        else if (fidgets < 10)
        {
            beatenStage = 1;
        }

        else if (fidgets < 20)
        {
            beatenStage = 2;
        }
        else if (fidgets > 30) 
        {
            beatenStage = 3;
        }
    }

    public IEnumerator Squeezed()
    {
        _dollSprite.sprite = squeezedSprite[beatenStage];
        _player.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        _player.spokeToManager = false;
        _player.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        ChangeState(GameStates.Idle);
    }

    public IEnumerator Shaken()
    {
        _doll.transform.parent = _player.gameObject.transform;
        _dollSprite.sprite = shakenSprite[beatenStage];
        //_doll.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(2f);

        _doll.transform.parent = _dollTrigger.transform;
        _doll.transform.position = Vector3.zero;
        _doll.transform.rotation = Quaternion.identity;
        //_doll.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        _player.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        _player.spokeToManager = false;
        ChangeState(GameStates.Idle);
    }

    public IEnumerator Smashed()
    {
        _dollSprite.sprite = smashedSprite[beatenStage];
        _player.gameObject.GetComponent<SpriteRenderer>().sprite = fist;
        _player.gameObject.GetComponent<Animator>().enabled = true;
        _player.gameObject.GetComponent<Animator>().Play("fist-smash");

        yield return new WaitForSeconds(.5f);

        _player.gameObject.GetComponent<Animator>().enabled = false;
        _player.gameObject.GetComponent<SpriteRenderer>().sprite = hand;
        _player.spokeToManager = false;
        ChangeState(GameStates.Idle);
    }
}
