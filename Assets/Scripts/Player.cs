using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float thrustY;
    public float thrustZ;

    public Animator animator;
    public AudioSource accordion;

    bool musicPlaying;

    public int dyingChance;

    public TextMeshProUGUI deathText;

    public GameObject accordionObj;

    private bool died;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        musicPlaying = false;
        animator.speed = 0;
        deathText.enabled = false;
        died = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!died)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _rb.AddForce(0, 1, -1, ForceMode.Impulse);
                //thrustY = thrustY + 1f;
                //thrustZ = thrustZ - 1f;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                //_rb.AddForce(0,thrustY,thrustZ,ForceMode.Impulse);
                thrustY = 0;
                thrustZ = 0;
            }
            if (transform.rotation.x < 0 || transform.rotation.x > 360)
            {
                //Debug.Log("fucking died");
                //Destroy(GetComponent<FixedJoint>());
            }

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                if (!musicPlaying)
                {
                    musicPlaying = true;
                    Music(true);
                }
            }

            else if (musicPlaying)
            {
                musicPlaying = false;
                accordion.Pause();
                animator.speed = 0;
            }

            if (dyingChance == 7)
            {
                FuckingDies();
                died = true;
            }

            else
            {
                dyingChance = Random.Range(0, 500);
            }
        }
    }

    void Music(bool playing)
    {
        accordion.Play();
        animator.speed = 1;
    }

    void FuckingDies()
    {
        Destroy(GetComponent<FixedJoint>());
        _rb.AddForce(0, 0, 50,ForceMode.Force);
        accordionObj.transform.parent = null;
        accordionObj.GetComponent<Rigidbody>().isKinematic = false;
        accordionObj.GetComponent<Rigidbody>().useGravity = true;
        accordionObj.GetComponent<Collider>().enabled = true;
        GetComponent<AudioSource>().Play();
        deathText.enabled = true;
        accordion.Pause();
        animator.speed = 0;
    }
}
