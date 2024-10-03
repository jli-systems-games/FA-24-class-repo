using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeSourceImg : MonoBehaviour
{
    public AudioSource audiosource;
    public TextMeshProUGUI sub;

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
    }



}
