using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public List<bool> boolList = new List<bool>();

    public bool dance;
    public bool horse;
    public bool pole;
    public bool roll;
    public bool weight;

    //private GameObject displayText;
    private bool counting;
    private float count;

    private bool thing;

    private GameObject displayText;

    void Start()
    {
        thing = false;

        boolList = new List<bool> { dance, horse, pole, roll, weight };


        DontDestroyOnLoad(gameObject);
        //PickSport();
    }

    public void Counter()
    {
        SceneManager.LoadScene("Transition");

        count = 3;
        counting = true;

    }

    private void Update()
    {
        if (counting)
        {
            count -= Time.deltaTime;
            displayText = GameObject.Find("next");

            displayText.GetComponent<TMP_Text>().text = $"NEXT SPORT IN {count:0}";

            if (count <= 0)
            {
                counting = false;
                StartCoroutine(PickSport());
                HasFalse();
            }
        }
    }

    private void HasFalse()
    {
        foreach (bool value in boolList)
        {
            if (!value)
            {
                thing = false;
            }
            else
            {
                thing = true;
            }
        }
    }

    IEnumerator PickSport()
    {
        if (dance == false || pole == false || roll == false || weight == false || horse == false)
        {

            bool picking = true;

            while (picking)
            {
                int sport = Random.Range(0, 5);

                if (sport == 0 && !dance)
                {
                    picking = false;

                    dance = true;
                    StartCoroutine(BreakingSwitch());
                }
                else if (sport == 1 && !horse)
                {
                    picking = false;

                    horse = true;
                    StartCoroutine(HorseSwitch());
                }

                else if (sport == 2 && !pole)
                {
                    picking = false;

                    pole = true;
                    StartCoroutine(PoleSwitch());
                }
                else if (sport == 3 && !roll)
                {
                    picking = false;

                    roll = true;
                    StartCoroutine(RunningSwitch());

                }
                else if (sport == 4 && !weight)
                {
                    picking = false;

                    weight = true;
                    StartCoroutine(WeightSwitch());
                }
            }
        }
        else
        {
            StartCoroutine(Ending());
        }
        yield return null;
    }

    IEnumerator BreakingSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("BreakDancign");
    }

    IEnumerator HorseSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Equestrian");
    }

    IEnumerator PoleSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("PoleVault");
    }

    IEnumerator RunningSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Running");
    }

    IEnumerator TrampolineSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Trampoline");
    }

    IEnumerator WeightSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Weight");
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Ending");
    }
}