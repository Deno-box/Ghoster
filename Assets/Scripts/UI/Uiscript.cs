using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uiscript : MonoBehaviour
{
    // 判定数テキスト格納用
    //[NamedArrayAttribute(new string[] { "Great Text", "Good Text", "Miss Text" })]
    [SerializeField]
    private Text[] decisionCountTexts = new Text[3];

    // トータルスコアテキスト
    [SerializeField]
    private Text scoreCountText = null;

    //スコア
    private int[] scorePoint = new int[(int)GameDataManager.SCORE_TYPE.ALL_TYPE];

    //判定の数
    private int[] decisionNum = new int[(int)GameDataManager.SCORE_TYPE.ALL_TYPE];

    //トータルスコア
    private int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        // スコア、判定数初期化
        for (int i = 0; i < (int)GameDataManager.SCORE_TYPE.ALL_TYPE; i++)
        {
            this.scorePoint[i] = GameDataManager.GetScore(i);
            this.decisionNum[i] = GameDataManager.GetDecisionNum(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // スコア計算
        this.CalcScore();

        // 各スコアの表示
        this.DrawText();
    }

    // スコア計算
    private void CalcScore()
    {
        int temp = 0;
        for (int i = 0; i < (int)GameDataManager.SCORE_TYPE.ALL_TYPE; i++)
        {
            // スコアの計算
            temp += this.scorePoint[i] * this.decisionNum[i];

            // スコアがマイナスにならないように
            if (this.totalScore < 0)
            {
                this.totalScore = 0;
            }
        }
    }

    // 各テキストの表示
    private void DrawText()
    {
        for (int i = 0; i < this.decisionCountTexts.Length; i++)
        {
            this.decisionCountTexts[i].text = this.decisionNum[i].ToString() + "回";
        }
        this.scoreCountText.text = this.totalScore.ToString() + "pt";
    }
}
