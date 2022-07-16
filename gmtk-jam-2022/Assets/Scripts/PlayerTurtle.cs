using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurtle : MonoBehaviour
{
    [SerializeField]
    private GameObject groundTracker;

    private bool isActive;
    private bool hasJetpack;
    private bool needsImpulse;
    private bool isTilted;
    private bool isFalling;
    private float fallCheckWaitTime;
    private int movementCommands;
    private string facing;
    private float rotation;
    private Vector2 velocity;
    private Vector2 acceleration;
    private Rigidbody2D rigidBody;
    private int FALLWAIT = 2; // How long grounded 'till not falling (i.e. not rolling down a mountain)

    void Start()
    {
        isActive = false;
        hasJetpack = false;
        needsImpulse = false;
        isTilted = false;
        isFalling = false;
        velocity = new Vector2();
        acceleration = new Vector2();
        rigidBody = GetComponent<Rigidbody2D>();
        facing = "right";
        fallCheckWaitTime = 0;
        EventController.current.onKeyEvent += KeyEventHandler;
        EventController.current.onClickEvent += ClickEventHandler;
    }

    private void OnDestroy()
    {
        EventController.current.onKeyEvent -= KeyEventHandler;
        EventController.current.onClickEvent -= ClickEventHandler;
    }

    private void Update()
    {
        // Debug.Log($"Grounded: {isGrounded()}, Falling:{isFalling}");
        if (!isGrounded())
        {
            isFalling = true;
        }
        else
        {
            // Debug.Log($"Current: {Time.time}, Wait till:{fallCheckWaitTime}");
            if (isFalling && fallCheckWaitTime == 0)
            {
                fallCheckWaitTime = Time.time + FALLWAIT;
            }
            else if (isFalling && Time.time > fallCheckWaitTime)
            {
                isFalling = false;
                fallCheckWaitTime = 0;
            }

        }
    }

    private void FixedUpdate()
    {
        // TODO: Jetpack + acceleration

        /*if (velocity == Vector2.zero)
        {
            if (isGrounded())
                rigidBody.velocity = Vector2.zero;
        }*/
        if (isTilted && !isFalling)
        {
            rotation = 15f;
            // Debug.Log($"Angular Velocity: {rigidBody.angularVelocity}");
            if (Mathf.Abs(rigidBody.angularVelocity) < rotation)
                if (facing == "left")
                    rigidBody.AddTorque(12);
                else
                    rigidBody.AddTorque(-12);

            if (isGrounded()) // Poor turtle can't hold it
                if (Mathf.Abs(rigidBody.rotation) > 30)
                    if (rigidBody.rotation > 0)
                        rigidBody.SetRotation(15);
                    else
                        rigidBody.SetRotation(-15);
        }

        if (Mathf.Abs(rigidBody.velocity.x) < Mathf.Abs(velocity.x))
            if (isGrounded())
            {
                if (needsImpulse) // Instantaneous change needed, double velocity to overcome
                {
                    needsImpulse = false;
                    rigidBody.velocity += velocity;
                }
                rigidBody.velocity += velocity;
            }
            else
                rigidBody.velocity += velocity * 0.05f; // Enough to wobble

    }


    private void KeyEventHandler(string id, string action)
    {
        // Debug.Log($"Turtle {gameObject.name} keyEvent: {id}");
        // TODO use discrete direction bools to account for up?
        if (id == EventController.UP)
        {
            if (action == EventController.PRESS && isGrounded())
                isTilted = true;
            else if (action == EventController.RELEASE)
                isTilted = false;
            return;
        }
        if (action == EventController.PRESS)
            movementCommands += 1;

        if (id == EventController.LEFT)
            velocity = new Vector2(1, 0);
        else if (id == EventController.RIGHT)
            velocity = new Vector2(-1, 0);

        // if (this.hasJetpack)

        if (action == EventController.RELEASE)
        {
            movementCommands -= 1;
            if (movementCommands == 0)
                velocity = new Vector2(0, 0);
            else if (movementCommands == 1)
            {
                needsImpulse = true;
                if (id == EventController.LEFT) // Other button is held, reverse
                    velocity = new Vector2(-1, 0);
                else
                    velocity = new Vector2(1, 0);
            }
            else
            {
                velocity = new Vector2(0, 0);
                movementCommands = 0;
            }
        }

        if (velocity.x > 0)
            facing = "left";
        else if (velocity.x < 0)
            facing = "right";
    }

    private void ClickEventHandler(string id)
    {
        // Debug.Log($"Turtle {gameObject.name} clickEvent: {id}");
        if (id == gameObject.name) { Debug.Log($"Selected {gameObject.name}"); }
    }

    private bool isGrounded()
    {
        return groundTracker.GetComponent<CollisionTracker>().isColliding();
    }


    /*
    // We need to rotate the raycast points with the gameObject or make some bespoke points on the turtle
    // So let's just do box collision for now
    private bool isGrounded()
    {
        Vector3 offset = new Vector3(1, 0, 0);
        Vector3 direction = feet.transform.position - transform.position;
        int length = 1;

        return Physics2D.Raycast(transform.position + offset, direction, length) || Physics2D.Raycast(transform.position - offset, direction, length);
    }

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(1, 0, 0);
        Vector3 direction = feet.transform.position - transform.position;
        //Vector3 direction = transform.position - feet.transform.position;
        int length = 1;

        Gizmos.DrawLine(transform.position + offset, transform.position + offset + direction.normalized * length);
        Gizmos.DrawSphere(transform.position + offset, 0.1f);
        Gizmos.DrawSphere(transform.position - offset, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset + direction.normalized * length, 0.1f);
        Gizmos.DrawSphere(transform.position - offset + direction.normalized * length, 0.1f);
        Gizmos.DrawLine(transform.position - offset, transform.position - offset + direction.normalized * length);
    }
    */
}
