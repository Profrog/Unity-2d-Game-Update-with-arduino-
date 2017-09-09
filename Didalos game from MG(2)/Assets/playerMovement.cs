using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    float sw = 0f;
    public GameObject controller;

    // Use this for initialization //if i getting joystick
    void Start () {
        PlayerPrefs.SetInt("okay", 0); 
    }

    
    void controlling()
    {
        if(controller != null)
        {
            if(controller.tag == "titleController")
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    PlayerPrefs.SetInt("downSelect" , 1);
                    PlayerPrefs.SetInt("upSelect", 0);
                }

                else if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    PlayerPrefs.SetInt("downSelect", 0);
                    PlayerPrefs.SetInt("upSelect", 1);
                }                    
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        sw += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sw = 0f;
            PlayerPrefs.SetInt("okay", 1);
        }


        if (sw >= 0.1f)
        {
            sw = 0f;
            PlayerPrefs.SetInt("okay", 0);
        }


        controlling();
    }


    }
