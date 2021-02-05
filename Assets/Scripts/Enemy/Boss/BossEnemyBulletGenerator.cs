﻿using System.Collections;
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

    [SerializeField, Header("弾を乗せるパスのリスト")]
    private List<CinemachinePathBase> attackPathList = new List<CinemachinePathBase>();

    [SerializeField, Header("弾を生成する範囲,開始地点"), PropertyBackingField("StartPos")]
    private float startPos = 0.0f;

    [SerializeField, Header("弾を生成する範囲,終了地点"), PropertyBackingField("EndPos")]
    private float endPos = 0.0f;

    [SerializeField, Header("弾の発射開始座標"), PropertyBackingField("ShootStartPos")]
    private float shootStartPos = 0.0f;

    [SerializeField, Header("撤退アニメーションを再生する地点"), PropertyBackingField("DrawlPos")]
    private float drawlPos = 0.0f;


    [SerializeField,Header("ボスの攻撃パターン")]
    private List<BossAttackPattern> bossAttackPT = new List<BossAttackPattern>();

    [Header("※ここから下は編集しない※")]
    [SerializeField]
    private float bossSpeed = 0.0f;
    // 生成する際のオフセット
    [SerializeField]
    private float instanceOffset = 0.0f;
    // 一度に生成する弾の数
    [SerializeField]
    private int instanceBulletNum = 0;
    // 弾のプレハブ
    [SerializeField]
    private List<GameObject> bulletPrefabList = new List<GameObject>();
    // 弾を生成するポジション
    private int positionCounter = 0;
    // 弾の発射間隔
    [SerializeField]
    private float shootIntervalMax = 0.0f;
    // 弾の現在のインターバル
    private float shootInterval = 0.0f;
    // 弾を格納するオブジェクト
    private Transform shootBullrtsListTrs = null;
    // 自身のカート
    private CinemachineDollyCart myCart = null;
    // 1フレーム前のボスの攻撃パターン
    private BossAttackPattern.BossAttackPatternEnum lastAttackPattern = BossAttackPattern.BossAttackPatternEnum.None;
    // 弾を発射するパスの番号
    private int shootPathNum = 0;

    // 弾の生成範囲
    [SerializeField]
    private GameObject startPosObj = null;
    [SerializeField]
    private GameObject endPosObj   = null;
    [SerializeField]
    private GameObject drawlPosObj = null;
    [SerializeField]
    private GameObject shootStartPosObj = null;

    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private GameObject attackFX = null;
    [SerializeField]
    private GameObject drawalFX = null;
    

    public enum AnimState { 
        None,
        Attack,
        Damage,
        Idle,
        Drawal
    }
    private AnimState currentAnimState = AnimState.None;

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
    public float DrawlPos
    {
        get { return drawlPos; }
        set {
            drawlPos = value;
            this.drawlPosObj.GetComponent<CinemachineDollyCart>().m_Path = this.bossMainPass;
            this.drawlPosObj.GetComponent<CinemachineDollyCart>().m_Position = this.drawlPos;
        }
    }
    public float ShootStartPos
    {
        get { return shootStartPos; }
        set {
            shootStartPos = value;
            this.shootStartPosObj.GetComponent<CinemachineDollyCart>().m_Path = this.bossMainPass;
            this.shootStartPosObj.GetComponent<CinemachineDollyCart>().m_Position = this.shootStartPos;
        }
    }

    private void Start()
    {
        this.shootBullrtsListTrs = GameObject.Find("BossBullets").transform;
        this.GetComponent<CinemachineDollyCart>().m_Speed = bossSpeed;

        myCart = this.GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TODO : あとで直す
        var clip = animator.GetCurrentAnimatorClipInfo(0)[0];
        // 攻撃
        if (currentAnimState == AnimState.None)
        {
            ChangeAnim(AnimState.Attack);
            GameObject obj = Instantiate(attackFX, this.transform);
            obj.transform.localPosition = new Vector3(0.0f,-1.0f,0.0f);
        }
        else
        // 撤退
        if (currentAnimState != AnimState.Drawal && this.myCart.m_Position >= this.drawlPos)
        {
            ChangeAnim(AnimState.Drawal);
            GameObject obj = Instantiate(drawalFX, this.transform);
            obj.transform.localPosition = new Vector3(0.0f, -1.0f, 0.0f);
        }
        else
        if (currentAnimState != AnimState.Drawal && clip.clip.name == "Idle")
            currentAnimState = AnimState.Idle;

        // 弾生成用カウンターを加算
        positionCounter++;
        // 発射インターバルを加算
        shootInterval += Time.deltaTime;
        // インターバルを超えたら && 発射可能範囲を超えたら 弾を生成
        if (shootInterval >= shootIntervalMax && myCart.m_Position >= shootStartPos && this.myCart.m_Position < this.drawlPos)
        {
            // 弾を発射
            ShootBullet();

            // 発射インターバルをリセット
            shootInterval = 0.0f;
        }


        // TODO : ボスに追加する
        if (this.GetComponent<CinemachineDollyCart>().m_Position >= this.GetComponent<CinemachineDollyCart>().m_Path.PathLength)
            Destroy(this.gameObject);
    }

    // 弾を生成
    // 生成したパスの番号を返す
    private void InstanceBullet(int _pathNum)
    {
        // メインパスに生成するなら
        if(this.attackPathList[_pathNum].name == "MovePath") {
            // 生成するポジションを計算
            float pathPos = this.startPos + positionCounter + instanceOffset;
            pathPos = Mathf.Clamp(pathPos, 0.0f, this.endPos);

            // 弾を生成
            GameObject obj = InstanceBulletPrefab();
            obj.transform.parent = this.shootBullrtsListTrs;
            obj.GetComponent<CinemachineDollyCart>().m_Path = this.stageMainPass;
            obj.GetComponent<CinemachineDollyCart>().m_Position = pathPos;
        }
        // サブパスに生成するなら
        else
        {
            // 生成するポジションを計算
            float pathMaxPos = this.attackPathList[_pathNum].PathLength - 0.0f;
            float pathPos = positionCounter + instanceOffset;
            pathPos = Mathf.Clamp(pathPos, 0.0f, this.attackPathList[_pathNum].PathLength);

            // 弾を生成
            GameObject obj = InstanceBulletPrefab();
            obj.transform.parent = this.shootBullrtsListTrs;
            obj.GetComponent<CinemachineDollyCart>().m_Path = this.attackPathList[_pathNum];
            obj.GetComponent<CinemachineDollyCart>().m_Position = pathPos;
        }

        //obj.transform.parent = this.transform;
    }

    // ボスの攻撃パターンを取得
    private BossAttackPattern.BossAttackPatternEnum AttackPattern()
    {
        BossAttackPattern.BossAttackPatternEnum ret = 0;
        foreach (var pattern in bossAttackPT)
        {
            if (this.myCart.m_Position <= pattern.position)
            {
                ret = pattern.pattern;
                break;
            }
        }
        return ret;
    }

    private void ShootBullet()
    {
        BossAttackPattern.BossAttackPatternEnum currentATKPattern = AttackPattern();

        // パターンに応じて弾を発射
        switch (currentATKPattern)
        {
            // 左から右に発射
            case BossAttackPattern.BossAttackPatternEnum.LeftToRight:
                if (currentATKPattern != lastAttackPattern)
                    shootPathNum = 0;

                InstanceBullet(shootPathNum);
                shootPathNum++;
                break;

            // 右から左に発射
            case BossAttackPattern.BossAttackPatternEnum.RightToLeft:
                if (currentATKPattern != lastAttackPattern)
                    shootPathNum = attackPathList.Count-1;

                InstanceBullet(shootPathNum);
                shootPathNum--;
                break;

            // 3連続で発射
            case BossAttackPattern.BossAttackPatternEnum.Consecutive3:
                if (currentATKPattern != lastAttackPattern)
                    shootPathNum = Random.Range(0,attackPathList.Count-1);

                InstanceBullet(shootPathNum);
                break;

            // 左から一つ空けて弾を発射
            case BossAttackPattern.BossAttackPatternEnum.SkipLeftToRight:
                if (currentATKPattern != lastAttackPattern)
                    shootPathNum = 0;

                InstanceBullet(shootPathNum);
                shootPathNum += 2;
                break;

            // 左から一つ空けて弾を発射
            case BossAttackPattern.BossAttackPatternEnum.SkipRightToLeft:
                if (currentATKPattern != lastAttackPattern)
                    shootPathNum = attackPathList.Count - 1;

                InstanceBullet(shootPathNum);
                shootPathNum -= 2;
                break;

            case BossAttackPattern.BossAttackPatternEnum.All:
                for(int i=0;i<attackPathList.Count;i++)
                    InstanceBullet(i);
                break;
        }

        shootPathNum = Mathf.Clamp(shootPathNum, 0, attackPathList.Count-1);

        this.lastAttackPattern = currentATKPattern;

    }

    private void ChangeAnim(AnimState _state)
    {
        switch (_state)
        {
            case AnimState.Attack:
                this.animator.Play("Attack");
                break;

            case AnimState.Damage:
                this.animator.Play("Damage");
                break;

            case AnimState.Drawal:
                this.animator.Play("Drawal");
                break;

            case AnimState.Idle:
                this.animator.Play("Idle");
                break;

            default: break;

        }

        currentAnimState = _state;
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            if (other.GetComponent<EnemyBulletStateController>().LastStateEnum == EnemyBulletStateController.BulletStateEnum.Parry &&
                this.myCart.m_Position < this.drawlPos)
            {
                ChangeAnim(AnimState.Damage);
            }
        }
    }

    private GameObject InstanceBulletPrefab()
    {
        int rand = Random.Range(0, this.bulletPrefabList.Count);
        GameObject instanceBullet = Instantiate(bulletPrefabList[rand], Vector3.zero, Quaternion.identity);

        return instanceBullet;
    }
}


// ボスの攻撃パターンを設定するクラス
[System.Serializable]
public class BossAttackPattern
{
    public enum BossAttackPatternEnum{
        None,
        LeftToRight,
        RightToLeft,
        Consecutive3,
        SkipLeftToRight,
        SkipRightToLeft,
        All
    }
    // ボスの攻撃パターン
    public BossAttackPatternEnum pattern = BossAttackPatternEnum.None;
    // 範囲
    public float position = 0.0f;
}
