using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleControlling : MonoBehaviour {

    // Use this for initialization
    public Text Title;
    public GameObject startButton;
    public GameObject exitButton;
    private float sw;
    private int textLength;
    private int textCount;
    private string original;
    private Vector3 startVector;
    private Vector3 exitVector;


    void titleDeco()
    {
        if (sw > 0.3f)
        {
            if (Title != null)
            {
                Title.text = original.Substring(0,textCount);

                textCount++;
                if (textCount > textLength)
                {
                    textCount = 1;
                }
            }
            sw = 0f;
        }
    }


	void Start() {
        sw = 0f;
        original = "labyrinth";
        Title.text = original;
        textLength = original.Length;
        Debug.Log(textLength);
        textCount = 1;

        startVector = startButton.transform.position;
        exitVector = exitButton.transform.position;
	}

 

    private void checkingButton()
    {
        if (PlayerPrefs.GetInt("downSelect") >= 1) //when you select "start" button
        {
            if(PlayerPrefs.GetInt("okay") >= 1)
            {
                Application.LoadLevel("startHouse");
            }
            startButton.transform.position = startVector + new Vector3(0, 0, -1f);
            exitButton.transform.position = exitVector;
        }

        else ////when you select "exit" button
        {
            if (PlayerPrefs.GetInt("okay") >= 1)
            {
                Application.Quit();
            }

            startButton.transform.position = startVector;
            exitButton.transform.position = exitVector + new Vector3(0, 0, -1f);
        }

    }


	// Update is called once per frame
	void Update () {
        sw += Time.deltaTime;
        titleDeco();
        checkingButton();
	}

}
