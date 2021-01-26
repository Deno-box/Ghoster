using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    //public FadeImage fadeImage;
    //public FadeImage.FadeType type;
    //public float speed;

    //public ResizeImage resizeImage;
    //public float scaleSpeed;

    public PlaySceneUIMediator playSceneMD;

    // Update is called once per frame
    void Update()
    {
        bool flag = playSceneMD.GoalExecute();
        Debug.Log(flag);
        //bool flag = fadeImage.Execute(type, speed);

        //resizeImage.Execute(scaleSpeed);
    }
}
