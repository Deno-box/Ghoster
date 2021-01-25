using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 指定のオブジェクトとの距離を計り、距離に応じてアクティブにする
public class DistanceActive : MonoBehaviour
{
    [SerializeField]
    private float distance = 0.0f;
    [SerializeField]
    private Transform targetTrs = null;

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, this.targetTrs.position);
        if (dist <= distance)
            this.transform.GetChild(0).gameObject.SetActive(true);
        else
            this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
