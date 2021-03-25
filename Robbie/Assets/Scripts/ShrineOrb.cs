using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineOrb : MonoBehaviour
{
    
    int playerlayerID;
    public GameObject ExplosionVFXPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        playerlayerID=LayerMask.NameToLayer("Player");
        GameManager.RegisterOrb(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==playerlayerID)
        {
            Instantiate(ExplosionVFXPrefab,transform.position,transform.rotation);
            AudioManager.PlayerordAudio();
            GameManager.PlayerGrabbedOrd(this);
           gameObject.SetActive(false);
          
        }
    }
}
