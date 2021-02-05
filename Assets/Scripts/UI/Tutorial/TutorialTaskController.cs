using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTaskController : MonoBehaviour
{
    [SerializeField]
    private RectTransform tutorialTextArea = null;
    [SerializeField]
    private Text tutorialTitleText = null;
    [SerializeField]
    private Text tutorialText = null;

    private List<ITutorialTask> tutorialTaskList = new List<ITutorialTask>();
    private ITutorialTask currentTutorialTask = null;
    private ITutorialTask lastTutorialTask = null;

    void Start()
    {
        tutorialTaskList = new List<ITutorialTask>()
        {
            new MovementTask(),
            new JumpTask(),
            new ParryTask(),
            //new EnemyBulletTask(),
            new GameoverConditionTask(),
            new GameClearConditionTask()
        };
        currentTutorialTask = this.tutorialTaskList[0];
        lastTutorialTask = currentTutorialTask;

        ChangeTaskText();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTutorialTask.CheckTast())
        {
            if (tutorialTaskList.Count > 1)
            {
                tutorialTaskList[0].ExitTaskSetting();
                tutorialTaskList.RemoveAt(0);
            }
            currentTutorialTask = this.tutorialTaskList[0];
        }
        // タスクが切り替わったら
        if(lastTutorialTask != currentTutorialTask)
            currentTutorialTask.OnTaskSetting();

        ChangeTaskText();

        lastTutorialTask = currentTutorialTask;
    }

    void ChangeTaskText()
    {
        tutorialTitleText.text = currentTutorialTask.GetTitle();
        tutorialText.text = currentTutorialTask.GetText();
    }
}
