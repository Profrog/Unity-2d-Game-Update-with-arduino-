using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Statue : MonoBehaviour {


    private int enemyNum = 4;


	// Use this for initialization
	void Start () {
        FileStream ar = new FileStream(Application.dataPath + "/Resources/" + "tentState.txt", FileMode.Open, FileAccess.ReadWrite);
        for (int i = 1; i <= enemyNum; i++)
        {    
                ar.Seek(i * 10, SeekOrigin.Begin);

                //gate1= 0
                string gateData = "gate" + i + "=" + ":" + "0";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(gateData);
                ar.Write(data, 0, data.Length);               
        }
        ar.Close();


        FileStream ar2 = new FileStream(Application.dataPath + "/Resources/" + "userStat.txt", FileMode.Open, FileAccess.ReadWrite);
        ar2.Seek(0, SeekOrigin.Begin);
        string hpValueData = "hpValue= 1";
        byte[] data2 = System.Text.Encoding.UTF8.GetBytes(hpValueData);
        ar2.Write(data2, 0, data2.Length);
        ar2.Close();



        FileStream ar3 = new FileStream(Application.dataPath + "/Resources/" + "dialogCheck.txt", FileMode.Open, FileAccess.ReadWrite);
        ar3.Seek(0, SeekOrigin.Begin);
        string hpValueData3 = "dialogCheck: 0";
        byte[] data3 = System.Text.Encoding.UTF8.GetBytes(hpValueData3);
        ar3.Write(data3, 0, data3.Length);
        ar3.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
