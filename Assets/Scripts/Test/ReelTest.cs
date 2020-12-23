using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelTest : MonoBehaviour
{
    // リールの数
    private const int REEL_NUM = 6;
    // リールの値の数
    private const int REEL_VALUES = 9;

    // リール
    [SerializeField]
    private GameObject[] reel = new GameObject[REEL_NUM];

    // リール速度
    private float[] reelSpeed = new float[REEL_NUM];
    [SerializeField]
    private float speed;

    // 初期座標
    private Vector3[] initPos = new Vector3[REEL_NUM];

    // リールの数値の座標
    private float[] reelValuePosY = new float[REEL_VALUES];
    private float offsetY = 68.0f;

    private bool spinFlag = true;

    // テスト用スコア
    private string score;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < REEL_NUM; i++)
        {
            // リール速度設定
            this.reelSpeed[i] = this.speed;
            // 初期座標設定
            this.initPos[i] = this.reel[i].transform.position;
            Debug.Log(initPos[i].y);

        }

        for (int i = 0; i < REEL_VALUES; i++)
        {
            this.reelValuePosY[i] = this.initPos[0].y - (this.offsetY * i);
        }

        //for (int i = score.Length - 1; i >= 0; i--)
        //{
        //    Debug.Log(score[i]);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (spinFlag)
        {
            for (int i = 0; i < REEL_NUM; i++)
            {
                // リールを下方向に動かす
                this.reel[i].transform.Translate(0, this.reelSpeed[i], 0);

                if (this.reel[i].transform.position.y < -180)
                {
                    this.reel[i].transform.position = this.initPos[i];
                }
            }
        }
        this.reel[0].transform.position = new Vector3(this.reel[0].transform.position.x, this.initPos[0].y - (68 * 3),
            this.reel[0].transform.position.z);
    }

    // リールを回す
    public void StartReel()
    {
        for (int i = 0; i < REEL_NUM; i++)
        {
            this.reelSpeed[i] = this.speed;
            this.spinFlag = true;
        }
    }

    // リールの回転を止める
    public void StopReel()
    {
        for (int i = 0; i < REEL_NUM; i++)
        {
            this.reelSpeed[i] = 0;
            this.spinFlag = false;
        }
    }

    // 数値に変更があったら
    public void ChangeNumber()
    {

    }
}
