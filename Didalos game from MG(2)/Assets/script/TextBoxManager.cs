﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextBoxManager : MonoBehaviour {

    public TextAsset textFile;
    public string[] textLine;

    public GameObject textBox;
    public Text theText;


    public int currentLine;
    public int endAtLine; //텍스트의 마지막 줄 넘버 -1.. 0부터 카운트 하니까
    public int check1 = 0;


    public GameObject playerCharacter; //플레이어 캐릭터와 상호작용을 위해.//with Player
    public GameObject enemyCharacter;// 보스 캐릭터와 상호작용을 위해 //with Boss

    void Start()
    {

        playerCharacter.GetComponent<NewBehaviourScript>().isActive = false;
        enemyCharacter.GetComponent<NewBehaviourScript1>().isActive = false;
     
            if (textFile != null)
            {
                textLine = (textFile.text.Split('\n')); //textFile에 있는 줄 하나가 textLine이 된다. //with textline
            }

            if (endAtLine == 0)
            {
                endAtLine = textLine.Length - 1;
            }
     
    }

    private void Update()
    {
       
            if (currentLine < endAtLine)
                theText.text = textLine[currentLine];


            if (currentLine + 1 > endAtLine)
            {
                DisableTextBox();

            }

            else
            {
                checkingNext(); //if mean that checking and next text
            }
        

    }

    public void checkingNext() //
    {
        if (PlayerPrefs.GetInt("okay") >= 1)
        {

            if (currentLine < endAtLine)
                currentLine += 1;

            PlayerPrefs.SetInt("okay", 0);
        }
    }


    public void EnableTextBox()
    {
        textBox.SetActive(true);
        playerCharacter.GetComponent<NewBehaviourScript>().isActive = false;
        enemyCharacter.GetComponent<NewBehaviourScript1>().isActive = false;
        
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        playerCharacter.GetComponent<NewBehaviourScript>().isActive = true; //플레이어 다시 움직일 수 있게 해주고
        enemyCharacter.GetComponent<NewBehaviourScript1>().isActive= true;
        
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText!=null)
        {
            textLine = new string[1];//remove one exist.
            textLine = (theText.text.Split('\n'));
        }
    }
}
