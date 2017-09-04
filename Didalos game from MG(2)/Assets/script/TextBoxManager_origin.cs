using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

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

    public GameObject playerCharacter; //플레이어 캐릭터와 상호작용을 위해.
    public GameObject enemyCharacter;// 보스 캐릭터와 상호작용을 위해
    public GameObject checkingMouse;
    private Stopwatch sw = new Stopwatch();

    private int messageSize = 1;

    void Start()
    {
        Debug.Log("currentLine" + currentLine);
        endAtLine = messageSize;
        //textBox.SetActive(true);

        playerCharacter.GetComponent<PlayerMovement>().isActive = false;
        enemyCharacter.GetComponent<ShopManager>().isActive = false;
      

        if (textFile != null)
        {
           // Debug.Log("text file" + textFile.ToString());
            textLine = (textFile.text.Split('\n')); //textFile에 있는 줄 하나가 textLine이 된다.
        }

        if (endAtLine == 0)
        {
            endAtLine = textLine.Length - 1;
        }

        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/" + "dialogCheck.txt");

        //sr.ReadLine();
        string line = sr.ReadLine();
        //int lengthOfLine = line.Length;
        string[] hpValue = line.Split(' ');
        Debug.Log("file is" + "//" + hpValue[1]); //dialog 상태 체크하기 위한 것
        check = int.Parse(hpValue[1]);
        sr.Close();
        sw.Start();
    }

    private void OnDisable() //start를 onenable로 바꿔도 각 isActive 영향을 끼쳐서 야매로 이 방법 사용.
    {
        playerCharacter.GetComponent<PlayerMovement>().isActive = true;
        enemyCharacter.GetComponent<ShopManager>().isActive = true;
    }

    private void Update()
    {
        if (check == 0)
        {
            //textBox.SetActive(false);
            theText.text = textLine[currentLine];

            if (sw.ElapsedMilliseconds >= 200)
            {
                sw.Reset();
                sw.Start();
                //speedCount = 7;
                //okay = false; //initialing shooting

                //if (checkingMouse != null)
                //checkingMouse.SetActive(false);

                if (Input.GetAxis("Fire1") > 0)
                {
                    if (currentLine + 1 > endAtLine)
                    {
                        DisableTextBox();
                        // JustDoIt.enabled = true;
                    }


                    else
                        currentLine += 1; //왼쪽마우스를 누르면 케런트의 값이 1증가
                }
            }
        }


        else
        {
            DisableTextBox();
        }


    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        playerCharacter.GetComponent<PlayerMovement>().isActive = false;
        enemyCharacter.GetComponent<ShopManager>().isActive = false;

    }

    public void DisableTextBox()
    {
       textBox.SetActive(false);
        playerCharacter.GetComponent<PlayerMovement>().isActive = true; //플레이어 다시 움직일 수 있게 해주고
        enemyCharacter.GetComponent<ShopManager>().isActive = true;

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

