using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // カメラを揺らす関数(揺れる時間,揺れの大きさ)
    public void Shake(float time, float magnitude)
    {
        StartCoroutine(DoShake(time, magnitude));
    }

    // カメラの揺れを実行するコルーチン
    private IEnumerator DoShake(float time, float magnitude)
    {
        var pos = transform.localPosition;
        float elapsedTime = 0.0f;

        // 設定した時間の間
        while (elapsedTime < time)
        {
            // ランダムな値を出す
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;
            // ランダムな値でカメラのポジションを更新
            transform.localPosition = new Vector3(x, y, pos.z);
            // 経過時間を計算
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // カメラのポジションを元に戻す
        transform.localPosition = pos;
    }
}
