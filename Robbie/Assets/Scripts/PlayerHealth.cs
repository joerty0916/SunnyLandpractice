using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    int trapsLayer;
    public GameObject deathVFXPrefab;
    void Start()
    {
        trapsLayer=LayerMask.NameToLayer("Traps");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==trapsLayer)
        {
            Instantiate(deathVFXPrefab,transform.position,transform.rotation);
            AudioManager.PlayerDeathAudio();
            gameObject.SetActive(false);
            GameManager.playerdied();
        }
    }
    
    
}
