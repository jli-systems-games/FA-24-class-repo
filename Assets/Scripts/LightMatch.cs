using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightMatch : MonoBehaviour
{
    private GameManager _gameManager;
    public Camera mainCamera;

    public GameObject fire;

    private Ray ray;
    private RaycastHit hit;

    public GameObject matchPrefab;
    private Vector3 initialPos;

    public float throwForce;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        fire.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") > 5 || Input.GetAxis("Mouse X") < -5) { 
            if (GameManager.state == GameState.matchOut)
            {
                audioSource.Play();
            }
            fire.SetActive(true); 
           _gameManager.ChangeState(GameState.burning); }
        Debug.Log(Input.GetAxis("Mouse X"));

        if (GameManager.state == GameState.burning)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                ThrowMatch();
            }
        }
    }

    void ThrowMatch()
    {
       

        if (Physics.Raycast(ray, out hit))
        {
            initialPos = transform.position;

            GameObject newMatch = Instantiate(matchPrefab, initialPos, Quaternion.identity);
            newMatch.GetComponent<Rigidbody>().AddForce(ray.direction * throwForce, ForceMode.Impulse);

            fire.SetActive(false);

            StartCoroutine(Burnout(newMatch));

            _gameManager.thrownMatches.Add(newMatch);
            _gameManager.CheckMatchLimit();
            _gameManager.ChangeState(GameState.moveable);
        }
    }

    public IEnumerator Burnout(GameObject newMatch)
    {
        yield return new WaitForSeconds(30f);

        newMatch.transform.Find("fire").gameObject.SetActive(false);
    }
}
