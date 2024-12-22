using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PointerControler : MonoBehaviour
{
    [Header("ITEMS")]
    //public GameObject button;
    public TMP_Text displayText;
    public RectTransform flyingShrimp;
    public GameObject slider;

    [Header("AUDIO")]
    private AudioSource audioSource;
    public AudioClip boing;
    public AudioClip smash;
    public AudioClip booing;
    public AudioClip cheering;

    [Header("LISTS")]
    private float moveSpeed;
    private Vector3 targetPosition;
    public RectTransform pointerTransform;

    public List<GameObject> scenes = new();
    public List<Transform> spots = new();
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;


    [Header("BOOLS")]

    private int result; //determinds type of ending
    private bool ending; //tells update were doing the ending

    private bool move;
    private bool runToPole;
    private bool setUp;
    private bool setUpTwo;
    private bool tooWeak;
    private bool tooStrong;
    private bool perfect;

    // Start is called before the first frame update
    void Start()
    {
        pointerTransform = pointerTransform.GetComponent<RectTransform>();
        flyingShrimp = flyingShrimp.GetComponent<RectTransform>();
        displayText = displayText.GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        //displayText.text = "";
        //targetPosition = pointB.position;

        move = false;

        foreach (GameObject item in scenes)
        {
            item.SetActive(false);
        }

        //button.SetActive(true);
        slider.SetActive(false);

        StartCoroutine(PV_Start());
        //look down
    }

    public void PoleVault()
    {
        StartCoroutine(PV_Start());

        //button.SetActive(false)
    }

    IEnumerator PV_Start()
    {
        displayText.text = "POLE VAULTING!\n\nthis is the closest ull ever get to flying, unless you get on a plane ig";
        yield return new WaitForSeconds(3f);

        displayText.text = "time pressing space to vault!";

        yield return new WaitForSeconds(5f);

        displayText.text = "START!";

        scenes[0].SetActive(true);

        runToPole = true;
        //shrimp carrying pole runs across screen
        targetPosition = spots[0].position;
        //look to update
    }

    IEnumerator PV_PlayScenes()
    {
        if (!runToPole)
        {
            displayText.text = "";

            yield return new WaitForSeconds(0.5f);
            //closeup shrimp to center
            targetPosition = spots[1].position;
            scenes[1].SetActive(true);
            setUp = true;
            yield return null;
            //look update
        }
    }

    IEnumerator PV_PlayScenesTwo()
    {
        if (!setUp)
        {
            yield return new WaitForSeconds(0.2f);
            //set up slider
            targetPosition = pointA.position;
            slider.SetActive(true);
            move = true;
            //look update
        }
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        if (runToPole)
        {
            moveSpeed = 2000f;

            RectTransform rectTransform = scenes[0].GetComponent<RectTransform>();
            Vector3 position = rectTransform.position; // Get the current position
            position.y = 380f + Mathf.Sin(Time.time * 50f) * 50; // Modify the y value
            rectTransform.position = position; // Assign the modified position back


            scenes[0].GetComponent<RectTransform>().position = Vector3.MoveTowards(scenes[0].GetComponent<RectTransform>().position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(scenes[0].GetComponent<RectTransform>().position, targetPosition) < 0.1f)
            {
                runToPole = false;
                StartCoroutine(PV_PlayScenes());
                //look up
            }
        }

        if (setUp)
        {
            moveSpeed = 5000f;

            RectTransform rectTransform = scenes[1].GetComponent<RectTransform>();
            Vector3 position = rectTransform.position; // Get the current position
            position.y = -23f + Mathf.Sin(Time.time * 100f) * 50; // Modify the y value
            rectTransform.position = position; // Assign the modified position back

            scenes[1].GetComponent<RectTransform>().position = Vector3.MoveTowards(scenes[1].GetComponent<RectTransform>().position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(scenes[1].GetComponent<RectTransform>().position, targetPosition) < 0.1f)
            {
                setUp = false;
                StartCoroutine(PV_PlayScenesTwo());
                //look up
            }
        }

        if (move)
        {
            moveSpeed = 3000f;

            pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if pointer reached pointA or pointB
            if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.01f)
            {
                targetPosition = pointB.position;
            }
            else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.01f)
            {
                targetPosition = pointA.position;
            }

            // Check for space key press to check success
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckSuccess();
                //look down
            }
        }

        if (setUpTwo)
        {
            moveSpeed = 5000f;

            RectTransform rectTransform = scenes[1].GetComponent<RectTransform>();
            Vector3 position = rectTransform.position; // Get the current position
            position.y = -23f + Mathf.Sin(Time.time * 100f) * 50; // Modify the y value
            rectTransform.position = position; // Assign the modified position back


            scenes[1].GetComponent<RectTransform>().position = Vector3.MoveTowards(scenes[1].GetComponent<RectTransform>().position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(scenes[1].GetComponent<RectTransform>().position, targetPosition) < 0.1f)
            {
                setUpTwo = false;
                StartCoroutine(PV_Result(result));
                //look down bottom
            }
        }

        if (ending)
        {
            Vector3 rotation = flyingShrimp.transform.eulerAngles;
            rotation.z -= 5f;

            // Modify the Z-axis rotation

            // Apply the new rotation
            flyingShrimp.transform.eulerAngles = rotation;

            flyingShrimp.position = Vector3.MoveTowards(flyingShrimp.position, targetPosition, moveSpeed * Time.deltaTime);

            if (tooWeak)
            {
                moveSpeed = 1000f;

                if (Vector3.Distance(flyingShrimp.position, spots[3].position) < 0.01f)
                {
                    targetPosition = spots[4].position;
                    audioSource.PlayOneShot(boing);
                    StartCoroutine(PV_TooWeak());
                }
                if (Vector3.Distance(flyingShrimp.position, spots[4].position) < 0.01f)
                {
                    tooWeak = false;
                }
            }

            if (tooStrong)
            {
                moveSpeed = 5000f;

                if (Vector3.Distance(flyingShrimp.position, targetPosition) < 0.01f)
                {
                    StartCoroutine(PV_TooStrong());

                    tooStrong = false;
                }
            }

            if (perfect)
            {
                moveSpeed = 2000f;

                if (Vector3.Distance(flyingShrimp.position, spots[6].position) < 0.01f)
                {
                    targetPosition = spots[7].position;
                }
                if (Vector3.Distance(flyingShrimp.position, spots[7].position) < 0.01f)
                {
                    StartCoroutine(PV_Perfect());

                    perfect = false;
                }

            }
        }
    }

    void CheckSuccess()
    {
        // Get the pointer's position in world space
        Vector3 pointerWorldPos = pointerTransform.position;

        // Check if the pointer is inside the safe zone (within the bounds of pointA and pointB)
        if (pointerWorldPos.x > pointC.position.x && pointerWorldPos.x < pointD.position.x)
        {
            result = 3; // Inside the safe zone
        }
        else if (pointerWorldPos.x < pointC.position.x)
        {
            result = 1; // Left of the safe zone
        }
        else if (pointerWorldPos.x > pointD.position.x)
        {
            result = 2; // Right of the safe zone
        }

        move = false;

        // Call the result method to handle the outcome based on the result value
        StartCoroutine(PV_PlayScenesThree());
        //look next
    }

    IEnumerator PV_PlayScenesThree()
    {
        yield return new WaitForSeconds(0.5f);
        slider.SetActive(false);
        setUpTwo = true;
        targetPosition = spots[2].position;
        //look update
    }

    IEnumerator PV_Result(int result)
    {
        if (!setUpTwo)
        {
            yield return new WaitForSeconds(0.5f);
            //the bar
            scenes[2].SetActive(true);

            if (result == 1)
            {
                //weak
                targetPosition = spots[3].position;
                tooWeak = true;
            }
            else if (result == 2)
            {
                //strong
                targetPosition = spots[5].position;
                tooStrong = true;
            }
            else if (result == 3)
            {
                //perfect
                targetPosition = spots[6].position;
                perfect = true;
            }
            ending = true;
            //look update
        }
    }

    IEnumerator PV_TooWeak()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(booing);
        displayText.text = "too weak!";

        yield return new WaitForSeconds(3f);
        NextGame();
    }

    IEnumerator PV_TooStrong()
    {
        yield return new WaitForSeconds(0.3f);

        scenes[3].SetActive(true);

        audioSource.PlayOneShot(smash);
        yield return new WaitForSeconds(0.3f);
        audioSource.PlayOneShot(booing);
        scenes[4].SetActive(true);
        yield return new WaitForSeconds(0.3f);
        scenes[5].SetActive(true);
        displayText.text = "too strong!";
        yield return new WaitForSeconds(0.3f);
        scenes[5].SetActive(false);
        scenes[4].SetActive(false);
        scenes[3].SetActive(false);

        yield return new WaitForSeconds(3f);
        NextGame();
    }

    IEnumerator PV_Perfect()
    {
        yield return new WaitForSeconds(0.5f);
        scenes[6].SetActive(true);

        audioSource.PlayOneShot(cheering);
        displayText.text = "shrimple as that";

        yield return new WaitForSeconds(3f);
        NextGame();
    }


    public void NextGame()
    {
        GameObject.Find("SportSwitcher").GetComponent<SceneChanger>().Counter();
    }
}
