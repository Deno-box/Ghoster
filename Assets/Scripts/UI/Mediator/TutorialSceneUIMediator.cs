using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneUIMediator : MonoBehaviour
{
    [SerializeField]
    private GameObject startImage = null;
    private FadeImage startImageFade = null;
    private ResizeImage startImageSize = null;
    [SerializeField]
    private FadeImage playButtonImage = null;
    [SerializeField]
    private GameObject goalImage = null;
    private FadeImage goalImageFade = null;
    private ResizeImage goalImageSize = null;

    [SerializeField]
    private float fadeInSpeed = 0.0f;
    [SerializeField]
    private float fadeOutSpeed = 0.0f;
    [SerializeField]
    private float scaleSpeed = 0.0f;


    private bool isStartImageFadeIn = false;
    private bool isStartImageFadeOut = false;

    private void Start()
    {
        this.startImageFade = this.startImage.GetComponent<FadeImage>();
        this.startImageSize = this.startImage.GetComponent<ResizeImage>();
        this.goalImageFade = this.goalImage.GetComponent<FadeImage>();
        this.goalImageSize = this.goalImage.GetComponent<ResizeImage>();
    }


    public bool StartExecute()
    {
        // StartImageをフェードイン
        if (!this.isStartImageFadeIn)
            this.isStartImageFadeIn = this.startImageFade.Execute(FadeImage.FadeType.FadeIn, this.fadeInSpeed);
        // StartImageをフェードアウト
        else
            this.isStartImageFadeOut = this.startImageFade.Execute(FadeImage.FadeType.FadeOut, this.fadeOutSpeed);
        // StartImageを拡大する
        startImageSize.Execute(scaleSpeed);

        // 操作ボタンをフェードイン
        bool isPlayButtonFade = this.playButtonImage.Execute(FadeImage.FadeType.FadeIn, this.fadeInSpeed);

        if (this.isStartImageFadeOut && isPlayButtonFade)
            return true;
        else
            return false;
    }
    public bool GoalExecute()
    {
        return this.goalImageFade.Execute(FadeImage.FadeType.FadeIn, this.fadeInSpeed);
    }
}
