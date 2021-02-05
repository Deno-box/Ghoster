using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTask : ITutorialTask
{
    int inputCounter = 0;
    int inputCounterMax = 3;

    float lastTrigget = 0.0f;

    public string GetTitle()
    {
        return "基本動作　はじき" + inputCounter + "/" + inputCounterMax;
    }
    public string GetText()
    {
        return "[LT] [RT] で弾をはじけるよ";
    }
    public void OnTaskSetting()
    {

    }
    public bool CheckTast()
    {
        float trigger = Input.GetAxis("LRTrigger");
        if (trigger != 0.0f && lastTrigget == 0.0f)
            inputCounter++;

        if (inputCounter >= inputCounterMax)
            return true;

        lastTrigget = trigger;

        return false;
    }
}
