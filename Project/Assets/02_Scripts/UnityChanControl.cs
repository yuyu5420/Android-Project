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
    public float speed;
    public float smoothing;
    public float fireCollideDelay;
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
    public GameObject coinsound;
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

        if (Time.realtimeSinceStartup-last > saveInterval)
        {
            FileStream fs = new FileStream(Application.persistentDataPath + "/save.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(transform.position.x);
            sw.WriteLine(transform.position.y);
            sw.WriteLine(transform.position.z);
            sw.WriteLine(GetComponent<Transform>().rotation);
            sw.WriteLine(SceneManager.GetActiveScene().name);
            sw.Close();
            fs.Close();
            Debug.Log("save");
            last = Time.realtimeSinceStartup;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
       if(collision.gameObject.name == "Coin(Clone)"){ 
           Instantiate(coinsound, UnityChanPosition, Quaternion.identity); 
           Destroy(collision.gameObject);
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
            this.transform.position = new Vector3(-22f, UnityChanPosition.y, 110f);
            this.transform.rotation = Quaternion.Euler(0f, 155.5f, 0f);
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
           this.transform.position = new Vector3(-13.5f, UnityChanPosition.y, 107.73f);
            this.transform.rotation = Quaternion.Euler(0f, 80f, 0f);
            MainCamera.GetComponent<Transform>().rotation = this.transform.rotation;
            MainCamera.GetComponent<Transform>().eulerAngles = new Vector3(20.0f, MainCamera.GetComponent<Transform>().eulerAngles.y, MainCamera.GetComponent<Transform>().eulerAngles.z);

            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }
        else if (fireCollideDelay == -1 && hpcnt >= 0 && (collision.gameObject.name == "Fire" || collision.gameObject.name == "Fire(Clone)"))
        {
            fireCollideDelay = 60;
            GameObject.Find("JoystickImage").GetComponent<Image>().rectTransform.anchoredPosition = Vector3.zero;
            Debug.Log("on Fire!");
            hp[hpcnt--].gameObject.SetActive(false);
            Vector3 firePosition = collision.gameObject.GetComponent<Transform>().position;

            this.transform.position = Vector3.LerpUnclamped(firePosition, this.transform.position, 2.5f);
            this.transform.position = new Vector3(this.transform.position.x, UnityChanPosition.y, this.transform.position.z);
            UnityChanPosition = this.GetComponent<Transform>().position;
            CameraFollowVector = new Vector3(UnityChanPosition.x, 3.2f, UnityChanPosition.z);
            MainCamera.GetComponent<Transform>().position = CameraFollowVector;
            MainCamera.GetComponent<Transform>().Translate(Vector3.back * 5);
        }

    }

    Quaternion QuaternionParse(string name)
    {
        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }
}
