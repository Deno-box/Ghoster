using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityEngine.SceneManagement;

public class Fadecontroller : MonoBehaviour
{
    //フェードアウト処理の開始、完了を管理するフラグ
    private bool isFadeOut = false;

    //フェードイン処理の開始、完了を管理するフラグ
    private bool isFadeIn = true;

    //透明度が変わるスピード
    [SerializeField]
    private float fadeSpeed = 0.75f;

    //画面をフェードさせるための画像をパブリックで取得
    [SerializeField]
    private Image fadeImage = null;

    //シーン遷移のための型
    private int afterScene;

    // Alpha(透明度)付きのカラーピッカー
    [ColorUsage(true, true), SerializeField]
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        this.SetColor();

        //シーン遷移が完了した際にフェードインを開始するように設定
        SceneManager.sceneLoaded += fadeInStart;
    }

    //シーン遷移が完了した際にフェードインを開始するように設定
    void fadeInStart(Scene scene, LoadSceneMode mode)
    {
        this.isFadeIn = true;
    }

    // フェードアウトスタート
    public void fadeOutStart(int _nextScene)
    {
        this.SetColor();
        this.isFadeOut = true;
        this.afterScene = _nextScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isFadeIn == true)
        {
            //不透明度を徐々に下げる
            this.color.a -= this.fadeSpeed * Time.deltaTime;

            //変更した透明度を画像に反映させる関数を呼ぶ
            this.SetColor();
            if (this.color.a <= 0)
                this.isFadeIn = false;
        }

        if (this.isFadeOut == true)
        {
            //不透明度を徐々に上げる
            this.color.a += this.fadeSpeed * Time.deltaTime;

            //変更した透明度を画像に反映させる関数を呼ぶ
            this.SetColor();
            if (this.color.a >= 1)
            {
                this.isFadeOut = false;
                SceneManager.LoadScene(this.afterScene);
            }
        }
    }
    //画像に色を代入する関数
    void SetColor()
    {
        this.fadeImage.color = this.color;
    }
}