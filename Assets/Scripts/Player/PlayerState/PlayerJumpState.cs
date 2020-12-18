using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    // ジャンプをさせたいオブジェクト
    [SerializeField]
    private GameObject playerModel = null;

    // ジャンプ時の最大ベクトル
    private float jumpVelMax = 0.0f;
    // ジャンプ時に加える力
    private float jumpVel = 0.0f;
    // 重力
    private float gravity = 0.0f;

    // 着地時の座標修正
    private float offsetPosY = 0.0f;

    private void Start()
    {
        // プレイヤーのステータスデータ
        PlayerStatusData playerStatus = Resources.Load("ScriptableObjectDatas/Player/PlayerStatus") as PlayerStatusData;
        this.jumpVelMax = playerStatus.jumpVelMax;
        this.gravity = playerStatus.gravity;

        this.offsetPosY = this.playerModel.transform.localPosition.y;
    }

    // 初期化処理
    public override void Initialize()
    {
        this.jumpVel = this.jumpVelMax;
        this.state = PlayerStateController.PlayerStateEnum.Jump;
    }

    // 実行処理
    public override void Execute()
    {
        jumpVel -= gravity;

        JumpMove();
        if (this.playerModel.transform.localPosition.y <= offsetPosY)
            this.state = PlayerStateController.PlayerStateEnum.Idle;
    }

    // 終了処理
    public override void Exit()
    {
        this.playerModel.transform.localPosition = new Vector3(0.0f,1.0f,0.0f);
    }

    // ジャンプ移動
    public void JumpMove()
    {
        this.playerModel.transform.position += new Vector3(0.0f, jumpVel * Time.deltaTime, 0.0f);
    }
}
