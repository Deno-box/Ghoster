using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    public Common.Scene.SceneNum nextSceneName = Common.Scene.SceneNum.PLAY_SCENE;
    //private FadeController fadeController = null;

    private void Start()
    {
        //fadeController = GameObject.Find("FadeCanvas").GetComponent<FadeController>();
    }

    // ボタンをクリックした時の処理
    public void OnClick()
    {
        // とりあえずプレイシーンへ遷移
        //this.fadeController.fadeOutStart((int)nextSceneName);
        FadeController.Instance.fadeOutStart((int)nextSceneName);
    }
}
