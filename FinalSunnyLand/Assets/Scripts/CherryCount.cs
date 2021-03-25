using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryCount : MonoBehaviour
{
    // Start is called before the first frame update
    public void Death()
    {
        FindObjectOfType<PlayerController>().CherryCount();
        Destroy(gameObject);
    }
}
