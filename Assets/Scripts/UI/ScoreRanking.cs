using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreRanking : MonoBehaviour
{
    [SerializeField, Header("数値")]
   // private int point;

    //ステージ1
    string[] ranking1 = { "1-1ランキング1位", "1-2ランキング2位", "1-3ランキング3位" };

    //ステージ2
    string[] ranking2 = { "2-1ランキング1位", "2-2ランキング2位", "2-3ランキング3位" };

    //ステージ3
    string[] ranking3 = { "3-1ランキング1位", "3-2ランキング2位", "3-3ランキング3位" };


    int[] rankingValue = new int[3];

    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[3];

    //獲得スコアの表示
    public Text scoreText;

    private int score = 0;

    //ビルド用
    public Test test;

    string stageNumber;

    //ビルド用
    bool sssss;

    //ステージ画像
    int clearStage; 
    public Sprite[] ImageStage;
    public GameObject StageImge;

    public GameObject[] ClearImageStampt = new GameObject[3]; 
 
    // Use this for initialization
    void Start()
    {

        test = FindObjectOfType<Test>(); // インスタンス化
        clearStage = PlayerPrefs.GetInt("clearStageNum");

        // StageImge.SetActive(PlayerFallState.ClearFlag/*ゲッターで受け取ったboolをいれる*/);

        //ビルド用
        // StageImge.SetActive(test.aaaClearFlag/*ゲッターで受け取ったboolをいれる*/);

        stageNumber = PlaySceneController.Stagenumber;

        //ビルド用
        //sssss = test.aaaClearFlag;

        //スコアを入れる
        score = Score.GetScore;

        GetRanking();

        SetRanking(score);

        StageImge.GetComponent<Image>().sprite=ImageStage[clearStage-1];

        
        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();

        }
        scoreText.text = score.ToString();
    }

    void Update()
    {
        //デバッグ用
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //  SceneManager.LoadScene("TestClearScene");
        //}
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {
        //ステージのクリア番号
        
        switch (clearStage)
        {

            //ランキング呼び出し
            case 1:

                for (int i = 0; i < ranking1.Length; i++)
                {
                    rankingValue[i] = PlayerPrefs.GetInt(ranking1[i]);
                    Debug.Log("1 : " + rankingValue[i]);
                }
                break;

            case 2:
                for (int i = 0; i < ranking2.Length; i++)
                {
                    rankingValue[i] = PlayerPrefs.GetInt(ranking2[i]);
                    Debug.Log("2 : " + rankingValue[i]);
                }
                break;


            case 3:
                for (int i = 0; i < ranking3.Length; i++)

                {
                    rankingValue[i] = PlayerPrefs.GetInt(ranking3[i]);
                }
                break;
        }
    }

    /// <summary>
    /// ランキング書き込み
    /// </summary>

    void SetRanking(int _value)
    {
        bool setColorFlag = false;

        //ステージ1
        //書き込み用
        for (int i = 0; i < ranking1.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                if (!setColorFlag)
                {
                    //獲得した自分のスコアの色を変える
                    rankingText[i].color = new Color32(127, 255, 212, 255);
                    setColorFlag = true;
                }
                
            }
            
        }

        //ステージ2
        for (int i = 0; i < ranking2.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                if (!setColorFlag)
                {
                   
                    
                    //獲得した自分のスコアの色を変える
                    rankingText[i].color = new Color32(127, 255, 212, 255);
                    setColorFlag = true;
                }
            }
          
        }

        //ステージ3
        for (int i = 0; i < ranking3.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                if (!setColorFlag)
                {
                   
                    
                    //獲得した自分のスコアの色を変える
                    rankingText[i].color = new Color32(127, 255, 212, 255);
                    setColorFlag = true;
                }
            }
           
        }

        // ステージをクリアしていたら
        int isStageClear = PlayerPrefs.GetInt("isStageClear");
        if (isStageClear == 1)
        {
            switch (stageNumber)
            {
                case "Stage1Scene":
                    {
                        ClearImageStampt[0].SetActive(true);
                    }
                    break;
                case "Stage2Scene":
                    {
                        ClearImageStampt[1].SetActive(true);
                    }
                    break;
                case "Stage3Scene":
                    {
                        ClearImageStampt[2].SetActive(true);
                    }
                    break;
                default:
                    break;

            }
        }

        switch (clearStage)
        {
            case 1:


                //入れ替えた値を保存
                for (int i = 0; i < ranking1.Length; i++)
                {
                    PlayerPrefs.SetInt(ranking1[i], rankingValue[i]);
                }
                break;


            case 2:
                for (int i = 0; i < ranking2.Length; i++)
                {
                    PlayerPrefs.SetInt(ranking2[i], rankingValue[i]);
                }
                break;

            case 3:
                for (int i = 0; i < ranking3.Length; i++)
                {
                    PlayerPrefs.SetInt(ranking3[i], rankingValue[i]);
                }
                break;
        }

        // numObj.View(point);

    }
}



