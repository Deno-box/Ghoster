using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;

public class StageSelectController2 : MonoBehaviour
{
    // ステージの数
    private const int STAGE_NUM = 3;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;
    // 選択中
    private int selecting = 0;

    // スピード
    float speed = 0.01f;

    // 透明度
    float alpha = 0;

    // フェード用スクリプト
    private Fadecontroller fadeScript;

    // ステージ画像
    [SerializeField]
    private Image[] stageImage = new Image[STAGE_NUM];

    // ステージボタン
    [SerializeField]
    private Button[] button = new Button[STAGE_NUM];

    // テキスト
    [SerializeField]
    private Text[] scoreText = new Text[STAGE_NUM];



    // インフォメーションパネル
    [SerializeField]
    private Image informationPanel;

    // シンボル
    [SerializeField]
    private Image symbol;

    // ランキング
    [SerializeField]
    private Image rankingPanel;







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

        alpha += speed;

        // ←キーが押されたら
        if (Input.GetKeyDown(KeyCode.LeftArrow) || stickHori < 0 && this.beforeStick == 0 || crossHori < 0 && this.beforeCross == 0)
        {
            alpha = 0;

            this.selecting += 1;
            if (this.selecting == 3)
                this.selecting = 0;
            ImageInvisible();
        }
        // →キーが押されたら
        if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 && this.beforeStick == 0 || crossHori > 0 && this.beforeCross == 0)
        {
            alpha = 0;

            this.selecting -= 1;
            if (this.selecting == -1)
                this.selecting = 2;
            ImageInvisible();
        }
        // Escキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.fadeScript.fadeOutStart(Common.Scene.TITLE_SCENE);
        }

        // アルファ値が1より小さい間
        if (alpha <= 1.0f)
        {
            // アルファ値変更
            ChengeAlpha();
        }
    }

    // 画像を見えなくする
    private void ImageInvisible()
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

    void ChengeAlpha()
    {
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
}
