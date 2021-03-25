
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterDialoglevel1 : MonoBehaviour
{
    public GameObject enterDialog,CollectDialog;
    public Text Cherry;
    public GameObject Ekey;
    public Transform detectplayer;
    public Transform player;
    private int cherrynum;

     void Start()
    {

    }
    void Update()
    {
        
        cherrynum =int.Parse(Cherry.text);
        Vector2 playerposition=player.position;
        Vector2 detectplayerposition=detectplayer.position;
        float distance=(playerposition-detectplayerposition).magnitude;
        if(Input.GetKeyDown(KeyCode.E)&&distance<3f&&cherrynum==6)
        {
            // enterDialog.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            // Invoke("enterDialogclose",3f);
        }else if(Input.GetKeyDown(KeyCode.E)&&distance<1f)
        {
            CollectDialog.SetActive(true);
            Invoke("CollectDialogclose",2f);
        }

    }



     
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {
            Ekey.SetActive(true);
            //  int x =int.Parse(Cherry.text);
            // if(x==6){
            //         enterDialog.SetActive(true);
            //         Invoke("enterDialogclose",3f);
            // }
            // if(x<6&&)
            // {
            //     CollectDialog.SetActive(true);
            //      Invoke("CollectDialogclose",3f);
            // }
    
        
        }
    }
     private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player")
        {
            Ekey.SetActive(false);
            // enterDialog.SetActive(false);
            // CollectDialog.SetActive(false);
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
