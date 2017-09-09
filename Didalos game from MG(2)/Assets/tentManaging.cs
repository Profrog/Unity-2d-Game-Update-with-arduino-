using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentManaging : MonoBehaviour
{

    public int numy = 0;

    // Use this for initialization
    void Start()
    {

        string tagging = "gate" + numy.ToString();
        int check = PlayerPrefs.GetInt(tagging);

        if (check >= 1)
        {
            gameObject.SetActive(true);
        }

        else
        {
            gameObject.SetActive(false);
        }

    }
}
