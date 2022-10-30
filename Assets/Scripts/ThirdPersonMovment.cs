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
    void Update()
    {
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
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        else if(speed >= 9)
        {
            speed = 6;
        }



    }
}
