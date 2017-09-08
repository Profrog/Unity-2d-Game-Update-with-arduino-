using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class TextBoxManager_origin : MonoBehaviour
{

    public TextAsset textFile;
    public string[] textLine;

    public GameObject textBox;
    public Text theText;

    public int currentLine;
    //public int currentLine;
    public int endAtLine; //텍스트의 마지막 줄 넘버 -1.. 0부터 카운트 하니까
    public int check = 0;

    public GameObject playerCharacter; //플레이어 캐릭터와 상호작용을 위해. //for connectin with player
    public GameObject enemyCharacter;// 보스 캐릭터와 상호작용을 위해 //wih bos

    private int messageSize = 2; //size of text
   

    void Start()
    {
       // Debug.Log("currentLine" + currentLine);
        endAtLine = messageSize;
        textBox.SetActive(true);

        playerCharacter.GetComponent<NewBehaviourScript>().isActive = false;
        enemyCharacter.GetComponent<ShopManager>().isActive = false;
      

        if (textFile != null)
        {
          textLine = (textFile.text.Split('\n')); //textFile에 있는 줄 하나가 textLine이 된다.
        }

        if (endAtLine == 0)
        {
            endAtLine = textLine.Length - 1;
        }
    }

    private void OnDisable() //start를 onenable로 바꿔도 각 isActive 영향을 끼쳐서 야매로 이 방법 사용.
    {
        playerCharacter.GetComponent<NewBehaviourScript>().isActive = true;
        enemyCharacter.GetComponent<ShopManager>().isActive = true;
    }

    private void Update()
    {

        if (PlayerPrefs.GetInt("startDialog") < 1)
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


        else
        {
            DisableTextBox();
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


    public void EnableTextBox() //enter dialog
    {
        textBox.SetActive(true);
        playerCharacter.GetComponent<NewBehaviourScript>().isActive = false;
        enemyCharacter.GetComponent<ShopManager>().isActive = false;

    }

    public void DisableTextBox()
    {
       textBox.SetActive(false);
       playerCharacter.GetComponent<NewBehaviourScript>().isActive = true; //플레이어 다시 움직일 수 있게 해주고 //dialog end
       enemyCharacter.GetComponent<ShopManager>().isActive = true;
       PlayerPrefs.SetInt("startDialog", 1);
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLine = new string[1];//remove one exist.
            textLine = (theText.text.Split('\n'));
        }
    }
}

