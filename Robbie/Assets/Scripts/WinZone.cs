using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    // Start is called before the first frame update
    int playerlayer;
    void Start()
    {
        playerlayer=LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==playerlayer)
        {
            Debug.Log("Player WIN!!!!!!!");
            GameManager.PlayerWon();
        }
    }
}
