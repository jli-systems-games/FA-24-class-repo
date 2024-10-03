using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraFading;

public class BananaWalk : MonoBehaviour
{
    public Animator animator;

    public GameObject umbrella;
    public GameObject umbrellaRB;

    public GameObject player;
    public float speed = 1f;
    public float rotation_speed = 0.5f;

    public bool slipped = false;

    public Vector3 startPosition;
    public GameObject bananaPrefab;

    AudioSource audiosource;
    public AudioClip woah;

    public Vector3 ztopLimit;
    public Vector3 zbotLimit;
    public Vector3 xtopLimit;
    public Vector3 xbotLimit;

    // Start is called before the first frame update
    void Start()
    {
        CameraFade.In(.2f);

        audiosource = GetComponent<AudioSource>();

        slipped = false;

        umbrella.SetActive(true);
        umbrellaRB.SetActive(false);

        startPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BananaMovement();

        if (transform.position.z > ztopLimit.z)
        {

            transform.position = new Vector3(transform.position.x, 0.4409999f, ztopLimit.z);
        }

        if (transform.position.z < zbotLimit.z)
        {

            transform.position = new Vector3(transform.position.x, 0.4409999f, zbotLimit.z);
        }

        if (transform.position.x > xtopLimit.x)
        {

            transform.position = new Vector3(xtopLimit.x, 0.4409999f, transform.position.z);
        }

        if (transform.position.x < xbotLimit.x)
        {

            transform.position = new Vector3(xbotLimit.x, 0.4409999f, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("trigger");
        if (collision.gameObject.CompareTag("human"))
        {
            BananaSlip();
            slipped = true;
        }
    }

    public void BananaSlip()
    {
        umbrella.SetActive(false);
        umbrellaRB.SetActive(true);

        audiosource.PlayOneShot(woah);

        animator.Play("fall");
        animator.SetBool("slipped", true);

        CameraFade.Out(1.8f);

        StartCoroutine(EndPause());
    }

    private IEnumerator EndPause()
    {
        yield return new WaitForSeconds(2f);
        Spawn();

        CameraFade.In(.2f);
    }

    public void Spawn()
    {
        GameObject banana;

        banana = Instantiate(bananaPrefab, startPosition, Quaternion.identity);

        banana.GetComponent<BananaWalk>().slipped = false;
    }

    public void BananaMovement()
    {
        if (slipped == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                player.transform.position -= player.transform.forward * speed * Time.deltaTime;
                animator.Play("walking");
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                player.transform.position += player.transform.forward * speed * Time.deltaTime;
                animator.Play("walking");
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                player.transform.Rotate(Vector3.down * rotation_speed);
                animator.Play("walking");
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                player.transform.Rotate(Vector3.up * rotation_speed);
                animator.Play("walking");
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }
}
