using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTask : ITutorialTask
{
    int inputCounter = 0;
    int inputCounterMax = 3;

    float lastStickHori = 0.0f;

    public string GetTitle()
    {
        return "基本動作　移動 " + inputCounter + "/" + inputCounterMax;
    }
    public string GetText()
    {
        return "左スティックを倒すと移動ができるよ!";
    }
    public void OnTaskSetting()
    {

    }
    public bool CheckTast()
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        if (stickHori != 0.0f && lastStickHori == 0.0f)
            inputCounter++;

        if(inputCounter >= inputCounterMax)
            return true;

        lastStickHori = stickHori;

        return false;
    }
}
