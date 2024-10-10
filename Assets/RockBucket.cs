using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBucket : MonoBehaviour
{
    public GameObject itemPrefab;
    public Vector3 bucketLocation;

    public Vector3 spawnLocation;

    private Vector3 screenPoint;


    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        GameObject item;
        item = Instantiate(itemPrefab, curPosition, Quaternion.identity);


    }
}
