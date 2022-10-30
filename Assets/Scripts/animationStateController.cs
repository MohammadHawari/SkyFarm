using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator mAnimator;
    int IsWalkingHash;
    int IsRunningHash;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsRunningHash = Animator.StringToHash("IsRunning");

    }

    // Update is called once per frame
    void Update()
    {
        bool IsWalking = mAnimator.GetBool(IsWalkingHash);
        bool IsRunning = mAnimator.GetBool(IsRunningHash);

        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if(!IsWalking && forwardPressed)
        {
            mAnimator.SetBool(IsWalkingHash, true);
        }

        if(IsWalking && !forwardPressed)
        {
            mAnimator.SetBool(IsWalkingHash, false);
        }
        
        if(!IsRunning && (forwardPressed && runPressed))
        {
            mAnimator.SetBool(IsRunningHash, true);
        }

        if(IsRunning && (!forwardPressed || !runPressed))
        {
            mAnimator.SetBool(IsRunningHash, false);
        }


    }
}
