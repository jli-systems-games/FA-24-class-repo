using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeteEnd : MonoBehaviour
{
    public Material _e,_b;
    public TMP_Text _t;
    public TextMeshPro _p;
    public int target = 2;
    MeshRenderer _mr;
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _p.text = target.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("EndPoint"))
        {
            //switch state/ change materials;
            int outputs = other.GetComponentInParent<PlayerHit>().Energy;

            if (outputs == target) 
            { 
                _mr.material = _e;
                GameManager.load();
            }
            else if (outputs > target && _mr != null)
            {
                _mr.material = _b;
                _t.text = "You Blew it!";
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("EndPoint") && _t.text != "")
        {
            _t.text = " ";
        }
    }


}
