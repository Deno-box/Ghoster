using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブジェクトを回転させる
public class ObjectRotation : MonoBehaviour
{
    // 逆回転させるか
    [SerializeField, Header("逆回転させるか")]
    private bool isReverse = false;
    // 回転速度
    [SerializeField,Range(0.0f,20.0f),Header("回転速度")]
    private float rotSpeed = 0.0f;

    // 回転軸をインスペクタで設定
    [SerializeField,Header("回転軸")]
    private bool axisX = false;
    [SerializeField]
    private bool axisY = false;
    [SerializeField]
    private bool axisZ = false;

    // Sin波の挙動にするか
    [SerializeField,Header("Sin波のような挙動にするか")]
    private bool isSinCurve = false;
    // Sin波の幅
    [SerializeField,Range(0.1f,5.0f),Header("Θの割合")]
    private float thetaRate = 1.0f;
    // Sin波の高さ?
    [SerializeField,Range(0.1f,5.0f),Header("振幅の最大値")]
    private float amplitude = 1.0f;

    // 回転軸
    private Vector3 rotAxis = Vector3.zero;

    // 反転時の掛け算
    private float reverse = 1.0f;

    private void Start()
    {
        // 回転軸を設定
        if (this.axisX)
            this.rotAxis.x = 1.0f;
        if (this.axisY)
            this.rotAxis.y = 1.0f;
        if (this.axisZ)
            this.rotAxis.z = 1.0f;

        if (this.isReverse)
            this.reverse *= -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TODO:: あえてTime.deltaTimeを掛けていない
        // Sin波の影響を受けながらオブジェクトを回転
        if (this.isSinCurve)
            this.transform.Rotate(this.rotAxis * this.rotSpeed * Mathf.Sin(Time.time*this.thetaRate) * this.amplitude * reverse);
        // 等速でオブジェクトを回転
        else
            this.transform.Rotate(this.rotAxis * this.rotSpeed * reverse);
    }
}
