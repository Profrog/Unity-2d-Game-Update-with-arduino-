using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UI;

[SerializeField]
public class NewBehaviourScript : MonoBehaviour
{
    
    public GameObject bullet;
    private int bulletCount = 5; // it is bullet num in Clip
    private GameObject[] bullet1 = new GameObject[5]; //it is bullet clip
    public AudioClip sndEXP; //it is sound of bullet
    /// //////////////
    /**/


    private int count = 0; //it is parameter for counting int data
    private bool startCheck = false; //it is parameter about android_unity connecting succed
    private static AndroidJavaObject _plugins;
    SerialPort arduino1;


    public Text message; 
    public Text message2;
    public Text message3;


    public GameObject enemy; //it is obeject of enmey, i will control it in here
    public GameObject nextGoing; //It mean that next portal, if you kill enemy , you can show it

    public float X_value = 0; //it is saved data X_value of joystick
    public float Y_value = 0; //it is saved data Y_value of joystick

    public string arduinoString = ""; //It is for saved data using in android studio
   
    AndroidJavaClass jc;
    AndroidJavaObject _activity; //It is for connecting with android studio to unity


    float sw = 0.4f;
    float sw2 = 0.6f; //there are for stopwatch;

    private Vector2 moving; //it is vector of main character
    int speedCount = 5; // it is speed of player
    Vector3 initalState = new Vector3(0, 0, 6f);// it is user's hp position

    private int enemyNum = 4; //it mean num of enemy
    public Text showingDamage; //for showing damage
    public float enemyDamage = 1f; //it's monser's attack power, you can modify it

    public Slider slie1; //it is user's hp

    private float practiceTime = 0.4f; //it is timing of  bullet 
    public bool isActive = true; //it is about script's life
    float getting_hpValue = 0f;



    public void androidGet_(string arg) // android use this funiction ,startCheck mean that using serial or android's library
    {
        arduinoString = arg; //getting in android studio saving data by parameter
        startCheck = true; //it mean that connecting is succeed


        if (arg == "")
        {
            setMessage_3("Error");
            return;
        }


        else
        {
            setMessage_3(arg);       
            making_string(arg); //for text_writting
            setText_andMoving(arg); //setting data
        }
    }


    void setMessage_2(string line) //control line1
    {
        if (message2 != null)
        {
            message2.text = line;
            Debug.Log(line);
        }
    }

    void setMessage_3(string line) //control line1 for test texting
    {
        if (message3 != null)
        {
            message3.text = line;
            Debug.Log(line);
        }
    }


    public void androidGet_2(string arg) //it is for getting hp_Data
    {
        getting_hpValue = float.Parse(arg);

        if(getting_hpValue > 0)
        PlayerPrefs.SetFloat("getting_hpValue" ,getting_hpValue);
    }



    void going() //getting serial line
    {
        try
        {           
            arduinoString = arduino1.ReadLine();  //succeed getting arduino line
            setMessage_3("arduinoString" + arduinoString);
            making_string(arduinoString); //for text_writting
            setText_andMoving(arduinoString);
        }

        catch (Exception)
        {
            return;
        }
    }


    void checkingCurrentCunnecty_() //connecty check
    {
        if (SerialPort.GetPortNames().Length > 0)
        {
            arduino1 = new SerialPort(SerialPort.GetPortNames()[0], 9600);
            arduino1.Open();
        }
    }


    //setting position
    // Use this for initialization

       
    void Start()
    {
        slie1.value = PlayerPrefs.GetFloat("hpValue"); //setting userHP


        if (nextGoing != null)
            nextGoing.SetActive(false);  //next portal(tent) setfalse

        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.example.unity0000.myapplication5555");
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
        catch (Exception a)
        {
            Debug.Log("error");
        }
        
        ///timeWatch1///
        sw = 0f;
        sw2 = 0f;
        ///timeWatch1///

        makingbullet_(); //making bullet
        bullet.SetActive(false); //transparency
    }



    void usingKeyBoard()
    {
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");
        float shooting1 = Input.GetAxis("Fire1");
        Debug.Log("Fire!" + shooting1);
        setMessage_3(keyHorizontal.ToString() + "@" + keyVertical.ToString());
        gameObject.transform.Translate(Vector2.right * speedCount * Time.smoothDeltaTime * keyHorizontal);
        gameObject.transform.Translate(Vector2.up * speedCount * Time.smoothDeltaTime * keyVertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            setMessage_3("space");
            shooting_();
            PlayerPrefs.SetInt("okay", 1);
        }
    }

   
    void sendingDataToEnemy()
    {
        string send = gameObject.transform.position.ToString();

        if (sw2  > 0.45f) //얼마나 빠르게 사용자의 위치를 추적할 것인지 //reaction rate
            if (enemy.activeSelf)
                enemy.SendMessage("getting", send);
    }


    void timeCounting() //it is for timeCounting
    {
        sw += Time.deltaTime;
        sw2 += Time.deltaTime;
        practiceTime -= Time.deltaTime;
    }



    void timeCheck() //it is for timeChecking
    {
        if (sw >= 1f)
        {
            sw = 0f;
            speedCount = 5;
           //initialing shooting

        }

        if (sw2 >= 0.5f)
        {
            sw2 = 0f;
         
            if (showingDamage != null)
                showingDamage.text = "";

           
        }

        if (practiceTime <= 0) //it is about bullet timing
        {

            if (message3 != null)
                message3.text = "time out";
            practiceTime = 0.4f;
        }
    }



