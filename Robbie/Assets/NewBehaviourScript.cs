using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float waitTime;
    public int y;
    
    // Start is called before the first frame update
    void Start()
    {
            
            if(waitTime%1>0)
            {
                waitTime=(int)waitTime+1;
            }else{
                waitTime=(int)waitTime;
            }
           
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
