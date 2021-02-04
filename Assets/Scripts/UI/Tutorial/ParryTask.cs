using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTask : ITutorialTask
{
    int inputCounter = 0;
    int inputCounterMax = 3;

    public string GetTitle()
    {
        return "基本動作　はじき";
    }
    public string GetText()
    {
        return "[LT] [RT] で弾をはじいてみよう!";
    }
    public void OnTaskSetting()
    {

    }
    public bool CheckTast()
    {
        float trigger = Input.GetAxis("LRTrigger");
        if (trigger != 0.0f)
            inputCounter++;

        if (inputCounter > inputCounterMax)
            return true;

        return false;
    }
}
