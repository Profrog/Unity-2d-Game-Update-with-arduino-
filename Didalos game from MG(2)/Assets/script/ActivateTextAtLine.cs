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
        if(collision.tag =="Player")  //it is about dialog
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
    }
}
