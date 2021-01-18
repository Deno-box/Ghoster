using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Candlelight;

// 弾の発射に必要なデータ
[System.Serializable]
public class shootPath
{
    public CinemachinePathBase path;
    public float minPos;
    public float maxPos;
}

// ボス用の弾を生成する
public class BossEnemyBulletGenerator : MonoBehaviour
{
    [SerializeField, Header("ボスのメインパス")]
    private CinemachinePathBase bossMainPass = null;
    [SerializeField, Header("ステージのメインパス")]
    private CinemachinePathBase stageMainPass = null;
    // 生成するパスのリスト
    [SerializeField, Header("弾を乗せるパスのリスト")]
    private List<CinemachinePathBase> subPathList = new List<CinemachinePathBase>();

    [SerializeField, Header("弾を生成する範囲,開始地点"), PropertyBackingField("StartPos")]
    private float startPos = 0.0f;

    [SerializeField, Header("弾を生成する範囲,終了地点"), PropertyBackingField("EndPos")]
    private float endPos = 0.0f;



    [Header("※ここから下は編集しない※")]
    // 生成する際のオフセット
    [SerializeField]
    private float instanceOffset = 0.0f;
    // 一度に生成する弾の数
    [SerializeField]
    private int instanceBulletNum = 0;
    // 弾のプレハブ
    [SerializeField]
    private GameObject bulletPrefab = null;
    // 弾を生成するポジション
    private int positionCounter = 0;
    // 弾の発射間隔
    [SerializeField]
    private float shootIntervalMax = 0.0f;
    // 弾の現在のインターバル
    private float shootInterval = 0.0f;
    // 弾を格納するオブジェクト
    private Transform shootBullrtsListTrs = null;

    // 弾の生成範囲
    [SerializeField]
    private GameObject startPosObj = null;
    [SerializeField]
    private GameObject endPosObj   = null;


    public float StartPos
    {
        get { return startPos; }
        set { 
            startPos = value;
            this.startPosObj.GetComponent<CinemachineDollyCart>().m_Path = this.stageMainPass;
            this.startPosObj.GetComponent<CinemachineDollyCart>().m_Position = this.startPos;
        }
    }
    public float EndPos
    {
        get { return endPos; }
        set { 
            endPos = value;
            this.endPosObj.GetComponent<CinemachineDollyCart>().m_Path = this.stageMainPass;
            this.endPosObj.GetComponent<CinemachineDollyCart>().m_Position = this.endPos;
        }
    }

    private void Start()
    {
        this.shootBullrtsListTrs = GameObject.Find("BossBullets").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 弾生成用カウンターを加算
        positionCounter++;
        // 発射インターバルを加算
        shootInterval += Time.deltaTime;
        // インターバルを超えたら弾を生成
        if (shootInterval >= shootIntervalMax)
        {
            // 弾を生成したパスの番号
            //int instancePathNum = -1;
            // 指定の個数弾を生成
            for (int i = 0; i < instanceBulletNum; i++)
            {
                // 密度はいい感じ
                // ランダムすぎる。面白さが平均化されない
                // パターン化する

                // 弾を生成して移動レーンを制限する
                ShootBullet();
                // 同時に同じパスに生成しないようにする
                //while(instancePathNum == ShootBullet(instancePathNum))
                //{
                //    break;
                //}
            }
            // 発射インターバルをリセット
            shootInterval = 0.0f;
        }


        // TODO : ボスに追加する
        if (this.GetComponent<CinemachineDollyCart>().m_Position >= this.GetComponent<CinemachineDollyCart>().m_Path.PathLength)
            Destroy(this.gameObject);
    }

    // 弾を生成
    // 生成したパスの番号を返す
    private void ShootBullet(int _lastInstancePathNum = 0)
    {
        // 生成するレーンの番号を設定
        int laneNum = Random.Range(0, this.subPathList.Count+1);
        // メインパスに生成する場合
        if (laneNum == this.subPathList.Count){
            // 生成するポジションを計算
            float pathMaxPos = this.endPos;
            float pathPos = this.startPos + positionCounter + instanceOffset;
            pathPos = Mathf.Clamp(pathPos, 0.0f, this.endPos);

            // 弾を生成
            GameObject obj = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.parent = this.shootBullrtsListTrs;
            obj.GetComponent<CinemachineDollyCart>().m_Path = this.stageMainPass;
            obj.GetComponent<CinemachineDollyCart>().m_Position = pathPos;
        }
        else
        {
            // 生成するポジションを計算
            float pathMaxPos = this.subPathList[laneNum].PathLength - 0.0f;
            float pathPos = positionCounter + instanceOffset;
            pathPos = Mathf.Clamp(pathPos, 0.0f, this.subPathList[laneNum].PathLength);

            // 弾を生成
            GameObject obj = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.parent = this.shootBullrtsListTrs;
            obj.GetComponent<CinemachineDollyCart>().m_Path = this.subPathList[laneNum];
            obj.GetComponent<CinemachineDollyCart>().m_Position = pathPos;
        }

        //obj.transform.parent = this.transform;
    }
}