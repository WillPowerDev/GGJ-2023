using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent (typeof(Controller2D))]
//[RequireComponent (typeof(EnemyDeath))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed; 
    [SerializeField] protected float accelerationTimeAirborne = .1f;
    [SerializeField] protected float accelerationTimeGrounded = .05f;

    [SerializeField] Vector2 knockbackIntensity;

    protected Controller2D controller;
    protected Health health;
    //protected EnemyDeath enemyDeath;

    protected float velocityXSmoothing;

    protected Vector2 directionalInput;
    protected Vector3 velocity;

    protected float terminalVelocity = -20f;
    protected float gravity = -52f;

    protected bool facingRight;

    protected void Awake()
    {
        controller = GetComponent<Controller2D>();
        health = GetComponent<Health>();
        //enemyDeath = GetComponent<EnemyDeath>();

        health.OnDamaged += Health_OnDamaged;
        health.OnDead += Health_OnDead;

    }

    protected void Update()
    {
        HandleMovement();

        //CheckContact();
    }

    protected virtual void HandleMovement()
    {
        CalculateVelocity();

        //directionalInput = Vector2.right; 
        if (directionalInput.x != 0)
        {
            facingRight = directionalInput.x == 1;
        }
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }

    protected virtual void Health_OnDamaged(object sender, EventArgs e)
    {
        return;
    }

    protected virtual void Health_OnDead(object sender, EventArgs e)
    {
        //enemyDeath.enabled = true;
        //enemyDeath.SetVelocity(velocity);
        this.enabled = false;
        return;
    }

    protected virtual void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        
        if (velocity.y < terminalVelocity)
        {
            velocity.y = terminalVelocity;
        }
    }

    public virtual void Knockback(float direction)
    {
        velocity = Vector2.up * knockbackIntensity.y + (Vector2.right * knockbackIntensity.x * direction);
    }

    public virtual bool CheckContact(out Transform player)
    {
        //Vector2 colliderSize = new Vector2(controller.Collider.bounds.size.x, controller.Collider.bounds.size.y);
        //Collider2D hitBox = Physics2D.OverlapBox(transform.position, colliderSize, 0, GlobalSettings.i.PlayerMask);
        //if (hitBox)
        //{
        //    player = hitBox.transform;
        //    return true;
        //}
        player = null;
        return false;
    }

    public virtual void DealDamage()
    {

    }
}
