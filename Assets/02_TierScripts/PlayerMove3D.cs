using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMove3D : MonoBehaviour
{
    [Header("사용자 값 입력 후 사용")]
    public float moveSpeed;
    public float jumpPower;
    public float RotateSpeed;
    [Header("현재 속도 [ 달리기 : 현재 속도 + 10 ]")]
    public float CurrentSpeed;
    private Rigidbody rb;
    float mouseMoveX;
    private int JumpCount;
    private Vector3 moveInput;
    private bool isground;
    private bool isRun;
    private float RunSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        
        moveInput = new Vector3(h, 0, v).normalized;
        moveInput = transform.TransformDirection(moveInput);
        mouseMoveX = Input.GetAxisRaw("Mouse X");
        RunSpeed = moveSpeed + 10;

        
        transform.Rotate(0, mouseMoveX * RotateSpeed * Time.deltaTime, 0);
       
        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 1)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // ForceMode.Impulse : 무게와 힘 모두 가함
            JumpCount++;
        }
        if(Input.GetButton("Run"))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }


    }

    private void FixedUpdate()
    {
        CurrentSpeed = isRun ? RunSpeed : moveSpeed;
        rb.MovePosition(rb.position + moveInput * CurrentSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isground = true;
            JumpCount = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isground = false;
        }
    }
    public Vector3 GetMoveInput()
    {
        return moveInput;
    }
    public bool GetIsGround()
    {
        return isground;
    }
}
