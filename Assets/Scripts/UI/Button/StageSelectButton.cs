using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    public Common.Scene.SceneNum nextSceneName = Common.Scene.SceneNum.PLAY_SCENE;
    //private FadeController fadeController = null;
    [SerializeField]
    StageSelectController selectController;

    int select;
    private void Start()
    {
        select = selectController.GetSelecting();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // 対応したステージへ遷移
            switch (this.select)
            {
                case 0:
                    //ステージ1へ
                    FadeController.Instance.fadeOutStart((int)nextSceneName);
                    break;
                case 1:
                    //ステージ2へ
                    FadeController.Instance.fadeOutStart((int)nextSceneName);
                    break;
                case 2:
                    //ステージ3へ
                    FadeController.Instance.fadeOutStart((int)nextSceneName);
                    break;
                default:
                    break;
            }
        }
    }

    public void OnClick()
    {

    }
}
