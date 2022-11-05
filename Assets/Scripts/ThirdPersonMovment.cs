using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovment : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    PlayerInteraction playerInteraction; 
    public float speed = 6f;

    bool running;
    bool walking;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // Update is called once per frame


    [Header("Gravity")]
    public float gravity = 10;
    public float constantGravity = -0.6f;
    public float maxGravity = -15;

    private Vector3 gravityDirection;
    private Vector3 gravityMovment;
    private float currentGravity;

    [Header("Jump")]
    public float jumpForce = 5.0f; 
    private float verticalVelocity;
    private bool jumpingTrigger;

    #region - Gravity -

    private bool IsGrounded()
    {
        return controller.isGrounded;
    }

    private void CalculateGravity()
    {
        if(IsGrounded() && !jumpingTrigger)
        {
            currentGravity = constantGravity;
        }
        else
        {
            if(currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        gravityMovment = gravityDirection * -currentGravity * Time.deltaTime;
        controller.Move(gravityMovment);
    }


    #endregion

    #region - Movment -

    void PlayerMovment()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        running = Input.GetKey(KeyCode.LeftShift);

        if(direction.magnitude >= 0.1f)
        {
            if(jumpingTrigger)
            {
                speed = 6f;
                running = false;
            }
            if(running)
            {
                speed = 9f;
            }
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // Debug.Log($"speed is {speed}");
            controller.Move((moveDir * speed * Time.deltaTime) + gravityMovment);
        }
        else
        {
            if(speed >= 9)
            {
                speed = 6;
            }
        }
    }

    #endregion

    #region - Jump -

    void Jump()
    {
        if(!IsGrounded())
        {
            return;
        }


        else if(Input.GetKey(KeyCode.Space))
        {
            jumpingTrigger = true;
            currentGravity = jumpForce;
            animator.SetTrigger("IsJumping");

        }
        else
        {
            jumpingTrigger = false;       
        }
        


        // if(IsGrounded())
        // {
        //     verticalVelocity = -gravity * Time.deltaTime;
        //     if(Input.GetKey(KeyCode.Space))
        //     {
        //         verticalVelocity = 0;
        //         verticalVelocity = jumpForce;
        //         Vector3 moveVector = new Vector3(0,verticalVelocity,0);
        //         controller.Move(moveVector * Time.deltaTime);
        //     }

        // }
    }

    #endregion

    #region - interact -

    public void Interact()
    {
        //Tool interaction
        if (Input.GetButtonDown("Fire1"))
        {
            //Interact
            playerInteraction.Interact(); 
        }

        //TODO: Set up item interaction
    }


    #endregion

    #region - Awake -
    private void Awake() 
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren(typeof(Animator)) as Animator;
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        gravityDirection = Vector3.down;
    }

    #endregion


    void Update()
    {

        CalculateGravity();
        PlayerMovment();
        Jump();
        Interact();
    }

    
}
