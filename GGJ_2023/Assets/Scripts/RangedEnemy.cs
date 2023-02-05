using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] GameObject attackPrefab;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;
    enum State
    {
        Idle,
        Active,
        Attack
    }

    State state = State.Idle;
    Timer attackTimer;
    Timer attackDurationTimer; 
    Player player;

    protected override void Awake(){
        base.Awake();

        player = FindObjectOfType<Player>();
    }
    void Start()
    {
        attackTimer = new Timer(attackCooldown, () => {
            //state = State.Attack;
            Instantiate(attackPrefab, player.transform.position, Quaternion.identity);
        }, true);
        
    }
    protected override void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                HandleMovement();
                if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
                {
                    state = State.Active;
                    attackTimer.Start();
                }
                break;
            case State.Active:
                if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
                {
                    state = State.Idle;
                    attackTimer.Stop(); 
                }
                attackTimer.Tick(Time.deltaTime);
                break;
            case State.Attack:
                state = State.Active;
                break;
        }
    }
}
