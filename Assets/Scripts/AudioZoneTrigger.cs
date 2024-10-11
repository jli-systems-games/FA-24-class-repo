using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    public int zoneNumber;  // Zone identifier (1, 2, or 3)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Swap to the new ambient track based on the zone number
            AudioManager.instance.SwapAmbientTrack(zoneNumber);
        }
    }
}
