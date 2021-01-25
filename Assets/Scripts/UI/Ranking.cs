using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Ranking : MonoBehaviour
{
    


    [SerializeField, Header("ハイスコア")]
    private int score = 0;

 
    [SerializeField, Header("表示させるテキスト")]
    private Text[] rankingText = new Text[5];

    private StreamWriter sw;


  

    

    private int highScore = 0;
    private string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    int[] rankingValue = new int[5];


    // Start is called before the first frame update
    void Start()
    {
        //スコアを入れる
        score = Score.GetScore;
        
        highScore = PlayerPrefs.GetInt(ranking[0]);

    
        //subscribe.onClick.AddListener(() => 
        {
            int j = 0;
            for (int i = 0; i < ranking.Length; i++)
            {
                if (score > PlayerPrefs.GetInt(ranking[i]))
                {
                    j = i;
                    SwapScore(score, ref i);
                    break;
                }
            }
           // Show();
            PlayerPrefs.Save();

        };
    }

    

    

    private void SwapScore(int score, ref int index)
    {
        var temp = PlayerPrefs.GetInt(ranking[index]);
        PlayerPrefs.SetInt(ranking[index], score);
        if (index < ranking.Length - 1)
        {
            index++;
            SwapScore(temp, ref index);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Save()
    {
        PlayerPrefs.Save();
    }


}

