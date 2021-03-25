using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu,startDialog;

    public AudioMixer audiomixer;
  

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public  void Quit() {
        {
            Application.Quit();
        }
    }
    public void UIEnable()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
         GameObject.Find("player").GetComponent<PlayerController>().enabled=false;
         GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=false;
        Time.timeScale=0f;
       if(startDialog){
           startDialog.SetActive(false);
       }
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale=1f;
        if(startDialog!=null)
        {
            startDialog.SetActive(true);
            

        }else{

        GameObject.Find("player").GetComponent<PlayerController>().enabled=true;
        GameObject.Find("BulletGenerator").GetComponent<bulletgenerator>().enabled=true;
        }
    }
     public void ReturnTitle()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SetVolume(float value)
    {
        audiomixer.SetFloat("MainVolume",value);
    }
    public void Return()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
         GameObject.Find("Canvas/MainMenu/LoadUI").SetActive(false);
    }

    public void Loadlevel()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(false);
         GameObject.Find("Canvas/MainMenu/LoadUI").SetActive(true);
    }
    public void Loadlevel1()
    {
        SceneManager.LoadScene(1);
    }
    public void Loadlevel2()
    {
         SceneManager.LoadScene(2);
    }
    public void Loadlevel3()
    {
         SceneManager.LoadScene(3);
    }

    

}
