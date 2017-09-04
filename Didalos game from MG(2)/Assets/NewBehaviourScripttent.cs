using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScripttent : MonoBehaviour {


    public GameObject forDestoring;
    public Scene gotoN;
    public string forTest1;
    private StreamReader sr;
    private StreamWriter wr;
    private FileStream ar;
    private FileStream ar2;
    public GameObject checkingDialog;


    public void OnTriggerStay(Collider coll)
    {
        Debug.Log("tent" + coll.gameObject.tag);
        if (coll.gameObject.tag == "Player")
        {
            bool check = false;
            var someScript2 = coll.transform.GetComponent<PlayerMovement>();

            if(someScript2 != null)
            check = someScript2.okay;


            Debug.Log("tent2" + check);
            //bool check = true;
            if (check == true)
            {
                //Debug.Log("check" + check); 

                //var text1 = Resources.Load<GameObject>("ManageData");
                //text1.GetComponent<ManageData>().hpValue =  someScript2.slie1.value; //convey hp data



                ar = new FileStream(Application.dataPath + "/Resources/" + "userStat.txt", FileMode.Open, FileAccess.ReadWrite);        
                ar.Seek(0,SeekOrigin.Begin);
                string hpValueData = "hpValue= " + someScript2.slie1.value.ToString();
                byte[] data = System.Text.Encoding.UTF8.GetBytes(hpValueData);
                ar.Write(data,0,data.Length);
                ar.Close();
                // sr = new StreamReader(Application.dataPath + "/Resources/" + "userStat.txt");
                //string line = sr.ReadLine();


               
                    if (checkingDialog != null)
                    {
                        ar2 = new FileStream(Application.dataPath + "/Resources/" + "dialogCheck.txt", FileMode.Open, FileAccess.ReadWrite);
                        ar2.Seek(0, SeekOrigin.Begin);
                        string hpValueData2 = "dialogCheck: 1";
                        byte[] data2 = System.Text.Encoding.UTF8.GetBytes(hpValueData2);
                        ar2.Write(data2, 0, data2.Length);
                        ar2.Close();
                    }

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
        //Destroy(forDestoring, .1f);

        Debug.Log(forTest1);
        SceneManager.LoadScene(forTest1);
  
    }
}
