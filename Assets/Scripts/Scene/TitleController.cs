using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;
using UnityEngine.Video;

// タイトルコントローラ
public class TitleController : MonoBehaviour
{
    // テキストの種類
    private enum TextType
    {
        NEW_GAME = 0,   // 始めから
        CONTINUE = 1,   // 続きから
        OPTIONS = 2,    // 設定

        ALL_TYPE = 3,
    }

    // テキスト
    //[NamedArrayAttribute(new string[] { "New Game", "Continue", "Options" })]
    [SerializeField]
    Text[] textType = new Text[(int)TextType.ALL_TYPE];

    // 矢印画像
    //[NamedArrayAttribute(new string[] { "Left Arrow", "Right Arrow" })]
    [SerializeField]
    Image[] arrowImage = new Image[2];

    // 選択中
    private int selecting = (int)TextType.NEW_GAME;

    // セレクトガイドのX座標を調整
    private float selectGuideOffsetXRate = 0.3f;

    [SerializeField]
    Canvas canvas = null;

    private FadeController fadeScript = null;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        this.fadeScript = this.canvas.GetComponent<FadeController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 選択中のテキスト位置を取得
        RectTransform temp = this.textType[this.selecting].rectTransform;

        // 画像オフセット値設定
        float offsetX = temp.sizeDelta.x + (temp.sizeDelta.x * selectGuideOffsetXRate);

        // モードの選択
        ChoiceMode(ref temp, offsetX);

        // モードの決定
        ModeSelect();

        // 画像を動かす
        MoveImage(ref temp, offsetX);
    }

    // モードの選択
    private void ChoiceMode(ref RectTransform _rect, float _offsetX)
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");

        if (beforeStick == 0.0f && beforeCross == 0.0f)
        {
            // 右キーを押下
            if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 || crossHori > 0)
            {
                if (this.selecting < (int)TextType.ALL_TYPE - 1)
                {
                    this.selecting++;
                }
                ChangeImagePos(ref _rect, _offsetX);
            }
            // 左キーを押下
            if (Input.GetKeyDown(KeyCode.LeftArrow) || stickHori < 0 || crossHori < 0)
            {
                if (this.selecting > 0)
                {
                    this.selecting--;
                }
                ChangeImagePos(ref _rect, _offsetX);
            }
        }

        beforeStick = stickHori;
        beforeCross = crossHori;
    }

    // 画像位置変更
    private void ChangeImagePos(ref RectTransform _rect, float _offsetX)
    {
        this.arrowImage[0].rectTransform.position = new Vector3(_rect.position.x + _offsetX, _rect.position.y, _rect.position.z);
        this.arrowImage[1].rectTransform.position = new Vector3(_rect.position.x - _offsetX, _rect.position.y, _rect.position.z);
    }

    // モード選択
    private void ModeSelect()
    {
        //　Enterキー（SPACEキー）を押下
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown("joystick button 0"))
        {
            switch (this.selecting)
            {
                // ゲームスタート
                case (int)TextType.NEW_GAME:
                    // プレイシーンへ遷移
                    fadeScript.fadeOutStart(Common.Scene.STAGE_SELECT_SCENE);
                    break;
                // 続きから
                case (int)TextType.CONTINUE:
                    // プレイシーンへ遷移
                    fadeScript.fadeOutStart(Common.Scene.STAGE_SELECT_SCENE);
                    break;
                // オプション
                case (int)TextType.OPTIONS:
                    // Quitの代わりにしておく
                    Application.Quit();
                    break;
            }
        }


    }

    // 画像を動かす
    private void MoveImage(ref RectTransform _rect, float _offsetX)
    {
        float t = 1.0f;
        float f = 1.0f / t;
        float sin = Mathf.Sin(3 * Mathf.PI * f * Time.time);

        this.arrowImage[0].rectTransform.position = new Vector3((_rect.position.x + _offsetX) + sin, _rect.position.y, _rect.position.z);
        this.arrowImage[1].rectTransform.position = new Vector3((_rect.position.x - _offsetX) - sin, _rect.position.y, _rect.position.z);
    }
}
