using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorialTask
{
    string GetTitle();
    string GetText();
    void OnTaskSetting();
    bool CheckTast();
    void ExitTaskSetting();
}
