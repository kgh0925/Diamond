using UnityEngine;

public class AttackReset : StateMachineBehaviour
{
    [SerializeField] string TriggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(TriggerName);
    }
}
