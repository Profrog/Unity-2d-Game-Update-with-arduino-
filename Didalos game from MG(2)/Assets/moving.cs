using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;

[SerializeField]
public class moving : MonoBehaviour
{

    //public AudioSource music;
    //public AudioClip a;
    public GameObject bullet;
    public AudioClip sndEXP;
    /// //////////////
    /**/

    //public Text text1;
    //public GameObject gameObject;
    private GameObject[] bullet1 = new GameObject[100];
    private int count = 0;
    private bool startCheck = false;
    private static AndroidJavaObject _plugins;
    SerialPort arduino1;


    public Text message;
    public Text message2;
    public Text message3;
    public GameObject enemy;
    public GameObject nextGoing;
    public float X_value = 0;
    public float Y_value = 0;
    public string arduinoString = "";
    private bool count1 = true;
    public bool okay = false;
    private Stopwatch sw = new Stopwatch();
    private Stopwatch sw2 = new Stopwatch();
    AndroidJavaClass jc;
    AndroidJavaObject _activity;
    private Vector2 Moving;
    int speedCount = 10;
    Vector3 initalState = new Vector3(0, 0, 6f);
    private StreamReader sr;
    private FileStream ar;
    private int enemyNum = 4;

    public Slider slie1;






    public void androidGet_(string arg) // android use this funiction ,startCheck mean that using serial or android's library
    {
        arduinoString = arg;


        if (SerialPort.GetPortNames().Length <= 0)
            startCheck = true;


        if (arg == "")
        {
            setMessage_("Error");
            return;
        }

        else
        {
            setMessage_(arg);
            making_string(arg); //for text_writting
            setText_andMoving(arg);
            Debug.Log("android_get");
        }
    }



    void setMessage_(string line) //control line1
    {
        if (message2 != null)
        {
            message2.text = line;
            Debug.Log(line);
        }
    }



    void setMessage_(int line)// control line2
    {
        if (message != null)
        {
            if (line > 0)
            {
                message.text = "1";
            }

            else
            {
                message.text = "0";
            }
        }

    }


    void going() //getting serial line
    {
        if (SerialPort.GetPortNames().Length <= 0)
        {
            return;
        }


        if (arduino1.IsOpen)
        {
            try
            {
                string line = arduino1.ReadLine();
                androidGet_(line);
                Debug.Log(line);

            }
            catch (Exception)
            {
                return;
            }

        }

        else
        {
            if (SerialPort.GetPortNames().Length > 0)
            {
                arduino1 = new SerialPort(SerialPort.GetPortNames()[0], 115200, Parity.None, 8, StopBits.None);
                arduino1.Close();
                arduino1.Open();

                string line = arduino1.ReadLine();
                //androidGet_(line);
            }
        }

    }


    void initialUserstat_() //renew userData
    {
        var text1 = Resources.Load<GameObject>("ManageData");
        slie1.value = text1.GetComponent<ManageData>().hpValue;
        return;
    }




    void checkingCurrentCunnecty_() //connecty check
    {
        if (SerialPort.GetPortNames().Length > 0)
        {
            arduino1 = new SerialPort(SerialPort.GetPortNames()[0], 115200, Parity.None, 8, StopBits.None);
            arduino1.Close();
            arduino1.Open();
        }
    }


    //setting position
    // Use this for initialization


    void readyFile(string file)
    {

        //set User stat
        string t = "";
        sr = new StreamReader(Application.dataPath + "/Resources/" + "userStat.txt");

        sr.ReadLine();
        string line = sr.ReadLine();
        //int lengthOfLine = line.Length;
        string[] hpValue = line.Split(' ');
        Debug.Log("file is" + "//" + line);
        slie1.value = float.Parse(hpValue[1]);
        sr.Close();
    }





    void Start()
    {
        //initialUserstat_();
        checkingCurrentCunnecty_();


        ///timeWatch1///
        sw.Start();
        sw2.Start();
        ///timeWatch1///
    }



    void usingKeyBoard()
    {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");
        float shooting1 = Input.GetAxis("Fire1");
        Debug.Log("Fire!" + shooting1);
        setMessage_(keyHorizontal.ToString() + "@" + keyVertical.ToString());
        gameObject.transform.Translate(Vector2.right * 5 * Time.smoothDeltaTime * keyHorizontal);
        gameObject.transform.Translate(Vector2.up * 5 * Time.smoothDeltaTime * keyVertical);

        if (shooting1 >= 1)
        {
            shooting_();
            okay = true;
        }
    }


    void sendingDataToEnemy()
    {
        string send = gameObject.transform.position.ToString();
        //Debug.Log("enemy" + enemy.ToString());

        if (sw2.ElapsedMilliseconds > 450) //얼마나 빠르게 사용자의 위치를 추적할 것인지
            if (enemy.activeSelf)
                enemy.SendMessage("getting", send);
    }

    void timeCheck()
    {
        if (sw.ElapsedMilliseconds >= 1000)
        {
            sw.Reset();
            sw.Start();
            speedCount = 7;
            okay = false; //initialing shooting
        }

        if (sw2.ElapsedMilliseconds >= 500)
        {
            sw2.Reset();
            sw2.Start();
            //speedCount = 7;
        }

    }


    private void settingPosition() //각종 부하 물체들의 위치 재정의  
    {
        if (slie1 != null)
        {
            slie1.transform.position = gameObject.transform.position;
            slie1.transform.position += new Vector3(0, 0.2f, 0);
        }
    }


