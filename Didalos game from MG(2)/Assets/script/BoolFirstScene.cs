using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolFirstScene : MonoBehaviour {

    public GameObject textManager;

	void Start () {

        if (PlayerPrefs.GetInt("FirstScene") == 2)
        {
            gameObject.SetActive(false);
            textManager.GetComponent<TextBoxManager_origin>().enabled = false;
            Debug.Log("PlayerPrefs.GetInt" + PlayerPrefs.GetInt("FirstScene"));
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
