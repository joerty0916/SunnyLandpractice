using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    SceneFader Fader;
    List<ShrineOrb> orbs;
    Door lockedDoor;
    float GameTime;
    bool gameIsOver;

    public int orbsNum,DeathNum;

    private void Awake() {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance=this;
        orbs=new List<ShrineOrb>();
        DontDestroyOnLoad(this);
    }
    private void Update() {
        if(gameIsOver)
        return;
        orbsNum=instance.orbs.Count;
        GameTime+=Time.deltaTime;
        UIManager.UpdateTimeUI(instance.GameTime);

    }
    public static void RegisterDoor(Door door)
    {
        instance.lockedDoor=door;
    }
    public static void RegisterScenceFader(SceneFader obj)
    {
        instance.Fader=obj;
    }
  
    public static void RegisterOrb(ShrineOrb orb)
    {
        if(!instance.orbs.Contains(orb))
        instance.orbs.Add(orb);
        UIManager.UpdateOrbUI(instance.orbs.Count);

    }
    public static void PlayerGrabbedOrd(ShrineOrb orb)
    {
        if(!instance.orbs.Contains(orb))
        return;
        instance.orbs.Remove(orb);
        if(instance.orbs.Count==0)
        {
            instance.lockedDoor.Open();
        }
        UIManager.UpdateOrbUI(instance.orbs.Count);
    }

    public static void playerdied()
    {
        instance.Fader.FadeOut();
        instance.DeathNum++;
        UIManager.UpdateDeathUI(instance.DeathNum);
        instance.Invoke("RestartScence",1.5f);
    }
    public static void PlayerWon()
    {
        instance.gameIsOver=true;
        UIManager.DisplayGameOver();
        AudioManager.PlayerWonAudio();
    }
    void RestartScence()
    {
        instance.orbs.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static bool GameOver()
    {
        return instance.gameIsOver;
    }

}
