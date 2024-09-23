using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;

    public float runSpeed;
    private Rigidbody body;

    public float jumpStrength = 10;


    public bool isGrounded;

    public float bruh;
    public AudioSource audioSource;
    public AudioClip[] soundEffects;
    public AudioClip[] memeEffects;

    private bool touching = false;
    private Vector3 normal = new Vector3(1, 1, 1);
    private Vector3 stretch = new Vector3(1, 1.25f, 1);


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, Input.GetAxis("Vertical") * runSpeed);

        body.velocity = transform.rotation * new Vector3(targetVelocity.x, body.velocity.y, targetVelocity.y); 
    }

    public void OnCollisionEnter(Collision collision)
    {
        //touching = true;
        //transform.localScale = normal;
        //GetComponent<SphereCollider>().radius = 1;

        bruh = Random.Range(1, 101);

        if (bruh == 1)
        {
            int randomIndex = Random.Range(0, memeEffects.Length);
            audioSource.PlayOneShot(memeEffects[randomIndex]);
        }
        else
        {
            int randomIndex = Random.Range(0, soundEffects.Length);
            audioSource.PlayOneShot(soundEffects[randomIndex]);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //touching = false;
        //transform.localScale = stretch;

        //GetComponent<SphereCollider>().radius = 1;
    }
}
