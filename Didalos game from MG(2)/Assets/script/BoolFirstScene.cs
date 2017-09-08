using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolFirstScene : MonoBehaviour {

    public GameObject textManager;

	void Start () {

        if (PlayerPrefs.GetInt("FirstScene") >= 2) //go same scene two times, dialog end
        {
            gameObject.SetActive(false);
            textManager.GetComponent<TextBoxManager_origin>().enabled = false;
            //Debug.Log("PlayerPrefs.GetInt" + PlayerPrefs.GetInt("FirstScene"));
        }
    }
}