    public void enemyCheck()
    {
        if (enemy != null)
        {
            sendingDataToEnemy();


            if (enemy.GetComponent<NewBehaviourScript1>().slie1.value <= 0)
            {
                enemy.SetActive(false);
                enemy.GetComponent<NewBehaviourScript1>().slie1.transform.position = new Vector2(0, 6f);
                if (nextGoing != null)
                    nextGoing.SetActive(true);


                ar = new FileStream(Application.dataPath + "/Resources/" + "tentState.txt", FileMode.Open, FileAccess.ReadWrite);
                for (int i = 1; i <= enemyNum; i++)
                {


                    if (enemy.tag == "enemy" + i)
                    {

                        ar.Seek(i * 10, SeekOrigin.Begin);

                        //gate1= 0
                        string gateData = "gate" + i + "=" + ":" + "1";
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(gateData);
                        ar.Write(data, 0, data.Length);
                        ar.Close();
                    }
                }

            }

        }
    }



    void UserMapInitial()
    {
        ar = new FileStream(Application.dataPath + "/Resources/" + "tentState.txt", FileMode.Open, FileAccess.ReadWrite);
        for (int i = 2; i <= enemyNum; i++)
        {
            ar.Seek(i * 10, SeekOrigin.Begin);

            //gate1= 0
            string gateData = "gate" + i + "=" + ":" + "0";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(gateData);
            ar.Write(data, 0, data.Length);
        }
        ar.Close();
    }




    void hpCheck()
    {
        if (slie1.value == 0)
        {
            UserMapInitial();
            SceneManager.LoadScene("a0");
        }
    }



    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector2(1, 1);


        if (arduino1 != null)
        {
            going(); //using serial Directly

        }



        else //using keyboard
        {
            checkingCurrentCunnecty_();
            usingKeyBoard();
        }
    }

    //



    void making_string(string line) //parsing
    {
        string[] result = new string[3];
        try
        {
            result = line.Split(' '); //split x_value, y_value


            if (System.Int32.Parse(result[1]) > 1000 || System.Int32.Parse(result[1]) > 1000)
            {
                if (sw.ElapsedMilliseconds % 200 == 0)
                {
                    speedCount--;
                }
            }


            else
            {
                speedCount = 7;
            }


            if (result[1] != null)
            {
                Y_value = (System.Int32.Parse(result[1]) - 500) / 500; //getting_result[1] value: (0~1023) ->(-1.x ~1.x)            


                if (result[2] != null)
                {
                    X_value = (System.Int32.Parse(result[2]) - 500) / 500; //getting_result[2] value: (0~1023) -> (-1.x ~ 1.x)
                    setMessage_(result[1] + "@" + result[2]);
                    Debug.Log("sending" + result[1] + " " + result[2]);
                }
            }


            if (System.Int32.Parse(result[0]) == 1)
            {
                setMessage_(1);
                okay = true;
                shooting_();
            }

            else
            {
                setMessage_(0);
            }

        }

        catch (System.Exception e)
        {
            // text1.text = "error2";

        }
    }



    void setText_andMoving(string line)//캐릭터 움직임을 구현
    {

        if (startCheck == true)
        {
            if (line != null)
            {
                message2.text = "X value is" + X_value + "Y_value is" + Y_value;

            }

            else
            {
                message2.text = "there is not string";
            }
        }

        if (X_value < 2 && Y_value < 2)
        {
            Moving = new Vector2(X_value, Y_value);
            Debug.Log("moving" + Moving);
            gameObject.transform.Translate(Moving * Time.smoothDeltaTime * speedCount);

        }
    }



    public void makingbullet_()//총알 생성
    {
        bullet1 = new GameObject[100];
        GameObject gameobj = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        gameobj.SetActive(false);

        for (int i = 0; i < 100; i++)
        {
            bullet1[i] = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            bullet1[i].transform.parent = gameObject.transform;
            bullet1[i].SetActive(false);
        }
    }


    public void shooting_()
    {
        if (count < 100)
        {
            //Debug.Log(count);
            bullet1[count].transform.position = gameObject.transform.position;
            bullet1[count].SetActive(true);
            Rigidbody2D obj = bullet1[count].GetComponent<Rigidbody2D>();
            AudioSource.PlayClipAtPoint(sndEXP, obj.transform.position);
            Vector3 arrow = new Vector3(0, 100f, 0) - gameObject.transform.position;
            obj.AddForce(800 * obj.transform.up); //회전을 하면 이상한 방향으로 튀김
            Destroy(bullet1[count], 2f);
            count++;
        }




        else
        {
            count = 0;
            makingbullet_();
        }
    }

    public void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "block")
            gameObject.transform.position = new Vector3(0, 0, 6);


        else if (coll.gameObject.tag == "attack2")
        {
            float damage = 1;
            /*var someScript = 5; //적은 시간이 지날수록 강해진다.

            
            if(someScript != null)
            damage = someScript.damage;*/


            if (slie1 != null)
                if (slie1.value > 0)
                    slie1.value -= 0.01f * damage;
        }

        else if (coll.gameObject.tag == "shoper")
        {

            Debug.Log("okay" + okay);
            if (okay == true)
            {

                //Transform a = coll.gameObject.transform.GetChild(1);
                // Debug.Log("AA" + a.ToString());
            }
        }


        //Debug.Log("collision" + coll.gameObject.tag);
    }
}