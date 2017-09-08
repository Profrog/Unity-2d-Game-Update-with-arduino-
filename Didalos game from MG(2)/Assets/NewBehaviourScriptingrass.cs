using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScriptingrass : MonoBehaviour {


    public void OnTriggerStay(Collider coll)
    {

        if (coll.gameObject.tag == "attack2")  //guard enemy attack
        {
            coll.gameObject.SetActive(false);

        }
    }
}
