using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera = null;
    [SerializeField]
    private float _position = 0.0f;

    private CinemachineTrackedDolly _dolly;
    private float pathLength = 0.0f;

    void Start()
    {
        // Virtual Cameraに対してGetCinemachineComponentでCinemachineTrackedDollyを取得する
        // GetComponentではなくGetCinemachineComponentなので注意
        _dolly = _virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        pathLength = _dolly.m_Path.PathLength;
    }

    // カメラを移動
    // 移動が完了するとtrueを返す
    public bool CameraMove()
    {
        bool ret = false;
        _position += 0.004f;
        // パスの位置を更新する
        // 代入して良いのか不安になる変数名だけどこれでOK
        _dolly.m_PathPosition = _position;
        if (_position >= 1.0f)
            ret = true;


        return ret;
    }
}
