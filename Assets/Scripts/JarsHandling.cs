using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarsHandling : MonoBehaviour
{
    public GameManager manage;
    [SerializeField] string childID;
    bool updated;
    CapsuleCollider collide;
    void Start()
    {
        manage.addJars(this.gameObject);
        collide = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //restrict to 2 bc there will a sign and an organ at the end
        if(transform.childCount == 3)
        {
            Transform child = transform.GetChild(1);
            //disable its collider
            collide.enabled = false;
            if(child.name == childID && !updated)
            {
                manage.Updatejars(this.gameObject);
                Debug.Log("updated" + transform.name);
                updated = true;
            }
        }
        else
        {
            collide.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("organs"))
        {
            //Debug.Log("parent is" + transform.name);
            EventManager.assignParent(transform);
            /*manage.checkJarStatus();*/
        }
    }
}
