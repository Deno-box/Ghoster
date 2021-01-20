using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


using UnityEngine.UI;

public class PlayerIdleState : PlayerState
{
    // プレイヤーのステータスデータ
    private PlayerStatusData playerStatus = null;

    private PlayerData playerData;

    private CinemachineDollyCart myCart = null;
    private float pathLength = 0.0f;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;
    private float beforeTrigger = 0.0f;

    private void Start()
    {
        this.playerStatus = this.GetComponent<PlayerData>().PlayerStatus;//Resources.Load("ScriptableObjectDatas/Player/PlayerStatus") as PlayerStatusData;

        playerData = this.GetComponent<PlayerData>();
        this.playerStatus = playerData.PlayerStatus;
    }

    // 初期化処理
    public override void Initialize()
    {
        this.myCart = this.GetComponent<CinemachineDollyCart>();
        this.pathLength = this.myCart.m_Path.PathLength;

        this.state = PlayerStateController.PlayerStateEnum.Idle;
    }

    // 実行処理
    public override void Execute()
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");
        float trigger = Input.GetAxis("LRTrigger");
        // TODO : 左右キー判定やマウスの左右判定はこのクラスで判定しているため、後で変更しておく
        // スペースキーでパリィ状態に遷移
        if (Input.GetKeyDown(KeyCode.Space) || trigger != 0 && this.beforeTrigger == 0)
        {
            this.state = PlayerStateController.PlayerStateEnum.Parry;
            playerData.GetComponent<Animator>().Play("Attack");
        }
        // Aキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.LeftArrow) || stickHori < 0 && this.beforeStick == 0 || crossHori < 0 && this.beforeCross == 0)
        {
            // 左入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMovePath.MoveDir.Left;
            this.state = PlayerStateController.PlayerStateEnum.MoveLR;
            playerData.GetComponent<Animator>().Play("Left_Jump");
        }
        // Dキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 && this.beforeStick == 0 || crossHori > 0 && this.beforeCross == 0)
        {
            // 右入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMovePath.MoveDir.Right;
            this.state = PlayerStateController.PlayerStateEnum.MoveLR;
            playerData.GetComponent<Animator>().Play("Right_Jump");
        }
        // Qキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.state = PlayerStateController.PlayerStateEnum.Jump;
            playerData.GetComponent<Animator>().Play("Jump");
        }

        // レーンの端まで到着すると状態を遷移
        if (this.pathLength * this.playerStatus.fallJudgeRate <= this.myCart.m_Position)
            this.state = PlayerStateController.PlayerStateEnum.Fall;

        this.beforeStick = stickHori;
        this.beforeCross = crossHori;
        this.beforeTrigger = trigger;

    }
    // 移動実行処理
    public override void ExecuteMove()
    {
    }

    // 終了処理
    public override void Exit()
    {
    }
}
