using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laddercontroller : MonoBehaviour
{
    public Transform playerposition,ladderposition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerpositionxy=playerposition.position;
        Vector2 detectplayerpositionxy=ladderposition.position;
        float distance=(playerpositionxy-detectplayerpositionxy).magnitude;
        if(distance<=0.5f&&Input.GetKeyDown(KeyCode.Q))
        {
            GameObject.Find("player").GetComponent<Animator>().SetBool("climbing",true);
        }
    }
    
}
