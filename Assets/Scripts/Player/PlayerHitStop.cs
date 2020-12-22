using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitStop : MonoBehaviour
{
    // 時間を遅くしている時間
    float slowTime;
    // 経過時間
    float elapsedTime;
    // 時間を遅くしているかどうか
    bool isSlowDown = false;

    void Start()
    {
        slowTime = 1.0f;
        elapsedTime = 0.0f;
    }
    void Update()
    {
        // スローダウンフラグがtrueの時は時間計測
        if (isSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;

            if (elapsedTime >= slowTime)
            {
                SetNormalTime();
            }
        }
    }
    // 時間を遅らせる処理(スローにする時間,どの程度遅くするか0～1)
    public void SlowDown(float time,float scale)
    {
        elapsedTime = 0.0f;

        slowTime = time;

        Time.timeScale = scale;

        isSlowDown = true;
    }
    // 時間を元に戻す処理
    public void SetNormalTime()
    {
        // スケールを1に戻す
        Time.timeScale = 1.0f;
        // フラグをオフにする
        isSlowDown = false;
    }
}
