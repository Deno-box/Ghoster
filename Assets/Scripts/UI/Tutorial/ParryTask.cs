using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryTask : ITutorialTask
{
    int inputCounter = 0;
    int inputCounterMax = 3;

    float lastTrigger = 0.0f;

    public string GetTitle()
    {
        return "基本動作　はじき" + inputCounter + "/" + inputCounterMax;
    }
    public string GetText()
    {
        return "弾を跳ね返してみよう!\n青色の弾 : [LT]\n赤色の弾 : [RT]\n白色の弾 : [LT] or [RT]";
    }
    public void OnTaskSetting()
    {

    }
    public bool CheckTast()
    {
        float trigger = Input.GetAxis("LRTrigger");
        if (trigger != 0.0f && lastTrigger == 0.0f)
            inputCounter++;

        if (inputCounter >= inputCounterMax)
            return true;

        lastTrigger = trigger;

        return false;
    }
    public void ExitTaskSetting()
    {

    }
}
