using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerTurtle : MonoBehaviour
{
    [SerializeField]
    private GameObject groundTracker;

    [SerializeField]
    private GameObject jetObject;

    [SerializeField]
    private GameObject jetVector1;
    [SerializeField]
    private GameObject jetVector2;

    [SerializeField]
    private Animator animationController;

    [SerializeField]
    private ParticleSystem[] particleList;

    [SerializeField]
    private CinemachineVirtualCamera turtleCamera;

    [SerializeField]
    private bool hasJetpack = false;

    private bool isActive;
    private bool needsImpulse;
    private bool isTilted;
    private bool isFalling;
    private bool isFlying;
    private float fallCheckWaitTime;
    private int movementCommands;
    private string facing;
    private float rotation;
    private Vector2 velocity;
    private Vector2 acceleration;
    private Rigidbody2D rigidBody;
    private int FALLWAIT = 2; // How long grounded 'till not falling (i.e. not rolling down a mountain)
    private int MAXOFFSET = 60;
    private int MINOFFSET = 25;

    public bool isCameraZoomed;
    public bool isCameraLocked;

    void Start()
    {
        isActive = false;
        needsImpulse = false;
        isTilted = false;
        isFalling = false;
        isCameraZoomed = true;
        isCameraLocked = false;
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

        if (velocity == Vector2.zero)
        {
            animationController.SetBool("isWalking", false);
            EventController.current.SoundEvent($"{ConfigController.SOUNDWALK}", $"{EventController.STOP}", 0.3f, true);
        }
        else
        {
            animationController.SetBool("isWalking", true);
            EventController.current.SoundEvent($"{ConfigController.SOUNDWALK}", $"{EventController.PLAY}", 0.3f, true);
        }

        // Flip for facing
        if (transform.up.y > 0)  // Upright only
        {
            if (facing == "left")
            {
                Vector3 facingVector = transform.localScale;
                facingVector.x = 1;
                transform.localScale = facingVector;
            }
            else
            {
                Vector3 facingVector = transform.localScale;
                facingVector.x = -1;
                transform.localScale = facingVector;
            }
        }

        // Jet
        if (hasJetpack)
        {
            if (isFlying)
            {
                foreach (var sfx in particleList)
                    sfx.Play();
                animationController.SetBool("isFlying", true);
                EventController.current.SoundEvent($"{ConfigController.SOUNDENGINE}", $"{EventController.PLAY}", 0.8f, true);
                if (!isCameraLocked)
                    isCameraZoomed = false;
            }
            else
            {
                foreach (var sfx in particleList)
                    sfx.Stop();
                animationController.SetBool("isFlying", false);
                EventController.current.SoundEvent($"{ConfigController.SOUNDENGINE}", $"{EventController.STOP}", 0.8f, true);
                if (!isCameraLocked)
                    isCameraZoomed = true;

            }
        }
        else
        {
        }

        if (isCameraZoomed)
        {
            // Zoom in while not
            var cameraTransposer = turtleCamera.GetCinemachineComponent<CinemachineTransposer>();
            Vector3 newOffset = cameraTransposer.m_FollowOffset;
            newOffset.z += newOffset.z > MINOFFSET ? -0.2f : 0;
            cameraTransposer.m_FollowOffset = newOffset;
        }
        else
        {
            // Zoom out while flying
            var cameraTransposer = turtleCamera.GetCinemachineComponent<CinemachineTransposer>();
            Vector3 newOffset = cameraTransposer.m_FollowOffset;
            newOffset.z += newOffset.z < MAXOFFSET ? 0.5f : 0;
            cameraTransposer.m_FollowOffset = newOffset;
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
            rotation = 30f;
            // Debug.Log($"Angular Velocity: {rigidBody.angularVelocity}");
            if (Mathf.Abs(rigidBody.angularVelocity) < rotation)
                if (facing == "left")
                    rigidBody.AddTorque(20);
                else
                    rigidBody.AddTorque(-20);

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

        // Jet physics
        if (isFlying)
        {
            var jetForce = jetVector1.transform.position - jetVector2.transform.position;
            rigidBody.AddForce(jetForce * 2, ForceMode2D.Impulse);
        }
        // rigidBody.velocity += acceleration;

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
        {
            if (id == EventController.LEFT || id == EventController.RIGHT)
            {
                movementCommands += 1;
                if (id == EventController.LEFT)  // Left is right and right is left, oops
                {
                    velocity = Vector2.right;
                    if (isGrounded())
                        facing = "left";
                }
                else if (id == EventController.RIGHT)
                {
                    velocity = Vector2.left;
                    if (isGrounded())
                        facing = "right";
                }
            }
            if (id == EventController.FLY && hasJetpack)
                isFlying = true;
        }

        if (action == EventController.RELEASE)
        {
            if (id == EventController.LEFT || id == EventController.RIGHT)
            {
                movementCommands -= 1;
                if (movementCommands == 0)
                    velocity = Vector2.zero;
                else if (movementCommands == 1)
                {
                    needsImpulse = true;
                    if (id == EventController.LEFT) // Other button is held, reverse
                    {
                        velocity = Vector2.left;
                        if (isGrounded())
                            facing = "right";
                    }
                    else
                    {
                        velocity = Vector2.right;
                        if (isGrounded())
                            facing = "left";
                    }
                }
                else
                {
                    velocity = Vector2.zero;
                    movementCommands = 0;
                }
            }

            if (id == EventController.FLY && hasJetpack)
                isFlying = false;
        }

        if (isFlying)
        {
            /*
            if (facing == "left")
                acceleration = Vector2.right * 5;
            else
                acceleration = Vector2.left * 5;
            */
        }
        else
        {
            acceleration = Vector2.zero;
        }

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

    public void SetJetpack(bool value)
    {
        hasJetpack = value;
        if (hasJetpack)
            jetObject.SetActive(true);
        else
            jetObject.SetActive(false);
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
