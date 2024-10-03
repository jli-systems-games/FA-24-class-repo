using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpawn : MonoBehaviour
{


    public GameObject goal;


    public float spawn;
    public List<Transform> spawnLocations = new();



    public GameObject firstPerson;
    public GameObject nextPerson;

    // Start is called before the first frame update
    void Start()
    {
        firstPerson.SetActive(true);
        //nextPerson.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PeopleFalls());

        }
    }

    public void PeopleFall()
    {
        //firstPerson.SetActive(false);
        //nextPerson.SetActive(true);

        //changes mesh so person fall through floor

        StartCoroutine(PeopleFalls());
    }


    private IEnumerator PeopleFalls()
    {
        yield return new WaitForSeconds(.5f);
        firstPerson.SetActive(false);
        //nextPerson.SetActive(true);

        StartCoroutine(PeopleSpawn());

        //if (goal.transform.position.y < -1f)
        //{

        //}


    }

    private IEnumerator PeopleSpawn()
    {
        yield return new WaitForSeconds(.8f);

        firstPerson.SetActive(true);
        //nextPerson.SetActive(false);

        goal.transform.Rotate(Vector3.up * Random.Range(1f, 360f));

        goal.transform.position = new Vector3(Random.Range(-3.8f, 1.1f), 9f, Random.Range(18f, 21f));
    }

}
