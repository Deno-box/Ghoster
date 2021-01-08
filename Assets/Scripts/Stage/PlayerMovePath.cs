using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// 移動先のパスのデータ
[System.Serializable]
public class ChangeNextPathData
{
    // 左右どちらのキーで移動するか
    public PlayerMovePath.MoveDir moveDir;
    // 移動先のパス
    public CinemachinePathBase changePath;
    // 移動先パスの移動可能範囲のPosの最小値
    public float changePosMin;
    // 移動先パスの移動可能範囲のPosの最大値
    public float changePosMax;
}

[System.Serializable]
public class PlayerMoveData
{
    // 移動元のパス
    public CinemachinePathBase nowPath;
    // 移動元の移動可能範囲のPosの最小値
    public float nowPosMin;
    // 移動元の移動可能範囲のPosの最大値
    public float nowPosMax;

    [SerializeField]
    List<ChangeNextPathData> changeNextPathDataList = new List<ChangeNextPathData>();
    public List<ChangeNextPathData> changeNextDataList { get { return changeNextPathDataList; } }
}


public class PlayerMovePath : MonoBehaviour
{
    // 移動キー
    public enum MoveDir
    {
        None,
        Right,
        Left
    }
    [SerializeField]
    List<PlayerMoveData> playerMoveDataList = new List<PlayerMoveData>();
    public List<PlayerMoveData> PlayerMoveDataList { get { return playerMoveDataList; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
