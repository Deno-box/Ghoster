using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Button : MonoBehaviour
{
    [SerializeField]
    private Fadecontroller fadeScript;

    // ボタンをクリックした時の処理
    public void OnClick()
    {
        // とりあえずタイトルシーン
        this.fadeScript.fadeOutStart(Common.Scene.TITLE_SCENE);
    }
}
