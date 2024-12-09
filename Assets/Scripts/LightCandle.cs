using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandle : MonoBehaviour
{
    private void Start()
    {
            Invoke("Burnout", 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("candle"))
        {
            other.gameObject.transform.Find("fire").gameObject.SetActive(true);
            other.gameObject.GetComponent<CheckCandleLit>().lit = true;
            other.gameObject.transform.parent.GetComponent<Puzzle>().CheckCandles();
        }
    }

    void Burnout()
    {
        GameObject fire = transform.Find("fire").gameObject;
        fire.SetActive(false);
    }
}
