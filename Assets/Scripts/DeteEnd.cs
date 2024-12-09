using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeteEnd : MonoBehaviour
{
    public Material _e, _b;
    public TMP_Text _t;
    public TextMeshPro _p;
    public int target = 2;
    public MeshRenderer _r;
    public AudioClip succeed, blew;
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
            PlayerHit p = other.GetComponentInParent<PlayerHit>();
            int outputs = p.Energy;

            if (outputs == target) 
            {   
                p.baseSource.Stop();
                p.baseSource.clip = succeed; 
                p.baseSource.Play();
                _r.enabled = true;
                LevelLoader.loadingLevel = true;
                GameManager.clearLevel();
              
                
            }
            else if (outputs > target && _mr != null)
            {
                p.baseSource.Stop();
                p.baseSource.clip = blew;
                p.baseSource.Play();
                _mr.material = _b;
                _t.text = "You Blew it!";
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("EndPoint") && _t.text != "")
        {
            PlayerHit p = other.GetComponentInParent<PlayerHit>();
            if(p.baseSource.isPlaying)
            {
                p.baseSource.Stop() ;
            }
            p.baseSource.clip = p.baseElectric;
            p.baseSource.Play();
            _mr.material = _e;
            _t.text = " ";
        }
    }


}
