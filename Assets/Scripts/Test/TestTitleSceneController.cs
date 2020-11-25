using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTitleSceneController : MonoBehaviour
{
    Fadecontroller fadeController = null;

    // Start is called before the first frame update
    void Start()
    {
        this.fadeController = GameObject.Find("FadeCanvas").GetComponent<Fadecontroller>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーを押す or 一定時間経過後タイトルに遷移する
       // if (Input.GetKeyDown(KeyCode.Space))
           // this.fadeController.fadeOutStart(0, 0, 0, 0, "DemoPlayScene");
    }
}
