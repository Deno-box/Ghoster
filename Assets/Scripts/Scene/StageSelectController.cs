using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectController : MonoBehaviour
{
    // ステージの数
    private const int STAGE_NUM = 3;

    // ボスのシルエット画像
    [SerializeField]
    private Image[] BossSilhouetteImage = new Image[STAGE_NUM];
    // 非選択時のランキングシート画像
    [SerializeField]
    private Image[] rankingSheetImage = new Image[STAGE_NUM];
    // 非選択時のクリアシンボル
    [SerializeField]
    private Image[] clearSymbolImage = new Image[STAGE_NUM];
    // クリアシンボルをオブジェクトとして取得
    [SerializeField]
    private GameObject[] clearSymbol = new GameObject[STAGE_NUM];

    // コウモリのスコアテキスト
    [SerializeField]
    private Text[] batScoreTexts = new Text[STAGE_NUM];
    // スカルのスコアテキスト
    [SerializeField]
    private Text[] skullScoreTexts = new Text[STAGE_NUM];
    // かぼちゃのスコアテキスト
    [SerializeField]
    private Text[] pumpkinScoreTexts = new Text[STAGE_NUM];

    // 選択中
    public int selecting;
    // ひとつ前の選択
    private int lastSelect;

    // イベントシステム
    [SerializeField]
    private EventSystem eventSystem = null;

    // 選択中のボタン
    [SerializeField]
    private GameObject selectedObject = null;

    // Start is called before the first frame update
    void Start()
    {
        ChangeImage();
        ChangeTextAlpha();

        //クリアフラッグは別の場所で管理する(予定)
        for(int i = 0;i<STAGE_NUM;i++)
        {
            clearSymbol[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");

        // ボタンオブジェクト以外をクリックした時
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            // 選択されているオブジェクトを再選択する(エラー防止のため)
            EventSystem.current.SetSelectedGameObject(selectedObject);
        }

        // 何かキーを押したら
        if (Input.anyKeyDown)
        {
            // selectingの値を変える
            ChangeSelecting();
            // 1フレーム前と選択しているボタンが違ったら
            if (selecting != lastSelect)
            {
                ChangeImage();
                ChangeTextAlpha();
            }
        }
        //1フレーム前の選択されていたボタン
        lastSelect = selecting;
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

    void ChangeImage()
    {
        // 選択中のステージのシルエットは消す
        switch (this.selecting)
        {
            case 0:
                BossSilhouetteImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                rankingSheetImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                clearSymbolImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                break;
            case 1:
                BossSilhouetteImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                rankingSheetImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                clearSymbolImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                break;
            case 2:
                BossSilhouetteImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                rankingSheetImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                clearSymbolImage[selecting].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                break;
            default:
                break;
        }
        // 選択中以外のシルエットは非選択状態にする
        for(int i = 0; i < STAGE_NUM;i++)
        {
            if(i != selecting)
            {
                BossSilhouetteImage[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                rankingSheetImage[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                clearSymbolImage[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    void ChangeTextAlpha()
    {
        // 選択中のステージのスコアのアルファは1にする
        switch (this.selecting)
        {
            case 0:
                for(int i = 0;i < STAGE_NUM;i++)
                {
                    batScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    skullScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                    pumpkinScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                }
                break;
            case 1:
                for (int i = 0; i < STAGE_NUM; i++)
                {
                    batScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                    skullScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    pumpkinScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                }
                break;

            case 2:
                for (int i = 0; i < STAGE_NUM; i++)
                {
                    batScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                    skullScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
                    pumpkinScoreTexts[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                break;

            default:
                break;
        }
    }

    // ステージクリアフラグ
    public void SetStageClearFlag(int stageNum)
    {
        clearSymbol[stageNum].SetActive(true);
    }

    // 選択中の値を返す
    public int GetSelecting()
    {
        return selecting;
    }
}
