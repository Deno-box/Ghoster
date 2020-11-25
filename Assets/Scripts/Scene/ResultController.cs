using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    GameObject fadeCanvas;
    Fadecontroller fadecontroller;
    // Start is called before the first frame update
    void Start()
    {
        fadeCanvas = GameObject.Find("FadeCanvas");
        fadecontroller = fadeCanvas.GetComponent<Fadecontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    fadecontroller.fadeOutStart(0, 0, 0, 0, "TitleScene");
        //}
    }
}
