using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI組件")]
    public Text textlabel;
    public Image  faceImager;
    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    List<string> textlist= new List<string>();
    bool textfinish;
    bool cancelTyping;
    [Header("頭像")]
    public Sprite face01,face02;
    
    // Start is called before the first frame update
    private void Awake() {
        getFileFromFile(textFile);
    }
    private void OnEnable() {
        // textlabel.text=textlist[index];
        // index++;
        StartCoroutine(SetTextUI());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&&index==textlist.Count)
        {
            gameObject.SetActive(false);
            index=0;
            return;
        }
        // if(Input.GetKeyDown(KeyCode.R)&&textfinish)
        // {
        //     // textlabel.text=textlist[index];
        //     // index++;
        //     StartCoroutine(SetTextUI());
        // }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(textfinish&&!cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if(!textfinish)
            {
                cancelTyping=!cancelTyping;
            }
        }
    }
    void getFileFromFile(TextAsset file)
    {
        textlist.Clear();
        index=0;
        var lineDate=file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textlist.Add(line);
        }

    }
    IEnumerator SetTextUI()
    {
        textfinish=false;
        textlabel.text="";
        switch(textlist[index])
        {
            case "A":
            faceImager.sprite=face01;
            index++;
            break;
            case "B":
            faceImager.sprite=face02;
            index++;
            break;


        }
        // for (int i = 0; i < textlist[index].Length; i++)
        // {
        //     textlabel.text+=textlist[index][i];
        //     yield return new WaitForSeconds(0.1f);
        // }
        int letter=0;
        while(!cancelTyping&&letter<textlist[index].Length-1)
        {
            textlabel.text+=textlist[index][letter];
            letter++;
            yield return new WaitForSeconds(0.1f);
        }
        textlabel.text=textlist[index];
        cancelTyping=false;
        textfinish=true;
         index++;
    }
}
