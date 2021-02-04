using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //「2」

public class Test : MonoBehaviour
{
    int StageNum = 0;



    // Start is called before the first frame update
    void Start()
    {
        
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

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("clearStageNum", 3);

            SceneManager.LoadScene("TestRanking2");
        }
    }
}
