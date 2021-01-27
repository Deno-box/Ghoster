using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrs : MonoBehaviour
{
    [SerializeField]
    PlaySceneController playSceneController = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playSceneController.ChangeState(PlaySceneController.State.Goal);
        }
    }
}
