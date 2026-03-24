using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

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
    private float stundeley = 1.2f;

    public InputAction Move;
    public InputAction Jump;
    public InputAction Run;
    [Header("스턴 시 사용 금지 무기")]
    public Weapon weapon;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        weapon = FindFirstObjectByType<Weapon>();
    }
    private void OnEnable()
    {
        Jump.Enable();
        Move.Enable();
        Run.Enable();
    }
    private void OnDisable()
    {
        Jump.Disable();
        Move.Disable();
        Run.Disable();
    }
    private void Update()
    {
        moveInput = Move.ReadValue<Vector3>();
        float v = moveInput.z;
        moveInput = transform.TransformDirection(moveInput);
        mouseMoveX = Input.GetAxisRaw("Mouse X");
        RunSpeed = moveSpeed + 10;


        transform.Rotate(0, mouseMoveX * RotateSpeed * Time.deltaTime, 0);

        if (Jump.WasPerformedThisFrame() && JumpCount < 1)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // ForceMode.Impulse : 무게와 힘 모두 가함
            JumpCount++;
        }
        if (Run.IsPressed() && v > 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }


    }


/*    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        weapon = FindFirstObjectByType<Weapon>();
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
        if(Input.GetButton("Run") && v > 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }


    }*/
    public bool GetisRun() { return isRun; }

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

    public void Stun_Time()
    {
        StartCoroutine(Stun());

    }
    IEnumerator Stun()
    {
        Jump.Disable();
        Move.Disable();
        Run.Disable();
        weapon.Attack.Disable();
        yield return new WaitForSeconds(stundeley);
        Jump.Enable();
        Move.Enable();
        Run.Enable();
        weapon.Attack.Enable();
    }
}
