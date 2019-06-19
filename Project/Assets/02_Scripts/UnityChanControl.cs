using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class UnityChanControl : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    public GameObject MainCamera;
    public GameObject BackgroundImage;
    public GameObject BackgroundImage2;
    public GameObject GameControl;
    public float speed;
    public float smoothing;
    public float fireImmuneFrame;
    public float fireBounceDistance;
    public float fireWallMedium;
    public int Coin_save;
    public AudioClip[] audios;
    public GameObject RealGold;
    private float fireCollideDelay;
    public int saveInterval = 30;
    private Vector3 UnityChanPosition;
    private VirtualJoystick joystick;
    private VirtualJoystick2 joystick2;
    private Vector3 CameraFollowVector;
    private Quaternion qua, rotation;
    private Transform[] hp;
    private int hpcnt;
    private string floor;
    private double last;
    private int coins_number;
    public GameObject Success;
    public GameObject coinsound, FireAttackSound, LoseSound, WinSound;
    public GameObject CoinText;
    public GameObject TimeText;
    public float fire1StartSec;
    public float fire2StartSec;
    private float stageTriggerTime;
    private bool isStageStarted;
    private double timeInit;
    private bool restart = true;
    // Start is called before the first frame update
    void Start()
    {
        last = Time.realtimeSinceStartup;
        if (PlayerPrefs.GetString("step") != "1" && System.IO.File.Exists(Application.persistentDataPath + "/save.txt"))
        {

            FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            this.transform.position = new Vector3(Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()), Convert.ToSingle(sr.ReadLine()));
            this.transform.rotation = QuaternionParse(sr.ReadLine());
            floor = sr.ReadLine();
            if (floor == "SceneMap2" && SceneManager.GetActiveScene().name == "SceneMap") SceneManager.LoadScene("SceneMap2");
            Coin_save = Convert.ToInt32(sr.ReadLine());
            RealGold.GetComponent<Text>().text = Convert.ToString(Coin_save);

            sr.Close();
            fs.Close();
            Debug.Log("read");
        }

        joystick = BackgroundImage.GetComponent<VirtualJoystick>();
        joystick2 = BackgroundImage2.GetComponent<VirtualJoystick2>();
        MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
        MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

        UnityChanPosition = this.GetComponent<Transform>().position;
        CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
        MainCamera.GetComponent<Transform>().position = CameraFollowVector;
        MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        hp = new Transform[] {
            gameObject.transform.Find("Cube"),
            gameObject.transform.Find("Cube (1)"),
            gameObject.transform.Find("Cube (2)"),
        };
        hpcnt = 2;
        fireCollideDelay = -1;

        isStageStarted = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("step") != "1" && floor == "SceneMap2" && SceneManager.GetActiveScene().name == "SceneMap") SceneManager.LoadScene("SceneMap2");
        Vector3 dir = Vector3.zero;
        Vector3 dir2 = Vector3.zero;

        dir2.x = joystick.Horizontal();
        dir2.z = joystick.Vertical();

        dir.x = joystick2.Horizontal();
        dir.z = joystick2.Vertical();

        //Debug.Log(this.transform.eulerAngles.y);
        rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        dir = rotation * dir;
        if (dir.x != 0 && dir.z != 0)
        {

            //this.transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
            qua = Quaternion.LookRotation(dir.normalized);//※  將Vector3型別轉換四元數型別
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, qua, Time.deltaTime * smoothing);//四元數的插值，實現平滑過渡
            MainCamera.GetComponent<Transform>().rotation = Quaternion.Lerp(MainCamera.GetComponent<Transform>().rotation, qua, Time.deltaTime * smoothing);
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);
            /*Debug.Log(this.transform.rotation.y);
            Debug.Log(dir.x);
            Debug.Log(dir.z);*/
            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);

        }
        if (fireCollideDelay >= 0)
            fireCollideDelay--;

        if (Time.realtimeSinceStartup - last > saveInterval)
        {
            FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(transform.position.x);
            sw.WriteLine(transform.position.y);
            sw.WriteLine(transform.position.z);
            sw.WriteLine(GetComponent<Transform>().rotation);
            sw.WriteLine(SceneManager.GetActiveScene().name);
            sw.WriteLine(RealGold.GetComponent<Text>().text);
            sw.Close();
            fs.Close();
            Debug.Log("save");
            last = Time.realtimeSinceStartup;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Coin(Clone)")
        {
            Destroy(collision.gameObject);
            Instantiate(coinsound, UnityChanPosition, Quaternion.identity);
            coins_number++;

            Coin_save++;
            RealGold.GetComponent<Text>().text = Convert.ToString(Coin_save);
        }
        else if (collision.gameObject.name == "Boxshelf" && SceneManager.GetActiveScene().name == "SceneMap")
        {

            PlayerPrefs.SetString("step", "1");

            SceneManager.LoadScene("SceneMap2");
            GameObject.Find("JoystickImage").GetComponent<Image>().rectTransform.anchoredPosition = Vector3.zero;
            this.transform.position = new Vector3(-11.77f, UnityChanPosition.y, -27.7f);
            this.transform.rotation = Quaternion.Euler(0f, 96.5f, 0f);
            MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }
        else if (collision.gameObject.name == "Boxshelf2" && SceneManager.GetActiveScene().name == "SceneMap")
        {

            PlayerPrefs.SetString("step", "1");

            SceneManager.LoadScene("SceneMap2");
            GameObject.Find("JoystickImage").GetComponent<Image>().rectTransform.anchoredPosition = Vector3.zero;
            this.transform.position = new Vector3(-13.5f, UnityChanPosition.y, 105.66f);
            this.transform.rotation = Quaternion.Euler(0f, 71f, 0f);
            MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }
        else if (collision.gameObject.name == "Boxshelf" && SceneManager.GetActiveScene().name == "SceneMap2")
        {

            PlayerPrefs.SetString("step", "1");
            SceneManager.LoadScene("SceneMap");
            GameObject.Find("JoystickImage").GetComponent<Image>().rectTransform.anchoredPosition = Vector3.zero;
            this.transform.position = new Vector3(-11.77f, UnityChanPosition.y, -27.7f);
            this.transform.rotation = Quaternion.Euler(0f, 96.5f, 0f);
            MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }
        else if (collision.gameObject.name == "Boxshelf2" && SceneManager.GetActiveScene().name == "SceneMap2")
        {

            PlayerPrefs.SetString("step", "1");
            SceneManager.LoadScene("SceneMap");
            GameObject.Find("JoystickImage").GetComponent<Image>().rectTransform.anchoredPosition = Vector3.zero;
            this.transform.position = new Vector3(-20.72f, UnityChanPosition.y, 109.82f);
            this.transform.rotation = Quaternion.Euler(0f, 144.77f, 0f);
            MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }
        else if (collision.gameObject.name == "goal1" || collision.gameObject.name == "goal2" || collision.gameObject.name == "goal3")
        {
            Success.SetActive(true);
            GetComponent<Animator>().Play("WIN00");
            WinSound.GetComponent<AudioSource>().Play();
            GameControl.GetComponent<AudioSource>().Stop();
            Time.timeScale = 0;
            CoinText.GetComponent<Text>().text = Convert.ToString(coins_number);

            double time = Time.realtimeSinceStartup - timeInit;
            if (time >= 60)
            {
                Debug.Log("Time: " + Math.Floor(time / 60) + "min" + Math.Floor(time % 60) + "sec");
                TimeText.GetComponent<Text>().text = Math.Floor(time / 60) + "min" + Math.Floor(time % 60) + "sec";
            }
            else
            {
                Debug.Log("Time: " + Math.Floor(time) + "sec");
                TimeText.GetComponent<Text>().text = Math.Floor(time) + "sec";
            }
        }
        else if (fireCollideDelay == -1 && (collision.gameObject.name == "Fire" || collision.gameObject.name == "Fire(Clone)"))
        {
            fireCollideDelay = fireImmuneFrame;
            GameObject.Find("JoystickImage").GetComponent<Image>().rectTransform.anchoredPosition = Vector3.zero;
            Debug.Log("on Fire!");
            if (hpcnt != -1)
            {
                hp[hpcnt--].gameObject.SetActive(false);
                FireAttackSound.GetComponent<AudioSource>().Play();
                GetComponent<Animator>().Play("DAMAGED00");
            }

            Vector3 firePosition = collision.gameObject.GetComponent<Transform>().position;
            Vector3 nextPosition = Vector3.LerpUnclamped(firePosition, this.transform.position, fireBounceDistance);
            //nextPosition.y = new Vector3(nextPosition.x, UnityChanPosition.y, nextPosition.z);
            nextPosition.y = UnityChanPosition.y;
            RaycastHit hitInfo;
            if (Physics.Linecast(this.transform.position, nextPosition, out hitInfo))
            {
                nextPosition = Vector3.LerpUnclamped(hitInfo.point, this.transform.position, fireWallMedium);
            }
            //this.transform.position = Vector3.LerpUnclamped(firePosition, this.transform.position, 2.5f);
            if (hpcnt == -1)
            {
                GetComponent<Animator>().Play("DAMAGED01");
                LoseSound.GetComponent<AudioSource>().Play();
                PlayerPrefs.SetString("step", "0");
                //string scene = SceneManager.GetActiveScene().name;
                GameObject.Destroy(GameObject.Find("Camera2"));
                GameObject.Destroy(GameObject.Find("VirtualJoystick"));
                GameObject.Destroy(GameObject.Find("VirtualJoystick (1)"));
                GameObject.Destroy(GameObject.Find("Coin"));
                GameObject.Destroy(GameObject.Find("FB"));
                GameObject.Destroy(GameObject.Find("unitychan"));
                GameObject.Destroy(GameObject.Find("Setting"));
                GameObject.Destroy(GameObject.Find("Pause"));
                GameObject.Destroy(GameObject.Find("GameController"));
                GameObject.Destroy(GameObject.Find("Cube"));
                GameObject.Destroy(GameObject.Find("Cube (1)"));
                SceneManager.LoadScene("SceneMenu");

                restart = true;
                hp[0].gameObject.SetActive(true);
                hp[1].gameObject.SetActive(true);
                hp[2].gameObject.SetActive(true);
                hpcnt = 2;
                isStageStarted = false;
            }

            //this.transform.position = new Vector3(this.transform.position.x, UnityChanPosition.y, this.transform.position.z);
            this.transform.position = nextPosition;
            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }
        else if (!isStageStarted && collision.gameObject.name == "Stage1")
        {

            isStageStarted = true;
            Debug.Log("Stage 1 Start!!");
            coins_number = 0;
            GameControl.GetComponent<AudioSource>().clip = audios[1];
            GameControl.GetComponent<AudioSource>().Play();
            GameObject.Find("goal1").GetComponent<BoxCollider>().isTrigger = true;
            GameObject.Find("goal2").GetComponent<BoxCollider>().isTrigger = true;
            GameObject.Find("goal3").GetComponent<BoxCollider>().isTrigger = true;
            Vector3 fire1Position = new Vector3(46.77003f, -1.98f, 117.75f);
            GameObject fire1 = Instantiate(GameObject.Find("Fire"), fire1Position, GameObject.Find("Fire").GetComponent<Transform>().rotation);
            fire1.GetComponent<FireSpread>().spreadTimeSec = 5;
            fire1.GetComponent<FireSpread>().gapPixel = 3;
            fire1.GetComponent<FireSpread>().setTime(Time.realtimeSinceStartup);
            fire1.GetComponent<FireSpread>().isTemplate = false;
            fire1.GetComponent<FireSpread>().isActive = true;

            Vector3 fire2Position = new Vector3(-11.7f, -21.98f, 102.1f);
            GameObject fire2 = Instantiate(GameObject.Find("Fire"), fire2Position, GameObject.Find("Fire").GetComponent<Transform>().rotation);
            fire2.GetComponent<FireSpread>().spreadTimeSec = 5;
            fire2.GetComponent<FireSpread>().gapPixel = 3;
            fire2.GetComponent<FireSpread>().setTime(Time.realtimeSinceStartup);
            fire2.GetComponent<FireSpread>().isTemplate = false;
            fire2.GetComponent<FireSpread>().isActive = false;
            //GameObject.Find("Stage1").SetActive(false);
            timeInit = Time.realtimeSinceStartup;
        }

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SceneMap" && restart)
        {
            /*Debug.Log(timeInit);
            double time = Time.realtimeSinceStartup - timeInit;
            if (time >= 60)
            {
                Debug.Log("Time: " + Math.Floor(time / 60) + "min" + Math.Floor(time % 60) + "sec");
                TimeText.GetComponent<Text>().text = Math.Floor(time / 60) + "min" + Math.Floor(time % 60) + "sec";
            }
            else
            {
                Debug.Log("Time: " + Math.Floor(time) + "sec");
                TimeText.GetComponent<Text>().text = Math.Floor(time) + "sec";
            }
            restart = false;
            timeInit = Time.realtimeSinceStartup;*/
        }
        else if (scene.name == "SceneMap2" && restart)
        {
            /*double time = Time.realtimeSinceStartup - timeInit;
            if (time >= 60)
            {
                Debug.Log("Time: " + Math.Floor(time / 60) + "min" + Math.Floor(time % 60) + "sec");
            }
            else
            {
                Debug.Log("Time: " + Math.Floor(time) + "sec");
            }
            restart = false;
            timeInit = Time.realtimeSinceStartup;*/
        }
    }

    Quaternion QuaternionParse(string name)
    {
        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }
}
