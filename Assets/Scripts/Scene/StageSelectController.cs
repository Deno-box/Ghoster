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
    // 拡大縮小させるパネル
    [SerializeField]
    private GameObject[] stagePanel = new GameObject[STAGE_NUM];
    private Vector3[] stagePanelScale = new Vector3[STAGE_NUM];

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

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        ChangeImage();
        ChangeTextAlpha();

        //クリアフラッグは別の場所で管理する(予定)
        for (int i = 0; i < STAGE_NUM; i++)
        {
            clearSymbol[i].SetActive(false);
            stagePanelScale[i] = stagePanel[i].transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");

        if (beforeStick == 0.0f && beforeCross == 0.0f)
        {
            // 右キーを押下
            if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 || crossHori > 0)
            {
                if (this.selecting < 2)
                {
                    this.selecting++;
                    ChangeImage();
                    ChangeTextAlpha();
                }

            }
            // 左キーを押下
            if (Input.GetKeyDown(KeyCode.LeftArrow) || stickHori < 0 || crossHori < 0)
            {
                if (this.selecting > 0)
                {
                    this.selecting--;
                    ChangeImage();
                    ChangeTextAlpha();
                }
            }
        }

        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Space))
        {
            int num = selecting + 2;
            FadeController.Instance.fadeOutStart(num);
        }
        
        ChangePanelSize(selecting);



        beforeStick = stickHori;
        beforeCross = crossHori;
    }

    void ChangePanelSize(int num)
    {
        switch (num)
        {
            case 0:
                stagePanel[selecting].transform.localScale = new Vector3(stagePanelScale[selecting].x + Mathf.PingPong(Time.time / 10, 0.05f), 
                    stagePanelScale[selecting].y + Mathf.PingPong(Time.time/10, 0.05f),
                    stagePanelScale[selecting].z + Mathf.PingPong(Time.time/10, 0.05f));

                stagePanel[1].transform.localScale = stagePanelScale[1];
                stagePanel[2].transform.localScale = stagePanelScale[2];
                break;
            case 1:
                stagePanel[selecting].transform.localScale = new Vector3(stagePanelScale[selecting].x + Mathf.PingPong(Time.time / 10, 0.05f),
                    stagePanelScale[selecting].y + Mathf.PingPong(Time.time / 10, 0.05f),
                    stagePanelScale[selecting].z + Mathf.PingPong(Time.time / 10, 0.05f));

                stagePanel[0].transform.localScale = stagePanelScale[0];
                stagePanel[2].transform.localScale = stagePanelScale[2];
                break;
            case 2:
                stagePanel[selecting].transform.localScale = new Vector3(stagePanelScale[selecting].x + Mathf.PingPong(Time.time / 10, 0.05f),
                    stagePanelScale[selecting].y + Mathf.PingPong(Time.time / 10, 0.05f),
                    stagePanelScale[selecting].z + Mathf.PingPong(Time.time / 10, 0.05f));

                stagePanel[1].transform.localScale = stagePanelScale[1];
                stagePanel[0].transform.localScale = stagePanelScale[0];

                break;
            default:
                break;
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
        for (int i = 0; i < STAGE_NUM; i++)
        {
            if (i != selecting)
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
                for (int i = 0; i < STAGE_NUM; i++)
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
