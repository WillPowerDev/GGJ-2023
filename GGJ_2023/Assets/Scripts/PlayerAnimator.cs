using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    int playerIdle;
    int playerWalk;
    int playerJump;
    int playerAttack;
    int playerHurt;
    int playerDead;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();

        playerIdle = Animator.StringToHash("DripIdle");
        playerWalk = Animator.StringToHash("DripWalk");
        playerJump = Animator.StringToHash("DripJump");
        playerAttack = Animator.StringToHash("DripAttack");
        playerHurt = Animator.StringToHash("DripHurt");
        playerDead = Animator.StringToHash("DripDead");
    }

    public void IdleAnimation()
    {
        animator.CrossFade(playerIdle, 0);
    }

    public void RunAnimation()
    {
        animator.CrossFade(playerWalk, 0);

    }

    public void JumpAnimation()
    {
        animator.CrossFade(playerJump, 0);

    }

    public void AttackAnimation()
    {
        animator.CrossFade(playerAttack, 0);

    }

    public void HurtAnimation()
    {
        animator.CrossFade(playerHurt, 0);

    }

    public void DeadAnimation()
    {
        animator.CrossFade(playerDead, 0);
    }
}
