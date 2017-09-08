using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScripttent2 : MonoBehaviour
{
    private int enemyNum = 4;

    void UserMapInitial() //when user die, tent state is inital
    {
        for (int i = 2; i <= enemyNum; i++)
        {
            string tagging1 = "gate" + i.ToString();
            PlayerPrefs.SetInt(tagging1, 0);
        }
    }

    public void OnTriggerStay(Collider coll)
    {
        if (PlayerPrefs.GetInt("okay") >= 1)
        {
            UserMapInitial();
            PlayerPrefs.SetFloat("hpValue", 1f); //hp inital
            PlayerPrefs.SetInt("startDialog", 0); //state inital
            PlayerPrefs.SetInt("FirstScene", 0);
        }
    }
}
