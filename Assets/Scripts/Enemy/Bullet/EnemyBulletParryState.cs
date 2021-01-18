using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyBulletParryState : BulletState
{
    // ボスのトランスフォーム
    private Transform bossTrs = null;
    // 跳ね返るときの弾の速度
    private float moveSpeed = 100.0f;
    private TrailRenderer trailRenderer = null;

    // ボスにダメージを与えたときのFX
    private GameObject damageFX;

    // SEを再生するオーディオソース
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip SE = null;

    private void Awake()
    {
        trailRenderer = this.GetComponent<TrailRenderer>();
        this.damageFX = Resources.Load("Effect/Enemy/Bullet/BossBulletCollision") as GameObject;

        // TODO : 変更しておく
        audioSource = GameObject.Find("SE").GetComponent<AudioSource>();
    }

    // 初期化処理
    public override void StateInitialize()
    {
        // レーン移動を削除
        Destroy(this.GetComponent<CinemachineDollyCart>());
        // ターゲットであるボスを検索

        // ボスが消えると参照できない
        if (GameObject.FindWithTag("BossEnemy"))
            bossTrs = GameObject.FindWithTag("BossEnemy").transform;

        state = EnemyBulletStateController.BulletStateEnum.Parry;

        //trailRenderer.emitting = true;

    }

    // 更新処理
    public override void Execute()
    {
        state = EnemyBulletStateController.BulletStateEnum.Parry;
        // TOdo:: 絶対やめたほうがいい
        if (!GameObject.FindWithTag("BossEnemy"))
            Destroy(this.gameObject);
        else{
            // 移動先を計算
            Vector3 movePos = Vector3.MoveTowards(this.transform.position, this.bossTrs.position, this.moveSpeed * Time.deltaTime);
            this.transform.position = movePos;
        }
    }

    // OnTrigger時の処理
    public override void StateOnTriggerEnter(Collider _other)
    {
        if (_other.tag == "BossEnemy")
        {
            Vector3 offset = _other.transform.position - this.transform.position;
            GameObject obj = Instantiate(damageFX, _other.transform);
            obj.transform.localPosition = offset;

            audioSource.PlayOneShot(SE);

            this.transform.GetChild(1).transform.parent = null;
            //UnityEditor.EditorApplication.isPaused = true;
            //Debug.Log(obj.transform.childCount);
            Destroy(this.gameObject);
        }
    }
}
