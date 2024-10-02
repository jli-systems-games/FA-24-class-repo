using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int bruh;

    // Start is called before the first frame update
    void Start()
    {
        slipped = false;

        umbrella.SetActive(true);
        umbrellaRB.SetActive(false);

        startPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BananaMovement();
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


        bruh = Random.Range(0,)

            overlay camera so the star cant clip into objects while ur holding him



                duplicate camera
            change from base to overlay
            go to maincamera
            go to stack

        player.transform.Rotate(Vector3.down * 100f);

        animator.Play("fall");
        animator.SetBool("slipped", true);

        StartCoroutine(EndPause());
    }

    private IEnumerator EndPause()
    {
        yield return new WaitForSeconds(2f);
        Spawn();
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
