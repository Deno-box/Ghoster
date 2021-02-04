using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTask : ITutorialTask
{
    int inputCounter = 0;
    int inputCounterMax = 3;

    public string GetTitle()
    {
        return "基本動作　ジャンプ";
    }
    public string GetText()
    {
        return "Aボタンでジャンプしよう!\n弾や障害物を飛び越えることができるよ";
    }
    public void OnTaskSetting()
    {

    }
    public bool CheckTast()
    {
        if (Input.GetKeyDown("joystick button 0"))
            inputCounter++;

        if (inputCounter > inputCounterMax)
            return true;

        return false;
    }
}
