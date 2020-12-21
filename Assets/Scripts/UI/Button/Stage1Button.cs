using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Button : MonoBehaviour
{
    [SerializeField]
    private Fadecontroller fadeScript;

    // ボタンをクリックした時の処理
    void OnClick()
    {
        // とりあえずタイトルシーン
        this.fadeScript.fadeOutStart(Common.Scene.TITLE_SCENE);
    }
}
