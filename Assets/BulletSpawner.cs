using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    public List<GameObject> children = new();


    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;


    private GameObject spawnedBullet;
    private float timer = 0f;

    public bool playing;

    void Start()
    {
        RandomizeStats(); // This will randomize stats and update the text when the game starts.


    }



    public void Example()
    {

    }

    public void Begin()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }


    private void Fire()
    {
        if (bullet)
        {
            foreach (GameObject item in children)
            {
                spawnedBullet = Instantiate(bullet, item.transform.position, Quaternion.identity);
                spawnedBullet.GetComponent<Bullet>().speed = speed;
                spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
                spawnedBullet.transform.rotation = item.transform.rotation;
            }

        }
    }

    public TextMeshProUGUI displayText;


    public void RandomizeStats()
    {
        // Randomize values
        spawnerType = (SpawnerType)Random.Range(0, System.Enum.GetValues(typeof(SpawnerType)).Length);
        firingRate = Random.Range(0.05f, 0.7f);
        speed = Random.Range(10f, 30f);

        // Display the values as strings using .ToString()
        if (displayText != null)
        {
            // If using TextMeshPro, directly assign text as normal
            displayText.text = "Spawner Type: " + spawnerType.ToString() +
                               "\nFiring Rate: " + firingRate.ToString("F2") +
                               "\nSpeed: " + speed.ToString("F2");
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on the assigned GameObject.");
        }
    }



}