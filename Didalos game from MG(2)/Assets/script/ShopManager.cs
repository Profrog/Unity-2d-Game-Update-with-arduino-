using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.IO;

public class ShopManager : MonoBehaviour {

    public GameObject menu;
    private bool menuState = false;
    private Stopwatch sw = new Stopwatch();
    private bool delayCheck = true;
    private FileStream ar;

    public bool isActive=true;

    // Use this for initialization
    void Start () {
        menu.SetActive(false);
        sw.Start();
	}
	

	// Update is called once per frame
	void Update () {

        if(!isActive)
        {
            return;
        }
	   
        else if(sw.ElapsedMilliseconds > 700)
        {
            sw.Stop();
            sw.Reset();
            sw.Start();
            delayCheck = true;
        }

	}



    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            bool check = false;
            var someScript2 = coll.transform.GetComponent<PlayerMovement>();

            if (someScript2 != null)
                check = someScript2.okay;


            if (check == true && delayCheck == true)
            {
                someScript2.slie1.value = 1;
                ar = new FileStream(Application.dataPath + "/Resources/" + "userStat.txt", FileMode.Open, FileAccess.ReadWrite);
                ar.Seek(0, SeekOrigin.Begin);
                string hpValueData = "hpValue= " + "1";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(hpValueData);
                ar.Write(data, 0, data.Length);
                //var text1 = Resources.Load<GameObject>("ManageData");
                //slie1.value = text1.GetComponent<ManageData>().hpValue;
                Debug.Log("hphpValueData" + data.ToString());
                ar.Close();

                if (menuState == false)
                {
                    menu.SetActive(true);
                    menuState = true;
                    delayCheck = false;

                }

                else
                {
                    menu.SetActive(false);
                    menuState = false;
                    delayCheck = false;
                }
            }
        }
    }

}
