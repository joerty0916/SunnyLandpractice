using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller1 : MonoBehaviour
{
    public Transform rightbullet,leftbullet;
    private float rightbulletx,leftbulletx;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        rightbulletx=rightbullet.position.x;
        leftbulletx=leftbullet.position.x;
        transform.DetachChildren();
        Destroy(rightbullet.gameObject);
        Destroy(leftbullet.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {   
        transform.Translate(1f,0,0);
        if(transform.position.x<rightbulletx){
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
         Enemy enemy=other.gameObject.GetComponent<Enemy>();
         if(other.tag=="Enemy")
         {
             enemy.JumpOn();

         }
        
    }
}
