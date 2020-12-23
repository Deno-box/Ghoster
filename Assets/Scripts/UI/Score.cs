using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // スコア用テキスト
    [SerializeField]
    Text scoreText = null;

    // スコア
    private static int score = 0;
    public static int GetScore
    {
        get
        {
            return score;
        }
    }


    //トータルスコア
    private int total = 0;


    // テキスト変動速度
    private const int FLUC_SPEED = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // テキスト更新(7桁で表示)
        this.scoreText.text = score.ToString("D7");
    }

    // スコアの計算
    public void CalcScore(int[] _score, int[] _decisionNum)
    {


        for (int i = 0; i < (int)GameDataManager.SCORE_TYPE.ALL_TYPE; i++)
        {
            total += _score[i] * _decisionNum[i];

            if (total < 0)
            {
                total = 0;
            }
        }
        // テキストの変動
        FluctuationText(total);

       total = 0;
    }

    // テキストを変動させる
    private void FluctuationText(int _total)
    {
        if(score != _total)
        {
            // スコア増加
            if (score < _total)
            {
                score += FLUC_SPEED;
            }
            // スコア減少
            else if (score > _total && score != 0)
            {
                score -= FLUC_SPEED;
            }
        }
    }

        

}
