using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScripttent3 : MonoBehaviour
{


    private void OnDisable()
    {

    }


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
        bool check = false;
        Debug.Log("tent" + coll.gameObject.tag);
        if (coll.gameObject.tag == "Player")
        {
            if (coll.transform.GetComponent<NewBehaviourScriptforHim2>() != null)
            {
                var someScript2 = coll.transform.GetComponent<NewBehaviourScriptforHim2>();
               check = someScript2.okay;
            }

            else
            {
                check = false;
            }

            Debug.Log("tent2" + check);
            //bool check = true;
            if (check == true)
            {
                //Debug.Log("check" + check);
                goingTent();

            }
        }

        else
        {
            Debug.Log("tent3" + coll.tag);
        }

    }

    public void goingTent()
    {
        OnDisable();
        SceneManager.LoadScene("a0");

    }

}
