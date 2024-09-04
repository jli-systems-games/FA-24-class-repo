using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Camera cam;

    public Ball[] ball;
    private Ball defaultBall;
    public Trajectory trajectory;
    [SerializeField] float PushForce = 4f;

    bool isDragging;

    private grab grabScript;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    private void Start()
    {
        cam = Camera.main;
        grabScript = FindObjectOfType<grab>();
    }

    private void Update()
    {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            defaultBall = null;
            for(int i = 0; i < ball.Length; i++)
            {
                if(ball[i].col == Physics2D.OverlapPoint(pos))
                {
                    Time.timeScale = 0.1f;
                    isDragging = true;
                    defaultBall = ball[i];
                    OnDragStart();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        //if(Input.GetKeyDown(KeyCode.Space))
        {
            if(defaultBall != null)
            {
                Time.timeScale = 1f;
                isDragging = false;
                OnDragEnd();
            }
        }
        if (isDragging)
        {
            OnDrag();
        }
    }


    #region Drag

    private void OnDragStart()
    {
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = distance * direction * PushForce;

        trajectory.UpdateDots(defaultBall.pos, force);
    }

    private void OnDragEnd()
    {
        defaultBall.Push(force);
        trajectory.Hide();
    }

    #endregion
}
