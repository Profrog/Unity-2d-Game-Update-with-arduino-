
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class NewBehaviourScript1 : MonoBehaviour
{

    // Use this for initialization


    private float X;
    private float Y;
    private float Z;
    public GameObject[] bullet1;
    public GameObject bullet;
    public Text showingDamage;
    private Vector3 player;
    private int count = 0;
    public AudioClip sndEXP2;
    private Stopwatch sw = new Stopwatch();
    private Stopwatch sw2 = new Stopwatch();
    bool shootchecking = true;
    public float damage = 1;
    public float PlayerDamage = 0.1f;
    private int bulletCount = 5;
    public bool isActive = true;

    public Slider slie1;



    void getting(string sending)
    {
        string liney = sending.Substring(1, sending.Length - 2);

        string[] point = liney.Split(',');
        X = float.Parse(point[0]);
        Y = float.Parse(point[1]);
        Z = float.Parse(point[2]);

        player = new Vector3(X, Y, Z);
        Debug.Log("enemy" + liney);

    }


    public void makingbullet_()//총알 생성
    {
        bullet1 = new GameObject[bulletCount];
        GameObject gameobj = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        gameobj.SetActive(false);

        for (int i = 0; i < bulletCount; i++)
        {
            bullet1[i] = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            // bullet1[i].transform.parent = gameObject.transform;
            bullet1[i].SetActive(false);
        }

    }


    public void shooting_()//총알 쏘고 충전
    {
        if (shootchecking)
        {
            if (count < bulletCount)
            {
                //Debug.Log(count);
                bullet1[count].transform.position = gameObject.transform.position;
                bullet1[count].SetActive(true);
                Rigidbody obj = bullet1[count].GetComponent<Rigidbody>();
                AudioSource.PlayClipAtPoint(sndEXP2, obj.transform.position);
                Vector3 arrow = player - gameObject.transform.position;
                obj.AddForce(500 * arrow); //회전을 하면 이상한 방향으로 튀김
                Destroy(bullet1[count], 15f);
                count++;
            }


            else
            {
                count = 0;
                makingbullet_();
            }

            //Destroy(gameobj, 3.0f);
        }
    }

    void Start()
    {
        sw.Start();
        sw2.Start();
        makingbullet_();
        showingDamage.text = "";

    }

    // Update is called once per frame
    void Update()
    {

        if (!isActive)
        {
            return;
        }



        timer();
        settingPosition();

        if (slie1.value <= 0)
        {
            showingDamage.text = "";
        }
    }


    private void timer()
    {
        if (sw.ElapsedMilliseconds >= 500)
        {
            showingDamage.text = "";
            sw.Reset();
            sw.Start();
            shootchecking = true;
            damage += 0.01f;
            shooting_();
        }

        else
        {
            shootchecking = false;
        }


        if (sw2.ElapsedMilliseconds > 2000)
        {
            sw2.Reset();
            sw2.Start();

            float x2 = Random.Range(-1, 1);
            //float y2 = Random.Range(-1, 1);

            gameObject.transform.position = new Vector3(x2, 1, 6);
        }
    }




    private void settingPosition() //각종 부하 물체들의 위치 재정의
    {
        slie1.transform.position = gameObject.transform.position;
        slie1.transform.position += new Vector3(0, 0.2f, 0);
        showingDamage.transform.position = gameObject.transform.position + new Vector3(0, 0, -3f);

        //showingDamage.transform.position = new Vector3(0, 6f, 0);
    }


    void OnTriggerStay(Collider coll)
    {

        if (coll.gameObject.tag == "attack1")
        {
            if (slie1.value > 0)
                slie1.value -= PlayerDamage;
            showingDamage.text = PlayerDamage.ToString();
        }
    }

}



