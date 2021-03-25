using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingHighlights : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform top;
    private float topy;

    void Start()
    {
      
        topy=top.position.y;
        // Destroy(top.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        transform.Translate(0,100f*Time.fixedDeltaTime,0);
        if(transform.position.y>topy){
            SceneManager.LoadScene(0);
        }
    }
}
