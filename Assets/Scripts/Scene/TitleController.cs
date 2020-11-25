using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    Canvas canvas = null;

    private Fadecontroller fadeController = null;


    // Start is called before the first frame update
    void Start()
    {
        this.fadeController = this.canvas.GetComponent<Fadecontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        // 選択中のテキスト位置を取得
        RectTransform temp = this.textType[this.selecting].rectTransform;

        // 画像オフセット値設定
        float offsetX = temp.sizeDelta.x / 1.2f;

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
        // ←キー（Aキー）を押下
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (this.selecting < (int)TextType.ALL_TYPE - 1)
            {
                this.selecting++;
            }
            ChangeImagePos(ref _rect, _offsetX);
        }
        // →キー（Dキー）を押下
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (this.selecting > 0)
            {
                this.selecting--;
            }
            ChangeImagePos(ref _rect, _offsetX);
        }
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
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (this.selecting)
            {
                // ゲームスタート
                case (int)TextType.NEW_GAME:
                    // プレイシーンへ遷移
                    //fadeController.fadeOutStart(0, 0, 0, 0, "DemoPlayScene");
                    //break;
                // 続きから
                case (int)TextType.CONTINUE:
                    break;
                // オプション
                case (int)TextType.OPTIONS:
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
