using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

[ExecuteInEditMode]
public class DebugPlayerStatus : MonoBehaviour
{
    [SerializeField,Header("メインパスを設定")]
    private GameObject mainPathObj = null;

    [SerializeField,Header("プレイヤーの移動速度")]
    private float playerMoveSpeed = 0.0f;
    [SerializeField,Header("プレイヤーの位置")]
    private float playerPosition = 0.0f;


    [Header("※以下の設定は変更しない※")]
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private GameObject playerCamera = null;
    [SerializeField]
    private GameObject offsetObj = null;
    [SerializeField]
    private GameObject targetObj = null;
    // データコレクション
    private PathDataCollection pathDataCollection = null;
    // メインパス
    private CinemachinePathBase mainPath = null;

    private float targetOffset = 15.0f;
    private bool isPlay = false;



    // Start is called before the first frame update
    void Awake()
    {
        // パスのデータコレクションを設定
        this.pathDataCollection = this.GetComponent<PathDataCollection>();
        this.pathDataCollection.rootStagePath = this.mainPathObj.transform.root.gameObject;

        // プレイヤーの情報を設定
        this.mainPath = this.mainPathObj.GetComponent<CinemachineSmoothPath>();
        CinemachineDollyCart playerCart = this.player.GetComponent<CinemachineDollyCart>();
        playerCart.m_Path = this.mainPath;
        playerCart.m_Speed = playerMoveSpeed;

        this.player.GetComponent<PlayerMoveLRState>().pathDataCollection = this.pathDataCollection;

        // カメラ周りの設定
        PlayerFollowCamera followCamera = playerCamera.GetComponent<PlayerFollowCamera>();
        followCamera.offsetObj = this.offsetObj;
        followCamera.target    = this.targetObj;

        // ターゲットのオフセットを設定
        this.targetObj.GetComponent<CinemachineDollyCart>().m_Path     = this.mainPath;
        this.targetObj.GetComponent<CinemachineDollyCart>().m_Speed    = playerMoveSpeed;
        this.targetObj.GetComponent<CinemachineDollyCart>().m_Position = playerCart.m_Position + this.targetOffset;

        this.isPlay = true;
    }

    private void Start()
    {
    }

    private void Update()
    {
        // エディタ上でプレイヤーの座標を修正
        if (this.mainPathObj != null && isPlay ==false)
        {
            this.player.GetComponent<CinemachineDollyCart>().m_Path = this.mainPathObj.GetComponent<CinemachineSmoothPath>();
            this.player.GetComponent<CinemachineDollyCart>().m_Position = this.playerPosition;
        }

        if (!Application.isPlaying)
            this.isPlay = false;
        if(this.isPlay)
            Destroy(this);
    }


    // 親子関係を解除
    void ParentRelease()
    {

    }
}
