using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    // 追跡するターゲット
    [SerializeField]
    private GameObject target = null;
    // カメラ位置となるオブジェクト
    [SerializeField]
    private GameObject offsetObj = null;
    // 移動速度
    [SerializeField]
    private float moveSpeed = 0.0f;
    // 回転速度
    [SerializeField]
    private float rotSpeed = 0.0f;
    // カメラとターゲットとのオフセット
    [SerializeField]
    private Vector3 offset = Vector3.zero;
    // 1フレーム前のカメラのポジション
    [SerializeField]
    private Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        //カメラの位置調整用
        offsetObj.transform.Translate(offset.x, offset.y, offset.z);
    }

    // Update is called once per frame
    void Update()
    {
        // ゴムひもカメラ
        this.transform.position = Vector3.Lerp(this.transform.position, oldPos, moveSpeed * Time.deltaTime);
        // 1フレーム前のカメラのポジション
        oldPos = offsetObj.transform.position;

        // 回転
        var vectorToTarget = target.transform.position - this.transform.position;
        var targetRotate = Quaternion.LookRotation(vectorToTarget);
        var newRotate = Quaternion.Lerp(this.transform.rotation, targetRotate, rotSpeed * Time.deltaTime).eulerAngles;
        this.transform.rotation = Quaternion.Euler(newRotate);
    }
}
