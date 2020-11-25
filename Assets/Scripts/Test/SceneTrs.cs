using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Fadecontroller fadeController = GameObject.Find("FadeCanvas").GetComponent<Fadecontroller>();
           // fadeController.fadeOutStart(0, 0, 0, 0, "DemoResultScene");
        }
    }
}
