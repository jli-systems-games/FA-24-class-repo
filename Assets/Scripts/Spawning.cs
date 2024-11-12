using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    GameObject _choice;
    public LayerMask _sets;
    Vector3 pos, offset;
    float timer = 0;
    SpriteRenderer _sprite;
    previewBehavior _prev;
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
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            
            rotatible = true;
            RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, _sets);
            if(_hit.collider != null)
            {
                _choice = _hit.collider.gameObject;
                SpriteRenderer s = _hit.collider.gameObject.GetComponent<SpriteRenderer>();

                _sprite.sprite = s.sprite;
             
                
            }else if(_hit.collider == null && _choice != null)
            {
                GameObject obj = spawning();
                obj.transform.position = pos + offset;
                obj.transform.rotation = transform.rotation;
            }
            timer = 0.2f;
        }

        else if(Input.GetMouseButton(0) && timer<= 0)
        {
            Debug.Log("start draging");
            //rotatible= false;
            
            if(_choice != null)
            {
                GameObject obj = spawning();
                obj.transform.position = pos + offset;
                obj.transform.rotation = transform.rotation;
            }
            timer = 0.2f;
        }
    }
    GameObject spawning()
    {
        GameObject spawn = Instantiate(_choice);
        spawn.layer = 0;
        return spawn;

    }
}
