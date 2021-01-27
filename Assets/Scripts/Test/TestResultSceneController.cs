using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResultSceneController : MonoBehaviour
{
    [SerializeField]
    private ResultSceneUIMediator resultSceneUIMediator =null;

    private bool isStartDec = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isStartDec)
            this.isStartDec = this.resultSceneUIMediator.StartExecute();

        // スペースキーを押すとタイトルに遷移する
        if (Input.GetKeyDown(KeyCode.Space))
            FadeController.Instance.fadeOutStart(Common.Scene.TITLE_SCENE);
    }

    public void OnClick()
    {
        FadeController.Instance.fadeOutStart(Common.Scene.TITLE_SCENE);
    }
}
