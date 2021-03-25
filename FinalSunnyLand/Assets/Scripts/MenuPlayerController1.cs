using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D  rb;
    public float speed;
    public float jumpforce;
    private Animator anim;
    public int cherry;
    public Text CherryNum;
    private bool isHurt;
    public LayerMask Ground,Enemy;
    public Collider2D coll;
    public Collider2D Discoll,Circoll;
    public AudioSource JumpAudio,HurtAudio,CherryAudio,GameoverAudio;
    public Transform CellingCheck,EnemyCelling;
    public int JumpcountMax;
    public int JumpCount=0;
    public bool death=false;

    
    
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
        SwitchAnim();
    }
    void Update()
    {    
 
       Jump();
       Crounch();
       Death();
        CherryNum.text=cherry.ToString()+("   /  ");
       
    }


    void Movement()
    {
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
    // 角色跳躍
    void Jump()
    {
      if(Input.GetButtonDown("Jump")&&JumpCount<JumpcountMax){
            JumpAudio.Play();
            rb.velocity=new Vector2(0,jumpforce);
            anim.SetBool("jumping",true);
            JumpCount++;
        }
    }
    // 切換動畫
    void SwitchAnim()
    {
        
        if(!coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling",true);
    
        }
    
        if(anim.GetBool("jumping")){
            if(rb.velocity.y<0){
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if(isHurt)
        {
            Circoll.enabled=false;
            Discoll.enabled=false;
            JumpCount=JumpcountMax;
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0);
            anim.SetBool("jumping",false);
            if(Mathf.Abs(rb.velocity.x)<0.1f){
                isHurt=false;
                anim.SetBool("hurt",false);
                // anim.SetBool("idle",true);
            }
        }
        else if(coll.IsTouchingLayers(Ground)){
                anim.SetBool("falling",false);
                JumpCount=0;
                // anim.SetBool("idle",true);
        }
    }
    //收集物品，碰撞觸發器

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Collection"){
            CherryAudio.Play();
            collision.GetComponent<Animator>().Play("isGot");
            // Destroy(collision.gameObject);
            // cherry+=1;
            // CherryNum.text=cherry.ToString();
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
            if(Physics2D.OverlapCircle(EnemyCelling.position,0.5f,Enemy)&&anim.GetBool("falling")){
                enemy.JumpOn();
                rb.velocity=new Vector2(rb.velocity.x,10);
                anim.SetBool("jumping",true);
             }
             else if(transform.position.x<collision.gameObject.transform.position.x){
                rb.velocity=new Vector2(-2,rb.velocity.y);
                HurtAudio.Play();
                isHurt=true;


            }else if(transform.position.x>collision.gameObject.transform.position.x){
                rb.velocity=new Vector2(2,rb.velocity.y);
                HurtAudio.Play();
                isHurt=true;
        
 
            }
        }
        
    }
    //蹲下
    void Crounch()
    {
        if(!Physics2D.OverlapCircle(CellingCheck.position,0.2f,Ground))
        {
            if(Input.GetButton("Crouch"))
            {
            Discoll.enabled=false;
            anim.SetBool("crouching",true);
            }
              else
            {
            anim.SetBool("crouching",false);
            Discoll.enabled=true;
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



    
}


