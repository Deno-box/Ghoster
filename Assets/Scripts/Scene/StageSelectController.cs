using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common;

public class StageSelectController : MonoBehaviour
{
    // フェード用キャンバス
    private GameObject fadeCanvas;

    // フェード用スクリプト
    private Fadecontroller fadeScript;

    // Start is called before the first frame update
    void Start()
    {
        fadeCanvas = GameObject.Find("FadeCanvas");
        fadeScript = fadeCanvas.GetComponent<Fadecontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        // ←キーが押されたら
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
        // →キーが押されたら
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {

        }
        // スペースキーが押されたら
        if(Input.GetKeyDown(KeyCode.Space))
        {
        }
        // Escキーが押されたら
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            fadeScript.fadeOutStart(Common.Scene.TITLE_SCENE);
        }
    }
}
