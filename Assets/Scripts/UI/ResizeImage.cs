using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeImage : MonoBehaviour
{
    // 拡大率を指定
    // 最大サイズを指定?,ラープ?

    private RectTransform rectTrs;

    // Start is called before the first frame update
    void Start()
    {
        this.rectTrs = this.transform as RectTransform;
    }

    public bool Execute(float _speed)
    {
        this.rectTrs.localScale *= _speed * Time.deltaTime + 1.0f;

        return false;
    }
}
