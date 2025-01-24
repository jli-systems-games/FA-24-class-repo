using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolObstacle : MonoBehaviour
{
    public ObstaclesStats _stats;
    public int _obstacleCount;
    public TMP_Text count;

    void Start()
    {
        EventManager.decideCount += AssignSelf;
        
    }
    private void Update()
    {
        count.text = _obstacleCount.ToString();
    }

    void AssignSelf(Level L)
    {
        
         _obstacleCount = L.assignValue(gameObject.name);
        Debug.Log("assignSelf");
        
        if ( !GameManager.tools.Contains(this)) GameManager.tools.Add(this);    
        
       
    }
    private void OnDisable()
    {
        EventManager.decideCount -= AssignSelf;
    }
}
