using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class NewBehaviourScripttent : MonoBehaviour
{
    public GameObject forDestoring; 
    public string forTest1;
   

    public void OnTriggerStay(Collider coll)
    {
        //Debug.Log("tent" + coll.gameObject.tag);
        if (coll.gameObject.tag == "Player") //conneting other scene
        {
            int check = PlayerPrefs.GetInt("okay");
            
            if(check >= 1)
            {
                goingTent();
            }
        }

    }


    public void goingTent() //scene Connect
    {
        Debug.Log(forTest1);
        Application.LoadLevel(forTest1);
    }
}
