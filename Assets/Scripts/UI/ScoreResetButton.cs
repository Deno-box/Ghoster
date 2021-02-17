using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreResetButton : MonoBehaviour
{
    public void OnClickButton()
    {
        PlayerPrefs.DeleteAll();
    }
}
