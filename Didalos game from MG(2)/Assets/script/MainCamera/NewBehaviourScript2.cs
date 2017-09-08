using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour{ //in main camera


    public AudioClip sndEXP3;

    // Use this for initialization
    void Start () {

        AudioSource.PlayClipAtPoint(sndEXP3, gameObject.transform.position); //this code is for playing backGround music
    }
	
	// Update is called once per frame
}
