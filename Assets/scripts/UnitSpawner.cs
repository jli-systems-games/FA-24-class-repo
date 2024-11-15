using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    public GameObject[] dentistPrefabs;
    public Vector3 dentistPosition;
    public float spawnRange = 2f;  // varies spawn locations

    public BattleManager battleManager;

    private bool dentistSelected = false;

    public void SpawnDentist(int dentistIndex)
    {
        if (dentistSelected)
        {
            Debug.Log("dentist already selected");
            return;
        }

        Vector3 spawnPosition = dentistPosition + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));

        GameObject dentistUnitObject = Instantiate(dentistPrefabs[dentistIndex], spawnPosition, Quaternion.identity);

        Unit dentistUnit = dentistUnitObject.GetComponent<Unit>();

        // adds unit to dentist team in battlemanager
        if (dentistUnit != null)
        {
            battleManager.DentistUnit(dentistUnit);
            dentistSelected = true;
        }
    }
}
