using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    bool fadeInFlag;
    bool fadeOutFlag;
    float fadeDeltaTime = 0.0f;

    // フェードインの時間
    [SerializeField]
    float fadeInSeconds = 1.0f;
    //　フェードアウトの時間
    [SerializeField]
    float fadeOutSeconds = 1.0f;
    // 操作するオーディオソース
    [SerializeField]
    private AudioSource audio;
    // オーディオクリップのリスト
    [SerializeField]
    private List<AudioClip> audioClipList = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeInFlag)
        {
            FadeIn();
        }
        else if(fadeOutFlag)
        {
            FadeOut();
        }
    }

    //道中の時のBGM再生
    public void PlayNormalBGM()
    {
        audio.Stop();
        audio.PlayOneShot(audioClipList[0]);
    }

    //ボス戦のBGM再生
    public void PlayBossBattleBGM()
    {
        audio.Stop();
        audio.PlayOneShot(audioClipList[1]);
    }

    // フェードインフラグをTrueにする
    public void FadeInFlagOn()
    {
        fadeInFlag = true;
    }

    // フェードアウトフラグをTrueにする
    public void FadeOutFlagOn()
    {
        fadeOutFlag = true;
    }

    // フェードイン
    void FadeIn()
    {
        fadeDeltaTime += Time.deltaTime;

        if (fadeDeltaTime <= fadeInSeconds)
        {
            audio.volume = (fadeDeltaTime / fadeInSeconds);
        }
        else
        {
            fadeDeltaTime = 0.0f;
            fadeInFlag = false;
        }
    }

    void FadeOut()
    {
        fadeDeltaTime += Time.deltaTime;

        if (fadeDeltaTime <= fadeOutSeconds)
        {
            audio.volume = 1.0f - (fadeDeltaTime / fadeOutSeconds);
        }
        else
        {
            audio.volume = 0.0f;
            fadeDeltaTime = 0.0f;
            fadeOutFlag = false;
        }
    }
}
