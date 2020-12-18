using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Button : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        // シーンを呼び出す
        Debug.Log("Button3 click!");
        SceneManager.LoadScene("DemoPlayScene");
    }
}
