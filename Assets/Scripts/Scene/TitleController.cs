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
    private enum ImageType
    {
        NEW_GAME = 0,   // 始めから
        CONTINUE = 1,   // 続きから
        OPTIONS = 2,    // 設定

        ALL_TYPE = 3,
    }

    // テキスト
    //[NamedArrayAttribute(new string[] { "New Game", "Continue", "Options" })]
    [SerializeField]
    Image[] imageType = new Image[(int)ImageType.ALL_TYPE];

    [SerializeField]
    GameObject exitImage = null;

    // 選択中
    private int selecting = (int)ImageType.NEW_GAME;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;

    private bool exitFlag = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // モードの選択
        if (!exitFlag)
        {
            ChoiceMode();
        }
        // モードの決定
        ModeSelect();
    }

    // モードの選択
    private void ChoiceMode()
    {
        float stickVertical = Input.GetAxisRaw("Vertical");
        float crossVertical = Input.GetAxisRaw("CrossVertical");

        if (beforeStick == 0.0f && beforeCross == 0.0f)
        {
            // 上キーを押下
            if (Input.GetKeyDown(KeyCode.UpArrow) || stickVertical > 0 || crossVertical > 0)
            {
                if (this.selecting > 0)
                {
                    this.selecting--;
                }

            }
            // 下キーを押下
            if (Input.GetKeyDown(KeyCode.DownArrow) || stickVertical < 0 || crossVertical < 0)
            {
                if (this.selecting < (int)ImageType.ALL_TYPE - 1)
                {
                    this.selecting++;
                }
            }
        }

        for (int i = 0; i < (int)ImageType.ALL_TYPE; i++)
        {
            if (this.selecting == i)
            {
                imageType[i].color = new Color(1.0f,1.0f,1.0f,1.0f);
            }
            else
            {
                imageType[i].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }

        }

        beforeStick = stickVertical;
        beforeCross = crossVertical;
    }

    // モード選択
    private void ModeSelect()
    {
        //　Enterキー（SPACEキー）を押下
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            switch (this.selecting)
            {
                // ゲームスタート
                case (int)ImageType.NEW_GAME:
                    // プレイシーンへ遷移
                    if (!exitFlag)
                        FadeController.Instance.fadeOutStart(Common.Scene.STAGE_SELECT_SCENE);
                    break;
                // 続きから
                case (int)ImageType.CONTINUE:
                    // プレイシーンへ遷移
                    if (!exitFlag)
                        FadeController.Instance.fadeOutStart(Common.Scene.STAGE_SELECT_SCENE);
                    break;
                // オプション
                case (int)ImageType.OPTIONS:
                    exitImage.SetActive(true);
                    exitFlag = true;
                    break;
            }
        }

        if(exitFlag)
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                Application.Quit();
            }
            if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown("joystick button 1"))
            {
                exitImage.SetActive(false);
                exitFlag = false;
            }
        }
    }
}
