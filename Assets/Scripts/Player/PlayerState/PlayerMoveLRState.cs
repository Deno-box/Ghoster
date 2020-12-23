using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMoveLRState : PlayerState
{
    // プレイヤーのステータスデータ
    private PlayerStatusData playerStatus = null;
    PlayerData playerData;

    // 追従するオブジェクト
    private CinemachineDollyCart myCart = null;
    // TODO : publicになってしまっている　要変更
    // 左右どちらのキーが押されたか
    public PlayerMoveData.MoveDir moveDir = PlayerMoveData.MoveDir.None;
    // 移動時間
    private float moveTimer    = 0.0f;

    // 移動可能範囲のリスト
    private PlayerMoveDataList playerMoveData;

    private GameObject nextPosObj = null;
    private bool isMove = false;
    //[SerializeField]
    //private GameObject playerModel = null;


    // 先行入力したステート
    private PlayerStateController.PlayerStateEnum typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Idle;

    float beforeTrigger = 0.0f;

    private void Start()
    {
        playerData = this.GetComponent<PlayerData>();
        this.playerStatus = playerData.PlayerStatus;
        playerMoveData = Resources.Load("ScriptableObjectDatas/Player/PlayerMoveData/DemoScene/TestStagePlayerMoveData") as PlayerMoveDataList;

        myCart = this.GetComponent<CinemachineDollyCart>();

        this.nextPosObj = new GameObject();
        this.nextPosObj.AddComponent<CinemachineDollyCart>();
        this.nextPosObj.GetComponent<CinemachineDollyCart>().m_UpdateMethod = CinemachineDollyCart.UpdateMethod.FixedUpdate;
        this.nextPosObj.transform.parent = this.transform;
        this.nextPosObj.transform.localPosition = Vector3.zero;
        this.nextPosObj.name = "NextPointObj";

    }

    // 初期化処理
    public override void Initialize()
    {
        // 先行入力の状態をリセット
        typeAheadNextStatus = PlayerStateController.PlayerStateEnum.MoveLR;

        this.state = PlayerStateController.PlayerStateEnum.MoveLR;
        this.nextPosObj.transform.localPosition = Vector3.zero;

        this.moveTimer = 0.0f;

        isMove = ChangeMove();

        if (isMove)
            playerData.AudioSource.PlayOneShot(playerStatus.moveSE);
    }

    // 実行処理
    public override void Execute()
    {
        float trigger = Input.GetAxis("LRTrigger");

        // 一定時間経過後状態を遷移
        if (moveTimer >= this.playerStatus.moveTime)
        {
            // 先行入力に応じて次のステートを変更
            switch(typeAheadNextStatus)
            {
                case PlayerStateController.PlayerStateEnum.Idle:
                    this.state = PlayerStateController.PlayerStateEnum.Idle;
                    break;

                case PlayerStateController.PlayerStateEnum.Jump:
                    this.state = PlayerStateController.PlayerStateEnum.Jump;
                    break;

                case PlayerStateController.PlayerStateEnum.Parry:
                    this.state = PlayerStateController.PlayerStateEnum.Parry;
                    break;

                default:
                    this.state = PlayerStateController.PlayerStateEnum.Idle;
                    break;
            }
        }
        // 弾きの先行入力
        if (Input.GetKeyDown(KeyCode.Space) || trigger != 0 && this.beforeTrigger == 0)
            typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Parry;
        // ジャンプの先行入力
        if(Input.GetKeyDown(KeyCode.Q))
            typeAheadNextStatus = PlayerStateController.PlayerStateEnum.Jump;

        this.beforeTrigger = trigger;


        if (!this.isMove)
            this.state = PlayerStateController.PlayerStateEnum.Idle;
    }   
    
    // 移動実行処理
    public override void ExecuteMove()
    {
        if (this.isMove)
        {
            // TODO : 仮の状態 移動方向に応じて回転方向を変更する
            //float dir = 1.0f;
            //if (moveDir == PlayerMoveData.MoveDir.Right)
            //    dir = -1.0f;
            // 

            this.moveTimer += Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, nextPosObj.transform.position, Time.deltaTime * 8.0f);// (this.playerStatus.moveTime));
            //float rate = 360.0f / this.playerStatus.moveTime;
            //playerModel.transform.rotation = Quaternion.Euler(new Vector3(moveTimer * rate * dir - 90.0f, -90.0f, 90.0f));
        }
    }


    // 終了処理
    public override void Exit()
    {
        // 移動していたら
        if (this.isMove)
        {
            myCart.m_Path = nextPosObj.GetComponent<CinemachineDollyCart>().m_Path;
            myCart.m_Position = nextPosObj.GetComponent<CinemachineDollyCart>().m_Position;
            myCart.enabled = true;
            //playerModel.transform.rotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f));
        }
    }


    // 移動するパスを変更
    public void ChangeMovePath(CinemachinePathBase _path, float _position = 0.0f)
    {
        // 移動先用の座標を取得
        nextPosObj.GetComponent<CinemachineDollyCart>().m_Path     = _path;
        nextPosObj.GetComponent<CinemachineDollyCart>().m_Position = _position;


        this.isMove    = true;
        myCart.enabled = false;
    }

    // キャラクターを移動させる
    private bool ChangeMove()
    {
        // 移動データを参照
        foreach (PlayerMoveData data in playerMoveData.moveDataList)
        {
            // 移動可能範囲内か
            if (CheckMovePossible(data))
            {
                // ポジションを修正
                float pos = myCart.m_Position - data.nowPosMin;
                float maxPos = data.nowPosMax - data.nowPosMin;

                // 現在のパスで割合を計算
                float nowPosPer = pos / maxPos;
                nowPosPer = Mathf.Clamp(nowPosPer, 0.0f, 1.0f);

                // 移動先のPositionを計算
                float changePosMax = data.changePosMax - data.changePosMin;
                float changePos = changePosMax * nowPosPer + data.changePosMin;

                float speed = this.GetComponent<CinemachineDollyCart>().m_Speed;
                // TODO : 誤差をマジックナンバーで修正している
                changePos += this.playerStatus.moveTime * speed + 0.5f;


                // 移動を開始
                ChangeMovePath(data.changePath, changePos);
                return true;

                //break;
            }
        }

        return false;
    }

    // 移動可能なのかを判定
    private bool CheckMovePossible(PlayerMoveData _data)
    {
        // 現在のPosition
        float position = myCart.m_Position;

        // パスの名前は一致しているか / 移動可能範囲内か / 入力されたキーは一致しているか
        if (myCart.m_Path.name == _data.nowPath.name
            && GhosterUtility.CalculationUtility.IsWithinRange(position, _data.nowPosMin, _data.nowPosMax)
            && moveDir == _data.moveDir)
            return true;

        return false;
    }
}