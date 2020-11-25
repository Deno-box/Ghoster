using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerMoveData")]
public class PlayerMoveData : ScriptableObject
{
    // 移動キー
    public enum MoveDir
    {
        None,
        Right,
        Left
    }

    // 左右どちらのキーで移動するか
    public MoveDir moveDir;
    // 移動元のパス
    public CinemachinePathBase nowPath;
    // 移動元の移動可能範囲のPosの最小値
    public float nowPosMin;
    // 移動元の移動可能範囲のPosの最小値
    public float nowPosMax;

    // 移動先のパス
    public CinemachinePathBase changePath;
    // 移動先パスの移動可能範囲のPosの最小値
    public float changePosMin;
    // 移動先パスの移動可能範囲のPosの最大値
    public float changePosMax;
}
