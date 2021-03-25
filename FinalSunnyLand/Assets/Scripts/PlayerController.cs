using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D  rb;
    public float speed;
    public float jumpforce,climbspeed;
    private Animator anim;
    public int cherry;
    public Text CherryNum;
    private bool isHurt,isClimb=false,clinbmovement=false;
    public LayerMask Ground,Enemy,OnewayGround;
    public Collider2D coll;
    public Collider2D Discoll,Circoll,Discoll2;
    public AudioSource JumpAudio,HurtAudio,CherryAudio,GameoverAudio;
    public Transform CellingCheck,EnemyCelling;
    public int JumpcountMax;
    public int JumpCount=0;
    public bool death=false,hightlighttruebool=false;
    public Transform playerposition,ladderposition;

    
    
    void Start()
    {
        Time.timeScale=1f;
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        //  GameObject.Find("Canvas/StartDialog").SetActive(true);
        // Time.timeScale=0f;



 
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
       if(isHurt==false){
                 Movement();
                 
       }
       
    }
    void Update()
    {    
        Climb();
       Crounch();
       Death();
       SwitchAnim();
        CherryNum.text=cherry.ToString();
       
    }


    void Movement()
    {
        //樓梯上下判斷
        if(clinbmovement)
        {
            FindObjectOfType<OneWayPlatformController>().closecollider2D();
            // anim.SetBool("climbing",true);   
            rb.gravityScale=0;
            float Verticalmove=Input.GetAxis("Vertical");
            float updowndirection = Input.GetAxisRaw("Vertical");
            if(Verticalmove!=0){
                transform.Translate(0,updowndirection*0.1f,0);
                anim.SetFloat("climbingupdown",Mathf.Abs(updowndirection));
                // anim.SetBool("climbing",true);   
            }
            GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=false;
        
          
            
        //正常行走  
        }
    
        else{
            FindObjectOfType<OneWayPlatformController>().opencollider2D();
            GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=true;
            anim.SetFloat("climbingupdown",0);
            if(hightlighttruebool)
            {
                rb.gravityScale=0;
            }
            else{
                rb.gravityScale=3;
            }   
                anim.SetBool("idleing",true);
            //  anim.SetBool("idleing",true); 
            // anim.SetBool("climbing",false);
            // anim.SetBool("idleing",true);
            float Horizontalmove=Input.GetAxis("Horizontal");
            float Facedirection = Input.GetAxisRaw("Horizontal");
            //角色移動
            if(Horizontalmove!=0){
                
                rb.velocity=new Vector2(Horizontalmove*speed*Time.fixedDeltaTime,rb.velocity.y);
                anim.SetFloat("running",Mathf.Abs(Facedirection));
            }
            if(Facedirection!=0){
                transform.localScale=new Vector3(Facedirection,1,1);
            }
                       
            
        }

        
             
        
    }
    // 角色跳躍
    void Jump()
    {
      if(Input.GetButtonDown("Jump")&&JumpCount<JumpcountMax){
            JumpAudio.Play();
            // anim.SetBool("climbing",false);
            rb.velocity=new Vector2(0,jumpforce);
            // rb.AddForce(transform.up*jumpforce);
            anim.SetBool("jumping",true);
            // anim.SetBool("crouching",false);
            anim.SetBool("climbing",false);
            isClimb=false;
            JumpCount++;
        }
    }
    // 切換動畫
    void SwitchAnim()
    {
        if(clinbmovement)
        {
            anim.SetBool("climbing",true);
        }
        else{
           
        if(!coll.IsTouchingLayers(Ground)||coll.IsTouchingLayers(OnewayGround))
        {
            anim.SetBool("falling",true);
            anim.SetBool("climbing",false);
    
        }

        if(anim.GetBool("jumping")){
            if(rb.velocity.y<0){
                 FindObjectOfType<OneWayPlatformController>().playerbooltrue();
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if(isHurt)
        {
            Circoll.enabled=false;
            Discoll.enabled=false;
            Discoll2.enabled=false;
            JumpCount=JumpcountMax;
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0);
            anim.SetBool("jumping",false);
            // if(Mathf.Abs(rb.velocity.x)<0.1f){
                // isHurt=false;
                // anim.SetBool("hurt",false);
                // anim.SetBool("idle",true);
            // }
        }
        else if(coll.IsTouchingLayers(Ground)||coll.IsTouchingLayers(OnewayGround)){
                anim.SetBool("falling",false);
                // anim.SetBool("idleing",false);

                // if(Physics2D.OverlapCircle(CellingCheck.position,0.3f,Ground))
                // {
                //     JumpCount=JumpcountMax; 
                // }
                // else
                // {
                    JumpCount=0;
                // }
                // anim.SetBool("idle",true);
        }
        }
    }
    //收集物品，碰撞觸發器

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Collection"){
            CherryAudio.Play();
            collision.GetComponent<BoxCollider2D>().enabled=false;
            collision.GetComponent<Animator>().Play("isGot");
            // Destroy(collision.gameObject);
            // cherry+=1;
            // CherryNum.text=cherry.ToString();
        }
        if(collision.tag=="ladder"){
            isClimb=true;     
        }


    }
    // 判斷是否碰觸樓梯
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag=="ladder"){
            isClimb=false;    
            anim.SetFloat("running",0);
            anim.SetFloat("climbing",0);
        }      
    }


    private void Death(){
        if(transform.position.y<-5f&&death==false){
                death=true;
                 GameoverAudio.Play();
                 Invoke("Restart",2f);
                 GetComponent<AudioSource>().enabled=false;

        }
    }

    //碰撞敵人
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag=="Enemy")
        {
            
            Enemy enemy=collision.gameObject.GetComponent<Enemy>();
            if(Physics2D.OverlapCircle(EnemyCelling.position,0.4f,Enemy)){
    
                clinbmovement=false;
                enemy.JumpOn();
                rb.velocity=new Vector2(rb.velocity.x,12);
                anim.SetBool("jumping",true);
                isHurt=false;
                JumpCount=0;
                
        
        
             }
             else if(transform.position.x<collision.gameObject.transform.position.x&&collision.rigidbody.position.y>EnemyCelling.position.y){
                rb.velocity=new Vector2(-2,rb.velocity.y);
                HurtAudio.Play();
                isHurt=true;
                GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=false;
                Circoll.enabled=false;
                Discoll.enabled=false;



            }else if(transform.position.x>collision.gameObject.transform.position.x&&collision.rigidbody.position.y>EnemyCelling.position.y){
                rb.velocity=new Vector2(2,rb.velocity.y);
                HurtAudio.Play();
                isHurt=true;
                GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=false;
                Circoll.enabled=false;
                Discoll.enabled=false;
        
 
            }else{
                enemy.JumpOn();
                rb.velocity=new Vector2(rb.velocity.x,12);
                anim.SetBool("jumping",true);
                isHurt=false;
            }
        }
    
        
    }
    //攀爬判斷
        void Climb()
    {

        if(isClimb&&(Input.GetKey(KeyCode.W)||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S))){
             rb.velocity=new Vector2(0,0);
            clinbmovement=true;
            anim.SetBool("climbing",true);
            JumpCount=0;
            
        }
        else if(!isClimb){
            clinbmovement=false;
            // anim.SetBool("climbing",false);
             anim.SetBool("idleing",true); 
            // anim.SetBool("idleing",true);
        }

        // Vector2 playerpositionxy=playerposition.position;
        // Vector2 detectplayerpositionxy=ladderposition.position;
        // float distance=(playerpositionxy-detectplayerpositionxy).magnitude;
        // if(distance<0.6f&&Input.GetKey(KeyCode.W))
        // {
        //     rb.gravityScale=0;
        //     rb.velocity=new Vector2(0,0);
        //     isClimb=true;
        //         anim.SetBool("climbing",true);
            
        // }
        // else if(distance<0.5f)
        // {
        //     anim.SetBool("climbing",false);
        //     rb.gravityScale=3;

        // }
    }
    //蹲下
    void Crounch()
    {

    
        if(!Physics2D.OverlapCircle(CellingCheck.position,0.3f,Ground))
        {
            Jump();
            if(Input.GetButton("Crouch")&&!clinbmovement)
            {
            Discoll.enabled=false;
            anim.SetBool("crouching",true);
                if(coll.IsTouchingLayers(OnewayGround)&&Input.GetButtonDown("Crouch"))
                {
                    // StartCoroutine(FallTimer());
                }  
            }               
            else
                {
                    Discoll.enabled=true;
                    anim.SetBool("crouching",false);
                }
              
        }     
    }
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CherryCount()
    {
        cherry+=1;
    }
    public void jumpmax()
    {
        JumpcountMax=0;
    }
    public void hightlighttrue()
    {
        hightlighttruebool=true;
    }
    


    
}


