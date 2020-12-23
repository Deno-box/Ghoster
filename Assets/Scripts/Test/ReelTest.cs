using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelTest : MonoBehaviour
{
    // リールの数
    private const int REEL_NUM = 6;

    // リール
    [SerializeField]
    private GameObject[] reel = new GameObject[REEL_NUM];

    // リール速度
    private float[] reelSpeed = new float[REEL_NUM];
    [SerializeField]
    private float speed;

    // 初期座標
    private Vector3[] initPos = new Vector3[REEL_NUM];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < REEL_NUM; i++)
        {
            // リール速度設定
            this.reelSpeed[i] = this.speed;
            // 初期座標設定
            this.initPos[i] = this.reel[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
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

    // リールを回す
    public void StartReel()
    {
        for (int i = 0; i < REEL_NUM; i++)
        {
            this.reelSpeed[i] = this.speed;
        }
    }
}
