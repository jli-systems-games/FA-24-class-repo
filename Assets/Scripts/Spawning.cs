using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Spawning : MonoBehaviour
{
    GameObject _choice;
    public LayerMask _sets, obs;
    Vector3 pos, offset;
    float timer = 0;
    SpriteRenderer _sprite;
    previewBehavior _prev;
    ToolObstacle _tol;
    public bool rotatible;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _prev = GetComponent<previewBehavior>();
        offset = new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        _prev.followMouse(offset);
        if(GameManager.currState == LevelState.Preparing)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
           {
            
              rotatible = true;
              RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, _sets);
              if(_hit.collider != null)
              {
                    _choice = _hit.collider.gameObject;
                     SpriteRenderer s = _hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    _tol = _choice.GetComponent<ToolObstacle>();
                    _sprite.sprite = s.sprite;
             
                
              }else if(_hit.collider == null && _choice != null && _tol._obstacleCount > 0)
              {
                    GameObject obj = spawning();
                     obj.transform.position = pos + offset;
                     obj.transform.rotation = transform.rotation;
                    //minus total count;
                   // _choice.GetComponent<ToolObstacle>()._obstacleCount--;
                    _tol._obstacleCount -= 1;
                    _tol.count.text = _tol._obstacleCount.ToString();


                }

               timer = 0.2f;
           }

           else if(Input.GetMouseButton(0) && timer<= 0)
           {
                Debug.Log("start draging");
                
            
                if(_choice != null && _tol._obstacleCount > 0)
                {
                    GameObject obj = spawning();
                    obj.transform.position = pos + offset;
                    obj.transform.rotation = transform.rotation;
                    //minus from the total count
                    _tol._obstacleCount--;
                    _tol.count.text = _tol._obstacleCount.ToString();
                }

             timer = 0.2f;
           }
        }
        
    }
    ToolObstacle findCount(string n)
    {
        ToolObstacle t = null;

        foreach(ToolObstacle i in GameManager.tools)
        {
            if(i.name == n)
            {
                t = i;
                
                break;
            }
        }
        return t;


    }
    GameObject spawning()
    {
        GameObject spawn = Instantiate(_choice);
        Destroy(spawn.GetComponent<ToolObstacle>());
        int m = LayerMask.NameToLayer("obstacle");
        TMP_Text _T = spawn.GetComponentInChildren<TMP_Text>();
        _T.text = string.Empty;
        spawn.layer = m;

        //add a way for them to be all unique later;
        if(spawn.TryGetComponent<ObstacleBase>(out ObstacleBase _obs))
        {
            _obs.id = Random.Range(0,10).ToString();
            _obs._stats = _tol._stats;

        }
        else
        {
            spawn.AddComponent<ObstacleBase>();
            spawn.GetComponent<ObstacleBase>().id = Random.Range(0, 10).ToString();
            spawn.GetComponent<ObstacleBase>()._stats = _tol._stats;

        }
        /*ToolObstacle t = spawn.GetComponent<ToolObstacle>();
        Destroy(t);*/
       
        return spawn;

    }
}
