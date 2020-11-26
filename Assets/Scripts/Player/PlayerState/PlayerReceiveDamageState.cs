using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDamageState : PlayerState
{
    // プレイヤーのステータスデータ
    private PlayerStatusData playerStatus = null;

    // 被ダメージ状態での経過時間
    private float damageTimer;
    // 点滅時間
    private float brinkInterval = 0.15f;
    // 点滅状態か
    private bool isBlink = false;
    

    private GameObject playerModel;
    private GameObject parrysuccessFx;

    private void Start()
    {
        this.playerStatus = Resources.Load("ScriptableObjectDatas/Player/PlayerStatus") as PlayerStatusData;
        this.parrysuccessFx = Resources.Load("Effect/Player/PlayerReceiveDamage") as GameObject;
        playerModel = this.transform.GetChild(3).gameObject;
    }

    // 初期化処理
    public override void Initialize()
    {
        this.state = PlayerStateController.PlayerStateEnum.ReceiveDamage;
        damageTimer = 0.0f;
        this.isBlink = true;

        GameObject obj = Instantiate(parrysuccessFx, this.transform);
        StartCoroutine("BlinkRenderer");
    }

    // 実行処理
    public override void Execute()
    {
        this.damageTimer += Time.deltaTime;
        if (this.damageTimer >= playerStatus.damageInvincibleTime)
            this.state = PlayerStateController.PlayerStateEnum.Idle;

        // スペースキーでパリィ状態に遷移
        if (Input.GetKeyDown(KeyCode.Space))
            this.state = PlayerStateController.PlayerStateEnum.Parry;
        // Aキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 左入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMoveData.MoveDir.Left;
            this.state = PlayerStateController.PlayerStateEnum.MoveLR;
        }
        // Dキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 右入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMoveData.MoveDir.Right;
            this.state = PlayerStateController.PlayerStateEnum.MoveLR;
        }
    }

    // 終了処理
    public override void Exit()
    {
        playerModel.SetActive(true);
        this.isBlink = false;
    }

    // TODO :: 演出用の処理で状態変更を行っているので変更する
    IEnumerator BlinkRenderer()
    {
        while (this.isBlink)
        {
            playerModel.SetActive(!playerModel.activeSelf);
            yield return new WaitForSeconds(this.brinkInterval);
        }
    }
}