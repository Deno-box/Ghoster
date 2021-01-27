using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreRanking : MonoBehaviour
{
    [SerializeField, Header("数値")]
    private int point;
   

    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位"};
    int[] rankingValue = new int[3];

    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[3];



    //獲得スコアの表示
    public Text scoreText;

    private int score = 0;

    // Use this for initialization
    void Start()
    {
        //スコアを入れる
        score = Score.GetScore;

        GetRanking();

        SetRanking(point);


        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
            
        }
       scoreText.text= point.ToString();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
            
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    void SetRanking(int _value)
    {
        bool setColorFlag = false;
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                if(!setColorFlag)
                {
                  //  rankingText[i].color = Color.white;
                    setColorFlag = true;
                }
            }
            
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
       
       // numObj.View(point);
    }
}

