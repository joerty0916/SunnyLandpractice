using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlatformEffector2D effector;
    public float waitTime;
    public bool playerbool=false;
    public Collider2D coll;
    void Start()
    {
        effector=GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerbool){
            if(Input.GetButtonUp("Crouch"))
                {
                    waitTime=0.2f;
                }
            if(Input.GetButton("Crouch"))
            {
                if(waitTime<=0){
                effector.rotationalOffset=180f;
                waitTime=0.2f;
                }
                else{
                    waitTime-=Time.deltaTime;
                }
            }
        }
        
        if(Input.GetButton("Jump")||Input.GetKey(KeyCode.W)){
            effector.rotationalOffset=0;
             playerbool=false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
           playerbool=true;
        }
    }
    public void playerbooltrue()
    {
        playerbool=false;
    }
    public void closecollider2D(){
        coll.enabled=false;
    }
    public void opencollider2D(){
        coll.enabled=true;
    }

    
}
