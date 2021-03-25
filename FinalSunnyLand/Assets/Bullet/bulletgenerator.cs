using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bulletgenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletPrefabR,bulletPrefabL;
    public Transform player;
    public int bulletMax;
    private int bulletNum=0;
    public Text bullet;


    void Start()
    {
        bullet.text=bulletMax.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F)&&bulletNum<bulletMax)
        {
            bulletMax--;
            bullet.text=bulletMax.ToString();
            if(player.localScale.x==1)
            {
                GameObject bulletPrefabRR =Instantiate(bulletPrefabR)as GameObject;
            bulletPrefabRR.transform.position=new Vector2(player.position.x,player.position.y);
            }
            else if(player.localScale.x==-1)
            {
                 GameObject bulletPrefabLL =Instantiate(bulletPrefabL)as GameObject;
            bulletPrefabLL.transform.position=new Vector2(player.position.x,player.position.y);
            }
        }
        
    // if(Input.GetKeyDown(KeyCode.F)&&player.localScale.x==1)
    //      {
           
    //         GameObject bulletPrefabRR =Instantiate(bulletPrefabR)as GameObject;
    //         bulletPrefabRR.transform.position=new Vector2(player.position.x,player.position.y);
    //     }
    //    else if(Input.GetKeyDown(KeyCode.F)&&player.localScale.x==-1)
    //     {
            
    //     }

    }
}
