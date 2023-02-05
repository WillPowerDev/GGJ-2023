using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanderEnemy : Enemy
{
    [SerializeField] float stateChangeDuration;
    [SerializeField] float maxTravelDistance;

    enum State 
    {
        Idle,
        Wander
    }

    State state = State.Idle; 
    float timer;

    Vector3 targetLocation;

    // Update is called once per frame
    protected override void Update()
    {
        switch(state)
        {
            case State.Idle:
                HandleMovement();
                //CheckContact();
                directionalInput = Vector2.zero;

                timer += Time.deltaTime;
                if (timer > stateChangeDuration)
                {
                    state = State.Wander;
                    float travelDistance = 0;
                    int direction = (UnityEngine.Random.value >= 0.5f)? 1 : -1;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, maxTravelDistance, controller.collisionMask);
                    
                    //Debug.DrawRay(transform.position, Vector2.right * directionX, Color.red);

                    float dist = maxTravelDistance;
                    if (hit)
                    {
                        dist = Vector3.Distance(transform.position, hit.point);
                        //targetLocation = hit.point;
                        //return;
                    }

                    targetLocation = transform.position + ((Vector3.right * dist) * direction);
                    directionalInput.x = direction;
                    timer = 0;
                }
                break;
            case State.Wander:
                HandleMovement();
                if (CheckContact(out Transform player))
                {
                    Health playerHealth = player.GetComponent<Health>();
                    playerHealth.TakeDamage(1); 
                }
                // Reached target location
                if (Vector3.Distance(transform.position, targetLocation) < .2f)
                {
                    state = State.Idle;
                    timer = 0; 
                    directionalInput = Vector2.zero;
                    return;
                }

                RaycastHit2D hitWall = Physics2D.Raycast(transform.position, Vector2.right * directionalInput.x, maxTravelDistance, controller.collisionMask);

                if (hitWall)
                {
                    float dist = Vector3.Distance(transform.position, hitWall.point);

                    if (dist <= .5)
                    {
                        state = State.Idle;
                        timer = 0; 
                        directionalInput = Vector2.zero;
                        return;
                    }
                }
                // Stopped by something
                if (Mathf.Abs(velocity.x) <= Mathf.Epsilon)
                {
                    state = State.Idle;
                    timer = 0; 
                    directionalInput = Vector2.zero;
                    return;
                }

                // Is about to fall off a cliff
                Collider2D hitPoint = Physics2D.OverlapPoint(transform.position + ((Vector3.right * directionalInput.x) * 1f) + (Vector3.down * 1.1f), controller.collisionMask);

                Debug.DrawRay(transform.position + ((Vector3.right * directionalInput.x) * 1f) + (Vector3.down * 1.1f), Vector2.right * .5f, Color.green);
                if (!hitPoint)
                {
                    state = State.Idle;
                    timer = 0; 
                    directionalInput = Vector2.zero;
                    return;
                }
                break;
        }
    }

    protected override void Health_OnDamaged(object sender, EventArgs e)
    {
        state = State.Idle;
        timer = 0;
    }

    void OnDrawGizmos()
    {
        /*
        // Right
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, maxTravelDistance, controller.collisionMask);
        
        float rightDist = maxTravelDistance;
        if (hitRight)
        {
            rightDist = Vector3.Distance(transform.position, hitRight.point); 
        }
        Debug.DrawRay(transform.position, Vector2.right * rightDist, Color.blue);

        // Left
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, maxTravelDistance, controller.collisionMask);
        
        float leftDist = maxTravelDistance;
        if (hitLeft)
        {
            leftDist = Vector3.Distance(transform.position, hitLeft.point); 
        }
        Debug.DrawRay(transform.position, Vector2.left * leftDist, Color.green);*/
    }
}
