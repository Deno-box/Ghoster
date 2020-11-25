using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    // 弾のプレハブ
    [SerializeField]
    private GameObject bulletPrefab = null;
    // 弾の発射間隔
    [SerializeField]
    private float shootIntervalMax = 0.0f;
    // 弾の現在のインターバル
    private float shootInterval = 0.0f;
    // 一度に生成する弾の数
    [SerializeField]
    private int instanceBulletNum = 0;
    // 生成する際のオフセット
    [SerializeField]
    private float instanceOffset = 0.0f;
    // 生成するパスのリスト
    [SerializeField]
    private List<shootPath> pathList = new List<shootPath>();
    // 弾を生成するポジション
    private int positionCounter = 0;

    

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
        int laneNum = Random.Range(0, this.pathList.Count);
        // 生成するポジションを計算
        float pathMaxPos = this.pathList[laneNum].maxPos - this.pathList[laneNum].minPos;
        float pathPos = positionCounter + this.pathList[laneNum].minPos + instanceOffset;
        pathPos = Mathf.Clamp(pathPos, this.pathList[laneNum].minPos, this.pathList[laneNum].maxPos);

        // 弾を生成
        GameObject obj = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        obj.GetComponent<CinemachineDollyCart>().m_Path = this.pathList[laneNum].path;
        obj.GetComponent<CinemachineDollyCart>().m_Position = pathPos;

        //obj.transform.parent = this.transform;
    }
}
