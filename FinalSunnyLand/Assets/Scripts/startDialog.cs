using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("player").GetComponent<PlayerController>().enabled=false;
        GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=false;
        // Time.timeScale=0f;
    }

    // Update is called once per frame
    void Update()
    {
             if(Input.GetKeyDown(KeyCode.R))
      {
          GameObject.Find("Canvas/Gear").SetActive(true);
          GameObject.Find("Canvas/Gear").SetActive(true);
          GameObject.Find("player").GetComponent<PlayerController>().enabled=true;
            GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=true;
        //   Time.timeScale=1f;
          Destroy(gameObject);
        
      }
    }
}
