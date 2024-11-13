using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public List<Unit> candyTeam;
    public List<Unit> dentistTeam;
    public TextMeshProUGUI winCandy;
    public TextMeshProUGUI winDentist;

    private void Start()
    {
        StartCoroutine(Battle());
    }

    private IEnumerator Battle()
    {
        while (candyTeam.Count > 0 && dentistTeam.Count > 0)
        {
            yield return new WaitForSeconds(1f);
            PerformTurn();
        }

        // victory conditions
        if (candyTeam.Count == 0)
            winDentist.gameObject.SetActive(true);
        else
            winCandy.gameObject.SetActive(true);
    }

    private void PerformTurn()
    {
        // candy attacks
        if (candyTeam.Count > 0 && dentistTeam.Count > 0)
        {
            foreach (var candyUnit in candyTeam)
            {
                if (dentistTeam.Count > 0)
                {
                    candyUnit.Attack(dentistTeam[0]);
                    if (dentistTeam[0].currentHealth <= 0)
                    {
                        dentistTeam.RemoveAt(0);
                    }
                }
            }
            // dentist attacks
            foreach (var dentistUnit in dentistTeam)
            {
                if (candyTeam.Count > 0)
                {
                    dentistUnit.Attack(candyTeam[0]);
                    if (candyTeam[0].currentHealth <= 0)
                    {
                        candyTeam.RemoveAt(0);
                    }
                }
            }
        }
    }
}
