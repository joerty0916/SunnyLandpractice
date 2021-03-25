using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private BoxCollider2D boxcoll;
    // private CircleCollider2D circlecoll;
    [Header("移動參數")]
    public float speed=8f;
    public float crouchSpeedDivisor =3f;
    [Header("跳躍參數")]
    public float jumpForce;
    // public float jumpHoldForce;
    // public float jumpHoldDuration=0.1f;
    // public float crouchJumpBoost=2.5f;
    public float hangingJumpForce;
    public int jumpMax,jumpNum;
    [Header("衝刺參數")]
    public float sprintForce=10f;




    [Header("狀態")]
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;
    public bool isHeadBlocked;
    public bool isHanging;

    
    private float footOffset=0.4f;
    [Header("環境檢測")]
    public float headClearance;
    public float groundDistance;
    float playerHeight;
    public float eyeHieght=1.5f;
    public float grabDistance=0.4f;
    public float reachOffset=0.7f;

    public LayerMask groundLayer;
    

    // bool jumpPressed;
    // bool jumpHeld;
    bool crouchHold;


    public float xVelocity;
    //碰撞體尺寸
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        boxcoll=GetComponent<BoxCollider2D>();
        // circlecoll=GetComponent<CircleCollider2D>();
        playerHeight=boxcoll.size.y;
        colliderStandSize=boxcoll.size;
        colliderStandOffset=boxcoll.offset;
        colliderCrouchSize=new Vector2(boxcoll.size.x,boxcoll.size.y/2f);
        colliderCrouchOffset=new Vector2(boxcoll.offset.x,boxcoll.offset.y/2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GameOver())
        return;
        // jumpPressed=Input.GetButtonDown("Jump");
        // jumpHeld=Input.GetButton("Jump");
        crouchHold=Input.GetButton("Crouch");
         MidAirMovement();
    }
    private void FixedUpdate() {
        if(GameManager.GameOver())
        return;
        PhysicsCheck();
        GroundMovement();
       
    }
    void PhysicsCheck()
    {
        //左右腳射線
        RaycastHit2D leftcheck=Raycast(new Vector2(-footOffset,0f),Vector2.down,groundDistance,groundLayer);
        RaycastHit2D rightcheck=Raycast(new Vector2(footOffset,0f),Vector2.down,groundDistance,groundLayer);

        if(leftcheck||rightcheck)
        {
            isOnGround=true;
            jumpNum=1;
            isJump=false;

        }
        else
        {
              isOnGround=false;
              isJump=true;
        }
        //頭頂射線
        RaycastHit2D leftheadcheck=Raycast(new Vector2(-footOffset,boxcoll.size.y),Vector2.up,headClearance,groundLayer);
        RaycastHit2D rightheadcheck=Raycast(new Vector2(footOffset,boxcoll.size.y),Vector2.up,headClearance,groundLayer);
        if(leftheadcheck||rightheadcheck)
        {
            isHeadBlocked=true;
        }else
        {
            isHeadBlocked=false;
        }
        float direction = transform.localScale.x;
        Vector2 grabDir=new Vector2(direction,0f);
        //攀牆射線
        RaycastHit2D blockedCheck=Raycast(new Vector2(direction*footOffset,playerHeight),grabDir,grabDistance,groundLayer);
        RaycastHit2D wallCheck=Raycast(new Vector2(direction*footOffset,eyeHieght),grabDir,grabDistance,groundLayer);
        RaycastHit2D ledgeCheck=Raycast(new Vector2(direction*reachOffset,playerHeight),Vector2.down,grabDistance,groundLayer);
        if(!isOnGround&&rb.velocity.y<0f&&ledgeCheck&&wallCheck&&!blockedCheck)
        {
            Vector3 pos=transform.position;
            pos.x+=(wallCheck.distance-0.05f)*direction;
            pos.y-=ledgeCheck.distance;
            transform.position=pos;
            rb.bodyType=RigidbodyType2D.Static;
            isHanging=true;
        }
    }
    void GroundMovement()
    {
        
        if(isHanging)
            return;
        if(crouchHold)
        {
            Crouch();
        }else if(!crouchHold&&isCrouch&&!isHeadBlocked)
        {
            Standup();
        }
        xVelocity=Input.GetAxis("Horizontal");
        if(isCrouch)
        xVelocity/=crouchSpeedDivisor;
        rb.velocity=new Vector2(xVelocity*speed,rb.velocity.y);
        
        FilpDirction();
    }
    void MidAirMovement()
    {
        if(isHanging)
        {
            if(Input.GetButtonDown("Jump"))
            {
                rb.bodyType=RigidbodyType2D.Dynamic;
                 rb.velocity=new Vector2(rb.velocity.x,jumpForce);
                 isHanging=false;
            }
            if(Input.GetButtonDown("Crouch"))
            {
                rb.bodyType=RigidbodyType2D.Dynamic;
                isHanging=false;
            }
        }
        if(Input.GetButtonDown("Jump")&&jumpNum<jumpMax)
        {
            // jumpTime=Time.time+jumpHoldDuration;
            rb.velocity=new Vector2(rb.velocity.x,jumpForce);
            isOnGround=false;
            jumpNum++;
            AudioManager.PlayerJumpAudio();
        }
        // else if(isJump)
        // {
        //     // if(jumpHeld)
        //     // rb.AddForce(new Vector2(0f,jumpHoldForce*Time.deltaTime),ForceMode2D.Impulse);        
        //     // jumpHeld=false;
        //     // isJump=false;
        //     // jumpHeld=false;
        //     // if(jumpTime<Time.time)
        //     // {
            
        //     // }
        // }
    }
    //移動方向判定
    void FilpDirction(){
        if(xVelocity<0)
        {
            transform.localScale=new Vector3(-1,1,1);
        }
        if(xVelocity>0)
        {
            transform.localScale=new Vector3(1,1,1);
        }
    }
    void Crouch()
    {
        isCrouch=true;
        boxcoll.size =colliderCrouchSize;
        boxcoll.offset =colliderCrouchOffset;
    }
    void Standup()
    {
        boxcoll.size =colliderStandSize;
        boxcoll.offset =colliderStandOffset;
        isCrouch=false;
    }
    // void jumpHeldDelay()
    // {
    //     jumpHeld=Input.GetButton("Jump");
    // }
    RaycastHit2D Raycast(Vector2 offset,Vector2 rayDiraction,float length,LayerMask layer)
    {
        Vector2 pos=transform.position;
        RaycastHit2D hit=Physics2D.Raycast(pos+offset,rayDiraction,length,layer);
        Color color=hit?Color.red:Color.green;
        Debug.DrawRay(pos+offset,rayDiraction*length,color);
        
        return hit;
    }
}
