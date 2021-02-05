using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverConditionTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本情報　ゲームオーバー";
    }
    public string GetText()
    {
        return "コーヒーカップに当たらないようにレーンを移動しよう!\n\n　　　　　　　　　　　　Bキーで次へ";
    }
    public void OnTaskSetting()
    {
        Time.timeScale = 0.0f;
    }
    public bool CheckTast()
    {
        if (Input.GetKeyDown("joystick button 1"))
            return true;

        return false;
    }
    public void ExitTaskSetting()
    {
        Time.timeScale = 1.0f;
    }
}
