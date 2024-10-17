using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zTasting : MonoBehaviour
{
    public GameObject rock;
    public Vector3 targetPositionOne = new Vector3(200, 200, 0);
    public Vector3 targetPositionTwo = new(200, 200, 0);
    public Vector3 targetPositionThree = new(200, 200, 0);
    public float moveSpeed = 200f;
    private bool isMovingOne = false;
    private bool isMovingTwo = false;
    private bool isMovingThree = false;

    z_SceneChange zs;

    AudioSource audioSource;
    public AudioClip grind;
    public AudioClip munch;
    public AudioClip slurp;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingOne)
        {
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, targetPositionOne, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(rock.transform.position, targetPositionOne) < 0.01f)
            {
                isMovingOne = false;
                audioSource.PlayOneShot(munch);
                StartCoroutine(zContinueTasting());
            }
        }

        if (isMovingTwo)
        {
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, targetPositionTwo, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(rock.transform.position, targetPositionTwo) < 0.01f)
            {
                isMovingTwo = false;
                audioSource.PlayOneShot(munch);
                StartCoroutine(zFinishTasting());
            }
        }

        if (isMovingThree)
        {
            rock.transform.position = Vector3.MoveTowards(rock.transform.position, targetPositionThree, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(rock.transform.position, targetPositionThree) < 0.01f)
            {
                isMovingThree = false;

            }
        }
    }

    public void zBeginTasting()
    {
        audioSource.PlayOneShot(grind);
        Debug.Log("click");
        isMovingOne = true; // Start the movement
    }

    private IEnumerator zContinueTasting()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(grind);
        isMovingTwo = true;
    }

    private IEnumerator zFinishTasting()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(grind);
        isMovingThree = true; // Now starting third movement
    }

}
