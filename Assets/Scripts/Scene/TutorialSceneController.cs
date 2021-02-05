using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialSceneController : MonoBehaviour
{
    public enum State
    {
        None,
        StartCameraDec,
        StartUI,
        Play,
        Goal,
        ExitScene
    }

    [SerializeField]
    private State activeState = State.StartCameraDec;


    [SerializeField]
    private GameObject mainCamera = null;
    [SerializeField]
    private TutorialSceneUIMediator UIMediator = null;

    // Start is called before the first frame update
    void Start()
    {

    }
    //CameraMove
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (this.activeState)
        {
            case State.StartCameraDec:
                UpdateStartCameraDec();
                break;

            case State.StartUI:
                UpdateStartUI();
                break;

            case State.Play:
                UpdatePlay();
                break;

            case State.Goal:
                UpdateGoal();
                break;

            case State.ExitScene:
                UpdateExitScene();
                break;

            default: break;
        }
    }

    public void ChangeState(State _state)
    {
        this.activeState = _state;
    }

    private void UpdateStartCameraDec()
    {
        if (mainCamera.GetComponent<MoveCamera>().CameraMove())
        {
            // 演出用カメラをオフに
            mainCamera.GetComponent<MoveCamera>().enabled = false;
            mainCamera.GetComponent<CinemachineBrain>().enabled = false;
            // プレイ用のカメラをオン
            mainCamera.GetComponent<PlayerFollowCamera>().enabled = true;
            mainCamera.GetComponent<PlayerFollowCamera>().SetOldPos(mainCamera.transform.position);

            // ステートを変更
            ChangeState(State.StartUI);
        }
    }
    private void UpdateStartUI()
    {
        // UIを表示
        if (this.UIMediator.StartExecute())
            ChangeState(State.Play);
    }
    private void UpdatePlay()
    {

    }
    private void UpdateGoal()
    {
        // UIを表示
        if (this.UIMediator.GoalExecute())
            ChangeState(State.ExitScene);
    }
    private void UpdateExitScene()
    {
        FadeController.Instance.fadeOutStart(Common.Scene.RESULT_SCENE);
    }
}
