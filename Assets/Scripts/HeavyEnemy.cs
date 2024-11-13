using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

enum SelfState {
    Moving,Breaking
}

public class HeavyEnemy : EnemyAttack
{

    RaycastHit2D[] results;
    Vector2 origin,size;
    SelfState currentState;
    Rigidbody2D rb;
    public LayerMask mask;
    protected override void Start()
    {
        base.Start();
        currentState = SelfState.Moving;
        rb = GetComponent<Rigidbody2D>();
        
        size = new Vector2(transform.localScale.x, transform.localScale.y);
    }
    protected override void Update()
    {   
        if(currentState == SelfState.Moving)
        {
            base.Update();
        }
        else
        {
            transform.position = transform.position;
            Debug.Log("breakthrough");
        }
        
        origin = new Vector2(transform.position.x + transform.localScale.x, transform.position.y);
        
/*
        if(results != null )
        {
            foreach(RaycastHit2D hit in results)
            {
                Debug.Log(hit.collider.name);
            }
        }*/

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.collider.CompareTag("blocks"))
        {
            ChangeState(SelfState.Breaking);
        }
    }
    void ChangeState(SelfState state)
    {
        

        if (state == SelfState.Breaking)
        {
            StartCoroutine(BreakingThrough());
        }
        
        currentState = state;
    }
    IEnumerator BreakingThrough()
    {
        Debug.Log(health);
        while(health > 0)
        {
            Debug.Log("breaking");
            results =  Physics2D.BoxCastAll(origin, size, 0, transform.right, Mathf.Infinity ,mask);
            foreach (RaycastHit2D hit in results)
            {
                Debug.Log(hit.collider.name);
            }
            if (results.Length > 0) {
                rb.AddForce(-transform.right * 2f,ForceMode2D.Impulse);

                yield return new WaitForSeconds(1f);

                rb.AddForce(transform.right * 5f, ForceMode2D.Impulse);
                yield return null;

            }
            else
            {
                Debug.Log("return");
                ChangeState(SelfState.Moving);
                
                break;

            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(origin, size);
    }
}
