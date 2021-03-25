
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterDialoglevel2 : MonoBehaviour
{
    public GameObject enterDialog,CollectDialog,frog;
    public GameObject Ekey;
    public Transform detectplayer;
    public Transform player;
    public GameObject Endhighlight;
    
    void Update()
    {
        Vector2 playerposition=player.position;
        Vector2 detectplayerposition=detectplayer.position;
        float distance=(playerposition-detectplayerposition).magnitude;
        if(Input.GetKeyDown(KeyCode.E)&&distance<1f&&!frog){
                    enterDialog.SetActive(true);
                    GameObject.Find("player").GetComponent<AudioSource>().enabled=false;
                    GameObject.Find("player").GetComponent<Rigidbody2D>().gravityScale=0;
                    GameObject.Find("player").GetComponent<Rigidbody2D>().velocity=new Vector2(0,2f);
                    GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=false;
                     FindObjectOfType<PlayerController>().hightlighttrue();
                    // GameObject.Find("BulletGenerator").SetActive(false);
                    GameObject.Find("Canvas").SetActive(false);
                    FindObjectOfType<PlayerController>().jumpmax();
                    Endhighlight.SetActive(true);
                    Invoke("enterDialogclose",2f);
            }else if(Input.GetKeyDown(KeyCode.E)&&distance<1f){
                CollectDialog.SetActive(true);
                Invoke("CollectDialogclose",2f);
            }
    

    }


     
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {
            Ekey.SetActive(true);
            // if(!frog){
            //         enterDialog.SetActive(true);
            // }else{
            //     CollectDialog.SetActive(true);
            // }
    
        
        }
    }
     private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player")
        {
           Ekey.SetActive(false);
        
        }
     
     }
      void enterDialogclose()
    {
            enterDialog.SetActive(false);
    }
    void CollectDialogclose()
    {
            CollectDialog.SetActive(false);
    }
}
