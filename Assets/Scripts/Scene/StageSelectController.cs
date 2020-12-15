using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Common;

public class StageSelectController : MonoBehaviour
{
    enum ImagePlace
    {
        Forward = 0,
        Middle = 1,
        Back = 2,
    }

    // ステージの数
    private const int STAGE_NUM = 3;

    // フェード用キャンバス
    private GameObject fadeCanvas;

    // フェード用スクリプト
    private Fadecontroller fadeScript;

    // ステージ画像
    [SerializeField]
    private Image[] stageImage = new Image[STAGE_NUM];

    // ステージ画像位置
    private Vector3[] imagePos = new Vector3[STAGE_NUM];

    // 選択中
    private int selecting = 0;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //fadeCanvas = GameObject.Find("FadeCanvas");
        //fadeScript = fadeCanvas.GetComponent<Fadecontroller>();

        for (int i = 0; i < STAGE_NUM; i++)
        {
            this.imagePos[i] = new Vector3(this.stageImage[i].rectTransform.position.x,
                                      this.stageImage[i].rectTransform.position.y,
                                      this.stageImage[i].rectTransform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float stickHori = Input.GetAxisRaw("StickHorizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");

        // ←キーが押されたら
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (stickHori < 0 && this.beforeStick == 0) || (crossHori < 0 && this.beforeCross == 0))
        {
            this.selecting += 1;
            if (this.selecting == 3)
                this.selecting = 0;
            MoveStageImage();
            Debug.Log(stickHori + "+" + crossHori);
        }
        // →キーが押されたら
        if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 && this.beforeStick == 0 || crossHori > 0 && this.beforeCross == 0)
        {
            this.selecting -= 1;
            if (this.selecting == -1)
                this.selecting = 2;
            MoveStageImage();
        }
        // Escキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.fadeScript.fadeOutStart(Common.Scene.TITLE_SCENE);
        }
    }

    // 画像の移動
    private void MoveStageImage()
    {
        switch (this.selecting)
        {
            case 0:
                this.stageImage[0].rectTransform.position = SetImagePos(ImagePlace.Forward);
                this.stageImage[1].rectTransform.position = SetImagePos(ImagePlace.Middle);
                this.stageImage[2].rectTransform.position = SetImagePos(ImagePlace.Back);
                break;
            case 1:
                this.stageImage[0].rectTransform.position = SetImagePos(ImagePlace.Back);
                this.stageImage[1].rectTransform.position = SetImagePos(ImagePlace.Forward);
                this.stageImage[2].rectTransform.position = SetImagePos(ImagePlace.Middle);
                break;
            case 2:
                this.stageImage[0].rectTransform.position = SetImagePos(ImagePlace.Middle);
                this.stageImage[1].rectTransform.position = SetImagePos(ImagePlace.Back);
                this.stageImage[2].rectTransform.position = SetImagePos(ImagePlace.Forward);
                break;
            default:
                break;
        }

        // 描画順を設定
        SetSibling();
    }

    // 画像位置の設定
    private Vector3 SetImagePos(ImagePlace _place)
    {
        return new Vector3(this.imagePos[(int)_place].x,
                           this.imagePos[(int)_place].y,
                           this.imagePos[(int)_place].z);
    }

    // 表示順の設定
    private void SetSibling()
    {
        float min, max;
        min = max = this.stageImage[0].rectTransform.position.x;
        int first, last;
        first = last = 0;

        for (int i = 0; i < STAGE_NUM; i++)
        {
            if (min > this.stageImage[i].rectTransform.position.x)
            {
                min = this.stageImage[i].rectTransform.position.x;
                last = i;
            }
            if (max < this.stageImage[i].rectTransform.position.x)
            {
                max = this.stageImage[i].rectTransform.position.x;
                first = i;
            }
        }

        // 表示を最前面にする
        this.stageImage[last].transform.SetAsLastSibling();
        // 表示を最奥にする
        this.stageImage[first].transform.SetAsFirstSibling();
    }
}
