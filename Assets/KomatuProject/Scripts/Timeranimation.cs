using UnityEngine;

public class TimerAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        animator.SetBool("NewBool", true);

        // 0.3•bŒã‚Éfalse‚É‚·‚é
        Invoke(nameof(StopAnimation), 1f);
    }

    void StopAnimation()
    {
        animator.SetBool("NewBool", false);
    }
}