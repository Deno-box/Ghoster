using UnityEngine;

public class EnemyBulletTask : ITutorialTask
{
    int inputCounter = 0;
    int inputCounterMax = 3;

    public string GetTitle()
    {
        return "基本情報　弾の種類" + inputCounter + "/" + inputCounterMax;
    }
    public string GetText()
    {
        return "白:左右どちらでも跳ね返せるよ!\n青:[LT]左トリガーで跳ね返せるよ!\n赤:[RT]右トリガーで跳ね返せるよ!";
    }
    public void OnTaskSetting()
    {

    }
    public bool CheckTast()
    {
        float trigger = Input.GetAxis("LRTrigger");
        if (trigger != 0.0f)
            inputCounter++;

        if (inputCounter >= inputCounterMax)
            return true;

        return false;
    }
}
