using UnityEngine;

public class AniController : MonoBehaviour
{
    private Animator animator;
    private PlayerMove3D PlayerMove3D;
    bool RunDown;
    void Awake()
    {
        animator = GetComponent<Animator>(); 
        PlayerMove3D = GetComponentInParent<PlayerMove3D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // ÁÂ¿ì
        float y = Input.GetAxisRaw("Vertical"); // »óÇÏ
        Vector2 tempvector = new Vector2(h, y);

        animator.SetFloat("MoveX", tempvector.x);
        animator.SetFloat("MoveY", tempvector.y);
        if(Input.GetButton("Run"))
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        if (tempvector.x != 0 || tempvector.y != 0)
        {
            animator.SetFloat("Speed", PlayerMove3D.moveSpeed);

        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        if(Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
            animator.SetBool("isGround", PlayerMove3D.GetIsGround());

    }


}
