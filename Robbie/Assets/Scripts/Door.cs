using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    int doorID;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        doorID=Animator.StringToHash("Open");
        GameManager.RegisterDoor(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {
        anim.SetTrigger(doorID);
        AudioManager.DoorOpenAudio();
    }
}
