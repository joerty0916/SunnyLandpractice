using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMovement movement;
    Rigidbody2D rb;
    int isOnGroundID,isHangingID,isCrouchID,isSpeedID,isFallID;
    void Start()
    {
        anim=GetComponent<Animator>();
        movement=GetComponentInParent<PlayerMovement>();
        rb=GetComponentInParent<Rigidbody2D>();
        isOnGroundID=Animator.StringToHash("isOnGround");
        isHangingID=Animator.StringToHash("isHanging");
        isCrouchID=Animator.StringToHash("isCrouching");
        isSpeedID=Animator.StringToHash("speed");
        isFallID=Animator.StringToHash("verticalVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat(isSpeedID,Mathf.Abs(movement.xVelocity));
        // anim.SetBool("isOnGround",movement.isOnGround);
        anim.SetBool(isOnGroundID,movement.isOnGround);
        anim.SetBool(isCrouchID,movement.isCrouch);
        anim.SetBool(isHangingID,movement.isHanging);
        anim.SetFloat(isFallID,rb.velocity.y);
    }
    public void StepAudio()
    {
        if(GameManager.GameOver())
        {
            return;
        }
        AudioManager.PlayerFootstepAudio();
    }
    public void CrouchStepAudio()
    {
        AudioManager.PlayerCrouchAudio();
    }
}
