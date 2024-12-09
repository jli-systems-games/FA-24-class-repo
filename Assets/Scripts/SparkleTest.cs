using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleTest : MonoBehaviour
{
    [SerializeField]
    LevelLoader lvLoader;
    public GameObject sparklePrefab, boundBox;
    public Camera cutScCam, mainCam;
    MeshRenderer rend;
    public Animator _fade;
    void Start()
    {
         rend = boundBox.GetComponent<MeshRenderer>();
        // _fade = GetComponent<Animator>();
        //Debug.Log(rend.bounds.min);
        /*Debug.Log(rend.bounds.max);*/
        //StartCoroutine(spawnSparkle());
        GameManager.cutaway += loadCutscene;
    }
    void loadCutscene()
    {
        StartCoroutine(spawnSparkle());
    }
    IEnumerator spawnSparkle()
    {
        yield return new WaitForSeconds(1.5f);

        _fade.SetBool("isFading", true);

        yield return new WaitForSeconds(1f);
        
        
        Camera.main.enabled = false;
        cutScCam.enabled = true;

        GameManager.load();
        

        Vector3 spawnLoc = new Vector3(Random.Range(rend.bounds.min.x, rend.bounds.max.x), Random.Range(rend.bounds.min.y, rend.bounds.max.y), rend.bounds.min.z);
        GameObject newParticle = Instantiate(sparklePrefab);
        newParticle.transform.position = spawnLoc;

        //set up the fading transition;
        ParticleSystemRenderer psr = newParticle.GetComponent<ParticleSystemRenderer>();
        Color _scolor = psr.material.GetColor("_TintColor");
        float _a = 0;
        _scolor.a = 0;
        while (_scolor.a < 1)
        {
            _a += 0.1f;
            _scolor = new Color(_scolor.r, _scolor.g, _scolor.b, _a );
            yield return null;
        }
        
        yield return new WaitForSeconds(1.5f);

        _fade.SetBool("isFading", false);

        yield return new WaitForSeconds(1f);
        _fade.SetBool("isFading", true);

        yield return new WaitForSeconds(1f);

        if(lvLoader.levelIndex <= lvLoader.lvStats.Length - 1)
        {
            cutScCam.enabled = false;
            mainCam.enabled = true;
        
            yield return new WaitForSeconds(1.5f);
            _fade.SetBool("isFading", false);
            LevelLoader.loadingLevel = false;
        }
       
    }
}
