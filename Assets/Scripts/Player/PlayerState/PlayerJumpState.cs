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

    // 現在のステートがアクティブかどうか
    private bool isChangeState;

    PlayerStatusData playerStatus;
    PlayerData playerData;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;
    private float beforeTrigger = 0.0f;

    // 先行入力したステート
    private PlayerStateController.PlayerStateEnum typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Idle;

    private void Start()
    {
        playerData = this.GetComponent<PlayerData>();
        // プレイヤーのステータスデータ
        playerStatus = playerData.PlayerStatus;
        this.jumpVelMax = playerStatus.jumpVelMax;
        this.gravity = playerStatus.gravity;

        this.offsetPosY = this.playerModel.transform.localPosition.y; 
    }

    // 初期化処理
    public override void Initialize()
    {
        this.jumpVel = this.jumpVelMax;
        this.state = PlayerStateController.PlayerStateEnum.Jump;
        typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Idle;

        isChangeState = false;


        this.GetComponent<PlayerData>().GetComponent<Animator>().Play("Jump");
        playerData.AudioSource.PlayOneShot(playerStatus.jumpSE);
    }

    // 実行処理
    public override void Execute()
    {
        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");
        float trigger = Input.GetAxis("LRTrigger");

        if (isChangeState)
        {
            // 先行入力に応じて次のステートを変更
            switch (typeAheadNextStatus)
            {
                case PlayerStateController.PlayerStateEnum.Idle:
                    this.state = PlayerStateController.PlayerStateEnum.Idle;
                    break;

                case PlayerStateController.PlayerStateEnum.Parry:
                    this.state = PlayerStateController.PlayerStateEnum.Parry;
                    break;

                case PlayerStateController.PlayerStateEnum.MoveLR:
                    this.state = PlayerStateController.PlayerStateEnum.MoveLR;
                    break;

                default:
                    this.state = PlayerStateController.PlayerStateEnum.Idle;
                    break;
            }
        }
        // 弾きの先行入力
        // Qキーで左パリィ状態に遷移
        if (Input.GetKeyDown(KeyCode.Q) || trigger != 0 && this.beforeTrigger < 0)
        {
            this.typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Parry;
            this.playerData.ParryDir = PlayerData.ParryDirection.Left;
        }
        // Eキーで右パリィ状態に遷移
        else
        if (Input.GetKeyDown(KeyCode.E) || trigger != 0 && this.beforeTrigger > 0)
        {
            this.typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Parry;
            this.playerData.ParryDir = PlayerData.ParryDirection.Right;
        } 
        // Aキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.LeftArrow) || stickHori < 0 && this.beforeStick == 0 || crossHori < 0 && this.beforeCross == 0)
        {
            // 左入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMovePath.MoveDir.Left;
            this.typeAheadNextStatus = PlayerStateController.PlayerStateEnum.MoveLR;
        }
        // Dキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 && this.beforeStick == 0 || crossHori > 0 && this.beforeCross == 0)
        {
            // 右入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMovePath.MoveDir.Right;
            this.typeAheadNextStatus = PlayerStateController.PlayerStateEnum.MoveLR;
        }

        this.beforeStick = stickHori;
        this.beforeCross = crossHori;
        this.beforeTrigger = trigger;

    }

    // 移動実行処理
    public override void ExecuteMove()
    {
        jumpVel -= gravity;
        JumpMove();
        if (this.playerModel.transform.localPosition.y <= offsetPosY)
            isChangeState = true;
    }


    // 終了処理
    public override void Exit()
    {
        this.playerModel.transform.localPosition = new Vector3(0.0f, this.offsetPosY, 0.0f);
    }

    // ジャンプ移動
    public void JumpMove()
    {
        this.playerModel.transform.position += new Vector3(0.0f, jumpVel * Time.deltaTime, 0.0f);
    }
}
