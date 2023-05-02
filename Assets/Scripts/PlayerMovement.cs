using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float stealthMovementSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float airAcceleration;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask groundMask;

    bool isGrounded;
    bool desiredJump;
    bool isStealthMode;
    Vector3 velocity, desiredVelocity;

    //Refs
    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        GroundCheck();
        Move();
        Jump();

        if (Input.GetButtonDown("Crouch"))
            SwitchStealth();
    }

    private void FixedUpdate()
    {
        velocity = rb.velocity;

        float maxSpeedDelta = isGrounded ? acceleration * Time.deltaTime : airAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedDelta);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedDelta);

        if (desiredJump)
        {
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            desiredJump = false;
        }

        rb.velocity = velocity;
        isGrounded = false;
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        anim.SetFloat("Strafe", xMove);
        anim.SetFloat("Throttle", zMove);

        Vector3 direction = new Vector3(xMove, 0, zMove).normalized;
        desiredVelocity = isStealthMode ? direction * stealthMovementSpeed : direction * movementSpeed;
    }

    private void Jump()
    {
        if (!isGrounded) return;

        desiredJump |= Input.GetButtonDown("Jump");

        if (!desiredJump) return;

        anim.SetBool("Jumping", true);

        if (isStealthMode)
            SwitchStealth();

    }

    private void GroundCheck()
    {
        if (Physics.CheckSphere(transform.position, 0.1f, groundMask))
        {
            isGrounded = true;
            anim.SetBool("Jumping", false);
        }
    }

    private void SwitchStealth()
    {
        isStealthMode = !isStealthMode;
        anim.SetBool("StealthMode", isStealthMode);
    }
}
