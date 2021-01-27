using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;
using UnityEngine.EventSystems;

public class StageSelectControllerOld : MonoBehaviour
{
    // ステージの数
    private const int STAGE_NUM = 3;

    //private float beforeStick = 0.0f;
    //private float beforeCross = 0.0f;

    // 選択中
    private int selecting;
    // ひとつ前の選択
    private int lastSelect;
    // フェード経過時間
    float fadeDeltaTime = 0.0f;
    // フェードインに掛かる時間
    [SerializeField]
    float fadeInSeconds = 1.0f;

    // 透明度
    float alpha = 0;

    // ステージ画像
    [SerializeField]
    private Image[] stageImage = new Image[STAGE_NUM];

    // テキスト
    [SerializeField]
    private Text[] scoreText = new Text[STAGE_NUM];

    // インフォメーションパネル
    [SerializeField]
    private Image informationPanel = null;

    // シンボル
    [SerializeField]
    private Image symbol = null;

    // ランキング
    [SerializeField]
    private Image rankingPanel = null;

    // イベントシステム
    [SerializeField]
    private EventSystem eventSystem = null;

    // 選択中のボタン
    [SerializeField]
    private GameObject selectedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");

        // アルファ値が1より小さい間
        if (alpha < 1.0f)
        {
            // アルファ値変更
            ChengeAlpha();
        }


        // ボタンオブジェクト以外をクリックした時
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            // 選択されているオブジェクトを再選択する(エラー防止のため)
            EventSystem.current.SetSelectedGameObject(selectedObject);
        }

        // 何かキーを押したら
        if(Input.anyKeyDown)
        {
            // selectingの値を変える
            ChangeSelecting();
            // 1フレーム前と選択しているボタンが違ったら
            if (selecting != lastSelect)
            {
                // いらない画像は透過する
                ImageInvisible();
                alpha = 0.0f;
                fadeDeltaTime = 0.0f;
            }
        }
        //1フレーム前の選択されていたボタン
        lastSelect = selecting;

        // Escキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FadeController.Instance.fadeOutStart(Common.Scene.TITLE_SCENE);
        }
    }

    // 表示しない画像を透明化
    void ImageInvisible()
    {
        switch (this.selecting)
        {
            case 0:
                stageImage[1].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                stageImage[2].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                break;
            case 1:
                stageImage[0].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                stageImage[2].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                break;
            case 2:
                stageImage[0].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                stageImage[1].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                break;
            default:
                break;
        }
    }

    // selectingの値を変える
    void ChangeSelecting()
    {
        // 選択しているオブジェクト(ボタン)を更新
        selectedObject = eventSystem.currentSelectedGameObject.gameObject;

        // 選択しているオブジェクトのタグがStage1
        if (selectedObject.tag == "Stage1")
        {
            this.selecting = 0;
        }
        // 選択しているオブジェクトのタグがStage2
        else if (selectedObject.tag == "Stage2")
        {
            this.selecting = 1;
        }
        // 選択しているオブジェクトのタグがStage3
        else if (selectedObject.tag == "Stage3")
        {
            this.selecting = 2;
        }
    }

    void ChengeAlpha()
    {
        // フェードイン
        FadeIn();
        //テクスチャの透明度を変更する
        stageImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, alpha);
        // インフォメーションパネルの透明度を変更する
        informationPanel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        // ランキングパネルの透明度を変更する
        rankingPanel.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        // シンボルの透明度を変更する
        symbol.color = new Color(1.0f, 1.0f, 0.0f, alpha);
        // スコアの透明度を変更
        for (int i = 0; i < STAGE_NUM; i++)
        {
            scoreText[i].color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }

    // alphaの値をフェードイン
    void FadeIn()
    {
        fadeDeltaTime += Time.deltaTime;

        if (fadeDeltaTime <= fadeInSeconds)
        {
            alpha = (fadeDeltaTime / fadeInSeconds);
        }
        else
        {
            alpha = 1.0f;
            fadeDeltaTime = 0.0f;
        }
    }
}
