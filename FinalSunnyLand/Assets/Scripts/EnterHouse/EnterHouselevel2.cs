using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHouselevel2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Endhighlight;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("player").GetComponent<AudioSource>().enabled=false;
            FindObjectOfType<PlayerController>().hightlighttrue();
            // GameObject.Find("player").GetComponent<Rigidbody2D>().velocity=new Vector2(0,2f);
            GameObject.Find("Canvas").SetActive(false);
            FindObjectOfType<PlayerController>().jumpmax();
            Endhighlight.SetActive(true);
        }
    }
}
