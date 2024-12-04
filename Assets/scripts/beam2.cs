using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class beam2 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform lightPoint;
    public int pointIndex;

    Vector2 linePointv2;
    Vector2 direction;

    public GameObject startButton;
    public GameObject fire;

    public Vector2 raycast1;
    public Vector2 raycast2;

    public Vector2 oldHitPoint;
    public bool firstHit;
    public Vector2 firstHitPoint;

    public bool stopLine;
    public bool won;

    public GameObject transitionAnim;

    void Start()
    {
        raycast1 = transform.position;
        raycast2 = transform.right;
        UpdateLight();
    }

    void UpdateLight()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast1, raycast2);

        if(firstHit == false)
        {
            lineRenderer.SetPosition(0, lightPoint.position);
            firstHitPoint = hit.point;
            lineRenderer.SetPosition(1, firstHitPoint);
            linePointv2 = new Vector2(lightPoint.position.x, lightPoint.position.y);
            direction = (hit.point - linePointv2);
            firstHit = true;
        }

        StartCoroutine(transition());
        
    }

    private IEnumerator transition()
    {
        fire.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        startButton.SetActive(false);
        transitionAnim.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LevelLoader");
    }

}