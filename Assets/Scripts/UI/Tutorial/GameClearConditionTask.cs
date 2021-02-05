using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearConditionTask : ITutorialTask
{
    public string GetTitle()
    {
        return "基本情報　ゲームクリア";
    }
    public string GetText()
    {
        return "弾を弾いて高スコアを目指そう!!";
    }
    public void OnTaskSetting()
    {
    }
    public bool CheckTast()
    {
        return false;
    }
}
