using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //「2」

public class Test : MonoBehaviour
{
    int StageNum = 0;

    //bool型の変数を作る
    private bool aaaclearflag = true;


    //aaaa
    private string aaaaaaaaStagenumber;

    //boolのプロパティゲッターを作る
    public bool aaaClearFlag
    {
        get { return this.aaaclearflag; }
    }

    // Start is called before the first frame update
    void Start()
    {
        aaaaaaaaStagenumber= SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("clearStageNum", 1); 

            SceneManager.LoadScene("TestRanking2");  
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("clearStageNum", 2);

            SceneManager.LoadScene("TestRanking2");
        }

        //if (SceneManager.GetActiveScene().name))
        //{
        //    PlayerPrefs.SetInt("clearStageNum", 3);

        //    SceneManager.LoadScene("TestRanking2");
        //}

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("FailureStageNum", 4);

            SceneManager.LoadScene("TestRanking2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.SetInt("FailureStageNum", 5);

            SceneManager.LoadScene("TestRanking2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerPrefs.SetInt("FailureStageNum", 6);

            SceneManager.LoadScene("TestRanking2");
        }

    }
}
