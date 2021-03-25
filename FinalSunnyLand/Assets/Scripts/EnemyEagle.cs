using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{
    // Start is called before the first frame update
    public Transform toppoint,buttompoint;
    private Rigidbody2D rb;
    private Collider2D coll;
    private float topy,buttomy;
    // private Animator Animator;
    public float Speed;
    private bool isUp=true;
    protected override  void Start()
    {
        base.Start();
        rb=GetComponent<Rigidbody2D>();
        // Animator=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
            transform.DetachChildren();
            topy=toppoint.position.y;
            buttomy=buttompoint.position.y;
            Destroy(toppoint.gameObject);
            Destroy(buttompoint.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
      void Movement()
    {
        if(isUp)
        {
            rb.velocity=new Vector2(rb.velocity.x,Speed);
            if(transform.position.y>topy)
            {
                isUp=false;
            }
        }
        else 
        {
            rb.velocity=new Vector2(rb.velocity.x,-Speed);
            if(transform.position.y<buttomy)
            {
                isUp=true;
            }
        }
    }
}
