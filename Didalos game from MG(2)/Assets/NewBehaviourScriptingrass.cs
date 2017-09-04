using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScriptingrass : MonoBehaviour {

    public float hpValue = 0;

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () { 

    }

    

    public void OnTriggerStay(Collider coll)
    {

        if (coll.gameObject.tag == "attack2")
        {
            coll.gameObject.SetActive(false);

        }
    }

   

}
