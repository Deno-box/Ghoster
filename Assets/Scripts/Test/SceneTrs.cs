using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FadeController.Instance.fadeOutStart(Common.Scene.RESULT_SCENE);
        }
    }
}
