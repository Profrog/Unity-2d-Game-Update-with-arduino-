using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;
    
    public int startLine;
    public int endLine;

    public TextBoxManager_origin theTextBox;

    public bool firstMeet;
    private FileStream ar;

	// Use this for initialization
	void Start () {
      
        theTextBox = FindObjectOfType<TextBoxManager_origin>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag =="Player")
        {

            StreamReader sr = new StreamReader(Application.dataPath + "/Resources/" + "dialogCheck.txt");

            //sr.ReadLine();
            string line = sr.ReadLine();
            //int lengthOfLine = line.Length;
            string[] hpValue = line.Split(' ');
            Debug.Log("file is" + "//" + hpValue[1]); //dialog 상태 체크하기 위한 것
            int check1 = int.Parse(hpValue[1]);
            sr.Close();

            if (check1 == 0)
            {
                if (firstMeet)
                {
                    theTextBox.ReloadScript(theText);
                    theTextBox.currentLine = startLine;
                    theTextBox.endAtLine = endLine;
                    theTextBox.EnableTextBox();
                    firstMeet = false;
                }

                else
                {
                    startLine = 6;
                    endLine = 6;
                    theTextBox.ReloadScript(theText);
                    theTextBox.currentLine = startLine;
                    theTextBox.endAtLine = endLine;
                    theTextBox.EnableTextBox();

                }
            }


            else
            {
                theTextBox.check = check1;
            }
                
        }
    }
}
