using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.Find("fire").gameObject.SetActive(true);
        other.gameObject.GetComponent<CheckCandleLit>().lit = true;
        other.gameObject.transform.parent.GetComponent<Puzzle>().CheckCandles();
    }
}
