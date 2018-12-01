using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    //public int timeLeft = 60; //เอาไว้นับเวลาถอยหลัง เริ่มที่ 5 วิ
    //public Text Timertext;   //เอาไว้เก็บเวลานับถอยหลัง
    //public string sceneName; //เอาไว้เห็บชื่อ scene 
    //public AudioSource audioTime;
    // private float startTime;
    public Text timerText;
    private float startTime;


    // Use this for initialization
    void Start()
    {
        //StartCoroutine("LoseTime");
        //startTime = 600;
        startTime = 600;
    }

    // Update is called once per frame
    void Update()
    {
        //Timertext.text = ("Time Left = " + timeLeft);

        //ถ้าหมดเวลาจะขึ้นtimp upแล้วเปลี่ยนไปsceneต่อไป
        //if (timeLeft <= 0)
        //{
        //    StopCoroutine("LoseTime");
        //    Timertext.text = "Time up!";
        //    SceneManager.LoadScene(sceneName);
        //    audioTime.Play();
        //}

        float t = startTime - Time.fixedTime;
        //Debug.log(Time.time);
        string min = ((int)t / 60).ToString();
        string sec = (t % 60).ToString("f0");  //bugggg มันมี 60 วินาที ควรจะขึ้นแต่ 59 วิ

        timerText.text = min + ":" + sec;

        

    }

    ////เอาไว้นับเวลาถอยหลัง
    //IEnumerator LoseTime()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        timeLeft--;


    //    }
    //}
}