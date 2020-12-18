using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Button : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        // シーンを呼び出す
        Debug.Log("Button1 click!");
        SceneManager.LoadScene("DemoPlayScene");

    }
}
