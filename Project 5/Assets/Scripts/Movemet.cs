using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemet : MonoBehaviour
{
    //Base movement
    [Header("Movement")]  //Header allows for better asortment in unity inspector
    public float moveSpeed;
    public AudioClip footSteps;

    //Checking for ground
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public float groundDrag;

    //Jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool redayToJump;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public AudioClip jump;

    public Transform orientation;

    //Left, Right, Up, Down movement
    float horizontalInput;
    float verticalInput;

    //Directional movement
    Vector3 moveDirection;

    Rigidbody rb;

    public AudioSource audioSource;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        redayToJump = true;

    }

    private void Update()
    {

        //checks for ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Different key inputs
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when getting ready to jump
        if (Input.GetKey(jumpKey) && redayToJump && grounded)
        {
            redayToJump = false;

            Jump();
            //Will be able to jump contuniously 
            Invoke(nameof(RestJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //this will caluclate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //on the gorund
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
           // audioSource.PlayOneShot(footSteps, 1f);

        }

        //in the air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity to 0 to always jump the same height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //applies the force but only once = impulse
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        audioSource.PlayOneShot(jump, 1f);
    }

    private void RestJump()
    {
        redayToJump = true;
    }
}
