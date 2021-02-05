using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    public Common.Scene.SceneNum nextSceneName = Common.Scene.SceneNum.PLAY_SCENE_1;
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
    }

    public void OnClick()
    {
    }
}
