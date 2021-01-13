using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Candlelight;

// 移動先のパスのデータ
[System.Serializable]
public class ChangeNextPathData
{
    [Header("移動先の情報")]
    // 左右どちらのキーで移動するか
    public PlayerMovePath.MoveDir moveDir;

    // 移動先のパス
    // パスが設定されるとパスの長さを自動取得
    [PropertyBackingField("ChangePath")]
    public CinemachinePathBase changePath;
    public CinemachinePathBase ChangePath 
    { 
        get { return changePath; }
        set { changePath = value; changePosMax = changePath.PathLength; }
    }
    // 移動先パスの移動可能範囲のPosの最小値
    public float changePosMin;
    // 移動先パスの移動可能範囲のPosの最大値
    public float changePosMax;
}

[System.Serializable]
public class PlayerMoveData
{
    // 移動元のパス
    [PropertyBackingField("NowPath"),Header("移動元の情報")]
    public CinemachinePathBase nowPath;
    // パスが設定されるとパスの長さを自動取得
    public CinemachinePathBase NowPath
    {
        get { return nowPath; }
        set {nowPath = value; }
    }

    // 移動元の移動可能範囲のPosの最小値
    public float nowPosMin;
    // 移動元の移動可能範囲のPosの最大値
    public float nowPosMax;

    [SerializeField, Header("移動先のリスト")]
    List<ChangeNextPathData> changeNextPathDataList = new List<ChangeNextPathData>();
    public List<ChangeNextPathData> changeNextDataList { get { return changeNextPathDataList; } }
}


public class PlayerMovePath : MonoBehaviour
{
    // 移動キー
    public enum MoveDir
    {
        None,
        Right = -1,
        Left = 1
    }
    public List<PlayerMoveData> PlayerMoveDataList = new List<PlayerMoveData>();
}
