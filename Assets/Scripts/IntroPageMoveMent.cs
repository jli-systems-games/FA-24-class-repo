using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPageMoveMent : MonoBehaviour
{
    private IntroPage introPage;
    public float moveSpeed = 1f;     // 水平方向移动速度    
    public SpriteRenderer head;
    public Transform body;
    public float jumpForce = 3f;     // 跳跃的力量
    private bool canJump = true;
    private Rigidbody rb;

    public Transform respawnAnchor;

    public float tiltAngle = 10f;

    // Start is called before the first frame update
    void Start()
    {
        introPage = FindObjectOfType<IntroPage>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (introPage.gameStage == IntroPage.GameStage.Stage2)
        {
            MoveR();
        }
        else if (introPage.gameStage == IntroPage.GameStage.Stage4)
        {
            MoveL();
        }

        if (introPage.gameStage != IntroPage.GameStage.Stage1)
        {
            Jump();
        }
            if (body.position.y < -4)
        {
            transform.position = respawnAnchor.position;
            body.position = respawnAnchor.position;
        }

    }

    void MoveR()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D)) 
        {
            moveInput = 1f;
        }

        Vector3 direction = new Vector3(moveInput, 0, 0); // 水平移动方向

        // 使用 transform.Translate 进行移动
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
        {
            head.flipX = true;
            body.rotation = Quaternion.Euler(0, 0, -tiltAngle);  // Z轴上旋转到-10度
        }
        else  // 恢复到正常角度
        {
            body.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void MoveL()
    {

        float moveInput = 0f;
        if (Input.GetKey(KeyCode.A)) // 只检测 D 键
        {
            moveInput = -1f; // 向右移动
        }

        // 创建移动方向
        Vector3 direction = new Vector3(moveInput, 0, 0); // 水平移动方向

        // 使用 transform.Translate 进行移动
        transform.Translate(direction * moveSpeed * Time.deltaTime);



        if (Input.GetKey(KeyCode.A))
        {
            head.flipX = false;
            body.rotation = Quaternion.Euler(0, 0, tiltAngle);  // Z轴上旋转到10度
        }        
        else  // 恢复到正常角度
        {
            body.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canJump = false;
            StartCoroutine(JumpCooldown());
        }
    }
    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.6f);
        canJump = true;
    }
}
