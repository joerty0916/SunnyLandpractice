using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfrog : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Transform leftpoint,rightpoint;
    private float leftx,rightx;
    public float Speed,JumpForce;

    private bool Faceleft=true;
    // private Animator Animator;
    public LayerMask ground;
    public Collider2D coll;
    private float froglocalScale;


    protected override void Start()
    {
        base.Start();
        froglocalScale=transform.localScale.x;
        rb=GetComponent<Rigidbody2D>();
        // Animator=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx=leftpoint.position.x;
        rightx=rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       SwitchAnim();
    }
    void Movement()
    {
        if(Faceleft)
        {
            if(coll.IsTouchingLayers(ground)){
                rb.velocity=new Vector2(-Speed,JumpForce);
                Animator.SetBool("jumping",true);
            }
            if(transform.position.x<leftx)
            {
                transform.localScale=new Vector3(-froglocalScale,transform.localScale.y,transform.localScale.z);
                Faceleft=false;
            }
        }else
            {   if(coll.IsTouchingLayers(ground)){
                rb.velocity=new Vector2(Speed,JumpForce);
                Animator.SetBool("jumping",true);
            }
                if(transform.position.x>rightx){
                transform.localScale=new Vector3(froglocalScale,transform.localScale.y,transform.localScale.z);
                Faceleft=true;
            }     
        }
    }
    void SwitchAnim()
    {
        if(Animator.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Animator.SetBool("jumping",false);
                Animator.SetBool("falling",true);
            }
        }
        if(coll.IsTouchingLayers(ground)&&Animator.GetBool("falling"))
        {
            Animator.SetBool("falling",false);
        }
    }

}
