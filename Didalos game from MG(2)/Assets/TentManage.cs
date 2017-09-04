using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Debug = UnityEngine.Debug;

public class TentManage : MonoBehaviour {


    //public GameObject tent1;
    public GameObject tent2;
    public GameObject tent3;
    public GameObject tent4;
    private StreamReader sr;
    private int sizeMap = 5; //reality is 4



    // Use this for initialization


    void checkState(int check1, GameObject objecting)
    {
        Debug.Log("lineline" + objecting.name + check1);


        if (check1 > 0)
        {
            objecting.SetActive(true);
        }


        else
            objecting.SetActive(false);
    }


   

    void Start () {
        string t = "";
        sr = new StreamReader(Application.dataPath + "/Resources/" + "tentState.txt");

        sr.ReadLine();
        for(int i = 1; i <= 4; i++)
        {
            string line = sr.ReadLine();
            string[] liney = line.Split(':');
            int check1 = Int32.Parse(liney[1]);

            Debug.Log("linelineQQ" + i + check1);

            switch (i)
            {
                case 1:
                    break;

                case 2:
                    checkState(check1, tent2);
                    break;

                case 3:
                    checkState(check1, tent3);
                    break;

                case 4:
                    checkState(check1, tent4);
                    break;

                default:
                    break;

            }
        }
   
        //int lengthOfLine = line.Length;
        //string[] hpValue = line.Split(' ');
        //Debug.Log("file is in Tent" + "//" + line);
        //slie1.value = float.Parse(hpValue[1]);
        sr.Close();
    }

}
