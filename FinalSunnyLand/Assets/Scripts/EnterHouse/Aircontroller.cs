using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircontroller : MonoBehaviour
{
    public Transform topline,buttomline;
    private float topy,buttomy;
    private bool Dowm;
    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
        topy=topline.position.y;
        buttomy=buttomline.position.y;
        Destroy(topline.gameObject);
        Destroy(buttomline.gameObject);
    }

    // Update is called once per frame
    void Update()
    {   
       if(Dowm)
       {
           transform.Translate(0,-0.01f,0);
           if(transform.position.y<buttomy)
             {
                 Dowm=false;
             }
       }else
       {
           transform.Translate(0,0.01f,0);
            if(transform.position.y>topy)
             {
                 Dowm=true;
             }
       }
        
        
    }
}
