using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject firework;
    public GameObject p1FireworkAnchor;
    public GameObject p2FireworkAnchor;

    public void PlayP1ScoreAnimation()
    {
        GameObject p1Firework = Instantiate(firework, p1FireworkAnchor.transform.position, Quaternion.Euler(-90, 0, 0), p1FireworkAnchor.transform);
       
        Destroy(p1Firework, 4f);
    }

    public void PlayP2ScoreAnimation()
    {
        GameObject p2Firework = Instantiate(firework, p2FireworkAnchor.transform.position, Quaternion.Euler(-90, 0, 0), p2FireworkAnchor.transform);
       
        Destroy(p2Firework, 4f);
    }
}
