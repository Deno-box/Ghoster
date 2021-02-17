using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要
using UnityEngine.SceneManagement;

public class FadeController : Singleton<FadeController>
{
    //フェードアウト処理の開始、完了を管理するフラグ
    private bool isFadeOut = false;

    //フェードイン処理の開始、完了を管理するフラグ
    private bool isFadeIn = true;
    public bool IsFadeIn
    {
        get
        {
            return isFadeIn;
        }
    }

    //透明度が変わるスピード
    [SerializeField]
    private float fadeSpeed = 0.75f;

    private GameObject fadeCanvas = null;
    private GameObject fadeImage = null;

    //シーン遷移のための型
    private int nextScene;

    // Alpha(透明度)付きのカラーピッカー
    [ColorUsage(true, true), SerializeField]
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("FadeCanvas");

        if (obj == null)
        {
            CreateFadeCanvas();
            DontDestroyOnLoad(this.fadeCanvas);
        }
        else
        {
            this.fadeCanvas = obj;
            this.fadeImage = GameObject.Find("FadeImage");
        }

        this.color.a = 1.0f;

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
        if (this.isFadeIn)
        {
            return;
        }
        else
        {
            this.SetColor();
            this.isFadeOut = true;
            this.nextScene = _nextScene;
        }
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
                SceneManager.LoadScene(this.nextScene);
                //SceneManager.LoadScene("ResultScene");
                //SceneManager.LoadScene("TestRanking2");
            }
        }
    }
    //画像に色を代入する関数
    void SetColor()
    {
        Image image = this.fadeImage.GetComponent<Image>();
        image.color = this.color;
    }

    // FadeCanvas作成
    private void CreateFadeCanvas()
    {
        // Canvas作成
        this.fadeCanvas = new GameObject("FadeCanvas");
        // コンポーネント追加
        this.fadeCanvas.AddComponent<Canvas>();
        this.fadeCanvas.AddComponent<CanvasScaler>();

        Canvas canvas = this.fadeCanvas.GetComponent<Canvas>();
        CanvasScaler canvasScaler = this.fadeCanvas.GetComponent<CanvasScaler>();

        // canvas設定
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.pixelPerfect = false;
        canvas.sortingOrder = 1;
        canvas.targetDisplay = 0;

        // CanvasScaler設定
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = 0f;
        canvasScaler.referencePixelsPerUnit = 100.0f;

        // Image作成
        this.fadeImage = new GameObject("FadeImage");
        // Canvasの子に設定
        this.fadeImage.transform.parent = this.fadeCanvas.transform;
        // コンポーネント追加
        this.fadeImage.AddComponent<Image>();

        Image image = this.fadeImage.GetComponent<Image>();

        // RectTransform設定
        RectTransform transform = this.fadeImage.GetComponent<RectTransform>();
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.sizeDelta = new Vector2(1920.0f, 1080.0f);
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Image設定
        image.color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
    }
}