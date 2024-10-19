using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zTasting : MonoBehaviour
{
    public List<GameObject> buttons = new();

    public GameObject rock;
    public GameObject emptyPlate;
    public GameObject emptyBowl;

    public List<GameObject> foods = new();
    public List<GameObject> waters = new();



    public Vector3 targetPositionOne = new();
    public Vector3 targetPositionTwo = new();
    public Vector3 targetPositionThree = new();
    public float moveSpeed = 100f;
    private bool isMovingOne = false;
    private bool isMovingTwo = false;
    private bool isMovingThree = false;

    public AudioSource audioSourceG;
    public AudioSource audioSource;

    public AudioClip grind;
    public AudioClip munch;
    public AudioClip slurp;
    public AudioClip boom;

    // Start is called before the first frame update
    void Start()
    {
        isMovingOne = false;
    }

    public void zButtonClose()
    {
        foreach (GameObject item in buttons)
        {
            item.SetActive(false);
        }
    }

    public void zBeginTasting()
    {
        audioSourceG.Play();

        Debug.Log("click");

        isMovingOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingOne)
        {
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, targetPositionOne, moveSpeed * Time.deltaTime);

            if (rock.transform.position == targetPositionOne)
            {
                audioSourceG.Stop();
                isMovingOne = false;
                StartCoroutine(ptTwo());
            }
        }

        if (isMovingTwo)
        {
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, targetPositionTwo, moveSpeed * Time.deltaTime);

            if (rock.transform.position == targetPositionTwo)
            {
                audioSourceG.Stop();
                isMovingTwo = false;

                StartCoroutine(ptThree());
            }
        }

        if (isMovingThree)
        {
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, targetPositionThree, moveSpeed * Time.deltaTime);

            if (rock.transform.position == targetPositionThree)
            {
                audioSourceG.Stop();
                isMovingThree = false;

                StartCoroutine(ptFinal());
            }
        }
    }

    private IEnumerator ptTwo()
    {
        audioSource.PlayOneShot(munch);
        foreach (GameObject item in foods)
        {
            item.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);

        isMovingTwo = true;
        audioSourceG.Play();
    }

    private IEnumerator ptThree()
    {
        audioSource.PlayOneShot(slurp);
        foreach (GameObject item in waters)
        {
            item.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);

        isMovingThree = true;
        audioSourceG.Play();
    }

    private IEnumerator ptFinal()
    {
        audioSource.PlayOneShot(boom);
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("4Reveal");
    }
}
