using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stamp : MonoBehaviour
{

    int FailureStage;

    bool StageClearFlag=false;

    // Start is called before the first frame update
    void Start()
    {
        FailureStage = PlayerPrefs.GetInt("FailrStageNum");
    }

    // Update is called once per frame
    void Update()
    {
        //失敗
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            SceneManager.LoadScene("TestClearScene");
        }

        //失敗
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("TestClearScene");
        }

        //失敗
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene("TestClearScene");
        }

    }
}
