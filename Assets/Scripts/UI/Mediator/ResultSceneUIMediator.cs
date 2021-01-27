using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSceneUIMediator : MonoBehaviour
{
    [SerializeField]
    private FadeImage startImageFade = null;
    [SerializeField]
    private float fadeSpeed = 0.0f;

    public bool StartExecute()
    {
        return this.startImageFade.Execute(FadeImage.FadeType.FadeIn, this.fadeSpeed);
    }
}
