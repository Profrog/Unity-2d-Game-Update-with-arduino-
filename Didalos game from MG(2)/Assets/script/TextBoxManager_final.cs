using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class TextBoxManager_final : MonoBehaviour
{

    public TextAsset textFile;
    public string[] textLine;

    public GameObject textBox;
    public Text theText;

    public int currentLine;
    public int endAtLine; //텍스트의 마지막 줄 넘버 -1.. 0부터 카운트 하니까

    public GameObject playerCharacter; //플레이어 캐릭터와 상호작용을 위해.

    public Text JustDoIt;
    private Stopwatch sw = new Stopwatch();
  

    void Start()
    {

        playerCharacter.GetComponent<PlayerMovement>().isActive = false;
    
        if (textFile != null)
        {
            textLine = (textFile.text.Split('\n')); //textFile에 있는 줄 하나가 textLine이 된다.
        }

        if (endAtLine == 0)
        {
            endAtLine = textLine.Length - 1;
        }

        sw.Start();
    }

    private void Update()
    {

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
                    JustDoIt.enabled = true;
                }


                else
                    currentLine += 1; //왼쪽마우스를 누르면 케런트의 값이 1증가
            }
        }


        //textBox.SetActive(false);
        theText.text = textLine[currentLine];

       

       
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        playerCharacter.GetComponent<PlayerMovement>().isActive = false;
     

    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        playerCharacter.GetComponent<PlayerMovement>().isActive = true; //플레이어 다시 움직일 수 있게 해주고
      

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