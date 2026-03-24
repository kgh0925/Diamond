using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit()
    {
       animator.SetTrigger("isHit");
    }
    public void Death()
    {
        animator.SetTrigger("Death");
    }
}
