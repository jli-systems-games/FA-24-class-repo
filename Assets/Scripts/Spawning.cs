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
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            
            
            RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, _sets);
            if(_hit.collider != null && !_hit.collider.gameObject.TryGetComponent<previewBehavior>(out previewBehavior prev))
            {
                _choice = _hit.collider.gameObject;
                SpriteRenderer s = _hit.collider.gameObject.GetComponent<SpriteRenderer>();

                _sprite.sprite = s.sprite;
             
                
            }
            timer = 0.3f;
        }

        else if(Input.GetMouseButton(0) && timer<= 0)
        {
             Debug.Log("start draging");
             pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(_choice != null)
            {
                offset = new Vector3(0, 0, 10);
                GameObject obj = spawning();
                obj.transform.position = pos + offset;
            }
            timer = 0.3f;
        }
    }
    GameObject spawning()
    {
        GameObject spawn = Instantiate(_choice, pos + offset, Quaternion.identity);
        return spawn;

    }
}