    private void settingPosition() //각종 부하 물체들의 위치 재정의 //object user's redefine 
    {
        if (slie1 != null) //user_hp position redefine
        {
            slie1.transform.position = gameObject.transform.position;
            slie1.transform.position += new Vector3(0, 0.2f, 0);
        }

        if (showingDamage != null) //damage dialog position redefine
        {
            showingDamage.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
        }
    }



    public void enemyCheck() //it is for enemyParameter control
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

                for (int i = 1; i <= enemyNum; i++)
                {
                    if (enemy.tag == "enemy" + i) //situation enemy die
                    {
                        string tagging1 = "gate" + i.ToString();
                        PlayerPrefs.SetInt(tagging1, 1);
                    }
                }

            }

        }
    }



    void UserMapInitial() //when user die, tent state is inital
    {
        for (int i = 2; i <= enemyNum; i++)
        {       
            string tagging1 = "gate" + i.ToString();
            PlayerPrefs.SetInt(tagging1, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector2(1, 1);
        if (!isActive)
        {
            return;
        }


        timeCounting();  


        if (startCheck) //situation with android studio
        {
            _activity.Call("onCreate");
        }


        else if (arduino1 != null) //situation port directly 
        {
            going(); //using serial Directly

        }


        else //using keyboard
        {
            /// checkingCurrentCunnecty_();
            
        }

        usingKeyBoard();
        timeCheck();
        settingPosition();
        enemyCheck();
    }

    //



    void making_string(string line) //parsing //about arudino parsing
    {
        string[] result = new string[3];
        try
        {
            result = line.Split(' '); //split x_value, y_value

            if (result[1] != null)
            {
                Y_value = (System.Int32.Parse(result[1]) - 500) / 500; //getting_result[1] value: (0~1023) ->(-1.x ~1.x)            


                if (result[2] != null)
                {
                    X_value = (System.Int32.Parse(result[2]) - 500) / 500; //getting_result[2] value: (0~1023) -> (-1.x ~ 1.x)
                    setMessage_3(result[1] + "@" + result[2]);
                    moving = new Vector2(X_value, Y_value);
                    gameObject.transform.Translate(moving * Time.smoothDeltaTime * 5);
                    //Debug.Log("sending" + result[1] + " " + result[2]);
                }
            }

            if (System.Int32.Parse(result[0]) == 1)
            {
                //setMessage_(1);
                PlayerPrefs.SetInt("okay", 1);
                shooting_();
            }
        }

        catch (System.Exception e)
        {
            // text1.text = "error2";

        }
    }



    void setText_andMoving(string line)//캐릭터 움직임을 구현 //about player vector
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
            moving = new Vector2(X_value, Y_value);
            Debug.Log("moving" + moving);
            gameObject.transform.Translate(moving * Time.smoothDeltaTime * speedCount);
        }
    }


    public void makingbullet_()//총알 생성 //making bullet
    {
        bullet1 = new GameObject[bulletCount];
        GameObject gameobj = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        gameobj.SetActive(false);

        for (int i = 0; i < bulletCount; i++)
        {
            bullet1[i] = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Rigidbody obj = bullet1[i].GetComponent<Rigidbody>();
        }
    }


    public void shooting_() //shootingBullet
    {
        if (count < bulletCount)
        {
            bullet1[count].transform.position = gameObject.transform.position;
            bullet1[count].SetActive(true);
            Rigidbody obj = bullet1[count].GetComponent<Rigidbody>();
            AudioSource.PlayClipAtPoint(sndEXP, obj.transform.position);
            Vector3 arrow = new Vector3(0, 100f, 0) - gameObject.transform.position;
            obj.AddForce(800 * obj.transform.up); //회전을 하면 이상한 방향으로 튀김
            Destroy(bullet1[count], 4f);
            count++;
        }

        else
        {
            count = 0;
            makingbullet_();
        }
    }


    public void OnTriggerStay(Collider coll) //when meeting component
    {
        if (message != null)
            message.text = coll.gameObject.tag;


        if (coll.gameObject.tag == "block") //user position redefine
            gameObject.transform.position = new Vector3(0, 0, 6);


        else if (coll.gameObject.tag == "tent")
        {
            PlayerPrefs.SetFloat("hpValue", slie1.value);
        }


        else if (coll.gameObject.tag == "attack2")
        {
            float damage = 1;
       
            if (slie1 != null)
            {
                if (slie1.value > 0)
                {
                    float damage1 = enemyDamage;
                    slie1.value -= damage1;
                    if (showingDamage != null)
                        showingDamage.text = damage1.ToString();
                }

                else
                {
                    if (message3 != null)
                        message3.text = slie1.value.ToString();

                    PlayerPrefs.SetFloat("hpValue", 0.1f); //hp inital
                    PlayerPrefs.SetInt("startDialog", 0); //state inital
                    PlayerPrefs.SetInt("FirstScene", 0);
                    UserMapInitial();
                    Application.LoadLevel("startHouse");
                }
            }
        }

        else if(coll.gameObject.tag == "shoper")
        {
            if (PlayerPrefs.GetInt("okay") >= 1) //if meeting shopper and clik it
            {
                if (PlayerPrefs.GetFloat("getting_hpValue") > 0)
                {
                    float sending_hpValue = PlayerPrefs.GetFloat("hpValue") + getting_hpValue;
                    slie1.value  = sending_hpValue;
                }

                else
                {
                    slie1.value = 1f;
                }
            }
        }

    }
}