using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float maxJumpHeight = 4;
    [SerializeField] float minJumpHeight = 1;
    [SerializeField] float timeToJumpApex = .4f;

    [Header ("Digging")]
    [SerializeField] LayerMask digLayer;
    [SerializeField] float range;
    [SerializeField] float radius;
    [SerializeField] float digTime;
    [SerializeField] float digDuration;

    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;

    Vector3 velocity;
    float velocityXSmoothing;
    float timeToWallUnstick;

    Controller2D controller;

    Vector2 directionalInput;
    Vector2 lastDirection;
    Vector3 digDirection;
    bool wallSliding;
    int wallDirX;

    enum State 
    {
        Normal,
        Digging,
        Death,
    }

    State state = State.Normal;

    Timer digTimer;
    Timer resetState;
    void Start()
    {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight);

        digTimer = new Timer(digTime, () => {
            List<Collider2D> collisions = Physics2D.OverlapCircleAll(transform.position + (digDirection * range), radius, digLayer).ToList();
            foreach (Collider2D collider in collisions)
            {
                Debug.Log("Player.cs target " + collider.gameObject.name);
                Destroy(collider.gameObject);
            }
        });
        resetState = new Timer(digDuration, () => {
            state = State.Normal;
        });
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleNormal();
                break;
            case State.Digging:
                HandleDigging();
                break;
        }
    }

    void HandleNormal()
    {
        CalculateVelocity();
        
        //Debug.Log("[Player.cs/Update()] velocity = " + velocity);
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    void HandleDigging()
    {
        digTimer.Tick(Time.deltaTime);
        resetState.Tick(Time.deltaTime);
        CalculateVelocity();
        
        //Debug.Log("[Player.cs/Update()] velocity = " + velocity);
        controller.Move(velocity * Time.deltaTime, Vector2.zero);
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;

        if (input == Vector2.zero) return;

        lastDirection = input;
    }

    public void OnJumpInputDown()
    {
        // Colliding with a ground object
        if (controller.collisions.below)
        {
            // not jumping against max slope
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                {
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }   
            }
            else
            {
                velocity.y = maxJumpVelocity;
            }
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {   
            velocity.y = minJumpVelocity;
        }
    }

    public void Dig()
    {
        state = State.Digging;
        digDirection = lastDirection;
        digTimer.Begin();
        resetState.Begin();
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }
}
