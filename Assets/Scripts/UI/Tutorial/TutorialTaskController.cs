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
            new EnemyBulletTask()
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
            tutorialTaskList.RemoveAt(0);
            ChangeTaskText();
        }
    }

    void ChangeTaskText()
    {
        tutorialTitleText.text = tutorialTaskList[0].GetTitle();
        tutorialText.text = tutorialTaskList[0].GetText();
    }
}
