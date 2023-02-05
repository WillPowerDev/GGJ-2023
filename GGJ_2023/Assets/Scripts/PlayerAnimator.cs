using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    int playerIdle;
    int playerWalk;
    int playerJump;
    int playerAttack;
    int playerHurt;
    int playerDead;

    // Start is called before the first frame update
    void Start()
    {
        playerIdle = Animator.StringToHash("DripIdle");
        playerWalk = Animator.StringToHash("DripWalk");
        playerJump = Animator.StringToHash("DripJump");
        playerAttack = Animator.StringToHash("DripAttack");
        playerHurt = Animator.StringToHash("DripHurt");
        playerDead = Animator.StringToHash("DripDead");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
