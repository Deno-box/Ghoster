using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("登録ボタン")]
    private Button subscribe = null;
    [SerializeField, Header("ハイスコア")]
    private int score = 0;
    [SerializeField, Header("名前親")]
    private Text[] namesText = new Text[5];
    [SerializeField, Header("表示させるテキスト")]
    private Text[] rankingText = new Text[5];
    [SerializeField, Header("Field")]
    private InputField inputField = null;

    private int highScore = 0;
    private string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };

    // Start is called before the first frame update
    void Start()
    {
        //スコアを入れる
        score = Score.GetScore;
        
        highScore = PlayerPrefs.GetInt(ranking[0]);

        Show();

        subscribe.onClick.AddListener(() => 
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
            Show();
            PlayerPrefs.Save();

        });
    }

    private void Show()
    {
        for (int i = 0; i < rankingText.Length; i++)
        {
            namesText[i].text = PlayerPrefs.GetString(ranking[i],"not player");
            rankingText[i].text = PlayerPrefs.GetInt(ranking[i], 0).ToString();
        }

        
    }

    private void SwapName(string name, ref int index)
    {
        var temp = PlayerPrefs.GetString(PlayerPrefs.GetInt(ranking[index]).ToString(), "not player");
        PlayerPrefs.SetString(PlayerPrefs.GetInt(ranking[index]).ToString(), name);
        if (index < ranking.Length - 1)
        {
            index++;
            SwapName(temp, ref index);
        }
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

