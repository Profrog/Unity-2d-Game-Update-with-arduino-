using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopManager : MonoBehaviour
{

   // public GameObject menu;
   
    private bool stateMenu = false;

    // Use this for initialization
    void Start()
    {
      

    }
    
    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            if (stateMenu == false)
            {
                //menu.setActivie(true);
                stateMenu = true;
            }


            else
            {
                //menu.setActivie(false);
                stateMenu = false;
            }

        }
    }


}