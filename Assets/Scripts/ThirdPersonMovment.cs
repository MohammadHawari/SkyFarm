using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovment : MonoBehaviour
{



    public CharacterController controller;
    public Transform cam;
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

    #region - Gravity -

    private bool IsGrounded()
    {
        return controller.isGrounded;
    }

    private void CalculateGravity()
    {
        if(IsGrounded())
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
    }

    #endregion

    private void Awake() 
    {
        controller = GetComponent<CharacterController>();
        gravityDirection = Vector3.down;
    }


    void Update()
    {


        CalculateGravity();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        running = Input.GetKey(KeyCode.LeftShift);

        if(direction.magnitude >= 0.1f)
        {
            if(running)
            {
                speed = 9f;
            }
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Debug.Log($"speed is {speed}");
            controller.Move((moveDir * speed * Time.deltaTime) + gravityMovment);
        }
        else
        {
            if(speed > 9)
            {
                speed = 6;
            }

            controller.Move(gravityMovment);
        }



    }

    
}
