using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireSpread : MonoBehaviour
{
    public float spreadTimeSec;
    public int type;
    public float gapPixel;
    public bool isTemplate;
    private int direction;
    private int isDuplicated;
    private Vector3 selfPosition;
    private Quaternion selfRotation;
    private double createTime;

    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        isDuplicated = 0;
        selfPosition = this.GetComponent<Transform>().position;
        selfRotation = this.GetComponent<Transform>().rotation;
        createTime = Time.realtimeSinceStartup;

        this.GetComponent<Transform>().position = selfPosition;
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("Firrrrre");

    }
    public void setTime(double t)
    {
        createTime = t;
    }
    public void setAct(bool act)
    {
        isActive = act;
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Fire(Clone)")
        {
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isTemplate || SceneManager.GetActiveScene().name == "SceneMenu")
        {
            createTime = Time.realtimeSinceStartup;
            return;
        }
        // Grow the fire in the X & Y directions
        //transform.localScale += new Vector3(Time.deltaTime/ speed, Time.deltaTime/ speed, 0f);

        if (isDuplicated < 4 && Time.realtimeSinceStartup > createTime + spreadTimeSec)
        {




            Vector3 newPosition = this.setNewPosition(isDuplicated); ;
            Vector3[] dir = new Vector3[4]{
                transform.TransformDirection(Vector3.right),
                transform.TransformDirection(Vector3.forward),
                transform.TransformDirection(Vector3.left),
                transform.TransformDirection(Vector3.back)};

            if (Physics.OverlapSphere(newPosition, 1).Length <= 0 && !Physics.Raycast(selfPosition, dir[isDuplicated], gapPixel))
            {
                GameObject newFire = Instantiate(GameObject.Find("Fire"), newPosition, selfRotation);
                newFire.GetComponent<FireSpread>().isActive = this.isActive;
            }

            isDuplicated++;

        }

    }

    void setDirection(int dir)
    {
        this.direction = dir;
    }

    Vector3 setNewPosition(int dir)
    {
        Vector3 newPosition = selfPosition;
        switch (dir)
        {
            case 0:
                newPosition.x += gapPixel;
                break;
            case 1:
                newPosition.z += gapPixel;
                break;
            case 2:
                newPosition.x -= gapPixel;
                break;
            case 3:
                newPosition.z -= gapPixel;
                break;
        }
        return newPosition;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SceneMenu")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
        if (scene.name == "SceneMap")
        {
            if (transform != null && transform.name == "Fire")
            {
                transform.position = new Vector3(transform.position.x, -22, transform.position.z);
                isActive = false;
            }
        }
        if (transform != null && scene.name == "SceneMap2")
        {
            if (transform != null && transform.name == "Fire")
            {
                transform.position = new Vector3(transform.position.x, -2, transform.position.z);
                isActive = true;
            }
        }

        Vector3 newPosition = this.GetComponent<Transform>().position;
        if (isActive)
            newPosition.y -= 20f;
        else
            newPosition.y += 20f;
        this.GetComponent<Transform>().position = newPosition;
        selfPosition = this.GetComponent<Transform>().position;
        isActive = !isActive;

    }

}
