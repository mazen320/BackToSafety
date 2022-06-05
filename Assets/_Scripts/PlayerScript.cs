using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.8f;
    public float playerSprint = 3f;

    [Header("Player Camera")]
    public Transform playerCamera;

    [Header("Gravity & Animator")]
    public CharacterController cC;
    public float gravity = -9.81f; // gravity number
    public Animator anim;

    [Header("Jumping & velocity")]
    public float turnTime = 0.1f;   //for smooth rotation
    float turnVelocity;
    public float jumpHeight = 1f;
    Vector3 velocity;

    public Transform grounded;
    bool isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(grounded.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        playerMovement();
        Jump();
        Sprint();
    }

    void playerMovement()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if(direction.magnitude >= 0.1f)
        {

            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);
            anim.SetBool("Running", false);
            anim.SetBool("RifleWalk", false);
            anim.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //we use the y angle for the camera so its left and right rather than up and down 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Running", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("Idle", false);
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.ResetTrigger("Jump");
        }    
    }

    void Sprint()   //same thing from playermove just changed one variable to playersprint
    {
        if(Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Running", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //we use the y angle for the camera so its left and right rather than up and down 
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Running", false);
            }
        }
    }
}
