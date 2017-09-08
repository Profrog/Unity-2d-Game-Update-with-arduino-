using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShopManager : MonoBehaviour {

    public GameObject menu;
    private bool menuState = false;
    private float sw = 0f;
    private bool delayCheck = true;
    private FileStream ar;

    public bool isActive=true;

    // Use this for initialization
    void Start () {
        menu.SetActive(false);
        sw = 0f;
	}
	

	// Update is called once per frame
	void Update () {

        sw += Time.deltaTime; //time is going

        if(!isActive)
        {
            return;
        }
	   

        else if(sw > 0.7f)
        {     
            sw = 0f;
            delayCheck = true;
        }
	}



    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            int check = PlayerPrefs.GetInt("okay"); //showing menu

            if (check >= 1 && delayCheck == true)
            {
                if (menuState == false)
                {
                    menu.SetActive(true);
                    menuState = true;
                    delayCheck = false;
                }

                else
                {
                    menu.SetActive(false);
                    menuState = false;
                    delayCheck = false;
                }
            }
        }
    }

}
