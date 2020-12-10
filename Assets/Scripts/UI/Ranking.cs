using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{

    [SerializeField, Header("数値")]

    int point;

    int i;

    public InputField text;



    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    int[] rankingValue = new int[5];


    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[5];


    // Start is called before the first frame update
    void Start()
    {
        GetRanking();

        SetRanking(point);

        text.text = PlayerPrefs.GetString("NAME", "Player");


        rankingValue[i] = Score.GetTotalScore();

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();
        }
        //test
        //string uName = "k";
        //int score = 000;
        //string str;
        
        //str = uName + ":" + score.ToString() + ",";

    }
    public void InputName()
    {
        PlayerPrefs.SetString("NAME", text.text);
        PlayerPrefs.Save();
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
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }



        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);

            //スコアをすべて0にする
            //PlayerPrefs.SetInt(ranking[i], rankingValue[i]=0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Save()
    {
        // PlayerPrefs.SetInt("SCORE", score_num);
        PlayerPrefs.Save();

    }
}
