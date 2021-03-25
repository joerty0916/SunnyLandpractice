using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDebug : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            if(GameObject.Find("Canvas/pauseMenu")==true)
            {
                GameObject.Find("Canvas/pauseMenu").SetActive(false);
                 GameObject.Find("player").GetComponent<PlayerController>().enabled=true;
                GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=true;
                Time.timeScale=1f;
            }

        }
      
    }
  
}
