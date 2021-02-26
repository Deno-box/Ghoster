using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateStageSelectScore : MonoBehaviour
{
    [SerializeField, Header("表示させるテキスト")]
    Text[] rankingText = new Text[3];

    string stageNumber;



    public GameObject[] ClearImageStampt = new GameObject[3];

    //ステージ1
    public string[] ranking1 = { "1-1ランキング1位", "1-2ランキング2位", "1-3ランキング3位" };

    //ステージ2
    public string[] ranking2 = { "2-1ランキング1位", "2-2ランキング2位", "2-3ランキング3位" };

    //ステージ3
    public string[] ranking3 = { "3-1ランキング1位", "3-2ランキング2位", "3-3ランキング3位" };

    public int[] rankingValue = new int[3];



    [SerializeField]
    private int stageNum = 1;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        switch (stageNum)
        {
            case 1:

                for (int i = 0; i < ranking1.Length; i++)
                {
                  rankingValue[i] = PlayerPrefs.GetInt(ranking1[i]);
                }
                break;

            case 2:

                for (int i = 0; i < ranking2.Length; i++)
                {
                    rankingValue[i] = PlayerPrefs.GetInt(ranking2[i]);
                }
                break;

            case 3:

                for (int i = 0; i < ranking3.Length; i++)
                {
                    rankingValue[i] = PlayerPrefs.GetInt(ranking3[i]);
                }
                break;
        }

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[i].text = rankingValue[i].ToString();

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
    }
}
