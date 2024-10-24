using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    public bool canMove = true;
    public SpriteRenderer head;
    public Transform body;
    private float jumpForce = 4f; 
    private bool canJump = true;
    private Rigidbody rb;

    public bool canGoRight = true;
    public bool canGoLeft = true;

    public Transform respawnAnchor;

    public float tiltAngle = 10f;

    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = playerData.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
            Jump();
        }
       

        if (body.position.y < -4)
        {
            transform.position = respawnAnchor.position;
            body.position = respawnAnchor.position;
        }
            
    }

    void Move()
    {
        // 初始化水平输入变量
        float moveInput = 0f;

        // 检测按键输入
        if (Input.GetKey(KeyCode.A) && canGoLeft)
        {
            moveInput = -1f; // 左移
            head.flipX = false;
            body.rotation = Quaternion.Euler(0, 0, tiltAngle); // 身体倾斜
        }
        else if (Input.GetKey(KeyCode.D) && canGoRight)
        {
            moveInput = 1f; // 右移
            head.flipX = true;
            body.rotation = Quaternion.Euler(0, 0, -tiltAngle); // 身体倾斜
        }
        else
        {
            // 没有按下 A 或 D，恢复身体正常角度
            body.rotation = Quaternion.Euler(0, 0, 0);
        }

        // 创建移动方向
        Vector3 direction = new Vector3(moveInput, 0, 0);

        // 使用 transform.Translate 进行移动
        transform.Translate(direction * moveSpeed * Time.deltaTime);
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
