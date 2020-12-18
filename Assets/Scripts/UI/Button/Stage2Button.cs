using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Button : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {

        // シーンを呼び出す
        Debug.Log("Button2 click!");
        SceneManager.LoadScene("DemoPlayScene");

    }
}
