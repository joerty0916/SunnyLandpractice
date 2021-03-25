using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator Animator;
    protected AudioSource DeathAudio;
    protected virtual void Start()
    {
        Animator=GetComponent<Animator>();
        DeathAudio=GetComponent<AudioSource>();
    }


    public void Death(){
        Destroy(gameObject);
        //  GetComponent<Collider2D>().enabled=false;
        //   GetComponent<Rigidbody2D>().gravityScale=0;
        // GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        
    }

    public void JumpOn()
    {
        GetComponent<Collider2D>().enabled=false;
        GetComponent<BoxCollider2D>().enabled=false;
        GetComponent<Rigidbody2D>().gravityScale=0;
        GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        Animator.SetTrigger("death");
        DeathAudio.Play();
    }
}
