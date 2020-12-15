using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBGMTrigger: MonoBehaviour
{
    [SerializeField]
    BGMController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // BGMをフェードアウトさせる
        if(other.tag == "BGMStopTrigger")
        {
            controller.FadeOutFlagOn();
        }
        // BGMをフェードインさせ道中のBGMを流す
        else if(other.tag == "NormalBGMTrigger")
        {
            controller.FadeInFlagOn();
            controller.PlayNormalBGM();
        }
        // BGMをフェードインさせ道中のBGMを流す
        else if(other.tag == "BossBattleBGMTrigger")
        {
            controller.FadeInFlagOn();
            controller.PlayBossBattleBGM();
        }
    }
}
