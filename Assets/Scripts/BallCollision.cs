using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallCollision : MonoBehaviour
{
    private PinballController pinballController;
    private Rigidbody ballRigidbody;

    public int score = 0;
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resetText;
    public GameObject resetUI;

    private float smallForce = 0f;
    private float mediumForce = 0f;
    private float largeForce = 0f;

    private bool isMultiplierActive = false;
    //private bool isSlowMotionActive = false;
    private float eventDuration = 8f;

    private float minEventCooldown = 20f;
    private float maxEventCooldown = 30f;
    private float nextEventTime;

    // Start is called before the first frame update
    void Start()
    {
        pinballController = FindObjectOfType<PinballController>();
        ballRigidbody = GetComponent<Rigidbody>();

        if (pinballController == null)
        {
            Debug.LogError("pinball manager not found");
        }

        UpdateScore();
        SetNextEventTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextEventTime)
        {
            TriggerRandomEvent();
            SetNextEventTime();
        }
    }

    #region Ball Collision

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("point5"))
        {
            AddScore(5);
            ApplyBumperForce(collision, smallForce);
        }
        else if (collision.gameObject.CompareTag("point10"))
        {
            AddScore(10);
            ApplyBumperForce(collision, largeForce);
        }
        else if (collision.gameObject.CompareTag("point25"))
        {
            AddScore(25);
            ApplyBumperForce(collision, mediumForce);
        }
        else if (collision.gameObject.CompareTag("bottom"))
        {
            resetUI.SetActive(true);
            resetText.text = "Your final score was " + score.ToString();
        }
    }

    void ApplyBumperForce(Collision collision, float force)
    {
        if (ballRigidbody != null)
        {
            Vector3 forceDirection = collision.contacts[0].normal;
            ballRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }

#endregion

    void AddScore(int points)
    {
        score += isMultiplierActive ? points * 2 : points;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    #region Events

    private void SetNextEventTime()
    {
        nextEventTime = Time.time + Random.Range(minEventCooldown, maxEventCooldown);
    }

    private void TriggerRandomEvent()
    {
        int eventType = Random.Range(0, 4);

        switch (eventType)
        {
            case 0:
                StartCoroutine(ScoreMultiplierEvent());
                break;
            case 1:
                StartCoroutine(SlowMotionEvent());
                break;
            case 2:
                StartCoroutine(GravityShiftEvent());
                break;
            case 3:
                StartCoroutine(BumperBoostEvent());
                break;
        }
    }

    IEnumerator ScoreMultiplierEvent()
    {
        eventText.text = "2X multiplier!";
        isMultiplierActive = true;

        yield return new WaitForSeconds(eventDuration);
        eventText.text = "";
        isMultiplierActive = false;
    }

    IEnumerator SlowMotionEvent()
    {
        eventText.text = "slow motion!";
        //isSlowMotionActive = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(eventDuration);
        eventText.text = "";
        Time.timeScale = 1.0f;
        //isSlowMotionActive = false;
    }

    IEnumerator GravityShiftEvent()
    {
        eventText.text = "gravity shift!";
        float originalGravity = Physics.gravity.y;
        Physics.gravity = new Vector3(0, Random.Range(-20f, -5f), 0);

        yield return new WaitForSeconds(eventDuration);
        eventText.text = "";
        Physics.gravity = new Vector3(0, originalGravity, 0);
    }

    IEnumerator BumperBoostEvent()
    {
        eventText.text = "bumper bounce!";
        smallForce = 0.8f;
        mediumForce = 1.5f;
        largeForce = 3f;

        yield return new WaitForSeconds(eventDuration);
        eventText.text = "";
        smallForce = 0f;
        mediumForce = 0f;
        largeForce = 0f;
    }

    #endregion
}
