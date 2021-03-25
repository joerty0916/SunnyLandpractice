using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public TextMeshProUGUI orbText,timeText,deathText,gameOverText;
    // Start is called before the first frame update
    private void Awake() 
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance=this;
        DontDestroyOnLoad(this);
        
    }

    // Update is called once per frame
    public static void UpdateOrbUI(int orbCount)
    {
        instance.orbText.text=orbCount.ToString();
    }
    public static void UpdateDeathUI(int deathCount)
    {
        instance.deathText.text=deathCount.ToString();
    }
    public static void UpdateTimeUI(float timeCount)
    {
        int minutes=(int)(timeCount/60);
        float seconds=timeCount%60;
        instance.timeText.text=minutes.ToString("00")+":"+seconds.ToString("00");
    }
    public static void DisplayGameOver()
    {
        instance.gameOverText.enabled=true;
    }
}
